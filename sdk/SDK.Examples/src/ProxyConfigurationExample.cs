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
        public EslClient EslClientWithHttpProxy;
        private readonly string HttpProxyURL = "localhost";
        private readonly int HttpProxyPort = 8001;

        public EslClient EslClientWithHttpProxyHasCredentials;
        private readonly string HttpProxyWithCredentialsURL = "localhost";
        private readonly int HttpProxyWithCredentialsPort = 8002;
        private readonly string HttpProxyUserName = "httpUser";
        private readonly string HttpProxyPassword = "httpPwd";

        public EslClient EslClientWithHttpsProxy;
        private readonly string HttpsProxyURL = "localhost";
        private readonly int HttpsProxyPort = 8003;

        public EslClient EslClientWithHttpsProxyHasCredentials;
        private readonly string HttpsProxyWithCredentialsURL = "localhost";
        private readonly int HttpsProxyWithCredentialsPort = 8004;
        private readonly string HttpsProxyUserName = "httpsUser";
        private readonly string HttpsProxyPassword = "httpsPwd";

        public readonly string Email1;
        private Stream FileStream1, FileStream2, FileStream3, FileStream4;

        public static void Main (string[] args)
        {
            new ProxyConfigurationExample(Props.GetInstance()).Run();
        }

        override public void Execute()
        {
            ExecuteViaHttpProxy();
            ExecuteViaHttpProxyWithCredentials();
            ExecuteViaHttpsProxy();
            ExecuteViaHttpsProxyWithCredentials();
        }

        public ProxyConfigurationExample( Props props ) : this(props.Get("api.url"), props.Get("api.key"), props.Get("allow.all.sslcertificates"), props.Get("1.email")) {
        }

        public ProxyConfigurationExample(string apiKey, string apiUrl, string allowAllSSLCertificates, string email1) 
            : base(apiKey, apiUrl)
        {

            ProxyConfiguration httpProxyConfiguration = ProxyConfigurationBuilder.NewProxyConfiguration()
                .WithHttpHost(HttpProxyURL)
                    .WithHttpPort(HttpProxyPort)
                    .Build();

            ProxyConfiguration httpProxyWithCredentialsConfiguration = ProxyConfigurationBuilder.NewProxyConfiguration()
                .WithHttpHost(HttpProxyWithCredentialsURL)
                    .WithHttpPort(HttpProxyWithCredentialsPort)
                    .WithCredentials(HttpProxyUserName, HttpProxyPassword)
                    .Build();

            ProxyConfiguration httpsProxyConfiguration = ProxyConfigurationBuilder.NewProxyConfiguration()
                .WithHttpsHost(HttpsProxyURL)
                    .WithHttpsPort(HttpsProxyPort)
                    .Build();

            ProxyConfiguration httpsProxyWithCredentialsConfiguration = ProxyConfigurationBuilder.NewProxyConfiguration()
                .WithHttpsHost(HttpsProxyWithCredentialsURL)
                    .WithHttpsPort(HttpsProxyWithCredentialsPort)
                    .WithCredentials(HttpsProxyUserName, HttpsProxyPassword)
                    .Build();

            this.Email1 = email1;
            this.FileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
            this.FileStream2 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
            this.FileStream3 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
            this.FileStream4 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);

            EslClientWithHttpProxy = new EslClient(apiKey, apiUrl, Boolean.Parse(allowAllSSLCertificates), httpProxyConfiguration);
            EslClientWithHttpProxyHasCredentials = new EslClient(apiKey, apiUrl, Boolean.Parse(allowAllSSLCertificates), httpProxyWithCredentialsConfiguration);
            EslClientWithHttpsProxy = new EslClient(apiKey, apiUrl, Boolean.Parse(allowAllSSLCertificates), httpsProxyConfiguration);
            EslClientWithHttpsProxyHasCredentials = new EslClient(apiKey, apiUrl, Boolean.Parse(allowAllSSLCertificates), httpsProxyWithCredentialsConfiguration);
        }

        private DocumentPackage CreateTestPackage(Stream documentStream) {
            DocumentPackage package = PackageBuilder.NewPackageNamed("C# CreatePackageFromStream " + DateTime.Now)
                .DescribedAs("This is a new package")
                    .WithSigner(SignerBuilder.NewSignerWithEmail(Email1)
                                .WithFirstName("John")
                                .WithLastName("Smith"))
                    .WithDocument(DocumentBuilder.NewDocumentNamed("My Document")
                                  .FromStream(documentStream, DocumentType.PDF)
                                  .WithSignature(SignatureBuilder.SignatureFor(Email1)
                                   .OnPage(0)
                                   .AtPosition(100, 100)))
                    .Build();

            return package;
        }

        public void ExecuteViaHttpProxy() {
            DocumentPackage packageTest = CreateTestPackage(FileStream1);
            packageId = EslClientWithHttpProxy.CreatePackage(packageTest);
            EslClientWithHttpProxy.SendPackage(packageId);
        }

        public void ExecuteViaHttpProxyWithCredentials() {
            DocumentPackage packageTest = CreateTestPackage(FileStream2);
            packageId = EslClientWithHttpProxyHasCredentials.CreatePackage(packageTest);
            EslClientWithHttpProxyHasCredentials.SendPackage(packageId);
        }

        public void ExecuteViaHttpsProxy() {
            DocumentPackage packageTest = CreateTestPackage(FileStream3);
            packageId = EslClientWithHttpsProxy.CreatePackage(packageTest);
            EslClientWithHttpsProxy.SendPackage(packageId);
        }

        public void ExecuteViaHttpsProxyWithCredentials() {
            DocumentPackage packageTest = CreateTestPackage(FileStream4);
            packageId = EslClientWithHttpsProxyHasCredentials.CreatePackage(packageTest);
            EslClientWithHttpsProxyHasCredentials.SendPackage(packageId);
        }

    }
}