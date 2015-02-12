using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
/**
 * Created by whou on 08/12/14.
 */
namespace SDK.Examples
{
    public class ProxyConfigurationExample : SDKSample
    {
        public EslClient eslClientWithHttpProxy;

        private readonly string httpProxyURL = "10.0.22.81";
        private readonly int httpProxyPort = 8001;
        private bool allowAllSSLCertificates = true;

        private readonly string httpProxyWithCredentialsURL = "10.0.22.81";
        private readonly int httpProxyWithCredentialsPort = 8002;
        private readonly string httpProxyUserName = "httpUser";
        private readonly string httpProxyPassword = "httpPwd";

        public string email1;
        public EslClient eslClientWithHttpProxyHasCredentials;
        private Stream fileStream1, fileStream2;

        public static void Main (string[] args)
        {
            new ProxyConfigurationExample(Props.GetInstance()).Run();
        }

        override public void Execute()
        {
            ExecuteViaHttpProxy();
        }

        public ProxyConfigurationExample( Props props ) : this(props.Get("api.key"), props.Get("api.url"), props.Get("1.email")) {
        }

        public ProxyConfigurationExample(string apiKey, string apiUrl, string email1) : base(apiKey, apiUrl)
        {

            ProxyConfiguration httpProxyConfiguration = ProxyConfigurationBuilder.NewProxyConfiguration()
                .WithHttpHost(httpProxyURL)
                    .WithHttpPort(httpProxyPort)
                    .Build();

            ProxyConfiguration httpProxyWithCredentialsConfiguration = ProxyConfigurationBuilder.NewProxyConfiguration()
                .WithHttpHost(httpProxyWithCredentialsURL)
                    .WithHttpPort(httpProxyWithCredentialsPort)
                    .WithCredentials(httpProxyUserName, httpProxyPassword)
                    .Build();

            this.email1 = email1;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
            this.fileStream2 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);

            eslClientWithHttpProxy = new EslClient(apiKey, apiUrl, allowAllSSLCertificates, httpProxyConfiguration);
            eslClientWithHttpProxyHasCredentials = new EslClient(apiKey, apiUrl, allowAllSSLCertificates, httpProxyWithCredentialsConfiguration);

        }

        private DocumentPackage CreateTestPackage(Stream documentStream) {
            DocumentPackage package = PackageBuilder.NewPackageNamed("ProxyConfigurationExample: " + DateTime.Now)
                .DescribedAs("This is a new package")
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                                .WithFirstName("John")
                                .WithLastName("Smith"))
                    .WithDocument(DocumentBuilder.NewDocumentNamed("My Document")
                                  .FromStream(documentStream, DocumentType.PDF)
                                  .WithSignature(SignatureBuilder.SignatureFor(email1)
                                   .OnPage(0)
                                   .AtPosition(100, 100)))
                    .Build();

            return package;
        }

        public void ExecuteViaHttpProxy() {
            DocumentPackage packageTest = CreateTestPackage(fileStream1);
            packageId = eslClientWithHttpProxy.CreatePackage(packageTest);
            eslClientWithHttpProxy.SendPackage(packageId);
        }

        public void ExecuteViaHttpProxyWithCredentials() {
            DocumentPackage packageTest = CreateTestPackage(fileStream2);
            packageId = eslClientWithHttpProxyHasCredentials.CreatePackage(packageTest);
            eslClientWithHttpProxyHasCredentials.SendPackage(packageId);
        }

    }
}