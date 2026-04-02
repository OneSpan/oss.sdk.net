using System;

namespace OneSpanSign.Sdk.Models
{
    [Serializable]
    public class PackageUpdateWorkflowResult
    {
        public enum Status
        {
            SUCCESS,
            FAILURE,
            SKIPPED
        }

        [Serializable]
        public class Result
        {
            public Result()
            {
            }

            public Result(Status status, string message)
            {
                Status = status;
                Message = message;
            }

            public Status Status { get; set; }

            public string? Message { get; set; }
        }

        [Serializable]
        public class ConsentLocalizationResult : Result
        {
            public ConsentLocalizationResult()
            {
            }

            public ConsentLocalizationResult(Status status, string message, ConsentLocalizationData? data)
                : base(status, message)
            {
                Data = data;
            }

            public ConsentLocalizationData? Data { get; set; }
        }

        public string? PackageUid { get; set; }

        public ConsentLocalizationResult? ConsentInfo { get; set; }
    }
}