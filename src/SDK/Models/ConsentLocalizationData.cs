using System;

namespace OneSpanSign.Sdk.Models
{
    [Serializable]
    public class ConsentLocalizationData
    {
        public string ConsentId { get; set; }

        public ConsentMetadataDetails ConsentMetadata { get; set; }

        [Serializable]
        public class ConsentMetadataDetails
        {
            public PackageInfo PackageInfo { get; set; }

            public ResourceMetadata Properties { get; set; }

            public ResourceMetadata Document { get; set; }
        }

        [Serializable]
        public class PackageInfo
        {
            public string Uid { get; set; }

            public string Language { get; set; }
        }

        [Serializable]
        public class ResourceMetadata
        {
            public string AccountId { get; set; }

            public string Language { get; set; }
        }
    }
}
