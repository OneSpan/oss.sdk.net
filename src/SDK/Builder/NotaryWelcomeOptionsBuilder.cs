using System;

namespace OneSpanSign.Sdk
{
    public class NotaryWelcomeOptionsBuilder
    {
        private Nullable<bool> title;
        private Nullable<bool> body;
        private Nullable<bool> recipientName;
        private Nullable<bool> recipientEmail;
        private Nullable<bool> recipientActionRequired;
        private Nullable<bool> notaryTag;
        private Nullable<bool> recipientRole;
        private Nullable<bool> recipientStatus;
        
        private NotaryWelcomeOptionsBuilder()
        {
        }

        public static NotaryWelcomeOptionsBuilder NewNotaryWelcomeOptions() {
            return new NotaryWelcomeOptionsBuilder();
        }

        public NotaryWelcomeOptionsBuilder WithTitle() {
            this.title = true;
            return this;
        }

        public NotaryWelcomeOptionsBuilder WithoutTitle() {
            this.title = false;
            return this;
        }
        
        public NotaryWelcomeOptionsBuilder WithBody() {
            this.body = true;
            return this;
        }

        public NotaryWelcomeOptionsBuilder WithoutBody() {
            this.body = false;
            return this;
        }
        
        public NotaryWelcomeOptionsBuilder WithRecipientName() {
            this.recipientName = true;
            return this;
        }

        public NotaryWelcomeOptionsBuilder WithoutRecipientName() {
            this.recipientName = false;
            return this;
        }
        
        public NotaryWelcomeOptionsBuilder WithRecipientEmail() {
            this.recipientEmail = true;
            return this;
        }

        public NotaryWelcomeOptionsBuilder WithoutRecipientEmail() {
            this.recipientEmail = false;
            return this;
        }
        
        public NotaryWelcomeOptionsBuilder WithRecipientActionRequired() {
            this.recipientActionRequired = true;
            return this;
        }

        public NotaryWelcomeOptionsBuilder WithoutRecipientActionRequired() {
            this.recipientActionRequired = false;
            return this;
        }

        public NotaryWelcomeOptionsBuilder WithNotaryTag() {
            this.notaryTag = true;
            return this;
        }

        public NotaryWelcomeOptionsBuilder WithoutNotaryTag() {
            this.notaryTag = false;
            return this;
        }
        
        public NotaryWelcomeOptionsBuilder WithRecipientRole() {
            this.recipientRole = true;
            return this;
        }

        public NotaryWelcomeOptionsBuilder WithoutRecipientRole() {
            this.recipientRole = false;
            return this;
        }
        
        public NotaryWelcomeOptionsBuilder WithRecipientStatus() {
            this.recipientStatus = true;
            return this;
        }

        public NotaryWelcomeOptionsBuilder WithoutRecipientStatus() {
            this.recipientStatus = false;
            return this;
        }
        
        public NotaryWelcomeOptions Build() {
            NotaryWelcomeOptions result = new NotaryWelcomeOptions();
            result.Title = title;
            result.Body = body;
            result.RecipientName = recipientName;
            result.RecipientEmail = recipientEmail;
            result.RecipientActionRequired = recipientActionRequired;
            result.NotaryTag = notaryTag;
            result.RecipientRole = recipientRole;
            result.RecipientStatus = recipientStatus;

            return result;
        }
    }
}