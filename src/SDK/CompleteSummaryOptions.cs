using System;

namespace OneSpanSign.Sdk
{
    public class CompleteSummaryOptions
    {

        private Nullable<bool> title;
        private Nullable<bool> message;
        private Nullable<bool> download;
        private Nullable<bool> review;
        private Nullable<bool> _continue;
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
        
        public Nullable<bool> Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
            }
        }
        
        public Nullable<bool> Download
        {
            get
            {
                return download;
            }
            set
            {
                download = value;
            }
        }
        
        public Nullable<bool> Review
        {
            get
            {
                return review;
            }
            set
            {
                review = value;
            }
        }
        
        public Nullable<bool> Continue
        {
            get
            {
                return _continue;
            }
            set
            {
                _continue = value;
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