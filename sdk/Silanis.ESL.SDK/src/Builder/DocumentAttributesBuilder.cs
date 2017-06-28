using System;
using System.Collections.Generic;

namespace Silanis.ESL.SDK
{
    public class DocumentAttributesBuilder
    {
        private IDictionary<string, object> data = new Dictionary<string, object>();

        public DocumentAttributesBuilder()
        {
        }

        public static DocumentAttributesBuilder NewDocumentAttributes() 
        {
            return new DocumentAttributesBuilder();
        }

        public DocumentAttributesBuilder(IDictionary<string, object> data)
        {
            this.data = data;
        }

        public DocumentAttributesBuilder AddAttribute(string name, Object value)
        {
            this.data.Add(name, value);
            return this;
        }

        public IDictionary<string, object> Build() {
            return data;
        }
    }
}

