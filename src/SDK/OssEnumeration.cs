using System;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
	public abstract class OssEnumeration
	{         
        private readonly string sdkValue;
        private readonly string apiValue;
        private readonly int index;

        protected OssEnumeration(string apiValue, int index):this(apiValue, apiValue, index) 
        {
		}

        protected OssEnumeration(string apiValue, string sdkValue, int index) 
        {
            this.apiValue = apiValue;
            this.sdkValue = sdkValue;           
            this.index = index;			
        }
		
		internal string getSdkValue() 
        {
			return this.sdkValue;
		}

        internal string getApiValue()
        {
			return this.apiValue;
        }

        public string GetName()
        {
            return getSdkValue();
        }

        public override string ToString()
        {
            return GetName();
        }

        public static T Convert<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        internal int Ordinal() 
        {
            return index;
        }

        public static implicit operator int(OssEnumeration eslEnum)
        {
            return eslEnum.Ordinal();
        }

        public static implicit operator string(OssEnumeration eslEnum)
        {
            return eslEnum.ToString();
        }
	}
}