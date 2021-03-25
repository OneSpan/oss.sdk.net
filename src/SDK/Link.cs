using System;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    public class Link : Handover
    {
        public Nullable<bool> AutoRedirect 
        {
            get;
            set;
        }
        
        public HashSet<String> Parameters 
        {
            get;
            set;
        }

    }
    
    public enum PARAMETETS
    {
        PACKAGE,
        SIGNER,
        STATUS
    }
}