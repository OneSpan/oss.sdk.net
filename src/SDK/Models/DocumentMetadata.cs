using System;
using System.Linq;
using Newtonsoft.Json;

namespace OneSpanSign.Sdk.Models
{
    [Serializable]
    public sealed class DocumentMetadata : IEquatable<DocumentMetadata>
    {
        public string DocumentName { get; private set; }
        public byte[] Content { get; private set; }
        public string MediaType { get; private set; }

        [JsonConstructor]
        public DocumentMetadata(
            string documentName,
            byte[] content,
            string mediaType)
        {
            DocumentName = documentName;
            Content = content?.ToArray();
            MediaType = mediaType;
        }

        public static Builder CreateBuilder()
        {
            return new Builder();
        }

        public bool Equals(DocumentMetadata other)
        {
            if (ReferenceEquals(null, other)) {return false;}
            if (ReferenceEquals(this, other)) {return true;}
            return DocumentName == other.DocumentName &&
                   ((Content == null && other.Content == null) || 
                    (Content != null && other.Content != null && Content.SequenceEqual(other.Content))) &&
                   MediaType == other.MediaType;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) {return false;}
            if (ReferenceEquals(this, obj)) {return true;}
            if (obj.GetType() != GetType()) {return false;}
            return Equals((DocumentMetadata)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = DocumentName?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (Content?.Aggregate(0, (acc, b) => acc ^ b.GetHashCode()) ?? 0);
                hashCode = (hashCode * 397) ^ (MediaType?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"DocumentMetadata{{documentName='{DocumentName}', content=[{(Content != null ? string.Join(", ", Content) : "null")}], mediaType='{MediaType}'}}";
        }

        public sealed class Builder
        {
            private string _documentName;
            private byte[] _content;
            private string _mediaType;

            public Builder WithDocumentName(string documentName)
            {
                _documentName = documentName;
                return this;
            }

            public Builder WithContent(byte[] content)
            {
                _content = content;
                return this;
            }

            public Builder WithMediaType(string mediaType)
            {
                _mediaType = mediaType;
                return this;
            }

            public DocumentMetadata Build()
            {
                return new DocumentMetadata(_documentName, _content, _mediaType);
            }
        }
    }
}