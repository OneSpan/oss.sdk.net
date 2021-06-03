using System;

namespace OneSpanSign.Sdk
{
    public class OverviewOptions
    {

        private Nullable<bool> title;
        private Nullable<bool> body;
        private Nullable<bool> documentSection;
        private Nullable<bool> uploadSection;
        

        
        public Nullable<bool> Title
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
        
        public Nullable<bool> Body
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
        
        public Nullable<bool> DocumentSection
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
        
        public Nullable<bool> UploadSection
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