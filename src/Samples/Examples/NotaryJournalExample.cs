using System;
using System.Collections.Generic;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Internal;

namespace SDK.Examples
{
    public class NotaryJournalExample : SDKSample
    {
        public List<NotaryJournalEntry> sdkJournalEntries;
        public DownloadedFile csvJournalEntries;

        public static void Main(string[] args)
        {
            new NotaryJournalExample().Run();
        }

        override public void Execute()
        {
            sdkJournalEntries = ossClient.PackageService.GetJournalEntries(senderUID);
            csvJournalEntries = ossClient.PackageService.GetJournalEntriesAsCSV(senderUID);
        }
    }
}

