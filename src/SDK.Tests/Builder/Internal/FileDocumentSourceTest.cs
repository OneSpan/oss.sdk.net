using System;
using System.IO;
using NUnit.Framework;
using OneSpanSign.Sdk.Builder.Internal;
using OneSpanSign.Sdk;

namespace SDK.Tests
{
	public class FileDocumentSourceTest
	{
		[Test]
		public void ReadsFileContent()
		{
			FileInfo file = new FileInfo (Directory.GetCurrentDirectory() + "/document.pdf");
			FileDocumentSource source = new FileDocumentSource (file.FullName);

			byte[] content = source.Content ();

			Assert.IsNotNull (content);
			Assert.IsTrue (content.Length > 0);
		}

		[Test]
		public void ValidatesFileExistence()
		{
            Assert.Throws<OssException>(() => new FileDocumentSource ("coco.pdf"));
		}
	}
}