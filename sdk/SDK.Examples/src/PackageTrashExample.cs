using System;
using System.IO;
using System.Globalization;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
	public class PackageTrashExample : SDKSample
	{
        public static void Main (string[] args)
        {
			new PackageTrashExample(Props.GetInstance()).Run();
        }

		public PackageTrashExample( Props props ) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email")) 
		{
        }

		public PackageTrashExample( string apiKey, string apiUrl, string email1 ) : base( apiKey, apiUrl ) 
		{        
        }

        override public void Execute()
        {
			DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed( "PackageTrashArchiveExample: " + DateTime.Now )
				.DescribedAs( "This package should be trashed" )					                                                           
                    .Build();

            PackageId packageId = eslClient.CreatePackage( superDuperPackage );
            
			Console.WriteLine("packageId = " + packageId);

			eslClient.PackageService.Trash(packageId);

			Console.WriteLine("Package {0} should be trashed", packageId);
        }
	}
}