using System;

namespace Silanis.ESL.SDK
{
    public class DocumentPackageAttributesBuilder
    {
        private DocumentPackageAttributes attributes = new DocumentPackageAttributes();

        public DocumentPackageAttributesBuilder withAttribute( String name, Object value ) {
            this.attributes.append(name, value);
            return this;
        }

        public DocumentPackageAttributes build() {
            return attributes;
        }
    }
}

