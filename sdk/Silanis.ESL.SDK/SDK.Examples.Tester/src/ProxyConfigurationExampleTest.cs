using System;
using NUnit.Framework;
using Silanis.ESL.SDK;
using NUnit.Framework.SyntaxHelpers;
using System.Collections.Generic;
using TrotiNet;

namespace SDK.Examples
{
    public class ProxyConfigurationExampleTest
    {
        private readonly int httpProxyPort = 8001;
        private readonly int httpProxyWithCredentialsPort = 8002;
        private readonly bool UseIPv6 = false;

        [Test]
        public void verifyResult()
        {
            ExecutionByHttpProxy();
            //ExecutionByHttpProxyWithCredentials();
        }

        public void ExecutionByHttpProxy(){

            var Server = new TcpServer(httpProxyPort, UseIPv6);
            Server.Start(TransparentProxy.CreateProxy);

            Server.InitListenFinished.WaitOne();
            if (Server.InitListenException != null)
                throw Server.InitListenException;

//            while (true)
//                System.Threading.Thread.Sleep(1000);

            ProxyConfigurationExample example = new ProxyConfigurationExample(Props.GetInstance());
            example.ExecuteViaHttpProxy();
            DocumentPackage documentPackage1 = example.EslClientWithHttpProxy.GetPackage(example.PackageId);
            Assert.AreEqual(documentPackage1.Id, example.PackageId);

            Server.Stop();

        }

        public void ExecutionByHttpProxyWithCredentials(){

            var Server = new TcpServer(httpProxyWithCredentialsPort, UseIPv6);
            Server.Start(TransparentProxy.CreateProxy);

            Server.InitListenFinished.WaitOne();
            if (Server.InitListenException != null)
                throw Server.InitListenException;

//            while (true)
//                System.Threading.Thread.Sleep(1000);

            ProxyConfigurationExample example = new ProxyConfigurationExample(Props.GetInstance());
            example.ExecuteViaHttpProxyWithCredentials();
            DocumentPackage documentPackage2 = example.EslClientWithHttpProxyHasCredentials.GetPackage(example.PackageId);
            Assert.AreEqual(documentPackage2.Id, example.PackageId);

            Server.Stop();

        }

        public class TransparentProxy : ProxyLogic
        {
            public TransparentProxy(HttpSocket clientSocket) : base(clientSocket) { }

            static new public TransparentProxy CreateProxy(HttpSocket clientSocket)
            {
                return new TransparentProxy(clientSocket);
            }

            protected override void OnReceiveRequest()
            {
                Console.WriteLine("-> " + RequestLine + " from HTTP referer " + RequestHeaders.Referer);
            }

            protected override void OnReceiveResponse()
            {
                Console.WriteLine("<- " + ResponseStatusLine + " with HTTP Content-Length: " + (ResponseHeaders.ContentLength ?? 0));
            }
        }

    }
}

