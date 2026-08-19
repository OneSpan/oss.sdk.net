namespace OneSpanSign.Sdk
{
    /// <summary>
    /// Explains an extraction outcome that is not <see cref="ExtractionStatus.COMPLETED"/>.
    /// </summary>
    public enum ExtractionReasonCode
    {
        /// <summary>Data extraction is not enabled for the account.</summary>
        EXTRACTION_NOT_ENABLED,

        /// <summary>The classifier could not determine the document type.</summary>
        CLASSIFICATION_UNKNOWN,

        /// <summary>The classified document type is not supported for extraction.</summary>
        CLASSIFICATION_UNSUPPORTED_TYPE,

        /// <summary>Classification flagged the attachment as low quality.</summary>
        CLASSIFICATION_QUALITY_WARNING,

        /// <summary>Extraction was invoked but errored or timed out.</summary>
        EXTRACTION_ERROR
    }
}
