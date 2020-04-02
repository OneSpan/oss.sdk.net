using System;

namespace OneSpanSign.Sdk
{
    internal class NotaryJournalEntryConverter
    {
        private OneSpanSign.API.NotaryJournalEntry apiNotaryJournalEntry;
        private NotaryJournalEntry sdkNotaryJournalEntry;

        public NotaryJournalEntryConverter( NotaryJournalEntry sdkNotaryJournalEntry ) {
            this.sdkNotaryJournalEntry = sdkNotaryJournalEntry;
            this.apiNotaryJournalEntry = null;
        }

        public NotaryJournalEntryConverter( OneSpanSign.API.NotaryJournalEntry apiNotaryJournalEntry ) {
            this.apiNotaryJournalEntry = apiNotaryJournalEntry;
            this.sdkNotaryJournalEntry = null;
        }

        public OneSpanSign.API.NotaryJournalEntry ToAPINotaryJournalEntry()
        {
            if (sdkNotaryJournalEntry == null)
            {
                return apiNotaryJournalEntry;
            }
            OneSpanSign.API.NotaryJournalEntry result = new OneSpanSign.API.NotaryJournalEntry();

            result.SequenceNumber = sdkNotaryJournalEntry.SequenceNumber;
            result.CreationDate = sdkNotaryJournalEntry.CreationDate;
            result.DocumentType = sdkNotaryJournalEntry.DocumentType;
            result.DocumentName = sdkNotaryJournalEntry.DocumentName;
            result.SignerName = sdkNotaryJournalEntry.SignerName;
            result.SignatureType = sdkNotaryJournalEntry.SignatureType;
            result.IdType = sdkNotaryJournalEntry.IdType;
            result.IdValue = sdkNotaryJournalEntry.IdValue;
            result.Jurisdiction = sdkNotaryJournalEntry.Jurisdiction;
            result.Comment = sdkNotaryJournalEntry.Comment;

            return result;
        }

        public NotaryJournalEntry ToSDKNotaryJournalEntry()
        {
            if (apiNotaryJournalEntry == null)
            {
                return sdkNotaryJournalEntry;
            }

            return NotaryJournalEntryBuilder.NewNotaryJournalEntry()
                .WithSequenceNumber( apiNotaryJournalEntry.SequenceNumber)
                    .WithCreationDate(apiNotaryJournalEntry.CreationDate)
                    .WithDocumentType(apiNotaryJournalEntry.DocumentType)
                    .WithDocumentName(apiNotaryJournalEntry.DocumentName)
                    .WithSignerName(apiNotaryJournalEntry.SignerName)
                    .WithSignatureType(apiNotaryJournalEntry.SignatureType)
                    .WithIdType(apiNotaryJournalEntry.IdType)
                    .WithIdValue(apiNotaryJournalEntry.IdValue)
                    .WithJurisdiction(apiNotaryJournalEntry.Jurisdiction)
                    .WithComment(apiNotaryJournalEntry.Comment)
                    .Build();
        }
    }
}

