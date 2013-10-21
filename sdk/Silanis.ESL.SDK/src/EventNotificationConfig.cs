using System;
using Silanis.ESL.API;
using System.Collections.Generic;

namespace Silanis.ESL.SDK
{
    public class EventNotificationConfig
    {
        private string url;
        private List<NotificationEvent> notificationEvents;

        public List<NotificationEvent> NotificationEvents
        {
            get
            {
                return notificationEvents;
            }
        }

        public EventNotificationConfig( string url )
        {
            this.url = url;
            notificationEvents = new List<NotificationEvent>();
        }

        internal Callback ToAPICallback() {
            Callback callback = new Callback();
            callback.Url = url;
            foreach ( NotificationEvent notificationEvent in notificationEvents ) {
                callback.AddEvent(ToAPICallbackEvent(notificationEvent));
            }
            return callback;
        }

        private static CallbackEvent ToAPICallbackEvent( NotificationEvent notificationEvent ) {
            switch (notificationEvent)
            {
                case NotificationEvent.PACKAGE_OPT_OUT:
                    return CallbackEvent.PACKAGE_OPT_OUT;
                case NotificationEvent.PACKAGE_COMPLETE:
                    return CallbackEvent.PACKAGE_COMPLETE;
                case NotificationEvent.PACKAGE_ACTIVATE:
                    return CallbackEvent.PACKAGE_ACTIVATE;
                case NotificationEvent.PACKAGE_CREATE:
                    return CallbackEvent.PACKAGE_CREATE;
                case NotificationEvent.PACKAGE_DEACTIVATE:
                    return CallbackEvent.PACKAGE_DEACTIVATE;
                case NotificationEvent.PACKAGE_DECLINE:
                    return CallbackEvent.PACKAGE_DECLINE;
                case NotificationEvent.PACKAGE_DELETE:
                    return CallbackEvent.PACKAGE_DELETE;
                case NotificationEvent.PACKAGE_READY_FOR_COMPLETION:
                    return CallbackEvent.PACKAGE_READY_FOR_COMPLETE;
                case NotificationEvent.PACKAGE_RESTORE:
                    return CallbackEvent.PACKAGE_RESTORE;
                case NotificationEvent.PACKAGE_TRASH:
                    return CallbackEvent.PACKAGE_TRASH;
                case NotificationEvent.ROLE_REASSIGN:
                    return CallbackEvent.ROLE_REASSIGN;
                default:
                    throw new InvalidCastException();
            }
        }
    }
}

