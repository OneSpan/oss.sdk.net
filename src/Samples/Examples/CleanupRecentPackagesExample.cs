using System;
using System.Collections.Generic;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    /// <summary>
    /// Deletes all packages, transactions, templates and invited/created senders
    /// that were created or updated since 2024-08-01. Intended to be the final
    /// example run in a test pass so that transient test data is removed from the account.
    /// </summary>
    public class CleanupRecentPackagesExample : SDKSample
    {
        public static readonly int CLEANUP_WINDOW_MINUTES = 1440;
        public static readonly int PAGE_SIZE = PageRequest.MaxPageSize;

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
            DateTime from = new DateTime(2024, 8, 1);

            foreach (DocumentPackageStatus status in PACKAGE_STATUSES)
            {
                DeletePackagesUpdatedWithinRange(status, from, to);
            }

            DeleteTemplatesUpdatedWithinRange(from);
            DeleteSendersCreatedWithinRange(from);
            DeleteGroupsCreatedOrUpdatedWithinRange();
        }

        private void DeletePackagesUpdatedWithinRange(DocumentPackageStatus status, DateTime from, DateTime to)
        {
            PageRequest request = new PageRequest(1, PAGE_SIZE);
            while (true)
            {
                Page<DocumentPackage> page = ossClient.PackageService
                    .GetUpdatedPackagesWithinDateRange(status, request, from, to);
                foreach (DocumentPackage pkg in page)
                {
                    ossClient.PackageService.DeletePackage(pkg.Id);
                    deletedPackageIds.Add(pkg.Id);
                    deletedPackagesCount++;
                    Console.WriteLine(deletedPackagesCount + " Deleted package " + pkg.Id);
                }
                if (!page.HasNextPage())
                {
                    break;
                }
                request = page.NextRequest;
            }
            Console.WriteLine("Deleted " + deletedPackagesCount + " packages");
        }

        private void DeleteTemplatesUpdatedWithinRange(DateTime from)
        {
            PageRequest request = new PageRequest(1, PAGE_SIZE);
            while (true)
            {
                Page<DocumentPackage> page = ossClient.PackageService.GetTemplates(request);
                foreach (DocumentPackage template in page)
                {
                    if (template.UpdatedDate.HasValue && template.UpdatedDate.Value >= from)
                    {
                        ossClient.PackageService.DeletePackage(template.Id);
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
    }
}
