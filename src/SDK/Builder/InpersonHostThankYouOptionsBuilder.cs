using System;

namespace OneSpanSign.Sdk
{
    public class InpersonHostThankYouOptionsBuilder
    {
        private Nullable<bool> title;
        private Nullable<bool> body;
        private Nullable<bool> recipientName;
        private Nullable<bool> recipientEmail;
        private Nullable<bool> recipientRole;
        private Nullable<bool> recipientStatus;
        private Nullable<bool> downloadButton;
        private Nullable<bool> reviewDocumentsButton;
        
        
        private InpersonHostThankYouOptionsBuilder()
        {
        }

        public static InpersonHostThankYouOptionsBuilder NewInpersonHostThankYouOptions() {
            return new InpersonHostThankYouOptionsBuilder();
        }

        public InpersonHostThankYouOptionsBuilder WithTitle() {
            this.title = true;
            return this;
        }

        public InpersonHostThankYouOptionsBuilder WithoutTitle() {
            this.title = false;
            return this;
        }
        
        public InpersonHostThankYouOptionsBuilder WithBody() {
            this.body = true;
            return this;
        }

        public InpersonHostThankYouOptionsBuilder WithoutBody() {
            this.body = false;
            return this;
        }
        
        public InpersonHostThankYouOptionsBuilder WithRecipientName() {
            this.recipientName = true;
            return this;
        }

        public InpersonHostThankYouOptionsBuilder WithoutRecipientName() {
            this.recipientName = false;
            return this;
        }
        
        public InpersonHostThankYouOptionsBuilder WithRecipientEmail() {
            this.recipientEmail = true;
            return this;
        }

        public InpersonHostThankYouOptionsBuilder WithoutRecipientEmail() {
            this.recipientEmail = false;
            return this;
        }
        
        public InpersonHostThankYouOptionsBuilder WithRecipientRole() {
            this.recipientRole = true;
            return this;
        }

        public InpersonHostThankYouOptionsBuilder WithoutRecipientRole() {
            this.recipientRole = false;
            return this;
        }
        
        public InpersonHostThankYouOptionsBuilder WithRecipientStatus() {
            this.recipientStatus = true;
            return this;
        }

        public InpersonHostThankYouOptionsBuilder WithoutRecipientStatus() {
            this.recipientStatus = false;
            return this;
        }
        
        public InpersonHostThankYouOptionsBuilder WithDownloadButton() {
            this.downloadButton = true;
            return this;
        }

        public InpersonHostThankYouOptionsBuilder WithoutDownloadButton() {
            this.downloadButton = false;
            return this;
        }
        
        public InpersonHostThankYouOptionsBuilder WithReviewDocumentsButton() {
            this.reviewDocumentsButton = true;
            return this;
        }

        public InpersonHostThankYouOptionsBuilder WithoutReviewDocumentsButton() {
            this.reviewDocumentsButton = false;
            return this;
        }

        public InpersonHostThankYouOptions Build() {
            InpersonHostThankYouOptions result = new InpersonHostThankYouOptions();
            result.Title = title;
            result.Body = body;
            result.RecipientName = recipientName;
            result.RecipientEmail = recipientEmail;
            result.RecipientRole = recipientRole;
            result.RecipientStatus = recipientStatus;
            result.DownloadButton = downloadButton;
            result.ReviewDocumentsButton = reviewDocumentsButton;

            return result;
        }
    }
}