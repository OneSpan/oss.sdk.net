using System;
using OneSpanSign.Sdk;

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
            new ListTemplatesExample().Run();
        }

        override public void Execute()
        {
            templates = ossClient.PackageService.GetTemplates(new PageRequest(0), Visibility.SENDER);
            Console.WriteLine("Templates = " + templates.Size);
        }
    }
}

