using System;

namespace OneSpanSign.Sdk
{
    public class CompleteSummaryOptions
    {

        private Nullable<bool> _from;
        private Nullable<bool> title;
        private Nullable<bool> message;
        private Nullable<bool> download;
        private Nullable<bool> review;
        private Nullable<bool> _continue;

        public Nullable<bool> From
        {
            get
            {
                return _from;
            }
            set
            {
                _from = value;
            }
        }
        
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

    }
}