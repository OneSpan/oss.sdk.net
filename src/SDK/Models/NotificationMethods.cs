using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OneSpanSign.API
{
    public class NotificationMethods
    {
        private ISet<NotificationMethod> _primary = new HashSet<NotificationMethod>();
        
        // Accessors
        [JsonProperty("primary")]
        public ISet<NotificationMethod> Primary
        {
            get { return _primary; }
            private set { _primary = value; }
        }

        public NotificationMethods(ISet<NotificationMethod> primary)
        {
            this.Primary =  primary;
        }
        
    }
}