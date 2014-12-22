using System;
using System.Collections.Generic;

namespace Silanis.ESL.SDK
{
	public abstract class EslEnumeration
	{         
        private readonly string sdkValue;

        private readonly string apiValue;

		protected EslEnumeration(string apiValue):this(apiValue, apiValue) {
		}

        protected EslEnumeration(string apiValue, string sdkValue) {
            this.apiValue = apiValue;
            this.sdkValue = sdkValue;			
        }
		
		internal string getSdkValue() {
			return this.sdkValue;
		}

        internal string getApiValue(){
			return this.apiValue;
        }

        public string GetName(){
            return getSdkValue();
        }

        public override string ToString()
        {
            return GetName();
        }

	}
}