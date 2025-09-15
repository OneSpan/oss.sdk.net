using System;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    public class SystemAlert
    {
        public SeverityLevel SeverityLevel 
        {
            get; 
            set;
        }
        public String Code 
        {
            get; 
            set;
        }
        public String DefaultMessage 
        {
            get; 
            set;
        }
        public Dictionary<string,string> Parameters 
        {
            get; 
            set;
        }
    }
}
