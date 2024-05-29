using System;
using OneSpanSign.Sdk;
using System.IO;
using OneSpanSign.Sdk.Internal;
using System.Collections.Generic;

namespace SDK.Examples
{
    public abstract class SDKSample
    {
        protected OssClient ossClient;
        protected PackageId packageId;
        protected string packageName;
        protected DocumentPackage retrievedPackage;
        protected Stream fileStream1, fileStream2;

        protected Props props = Props.GetInstance();

        public string email1,
            email2,
            email3,
            email4,
            email5,
            email6,
            senderEmail,
            webpageUrl,
            senderUID,
            proxyHost,
            proxyWithCredentialsHost,
            proxyUserName,
            proxyPassword;

        public int proxyPort, proxyWithCredentialsPort;

        public SDKSample()
        {
            ossClient = OssClientBuilder.NewOssClientBuilder()
                .WithAdditionalHeaders(new Dictionary<String,String>())
                .WithProxyConfiguration(null)
                .WithProperties(props)
                .WithAuthenticationType(props.Get("api.auth.type"))
                .Build();

            SetProperties();
        }

        public SDKSample(string apiKey, string apiUrl)
        {
            ossClient = new OssClient(apiKey, apiUrl);
            SetProperties();
        }

        public abstract void Execute();

        public void Run()
        {
            Execute();
        }

        private void SetProperties()
        {
            email1 = props.Get("1.email");
            email2 = props.Get("2.email");
            email3 = props.Get("3.email");
            email4 = props.Get("4.email");
            email5 = props.Get("5.email");
            email6 = props.Get("6.email");
            senderEmail = props.Get("sender.email");
            webpageUrl = props.Get("webpage.url");
            proxyHost = props.Get("proxy.host");
            proxyPort = Int32.Parse(props.Get("proxy.port"));
            proxyWithCredentialsHost = props.Get("proxyWithCredentials.host");
            proxyWithCredentialsPort = Int32.Parse(props.Get("proxyWithCredentials.port"));
            proxyUserName = props.Get("proxy.userName");
            proxyPassword = props.Get("proxy.password");
            senderUID = Converter.apiKeyToUID(props.Get("api.key"));

            this.fileStream1 =
                File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/SampleDocuments/document.pdf").FullName);
            this.fileStream2 =
                File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/SampleDocuments/document.pdf").FullName);
        }

        public OssClient OssClient
        {
            get { return ossClient; }
        }

        public PackageId PackageId
        {
            get { return packageId; }
        }

        public DocumentPackage RetrievedPackage
        {
            get
            {
                if (null == retrievedPackage)
                {
                    retrievedPackage = ossClient.GetPackage(packageId);
                }

                return retrievedPackage;
            }
        }

        public string PackageName
        {
            get
            {
                if (null == packageName)
                {
                    packageName = this.GetType().Name + " : " + DateTime.Now;
                }

                return packageName;
            }
        }

        protected string GetRandomEmail()
        {
            return System.Guid.NewGuid().ToString().Replace("-", "") + "@simulator.amazonses.com";
        }
    }
}