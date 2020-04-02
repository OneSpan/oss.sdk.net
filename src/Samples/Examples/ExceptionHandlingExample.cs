using System;
using OneSpanSign.Sdk.Builder;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    public class ExceptionHandlingExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new ExceptionHandlingExample().Run();
        }

        override public void Execute()
        {
            SignerBuilder signerBuilder = SignerBuilder.NewSignerWithEmail("john.smith@email.com")
                            .WithFirstName("John")
                            .WithLastName("Smith")
                            .WithTitle("Managing Director")
                            .WithCompany("Acme Inc.");
                            
            Signer signer = null;
            try
            {
                signer = signerBuilder.Build();
            }
            catch (OssException OssException)
            {
                Console.Out.WriteLine(OssException.Message);
                return;
            }

            try
            {
                ossClient.PackageService.AddSigner(new PackageId("myPackageId"),signer);                                
            }
            catch (OssServerException OssServerException)
            {
                Console.Out.WriteLine(OssServerException.Message);
                Console.Out.WriteLine(OssServerException.ServerError.Code);
                Console.Out.WriteLine(OssServerException.ServerError.Message);
                Console.Out.WriteLine(OssServerException.ServerError.Technical);
            }
            catch (OssException OssException)
            {
                Console.Out.WriteLine(OssException.Message);
                return;
            }
        }
    }
}

