using System;
using System.Collections.Generic;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    /// <summary>
    /// Deletes all packages, transactions, templates and invited/created senders
    /// that were created or updated within the last <see cref="CLEANUP_WINDOW_MINUTES"/> minutes.
    /// Intended to be the final example run in a test pass so that transient test data is
    /// removed from the account.
    /// </summary>
    public class CleanupRecentPackagesExample : SDKSample
    {
        public static readonly int CLEANUP_WINDOW_MINUTES = 1440;
        public static readonly int PAGE_SIZE = PageRequest.MaxPageSize;

        /// <summary>
        /// Width of each date slice, in days, used when querying packages to delete.
        /// Querying the whole range in one request is expensive enough on a busy account to
        /// exceed the request timeout, so the range is walked in slices instead. This changes
        /// only how the range is queried, not which packages fall inside it.
        /// </summary>
        public static readonly int CLEANUP_CHUNK_DAYS = 30;

        private static readonly DocumentPackageStatus[] PACKAGE_STATUSES = {
            DocumentPackageStatus.DRAFT,
            DocumentPackageStatus.SENT,
            DocumentPackageStatus.COMPLETED,
            DocumentPackageStatus.ARCHIVED,
            DocumentPackageStatus.DECLINED,
            DocumentPackageStatus.OPTED_OUT,
            DocumentPackageStatus.EXPIRED
        };

        public int deletedPackagesCount = 0;
        public int deletedTemplatesCount = 0;
        public int deletedSendersCount = 0;
        public int deletedGroupsCount = 0;
        public int skippedPackagesCount = 0;
        public List<PackageId> deletedPackageIds = new List<PackageId>();
        public List<PackageId> deletedTemplateIds = new List<PackageId>();
        public List<string> deletedSenderIds = new List<string>();
        public List<GroupId> deletedGroupIds = new List<GroupId>();

        public static void Main(string[] args)
        {
            new CleanupRecentPackagesExample().Run();
        }

        override public void Execute()
        {
            DateTime to = DateTime.Now;
            DateTime from = to.AddMinutes(-CLEANUP_WINDOW_MINUTES);

            foreach (DocumentPackageStatus status in PACKAGE_STATUSES)
            {
                DeletePackagesUpdatedWithinRangeInChunks(status, from, to);
            }

            Console.WriteLine("Skipped " + skippedPackagesCount + " packages that cannot be deleted");

            DeleteTemplatesUpdatedWithinRange(from);
            DeleteSendersCreatedWithinRange(from);
            DeleteGroupsCreatedOrUpdatedWithinRange();
        }

        /// <summary>
        /// Deletes packages for a status by walking [from, to] in <see cref="CLEANUP_CHUNK_DAYS"/>-day
        /// slices. One request covering the whole range times out on a busy account, so each slice is
        /// queried separately; the set of packages covered is unchanged.
        /// </summary>
        private void DeletePackagesUpdatedWithinRangeInChunks(DocumentPackageStatus status, DateTime from, DateTime to)
        {
            DateTime chunkStart = from;
            while (chunkStart < to)
            {
                DateTime chunkEnd = chunkStart.AddDays(CLEANUP_CHUNK_DAYS);
                if (chunkEnd > to)
                {
                    chunkEnd = to;
                }

                DeletePackagesUpdatedWithinRange(status, chunkStart, chunkEnd);
                chunkStart = chunkEnd;
            }
            Console.WriteLine("Deleted " + deletedPackagesCount + " packages after status " + status);
        }

        private void DeletePackagesUpdatedWithinRange(DocumentPackageStatus status, DateTime from, DateTime to)
        {
            while (true)
            {
                // Always read from the start of the range. Deleting a package removes it from
                // this query's result set, so everything after it shifts forward — advancing the
                // offset instead would step over the packages that moved into the pages already
                // read, and they would survive the sweep.
                Page<DocumentPackage> page = ossClient.PackageService
                    .GetUpdatedPackagesWithinDateRange(status, new PageRequest(1, PAGE_SIZE), from, to);

                int deletedThisPass = 0;
                foreach (DocumentPackage pkg in page)
                {
                    if (DeletePackageIfAllowed(pkg.Id, "package"))
                    {
                        deletedPackageIds.Add(pkg.Id);
                        deletedPackagesCount++;
                        deletedThisPass++;
                        Console.WriteLine(deletedPackagesCount + " Deleted package " + pkg.Id);
                    }
                }

                // A short page means the range is exhausted. This deliberately does not consult
                // HasNextPage(): the server's TotalElements has been observed exceeding the number
                // of rows the filtered query actually returns, and trusting it makes the loop
                // request pages that do not exist — which is where it used to hang.
                // DeleteSendersCreatedWithinRange() breaks on a short page for the same reason.
                if (page.NumberOfElements < PAGE_SIZE)
                {
                    break;
                }

                // A full page may hide more, but only re-read if this pass actually removed
                // something. Otherwise the page holds nothing but packages the server refuses to
                // delete, and re-reading it would loop forever.
                if (deletedThisPass == 0)
                {
                    Console.WriteLine("Stopping " + status + " sweep for "
                        + from.ToShortDateString() + " - " + to.ToShortDateString()
                        + ": a full page yielded no deletable packages");
                    break;
                }
            }
        }

        /// <summary>
        /// Deletes a package or template, tolerating the cases the server refuses to
        /// delete. Completed RON / IPEN transactions, for instance, are rejected with
        /// a 403 (error.forbidden.deletion.completedRonOrIpenTransaction); such
        /// transactions are permanent, so cleanup skips them instead of failing.
        /// </summary>
        /// <returns>true when the package was deleted, false when it was skipped.</returns>
        private bool DeletePackageIfAllowed(PackageId packageId, string label)
        {
            try
            {
                ossClient.PackageService.DeletePackage(packageId);
                return true;
            }
            catch (OssServerException e)
            {
                ServerError error = e.ServerError;
                if (error != null && error.Code.HasValue && error.Code.Value == 403)
                {
                    skippedPackagesCount++;
                    Console.WriteLine("Skipping " + label + " " + packageId
                        + " which cannot be deleted: " + error.Message
                        + " (" + error.MessageKey + ")");
                    return false;
                }
                throw;
            }
        }

        private void DeleteTemplatesUpdatedWithinRange(DateTime from)
        {
            PageRequest request = new PageRequest(1, PAGE_SIZE);
            while (true)
            {
                Page<DocumentPackage> page = ossClient.PackageService.GetTemplates(request);
                foreach (DocumentPackage template in page)
                {
                    if (template.UpdatedDate.HasValue && template.UpdatedDate.Value >= from
                        && DeletePackageIfAllowed(template.Id, "template"))
                    {
                        deletedTemplateIds.Add(template.Id);
                        deletedTemplatesCount++;
                        Console.WriteLine(deletedTemplatesCount + " Deleted template " + template.Id);
                    }
                }
                if (!page.HasNextPage())
                {
                    break;
                }
                request = page.NextRequest;
            }
            Console.WriteLine("Deleted " + deletedTemplatesCount + " templates");
        }

        private void DeleteGroupsCreatedOrUpdatedWithinRange()
        {
            List<Group> groups = ossClient.GroupService.GetMyGroups();
            foreach (Group group in groups)
            {
                if ((group.Name != null && group.Name.StartsWith(GroupManagementExample.GROUP_NAME_PREFIX)) ||
                    (group.Email != null && group.Email.StartsWith(GroupManagementExample.EMAIL)))
                {
                    ossClient.GroupService.DeleteGroup(group.Id);
                    deletedGroupIds.Add(group.Id);
                    deletedGroupsCount++;
                    Console.WriteLine(deletedGroupsCount + " Deleted group " + group.Id.Id);
                }
            }
            Console.WriteLine("Deleted " + deletedGroupsCount + " groups");
        }

        private void DeleteSendersCreatedWithinRange(DateTime from)
        {
            // Senders whose email is configured in signers.properties must never be
            // deleted, since they are reused across test runs.
            HashSet<string> protectedEmails = GetProtectedSenderEmails();

            // First pass: collect all candidate sender IDs across every page into a Set.
            // Using a Set deduplicates IDs that re-appear on later pages because a prior
            // deletion attempt silently failed and left the sender in the account.
            HashSet<string> candidateIds = new HashSet<string>();
            int pageIndex = 0;
            while (true)
            {
                IDictionary<string, Sender> senders = ossClient.AccountService
                    .GetSenders(Direction.ASCENDING, new PageRequest(pageIndex * PAGE_SIZE + 1, PAGE_SIZE));
                foreach (Sender sender in senders.Values)
                {
                    if (senderUID.Equals(sender.Id))
                    {
                        continue; // never delete the account owner running the tests
                    }
                    if (sender.Email != null
                        && protectedEmails.Contains(sender.Email.ToLower()))
                    {
                        Console.WriteLine("Skipping protected sender " + sender.Id
                            + " (" + sender.Email + ") defined in signers.properties");
                        continue; // never delete senders defined in signers.properties
                    }
                    if (sender.Created.HasValue && sender.Created.Value >= from)
                    {
                        candidateIds.Add(sender.Id);
                        Console.WriteLine("Candidate sender " + sender.Id + " in page " + pageIndex + " Added");
                    }
                }
                if (senders.Count < PAGE_SIZE)
                {
                    break;
                }
                pageIndex++;
            }

            // Second pass: attempt each deletion exactly once regardless of whether
            // a previous page already saw the same sender.
            foreach (string senderId in candidateIds)
            {
                ossClient.AccountService.DeleteSender(senderId);
                deletedSenderIds.Add(senderId);
                deletedSendersCount++;
                Console.WriteLine(deletedSendersCount + " Deleted sender " + senderId);
            }
            Console.WriteLine("Deleted " + deletedSendersCount + " senders");
        }

        /// <summary>
        /// Collects every email address configured in signers.properties so that the
        /// senders they refer to are protected from deletion. Any property value that
        /// looks like an email address (contains '@') is treated as a sender email,
        /// which covers sender.email, the numbered N.email entries, the delegator /
        /// delegatee emails and any future email entries. Emails are lower-cased so
        /// the comparison is case-insensitive.
        /// </summary>
        private HashSet<string> GetProtectedSenderEmails()
        {
            HashSet<string> protectedEmails = new HashSet<string>();
            foreach (string value in props.Values)
            {
                if (value != null && value.Contains("@"))
                {
                    protectedEmails.Add(value.Trim().ToLower());
                }
            }
            return protectedEmails;
        }
    }
}
