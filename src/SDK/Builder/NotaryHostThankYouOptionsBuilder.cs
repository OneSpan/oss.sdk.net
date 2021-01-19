namespace OneSpanSign.Sdk
{
    public class NotaryHostThankYouOptionsBuilder
    {
        private bool title;
        private bool body;
        private bool recipientName;
        private bool recipientEmail;
        private bool recipientRole;
        private bool notaryTag;
        private bool recipientStatus;
        private bool downloadButton;
        private bool reviewDocumentsButton;
        
        
        private NotaryHostThankYouOptionsBuilder()
        {
        }

        public static NotaryHostThankYouOptionsBuilder NewNotaryHostThankYouOptions() {
            return new NotaryHostThankYouOptionsBuilder();
        }

        public NotaryHostThankYouOptionsBuilder WithTitle() {
            this.title = true;
            return this;
        }

        public NotaryHostThankYouOptionsBuilder WithoutTitle() {
            this.title = false;
            return this;
        }
        
        public NotaryHostThankYouOptionsBuilder WithBody() {
            this.body = true;
            return this;
        }

        public NotaryHostThankYouOptionsBuilder WithoutBody() {
            this.body = false;
            return this;
        }
        
        public NotaryHostThankYouOptionsBuilder WithRecipientName() {
            this.recipientName = true;
            return this;
        }

        public NotaryHostThankYouOptionsBuilder WithoutRecipientName() {
            this.recipientName = false;
            return this;
        }
        
        public NotaryHostThankYouOptionsBuilder WithRecipientEmail() {
            this.recipientEmail = true;
            return this;
        }

        public NotaryHostThankYouOptionsBuilder WithoutRecipientEmail() {
            this.recipientEmail = false;
            return this;
        }
        
        public NotaryHostThankYouOptionsBuilder WithRecipientRole() {
            this.recipientRole = true;
            return this;
        }

        public NotaryHostThankYouOptionsBuilder WithoutRecipientRole() {
            this.recipientRole = false;
            return this;
        }
        
        public NotaryHostThankYouOptionsBuilder WithNotaryTag() {
            this.notaryTag = true;
            return this;
        }

        public NotaryHostThankYouOptionsBuilder WithoutNotaryTag() {
            this.notaryTag = false;
            return this;
        }
        
        public NotaryHostThankYouOptionsBuilder WithRecipientStatus() {
            this.recipientStatus = true;
            return this;
        }

        public NotaryHostThankYouOptionsBuilder WithoutRecipientStatus() {
            this.recipientStatus = false;
            return this;
        }
        
        public NotaryHostThankYouOptionsBuilder WithDownloadButton() {
            this.downloadButton = true;
            return this;
        }

        public NotaryHostThankYouOptionsBuilder WithoutDownloadButton() {
            this.downloadButton = false;
            return this;
        }
        
        public NotaryHostThankYouOptionsBuilder WithReviewDocumentsButton() {
            this.reviewDocumentsButton = true;
            return this;
        }

        public NotaryHostThankYouOptionsBuilder WithoutReviewDocumentsButton() {
            this.reviewDocumentsButton = false;
            return this;
        }

        public NotaryHostThankYouOptions Build() {
            NotaryHostThankYouOptions result = new NotaryHostThankYouOptions();
            result.Title = title;
            result.Body = body;
            result.RecipientName = recipientName;
            result.RecipientEmail = recipientEmail;
            result.RecipientRole = recipientRole;
            result.NotaryTag = notaryTag;
            result.RecipientStatus = recipientStatus;
            result.DownloadButton = downloadButton;
            result.ReviewDocumentsButton = reviewDocumentsButton;

            return result;
        }
    }
}