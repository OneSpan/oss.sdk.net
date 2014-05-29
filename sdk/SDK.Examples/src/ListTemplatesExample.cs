using System;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    public class ListTemplatesExample : SDKSample
    {
        public static void Main (string[] args)
        {
            new ListTemplatesExample(Props.GetInstance()).Run();
        }

        public ListTemplatesExample( Props props ) : this(props.Get("api.url"), props.Get("api.key")) {
        }

        public ListTemplatesExample( string apiKey, string apiUrl ) : base( apiKey, apiUrl ) {
        }

        override public void Execute()
        {
            Page<DocumentPackage> templates = eslClient.PackageService.GetTemplates(new PageRequest(0));
            Console.WriteLine("Templates = " + templates.Size);
        }
    }
}

