namespace OneSpanSign.Sdk
{
    public class IdvWorkflowConfig
    {
        public string Id { get; set; }

        public string Type { get; set; }

        public string Tenant { get; set; }

        public string Desc { get; set; }

        public bool SkipWhenAccessingSignedDocuments { get; set; }
    }
}