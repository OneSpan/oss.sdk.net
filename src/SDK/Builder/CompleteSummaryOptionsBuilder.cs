using System;

namespace OneSpanSign.Sdk
{
    public class CompleteSummaryOptionsBuilder
    {
        private Nullable<bool> from;
        private Nullable<bool> title;
        private Nullable<bool> message;
        private Nullable<bool> download;
        private Nullable<bool> review;
        private Nullable<bool> _continue;
        
        
        private CompleteSummaryOptionsBuilder()
        {
        }

        public static CompleteSummaryOptionsBuilder NewCompleteSummaryOptions() {
            return new CompleteSummaryOptionsBuilder();
        }

        public CompleteSummaryOptionsBuilder WithFrom() {
            this.from = true;
            return this;
        }

        public CompleteSummaryOptionsBuilder WithoutFrom() {
            this.from = false;
            return this;
        }
        
        public CompleteSummaryOptionsBuilder WithTitle() {
            this.title = true;
            return this;
        }

        public CompleteSummaryOptionsBuilder WithoutTitle() {
            this.title = false;
            return this;
        }
        
        public CompleteSummaryOptionsBuilder WithMessage() {
            this.message = true;
            return this;
        }

        public CompleteSummaryOptionsBuilder WithoutMessage() {
            this.message = false;
            return this;
        }
        
        public CompleteSummaryOptionsBuilder WithDownload() {
            this.download = true;
            return this;
        }

        public CompleteSummaryOptionsBuilder WithoutDownload() {
            this.download = false;
            return this;
        }
        
        public CompleteSummaryOptionsBuilder WithReview() {
            this.review = true;
            return this;
        }

        public CompleteSummaryOptionsBuilder WithoutReview() {
            this.review = false;
            return this;
        }
        
        public CompleteSummaryOptionsBuilder WithContinue() {
            this._continue = true;
            return this;
        }

        public CompleteSummaryOptionsBuilder WithoutContinue() {
            this._continue = false;
            return this;
        }

        public CompleteSummaryOptions Build() {
            CompleteSummaryOptions result = new CompleteSummaryOptions();
            result.From = from;
            result.Title = title;
            result.Message = message;
            result.Download = download;
            result.Review = review;
            result.Continue = _continue;

            return result;
        }
    }
}