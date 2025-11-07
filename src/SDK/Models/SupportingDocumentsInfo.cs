using System;
using Newtonsoft.Json;

namespace OneSpanSign.Sdk.Models
{
    [Serializable]
    public class SupportingDocumentsInfo : IEquatable<SupportingDocumentsInfo>
    {
        public int? Id { get; private set; }
        public string TransactionUid { get; private set; }
        public string DocumentRefId { get; private set; }
        public string DocumentName { get; private set; }
        public int? Version { get; private set; }
        public long FileSize { get; private set; }

        [JsonConstructor]
        public SupportingDocumentsInfo(
            int? id,
            string transactionUid,
            string documentRefId,
            string documentName,
            int? version,
            long fileSize)
        {
            Id = id;
            TransactionUid = transactionUid;
            DocumentRefId = documentRefId;
            DocumentName = documentName;
            Version = version;
            FileSize = fileSize;
        }

        public bool Equals(SupportingDocumentsInfo other)
        {
            if (ReferenceEquals(null, other)) { return false;}
            if (ReferenceEquals(this, other)) {return true;}
            return Id == other.Id &&
                   TransactionUid == other.TransactionUid &&
                   DocumentRefId == other.DocumentRefId &&
                   DocumentName == other.DocumentName &&
                   Version == other.Version &&
                   FileSize == other.FileSize;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) {return false;}
            if (ReferenceEquals(this, obj)) {return true;}
            if (obj.GetType() != GetType()) {return false;}
            return Equals((SupportingDocumentsInfo)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id.GetHashCode();
                hashCode = (hashCode * 397) ^ (TransactionUid?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (DocumentRefId?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (DocumentName?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ Version.GetHashCode();
                hashCode = (hashCode * 397) ^ FileSize.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return
                $"DocumentInfo{{id={Id}, transactionUid='{TransactionUid}', documentRefId='{DocumentRefId}', documentName='{DocumentName}', version={Version}, fileSize={FileSize}}}";
        }
    }
}