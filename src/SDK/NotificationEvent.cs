using System;
using System.Collections.Generic;
using System.Reflection;
using OneSpanSign.Sdk.Internal;

namespace OneSpanSign.Sdk
{
    public class NotificationEvent : OssEnumeration
    {
        private static ILogger log = LoggerFactory.get(typeof(NotificationEvent));

        public static NotificationEvent PACKAGE_ACTIVATE = new NotificationEvent("PACKAGE_ACTIVATE", "PACKAGE_ACTIVATE", 0);
        public static NotificationEvent PACKAGE_COMPLETE = new NotificationEvent("PACKAGE_COMPLETE", "PACKAGE_COMPLETE", 1);
        public static NotificationEvent PACKAGE_EXPIRE = new NotificationEvent("PACKAGE_EXPIRE", "PACKAGE_EXPIRE", 2);
        public static NotificationEvent PACKAGE_DECLINE = new NotificationEvent("PACKAGE_DECLINE", "PACKAGE_DECLINE", 4);
        public static NotificationEvent SIGNER_COMPLETE = new NotificationEvent("SIGNER_COMPLETE", "SIGNER_COMPLETE", 5);
        public static NotificationEvent DOCUMENT_SIGNED = new NotificationEvent("DOCUMENT_SIGNED", "DOCUMENT_SIGNED", 6);
        public static NotificationEvent ROLE_REASSIGN = new NotificationEvent("ROLE_REASSIGN", "ROLE_REASSIGN", 7);
        public static NotificationEvent PACKAGE_CREATE = new NotificationEvent("PACKAGE_CREATE", "PACKAGE_CREATE", 8);
        public static NotificationEvent PACKAGE_DEACTIVATE = new NotificationEvent("PACKAGE_DEACTIVATE", "PACKAGE_DEACTIVATE", 9);
        public static NotificationEvent PACKAGE_READY_FOR_COMPLETION = new NotificationEvent("PACKAGE_READY_FOR_COMPLETE", "PACKAGE_READY_FOR_COMPLETION", 10);
        public static NotificationEvent PACKAGE_TRASH = new NotificationEvent("PACKAGE_TRASH", "PACKAGE_TRASH", 11);
        public static NotificationEvent PACKAGE_RESTORE = new NotificationEvent("PACKAGE_RESTORE", "PACKAGE_RESTORE", 12);
        public static NotificationEvent PACKAGE_DELETE = new NotificationEvent("PACKAGE_DELETE", "PACKAGE_DELETE", 13);
        public static NotificationEvent KBA_FAILURE = new NotificationEvent("KBA_FAILURE", "KBA_FAILURE", 14);
        public static NotificationEvent EMAIL_BOUNCE = new NotificationEvent("EMAIL_BOUNCE", "EMAIL_BOUNCE", 15);
        public static NotificationEvent PACKAGE_ATTACHMENT = new NotificationEvent("PACKAGE_ATTACHMENT", "PACKAGE_ATTACHMENT", 16);
        public static NotificationEvent SIGNER_LOCKED = new NotificationEvent("SIGNER_LOCKED", "SIGNER_LOCKED", 17);
        public static NotificationEvent PACKAGE_ARCHIVE = new NotificationEvent("PACKAGE_ARCHIVE", "PACKAGE_ARCHIVE", 18);
        public static NotificationEvent TEMPLATE_CREATE = new NotificationEvent("TEMPLATE_CREATE", "TEMPLATE_CREATE", 19);
        public static NotificationEvent DOCUMENT_VIEWED = new NotificationEvent("DOCUMENT_VIEWED", "DOCUMENT_VIEWED", 20);
        public static NotificationEvent ATTACHMENT_VALIDATION_RESULT = new NotificationEvent("ATTACHMENT_VALIDATION_RESULT", "ATTACHMENT_VALIDATION_RESULT", 21);
        public static NotificationEvent PACKAGE_DISPUTE = new NotificationEvent("PACKAGE_DISPUTE", "PACKAGE_DISPUTE", 22);
        public static NotificationEvent PACKAGE_ARCHIVAL_PENDING = new NotificationEvent("PACKAGE_ARCHIVAL_PENDING", "PACKAGE_ARCHIVAL_PENDING", 23);
        public static NotificationEvent PACKAGE_ARCHIVAL_VALIDATING = new NotificationEvent("PACKAGE_ARCHIVAL_VALIDATING", "PACKAGE_ARCHIVAL_VALIDATING", 24);
        public static NotificationEvent PACKAGE_ARCHIVAL_COMPLETED = new NotificationEvent("PACKAGE_ARCHIVAL_COMPLETED", "PACKAGE_ARCHIVAL_COMPLETED", 25);
        public static NotificationEvent PACKAGE_ARCHIVAL_INTERVENTION_REQUIRED = new NotificationEvent("PACKAGE_ARCHIVAL_INTERVENTION_REQUIRED", "PACKAGE_ARCHIVAL_INTERVENTION_REQUIRED", 26);
        public static NotificationEvent RECIPIENTS_SPECIFIED = new NotificationEvent("RECIPIENTS_SPECIFIED", "RECIPIENTS_SPECIFIED", 27);
        public static NotificationEvent RECORDINGS_READY = new NotificationEvent("RECORDINGS_READY", "RECORDINGS_READY", 28);
        public static NotificationEvent COMPOSITION_FAILED = new NotificationEvent("COMPOSITION_FAILED", "COMPOSITION_FAILED", 29);
        public static NotificationEvent EDEPOSIT_SUCCESS = new NotificationEvent("EDEPOSIT_SUCCESS", "EDEPOSIT_SUCCESS", 30);
        public static NotificationEvent EDEPOSIT_ATTEMPT_FAILURE = new NotificationEvent("EDEPOSIT_ATTEMPT_FAILURE", "EDEPOSIT_ATTEMPT_FAILURE", 31);
        public static NotificationEvent EDEPOSIT_FAILURE = new NotificationEvent("EDEPOSIT_FAILURE", "EDEPOSIT_FAILURE", 32);
        public static NotificationEvent COBROWSE_REQUEST = new NotificationEvent("COBROWSE_REQUEST", "COBROWSE_REQUEST", 33);
        public static NotificationEvent NOTARY_COMMISSION_CREATED = new NotificationEvent("NOTARY_COMMISSION_CREATED", "NOTARY_COMMISSION_CREATED", 34);
        public static NotificationEvent RECIPIENT_OPT_OUT_SMS = new NotificationEvent("RECIPIENT_OPT_OUT_SMS", "RECIPIENT_OPT_OUT_SMS", 35);
        public static NotificationEvent PACKAGE_SMS_NOTIFICATIONS_LIMIT_REACHED = new NotificationEvent("PACKAGE_SMS_NOTIFICATIONS_LIMIT_REACHED", "PACKAGE_SMS_NOTIFICATIONS_LIMIT_REACHED", 36);
        public static NotificationEvent RECIPIENT_BLOCKED = new NotificationEvent("RECIPIENT_BLOCKED", "RECIPIENT_BLOCKED", 37);

        private static Dictionary<string,NotificationEvent> allNotificationEvents = new Dictionary<string,NotificationEvent>();

        static NotificationEvent()
        {
            allNotificationEvents.Add(PACKAGE_ACTIVATE.getApiValue(), PACKAGE_ACTIVATE);
            allNotificationEvents.Add(PACKAGE_COMPLETE.getApiValue(), PACKAGE_COMPLETE);
            allNotificationEvents.Add(PACKAGE_EXPIRE.getApiValue(), PACKAGE_EXPIRE);
            allNotificationEvents.Add(PACKAGE_DECLINE.getApiValue(), PACKAGE_DECLINE);
            allNotificationEvents.Add(SIGNER_COMPLETE.getApiValue(), SIGNER_COMPLETE);
            allNotificationEvents.Add(DOCUMENT_SIGNED.getApiValue(), DOCUMENT_SIGNED);
            allNotificationEvents.Add(DOCUMENT_VIEWED.getApiValue(), DOCUMENT_VIEWED);
            allNotificationEvents.Add(ROLE_REASSIGN.getApiValue(), ROLE_REASSIGN);
            allNotificationEvents.Add(PACKAGE_CREATE.getApiValue(), PACKAGE_CREATE);
            allNotificationEvents.Add(PACKAGE_DEACTIVATE.getApiValue(), PACKAGE_DEACTIVATE);
            allNotificationEvents.Add(PACKAGE_READY_FOR_COMPLETION.getApiValue(), PACKAGE_READY_FOR_COMPLETION);
            allNotificationEvents.Add(PACKAGE_TRASH.getApiValue(), PACKAGE_TRASH);
            allNotificationEvents.Add(PACKAGE_RESTORE.getApiValue(), PACKAGE_RESTORE);
            allNotificationEvents.Add(PACKAGE_DELETE.getApiValue(), PACKAGE_DELETE);
            allNotificationEvents.Add(KBA_FAILURE.getApiValue(), KBA_FAILURE);
            allNotificationEvents.Add(EMAIL_BOUNCE.getApiValue(), EMAIL_BOUNCE);
            allNotificationEvents.Add(PACKAGE_ATTACHMENT.getApiValue(), PACKAGE_ATTACHMENT);
            allNotificationEvents.Add(SIGNER_LOCKED.getApiValue(), SIGNER_LOCKED);
            allNotificationEvents.Add(PACKAGE_ARCHIVE.getApiValue(), PACKAGE_ARCHIVE);
            allNotificationEvents.Add(TEMPLATE_CREATE.getApiValue(), TEMPLATE_CREATE);
            allNotificationEvents.Add(ATTACHMENT_VALIDATION_RESULT.getApiValue(), ATTACHMENT_VALIDATION_RESULT);
            allNotificationEvents.Add(PACKAGE_DISPUTE.getApiValue(), PACKAGE_DISPUTE);
            allNotificationEvents.Add(PACKAGE_ARCHIVAL_PENDING.getApiValue(), PACKAGE_ARCHIVAL_PENDING);
            allNotificationEvents.Add(PACKAGE_ARCHIVAL_VALIDATING.getApiValue(), PACKAGE_ARCHIVAL_VALIDATING);
            allNotificationEvents.Add(PACKAGE_ARCHIVAL_COMPLETED.getApiValue(), PACKAGE_ARCHIVAL_COMPLETED);
            allNotificationEvents.Add(PACKAGE_ARCHIVAL_INTERVENTION_REQUIRED.getApiValue(), PACKAGE_ARCHIVAL_INTERVENTION_REQUIRED);
            allNotificationEvents.Add(RECIPIENTS_SPECIFIED.getApiValue(), RECIPIENTS_SPECIFIED);
            allNotificationEvents.Add(RECORDINGS_READY.getApiValue(), RECORDINGS_READY);
            allNotificationEvents.Add(COMPOSITION_FAILED.getApiValue(), COMPOSITION_FAILED);
            allNotificationEvents.Add(EDEPOSIT_SUCCESS.getApiValue(), EDEPOSIT_SUCCESS);
            allNotificationEvents.Add(EDEPOSIT_ATTEMPT_FAILURE.getApiValue(), EDEPOSIT_ATTEMPT_FAILURE);
            allNotificationEvents.Add(EDEPOSIT_FAILURE.getApiValue(), EDEPOSIT_FAILURE);
            allNotificationEvents.Add(COBROWSE_REQUEST.getApiValue(), COBROWSE_REQUEST);
            allNotificationEvents.Add(NOTARY_COMMISSION_CREATED.getApiValue(), NOTARY_COMMISSION_CREATED);
            allNotificationEvents.Add(RECIPIENT_OPT_OUT_SMS.getApiValue(), RECIPIENT_OPT_OUT_SMS);
            allNotificationEvents.Add(PACKAGE_SMS_NOTIFICATIONS_LIMIT_REACHED.getApiValue(), PACKAGE_SMS_NOTIFICATIONS_LIMIT_REACHED);
            allNotificationEvents.Add(RECIPIENT_BLOCKED.getApiValue(), RECIPIENT_BLOCKED);
        }

        private NotificationEvent(string apiValue, string sdkValue, int index):base(apiValue,sdkValue,index) 
        {           
        }

        internal static NotificationEvent valueOf (string apiValue)
        {

            if (!String.IsNullOrEmpty(apiValue) && allNotificationEvents.ContainsKey(apiValue))
            {
                return allNotificationEvents[apiValue];
            }
            log.Warn("Unknown API NotificationEvent {0}. The upgrade is required.", apiValue);
            return new NotificationEvent(apiValue, "UNRECOGNIZED", allNotificationEvents.Values.Count);
        }

        public static string[] GetNames()
        {
            string[] names = new string[allNotificationEvents.Count];
            int i = 0;
            foreach(NotificationEvent notificationEvent in allNotificationEvents.Values)
            {
                names[i] = notificationEvent.GetName();
                i++;
            }
            return names;
        }

        public static explicit operator NotificationEvent(Enum enumType)
        {
            return parse(enumType.ToString());
        }

        public static NotificationEvent[] Values(){
            return (new List<NotificationEvent>(allNotificationEvents.Values)).ToArray();
        }
        
        public static NotificationEvent parse(string value)
        {

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

