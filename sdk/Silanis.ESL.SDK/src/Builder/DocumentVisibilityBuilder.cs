using System;
using System.Collections.Generic;

namespace Silanis.ESL.SDK
{
    public class DocumentVisibilityBuilder
    {
        private IList<DocumentVisibilityConfiguration> configurations = new List<DocumentVisibilityConfiguration>();

        public DocumentVisibilityBuilder()
        {
        }

        public static DocumentVisibilityBuilder NewDocumentVisibility() 
        {
            return new DocumentVisibilityBuilder();
        }

        public DocumentVisibilityBuilder WithConfigurations(List<DocumentVisibilityConfiguration> configurations) 
        {
            this.configurations = configurations;
            return this;
        }

        public DocumentVisibilityBuilder AddConfiguration(DocumentVisibilityConfiguration configuration) 
        {
            this.configurations.Add(configuration);
            return this;
        }

        public DocumentVisibilityBuilder AddConfiguration(DocumentVisibilityConfigurationBuilder builder) 
        {
            return AddConfiguration(builder.Build());
        }

        public DocumentVisibility Build() 
        {
            DocumentVisibility visibility = new DocumentVisibility();
            visibility.AddConfigurations(configurations);
            return visibility;
        }
    }
}

