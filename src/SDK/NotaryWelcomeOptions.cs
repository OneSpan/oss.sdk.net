using System;

namespace OneSpanSign.Sdk
{
    public class NotaryWelcomeOptions
    {

        private Nullable<bool> title;
        private Nullable<bool> body;
        private Nullable<bool> recipientName;
        private Nullable<bool> recipientEmail;
        private Nullable<bool> recipientActionRequired;
        private Nullable<bool> notaryTag;
        private Nullable<bool> recipientRole;
        private Nullable<bool> recipientStatus;

        
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
        
        public Nullable<bool> RecipientName
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
        
        public Nullable<bool> RecipientEmail
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
        
        public Nullable<bool> RecipientActionRequired
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
        
        public Nullable<bool> NotaryTag
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
        
        public Nullable<bool> RecipientRole
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
        
        public Nullable<bool> RecipientStatus
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