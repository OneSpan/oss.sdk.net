using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    public class AccountUploadSettings
    {
        private List<string> _allowedFileTypes;
        
        public List<string> AllowedFileTypes
        {
            get
            {
                return _allowedFileTypes;
            }
            set
            {
                _allowedFileTypes = value;
            }
        }
    }
}