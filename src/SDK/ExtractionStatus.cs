namespace OneSpanSign.Sdk
{
    /// <summary>
    /// Outcome of the attachment data extraction step.
    /// When the status is not <see cref="COMPLETED"/>, <see cref="ExtractionReasonCode"/> explains why.
    /// </summary>
    public enum ExtractionStatus
    {
        /// <summary>Extraction ran and returned data.</summary>
        COMPLETED,

        /// <summary>Extraction was not attempted.</summary>
        NOT_PERFORMED,

        /// <summary>Extraction was invoked but errored or timed out.</summary>
        FAILED
    }
}
