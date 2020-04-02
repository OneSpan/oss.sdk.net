using System;
using OneSpanSign.Sdk;
using NUnit.Framework;
using OneSpanSign.Sdk.Builder;

namespace SDK.Tests
{
	[TestFixture]
	public class ossClientTest
	{
		[Test]
		public void CannotCreateClientWithNullAPIKey()
		{
            Assert.Throws<OssException>(() => new OssClient (null, "http://localhost"));
		}

		[Test]
		public void CannotCreateClientWithNullURL() 
		{
            Assert.Throws<OssException>(() => new OssClient ("key", null));
		}
        
        [Test]
        public void GetVersionFromAbsentAttributes()
        {
            DocumentPackage package = CreateDefaultDocumentPackage();
            OssClient ossClient = CreateDefaultossClient();
            Assert.AreEqual( false, ossClient.IsSdkVersionSetInPackageData(package) );
        }
        
        [Test]
        public void GetVersionFromEmptyAttributes()
        {
            DocumentPackage package = CreateDefaultDocumentPackage();
            package.Attributes = new DocumentPackageAttributes();
            OssClient ossClient = CreateDefaultossClient();
            Assert.AreEqual( false, ossClient.IsSdkVersionSetInPackageData(package) );
        }
        
        [Test]
        public void GetVersionFromNonEmptyAttributes()
        {
            DocumentPackage package = CreateDefaultDocumentPackage();
            package.Attributes = new DocumentPackageAttributes();
            package.Attributes.Append("key", "value");
            OssClient ossClient = CreateDefaultossClient();
            Assert.AreEqual( false, ossClient.IsSdkVersionSetInPackageData(package) );
        }
        
        [Test]
        public void GetVersionWhenPresentInAttributes()
        {
            DocumentPackage package = CreateDefaultDocumentPackage();
            package.Attributes = new DocumentPackageAttributes();
            package.Attributes.Append("key", "value");
            package.Attributes.Append("sdk", "v???");
            OssClient ossClient = CreateDefaultossClient();
            Assert.AreEqual( true, ossClient.IsSdkVersionSetInPackageData(package) );
        }
        
        private DocumentPackage CreateDefaultDocumentPackage()
        {
            return PackageBuilder.NewPackageNamed("Package Name").Build();
        }
        
        private OssClient CreateDefaultossClient()
        {
            return new OssClient("key","url");
        }
	}
}