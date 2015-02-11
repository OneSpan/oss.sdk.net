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
//        private readonly string HttpProxyURL = Props.GetInstance().Get("proxy.http.host");
//        private readonly int HttpProxyPort = Int32.Parse(Props.GetInstance().Get("proxy.http.port"));

        private readonly string HttpProxyURL = "localhost";
        private readonly int HttpProxyPort = 8001;

        public EslClient EslClientWithHttpProxyHasCredentials;
        private readonly string HttpProxyWithCredentialsURL = Props.GetInstance().Get("proxy.httpWithCredentials.host");
        private readonly int HttpProxyWithCredentialsPort = Int32.Parse(Props.GetInstance().Get("proxy.httpWithCredentials.port"));
        private readonly string HttpProxyUserName = Props.GetInstance().Get("proxy.UserName");
        private readonly string HttpProxyPassword = Props.GetInstance().Get("proxy.Password");

        public readonly string Email1;
        private Stream FileStream1, FileStream2;

        public static void Main (string[] args)
        {
            new ProxyConfigurationExample(Props.GetInstance()).Run();
        }

        override public void Execute()
        {
            ExecuteViaHttpProxy();
            //ExecuteViaHttpProxyWithCredentials();
        }

        public ProxyConfigurationExample( Props props ) : this(props.Get("api.key"), props.Get("api.url"), props.Get("allow.all.sslcertificates"), props.Get("1.email")) {
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

            this.Email1 = email1;
            this.FileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
            this.FileStream2 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);

            EslClientWithHttpProxy = new EslClient(apiKey, apiUrl, Boolean.Parse(allowAllSSLCertificates), httpProxyConfiguration);
            EslClientWithHttpProxyHasCredentials = new EslClient(apiKey, apiUrl, Boolean.Parse(allowAllSSLCertificates), httpProxyWithCredentialsConfiguration);

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

    }
}