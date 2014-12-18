using System;
using System.Collections.Generic;
using System.Reflection;
using log4net;

namespace Silanis.ESL.SDK
{
    public class NotificationEvent : EslEnumeration
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static NotificationEvent PACKAGE_ACTIVATE = new NotificationEvent("PACKAGE_ACTIVATE", "PACKAGE_ACTIVATE");
        public static NotificationEvent PACKAGE_COMPLETE = new NotificationEvent("PACKAGE_COMPLETE", "PACKAGE_COMPLETE");
        public static NotificationEvent PACKAGE_EXPIRE = new NotificationEvent("PACKAGE_DELETE", "PACKAGE_EXPIRE");
        public static NotificationEvent PACKAGE_OPT_OUT = new NotificationEvent("PACKAGE_OPT_OUT", "PACKAGE_OPT_OUT");
        public static NotificationEvent PACKAGE_DECLINE = new NotificationEvent("PACKAGE_DECLINE", "PACKAGE_DECLINE");
        public static NotificationEvent SIGNER_COMPLETE = new NotificationEvent("SIGNER_COMPLETE", "SIGNER_COMPLETE");
        public static NotificationEvent DOCUMENT_SIGNED = new NotificationEvent("DOCUMENT_SIGNED", "DOCUMENT_SIGNED");
        public static NotificationEvent ROLE_REASSIGN = new NotificationEvent("ROLE_REASSIGN", "ROLE_REASSIGN");
        public static NotificationEvent PACKAGE_CREATE = new NotificationEvent("PACKAGE_CREATE", "PACKAGE_CREATE");
        public static NotificationEvent PACKAGE_DEACTIVATE = new NotificationEvent("PACKAGE_DEACTIVATE", "PACKAGE_DEACTIVATE");
        public static NotificationEvent PACKAGE_READY_FOR_COMPLETION = new NotificationEvent("PACKAGE_READY_FOR_COMPLETE", "PACKAGE_READY_FOR_COMPLETION");
        public static NotificationEvent PACKAGE_TRASH = new NotificationEvent("PACKAGE_TRASH", "PACKAGE_TRASH");
        public static NotificationEvent PACKAGE_RESTORE = new NotificationEvent("PACKAGE_RESTORE", "PACKAGE_RESTORE");
        public static NotificationEvent PACKAGE_DELETE = new NotificationEvent("PACKAGE_DELETE", "PACKAGE_DELETE");
        private static Dictionary<string,NotificationEvent> allNotificationEvents = new Dictionary<string,NotificationEvent>();

        static NotificationEvent(){
            allNotificationEvents.Add(PACKAGE_ACTIVATE.getApiValue(), PACKAGE_ACTIVATE);
            allNotificationEvents.Add(PACKAGE_COMPLETE.getApiValue(), PACKAGE_COMPLETE);
            allNotificationEvents.Add(PACKAGE_OPT_OUT.getApiValue(), PACKAGE_OPT_OUT);
            allNotificationEvents.Add(PACKAGE_DECLINE.getApiValue(), PACKAGE_DECLINE);
            allNotificationEvents.Add(SIGNER_COMPLETE.getApiValue(), SIGNER_COMPLETE);
            allNotificationEvents.Add(DOCUMENT_SIGNED.getApiValue(), DOCUMENT_SIGNED);
            allNotificationEvents.Add(ROLE_REASSIGN.getApiValue(), ROLE_REASSIGN);
            allNotificationEvents.Add(PACKAGE_CREATE.getApiValue(), PACKAGE_CREATE);
            allNotificationEvents.Add(PACKAGE_DEACTIVATE.getApiValue(), PACKAGE_DEACTIVATE);
            allNotificationEvents.Add(PACKAGE_READY_FOR_COMPLETION.getApiValue(), PACKAGE_READY_FOR_COMPLETION);
            allNotificationEvents.Add(PACKAGE_TRASH.getApiValue(), PACKAGE_TRASH);
            allNotificationEvents.Add(PACKAGE_RESTORE.getApiValue(), PACKAGE_RESTORE);
            allNotificationEvents.Add("PACKAGE_EXPIRE", PACKAGE_DELETE);
            allNotificationEvents.Add(PACKAGE_DELETE.getApiValue(), PACKAGE_DELETE);

        }

        private NotificationEvent(string apiValue, string sdkValue):base(apiValue,sdkValue) {           
        }

        internal static NotificationEvent valueOf (String apiValue){

            if (!String.IsNullOrEmpty(apiValue) && allNotificationEvents.ContainsKey(apiValue))
            {
                return allNotificationEvents[apiValue];
            }
            log.WarnFormat("Unknown API NotificationEvent {0}. The upgrade is required.", apiValue);
            return new NotificationEvent(apiValue, "UNRECOGNIZED");
        }

        public static string[] GetNames(){
            string[] names = new string[allNotificationEvents.Count];
            int i = 0;
            foreach(NotificationEvent notificationEvent in allNotificationEvents.Values){
                names[i] = notificationEvent.GetName();
                i++;
            }
            return names;
        }
        
        public static NotificationEvent parse(string value){

            if (null == value)
            {
                throw new ArgumentNullException("value is null");
            }

            if (value.Length == 0 || value.Trim().Length==0)
            {
                throw new ArgumentException("value is either an empty string or only contains white space");
            }
            foreach(NotificationEvent notificationEvent in allNotificationEvents.Values){
                if (String.Equals(notificationEvent.GetName(), value))
                {
                    return notificationEvent;
                }
            }
            throw new ArgumentException("value is a name, but not one of the named constants defined for the NotificationEvent");
        }
    }
}

