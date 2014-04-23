using System;

namespace Silanis.ESL.SDK.Builder
{
    public class DocumentPackageAttributesBuilder
    {
        private DocumentPackageAttributes attributes = new DocumentPackageAttributes();

        
        public DocumentPackageAttributesBuilder WithAttribute(string name, Object value)
        {
            this.attributes.Append(name, value);
            return this;
        }
        
        [Obsolete]
        public DocumentPackageAttributesBuilder withAttribute( string name, Object value ) {
            return WithAttribute( name, value );
        }

        [Obsolete]
        public DocumentPackageAttributes build()
        {
            return Build();
        }

        public DocumentPackageAttributes Build() {
			Support.LogMethodEntry();
			Support.LogMethodExit(attributes);
            return attributes;
        }
    }
}

