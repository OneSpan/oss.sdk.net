namespace OneSpanSign.Sdk
{
    public class InpersonHostThankYouOptions
    {

        private bool title;
        private bool body;
        private bool recipientName;
        private bool recipientEmail;
        private bool recipientRole;
        private bool recipientStatus;
        private bool downloadButton;
        private bool reviewDocumentsButton;
        
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
        
        public bool RecipientName
        {
            get
            {
                return recipientName;
            }
            set
            {
                recipientName = value;
            }
        }
        
        public bool RecipientEmail
        {
            get
            {
                return recipientEmail;
            }
            set
            {
                recipientEmail = value;
            }
        }
        
        public bool RecipientRole
        {
            get
            {
                return recipientRole;
            }
            set
            {
                recipientRole = value;
            }
        }
        
        public bool RecipientStatus
        {
            get
            {
                return recipientStatus;
            }
            set
            {
                recipientStatus = value;
            }
        }
        
        public bool DownloadButton
        {
            get
            {
                return downloadButton;
            }
            set
            {
                downloadButton = value;
            }
        }
        
        public bool ReviewDocumentsButton
        {
            get
            {
                return reviewDocumentsButton;
            }
            set
            {
                reviewDocumentsButton = value;
            }
        }
    }
}