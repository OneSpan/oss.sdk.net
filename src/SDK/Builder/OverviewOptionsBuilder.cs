namespace OneSpanSign.Sdk
{
    public class OverviewOptionsBuilder
    {
        private bool title;
        private bool body;
        private bool documentSection;
        private bool uploadSection;
        
        private OverviewOptionsBuilder()
        {
        }

        public static OverviewOptionsBuilder NewOverviewOptions() {
            return new OverviewOptionsBuilder();
        }

        public OverviewOptionsBuilder WithTitle() {
            this.title = true;
            return this;
        }

        public OverviewOptionsBuilder WithoutTitle() {
            this.title = false;
            return this;
        }
        
        public OverviewOptionsBuilder WithBody() {
            this.body = true;
            return this;
        }

        public OverviewOptionsBuilder WithoutBody() {
            this.body = false;
            return this;
        }
        
        public OverviewOptionsBuilder WithDocumentSection() {
            this.documentSection = true;
            return this;
        }

        public OverviewOptionsBuilder WithoutDocumentSection() {
            this.documentSection = false;
            return this;
        }
        
        public OverviewOptionsBuilder WithUploadSection() {
            this.uploadSection = true;
            return this;
        }

        public OverviewOptionsBuilder WithoutUploadSection() {
            this.uploadSection = false;
            return this;
        }
        
        

        public OverviewOptions Build() {
            OverviewOptions result = new OverviewOptions();
            result.Title = title;
            result.Body = body;
            result.DocumentSection = documentSection;
            result.UploadSection = uploadSection;
            
            return result;
        }
    }
}