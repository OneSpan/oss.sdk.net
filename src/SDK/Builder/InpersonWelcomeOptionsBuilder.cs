namespace OneSpanSign.Sdk
{
    public class InpersonWelcomeOptionsBuilder
    {
        private bool title;
        private bool body;
        private bool recipientName;
        private bool recipientEmail;
        private bool recipientActionRequired;
        private bool recipientRole;
        private bool recipientStatus;
        
        private InpersonWelcomeOptionsBuilder()
        {
        }

        public static InpersonWelcomeOptionsBuilder NewInpersonWelcomeOptions() {
            return new InpersonWelcomeOptionsBuilder();
        }

        public InpersonWelcomeOptionsBuilder WithTitle() {
            this.title = true;
            return this;
        }

        public InpersonWelcomeOptionsBuilder WithoutTitle() {
            this.title = false;
            return this;
        }
        
        public InpersonWelcomeOptionsBuilder WithBody() {
            this.body = true;
            return this;
        }

        public InpersonWelcomeOptionsBuilder WithoutBody() {
            this.body = false;
            return this;
        }
        
        public InpersonWelcomeOptionsBuilder WithRecipientName() {
            this.recipientName = true;
            return this;
        }

        public InpersonWelcomeOptionsBuilder WithoutRecipientName() {
            this.recipientName = false;
            return this;
        }
        
        public InpersonWelcomeOptionsBuilder WithRecipientEmail() {
            this.recipientEmail = true;
            return this;
        }

        public InpersonWelcomeOptionsBuilder WithoutRecipientEmail() {
            this.recipientEmail = false;
            return this;
        }
        
        public InpersonWelcomeOptionsBuilder WithRecipientActionRequired() {
            this.recipientActionRequired = true;
            return this;
        }

        public InpersonWelcomeOptionsBuilder WithoutRecipientActionRequired() {
            this.recipientActionRequired = false;
            return this;
        }

        
        public InpersonWelcomeOptionsBuilder WithRecipientRole() {
            this.recipientRole = true;
            return this;
        }

        public InpersonWelcomeOptionsBuilder WithoutRecipientRole() {
            this.recipientRole = false;
            return this;
        }
        
        public InpersonWelcomeOptionsBuilder WithRecipientStatus() {
            this.recipientStatus = true;
            return this;
        }

        public InpersonWelcomeOptionsBuilder WithoutRecipientStatus() {
            this.recipientStatus = false;
            return this;
        }
        
        public InpersonWelcomeOptions Build() {
            InpersonWelcomeOptions result = new InpersonWelcomeOptions();
            result.Title = title;
            result.Body = body;
            result.RecipientName = recipientName;
            result.RecipientEmail = recipientEmail;
            result.RecipientActionRequired = recipientActionRequired;
            result.RecipientRole = recipientRole;
            result.RecipientStatus = recipientStatus;

            return result;
        }
    }
}