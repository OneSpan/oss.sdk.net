using System;
using System.Collections.Generic;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Internal;

namespace SDK.Examples
{
    public class NotaryJournalExample : SDKSample
    {
        public List<NotaryJournalEntry> sdkJournalEntries;
        public DownloadedFile csvJournalEntries;
        private string senderUID;

        public static void Main(string[] args)
        {
            new NotaryJournalExample(Props.GetInstance()).Run();
        }

        public NotaryJournalExample(Props props) : this(props.Get("api.key"), props.Get("api.url"))
        {
        }

        public NotaryJournalExample(string apiKey, string apiUrl) : base(apiKey, apiUrl)
        {
            this.senderUID = Converter.apiKeyToUID(apiKey);
        }

        override public void Execute()
        {
            sdkJournalEntries = eslClient.PackageService.GetJournalEntries(senderUID);
            csvJournalEntries = eslClient.PackageService.GetJournalEntriesAsCSV(senderUID);
        }
    }
}

