namespace OneSpanSign.Sdk
{
    public class CompleteSummaryOptions
    {

        private bool _from;
        private bool title;
        private bool message;
        private bool download;
        private bool review;
        private bool _continue;

        public bool From
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
        
        public bool Message
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
        
        public bool Download
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
        
        public bool Review
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
        
        public bool Continue
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