using System;
using System.Collections.Generic;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Internal;

namespace SDK.Examples
{
    public class NotaryJournalExample : SDKSample
    {
        public List<NotaryJournalEntry> sdkJournalEntries;
        public string csvJournalEntries;
        private string senderUID;

        public static void Main(string[] args)
        {
            new NotaryJournalExample(Props.GetInstance()).Run();
        }

        public NotaryJournalExample(Props props) : this(props.Get("api.url"), props.Get("api.key"))
        {
        }

        public NotaryJournalExample(string apiUrl, string apiKey) : base(apiUrl, apiKey)
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

