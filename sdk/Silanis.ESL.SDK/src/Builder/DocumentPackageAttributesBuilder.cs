using System;

namespace Silanis.ESL.SDK.Builder
{
    public class DocumentPackageAttributesBuilder
    {
        private DocumentPackageAttributes attributes = new DocumentPackageAttributes();

        public DocumentPackageAttributesBuilder withAttribute( string name, Object value ) {
            this.attributes.Append(name, value);
            return this;
        }

        public DocumentPackageAttributes build() {
			Support.LogMethodEntry();
			Support.LogMethodExit(attributes);
            return attributes;
        }
    }
}

