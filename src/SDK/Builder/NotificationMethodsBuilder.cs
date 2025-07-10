using System.Collections.Generic;

namespace OneSpanSign.Sdk.Builder
{
    public class NotificationMethodsBuilder
    {
        private ISet<NotificationMethod> primary = new HashSet<NotificationMethod>();
        private string phone;
        
        private NotificationMethodsBuilder() {}
        
        public static NotificationMethodsBuilder NewNotificationMethods()
        {
            return new NotificationMethodsBuilder();
        }

        public NotificationMethodsBuilder WithPrimaryMethods(ISet<NotificationMethod> primary)
        {
            this.primary = primary;
            return this;
        }
        
        public NotificationMethodsBuilder WithPrimaryMethods(params NotificationMethod[] primaryMethods)
        {
            this.primary = new HashSet<NotificationMethod>(primaryMethods);
            return this;
        }

        public NotificationMethodsBuilder WithPhoneNumber(string phone)
        {
            this.phone = phone;
            return this;
        }

        public virtual NotificationMethods Build()
        {
            if (this.primary == null)
            {
                throw new BuilderException("No primary notification methods have been registered.");
            }
            NotificationMethods result = new NotificationMethods(primary, phone);
            return result;
        }
    }
}