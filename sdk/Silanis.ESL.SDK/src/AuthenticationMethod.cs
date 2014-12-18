using System;
using System.Collections.Generic;
using System.Reflection;
using log4net;

namespace Silanis.ESL.SDK
{
    public class AuthenticationMethod : EslEnumeration
	{
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static AuthenticationMethod EMAIL = new AuthenticationMethod("NONE", "EMAIL");
        public static AuthenticationMethod CHALLENGE = new AuthenticationMethod("CHALLENGE", "CHALLENGE");
        public static AuthenticationMethod SMS = new AuthenticationMethod("SMS", "SMS");
        public static AuthenticationMethod KBA = new AuthenticationMethod("KBA", "KBA");
        private static Dictionary<string,AuthenticationMethod> allAuthenticationMethods = new Dictionary<string,AuthenticationMethod>();

        static AuthenticationMethod(){
            allAuthenticationMethods.Add(EMAIL.getApiValue(), AuthenticationMethod.EMAIL);
            allAuthenticationMethods.Add(CHALLENGE.getApiValue(), AuthenticationMethod.CHALLENGE);
            allAuthenticationMethods.Add(SMS.getApiValue(), AuthenticationMethod.SMS);
            allAuthenticationMethods.Add(KBA.getApiValue(), AuthenticationMethod.KBA);
        }

        private AuthenticationMethod(string apiValue, string sdkValue):base(apiValue,sdkValue) {           
        }

        internal static AuthenticationMethod valueOf (String apiValue){

            if (!String.IsNullOrEmpty(apiValue) && allAuthenticationMethods.ContainsKey(apiValue))
            {
                return allAuthenticationMethods[apiValue];
            }
            log.WarnFormat("Unknown API AuthenticationMethod {0}. The upgrade is required.", apiValue);
            return new AuthenticationMethod(apiValue, "UNRECOGNIZED");
        }

        public static string[] GetNames(){
            string[] names = new string[allAuthenticationMethods.Count];
            int i = 0;
            foreach(AuthenticationMethod authenticationMethod in allAuthenticationMethods.Values){
                names[i] = authenticationMethod.GetName();
                i++;
            }
            return names;
        }

        public static AuthenticationMethod parse(string value){

            if (null == value)
            {
                throw new ArgumentNullException("value is null");
            }

            if (value.Length == 0 || value.Trim().Length==0)
            {
                throw new ArgumentException("value is either an empty string or only contains white space");
            }
            foreach(AuthenticationMethod authenticationMethod in allAuthenticationMethods.Values){
                if (String.Equals(authenticationMethod.GetName(), value))
                {
                    return authenticationMethod;
                }
            }
            throw new ArgumentException("value is a name, but not one of the named constants defined for the AuthenticationMethod");
        }


	}


}