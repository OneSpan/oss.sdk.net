namespace OneSpanSign.Sdk
{
    public class OverviewOptions
    {

        private bool title;
        private bool body;
        private bool documentSection;
        private bool uploadSection;
        

        
        public bool Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }
        
        public bool Body
        {
            get
            {
                return body;
            }
            set
            {
                body = value;
            }
        }
        
        public bool DocumentSection
        {
            get
            {
                return documentSection;
            }
            set
            {
                documentSection = value;
            }
        }
        
        public bool UploadSection
        {
            get
            {
                return uploadSection;
            }
            set
            {
                uploadSection = value;
            }
        }
        
    }
}