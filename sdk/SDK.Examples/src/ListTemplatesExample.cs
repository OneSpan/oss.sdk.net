using System;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    public class ListTemplatesExample : SDKSample
    {
        private Page<DocumentPackage> templates;

        public Page<DocumentPackage> Templates
        {
            get
            {
                return templates;
            }
        }

        public static void Main (string[] args)
        {
            new ListTemplatesExample(Props.GetInstance()).Run();
        }

        public ListTemplatesExample( Props props ) : this(props.Get("api.key"), props.Get("api.url")) {
        }

        public ListTemplatesExample( string apiKey, string apiUrl ) : base( apiKey, apiUrl ) {
        }

        override public void Execute()
        {
            templates = eslClient.PackageService.GetTemplates(new PageRequest(0));
            Console.WriteLine("Templates = " + templates.Size);
        }
    }
}

