using System;
using System.Collections.Generic;
using System.Reflection;
using log4net;

namespace Silanis.ESL.SDK
{
    public class MessageStatus : EslEnumeration
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static MessageStatus NEW = new MessageStatus("NEW", "NEW", 0);
        public static MessageStatus READ = new MessageStatus("READ", "READ", 1);
        public static MessageStatus TRASHED = new MessageStatus("TRASHED", "TRASHED", 2);
        private static Dictionary<string,MessageStatus> allMessageStatus = new Dictionary<string,MessageStatus>();

        static MessageStatus(){
            allMessageStatus.Add(NEW.getApiValue(), MessageStatus.NEW);
            allMessageStatus.Add(READ.getApiValue(), MessageStatus.READ);
            allMessageStatus.Add(TRASHED.getApiValue(), MessageStatus.TRASHED);
        }

        private MessageStatus(string apiValue, string sdkValue, int index):base(apiValue,sdkValue,index) {           
        }

        internal static MessageStatus valueOf (String apiValue){

            if (!String.IsNullOrEmpty(apiValue) && allMessageStatus.ContainsKey(apiValue))
            {
                return allMessageStatus[apiValue];
            }
            log.WarnFormat("Unknown API MessageStatus {0}. The upgrade is required.", apiValue);
            return new MessageStatus(apiValue, "UNRECOGNIZED", allMessageStatus.Values.Count);
        }

        public static string[] GetNames(){
            string[] names = new string[allMessageStatus.Count];
            int i = 0;
            foreach(MessageStatus messageStatus in allMessageStatus.Values){
                names[i] = messageStatus.GetName();
                i++;
            }
            return names;
        }

        public static MessageStatus[] Values(){
            return (new List<MessageStatus>(allMessageStatus.Values)).ToArray();
        }

        public static MessageStatus parse(string value){

            if (null == value)
            {
                throw new ArgumentNullException("value is null");
            }

            if (value.Length == 0 || value.Trim().Length==0)
            {
                throw new ArgumentException("value is either an empty string or only contains white space");
            }
            foreach(MessageStatus messageStatus in allMessageStatus.Values){
                if (String.Equals(messageStatus.GetName(), value))
                {
                    return messageStatus;
                }
            }
            throw new ArgumentException("value is a name, but not one of the named constants defined for the MessageStatus");
        }

    }
}

