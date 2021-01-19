namespace OneSpanSign.Sdk
{
    public class NotaryWelcomeOptions
    {

        private bool title;
        private bool body;
        private bool recipientName;
        private bool recipientEmail;
        private bool recipientActionRequired;
        private bool notaryTag;
        private bool recipientRole;
        private bool recipientStatus;

        
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
        
        public bool RecipientActionRequired
        {
            get
            {
                return recipientActionRequired;
            }
            set
            {
                recipientActionRequired = value;
            }
        }
        
        public bool NotaryTag
        {
            get
            {
                return notaryTag;
            }
            set
            {
                notaryTag = value;
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
        
    }
}