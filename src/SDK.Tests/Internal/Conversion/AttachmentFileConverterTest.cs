using NUnit.Framework;
using System;
using OneSpanSign.Sdk;

namespace SDK.Tests
{

    [TestFixture]
    public class AttachmentFileConverterTest
    {

        private static readonly DateTime NOW = new DateTime ();

        [Test]
        public void convertNullSDKToAPI ()
        {
            OneSpanSign.API.AttachmentFile attachmentFile =
                    new AttachmentFileConverter ((OneSpanSign.Sdk.AttachmentFile)null).ToApiAttachmentFile ();

            Assert.IsNull (attachmentFile);
        }

        [Test]
        public void convertNullAPIToSDK ()
        {
            OneSpanSign.Sdk.AttachmentFile attachmentFile =
                    new AttachmentFileConverter ((OneSpanSign.API.AttachmentFile)null).ToSDKAttachmentFile ();

            Assert.IsNull (attachmentFile);

        }

        [Test]
        public void convertNullSDKToSDK ()
        {
            OneSpanSign.Sdk.AttachmentFile attachmentFile =
                    new AttachmentFileConverter ((OneSpanSign.Sdk.AttachmentFile)null).ToSDKAttachmentFile ();

            Assert.IsNull (attachmentFile);
        }

        [Test]
        public void convertNullAPIToAPI ()
        {
            OneSpanSign.API.AttachmentFile attachmentFile =
                    new AttachmentFileConverter ((OneSpanSign.API.AttachmentFile)null).ToApiAttachmentFile ();

            Assert.IsNull (attachmentFile);
        }

        [Test]
        public void convertSDKToSDK ()
        {
            OneSpanSign.Sdk.AttachmentFile sdkAttachmentFile1 = createSdkAttachmentFile ();
            OneSpanSign.Sdk.AttachmentFile sdkAttachmentFile2 = new AttachmentFileConverter (sdkAttachmentFile1).ToSDKAttachmentFile ();

            Assert.IsNotNull (sdkAttachmentFile2);
            Assert.AreEqual (sdkAttachmentFile2, sdkAttachmentFile1);
        }

        [Test]
        public void convertAPIToAPI ()
        {
            OneSpanSign.API.AttachmentFile apiAttachmentFile1 = createApiAttachmentFile ();
            OneSpanSign.API.AttachmentFile apiAttachmentFile2 = new AttachmentFileConverter (apiAttachmentFile1).ToApiAttachmentFile ();

            Assert.IsNotNull (apiAttachmentFile2);
            Assert.AreEqual (apiAttachmentFile2, apiAttachmentFile1);
        }

        [Test]
        public void convertAPIToSDK ()
        {
            OneSpanSign.API.AttachmentFile apiAttachmentFile1 = createApiAttachmentFile ();
            OneSpanSign.Sdk.AttachmentFile sdkAttachmentFile1 = new AttachmentFileConverter (apiAttachmentFile1).ToSDKAttachmentFile ();

            Assert.IsNotNull (sdkAttachmentFile1);
            Assert.AreEqual (sdkAttachmentFile1.Id, apiAttachmentFile1.Id);
            Assert.AreEqual (sdkAttachmentFile1.InsertDate, apiAttachmentFile1.GetInsertDate());
            Assert.AreEqual (sdkAttachmentFile1.Name, apiAttachmentFile1.Name);
            Assert.AreEqual (sdkAttachmentFile1.Preview, apiAttachmentFile1.Preview);
        }

        [Test]
        public void convertSDKToAPI ()
        {
            OneSpanSign.Sdk.AttachmentFile sdkAttachmentFile1 = createSdkAttachmentFile ();
            OneSpanSign.API.AttachmentFile apiAttachmentFile1 = new AttachmentFileConverter (sdkAttachmentFile1).ToApiAttachmentFile ();

            Assert.IsNotNull (apiAttachmentFile1);
            Assert.AreEqual (apiAttachmentFile1.Id, sdkAttachmentFile1.Id);
            Assert.AreEqual (apiAttachmentFile1.GetInsertDate(), sdkAttachmentFile1.InsertDate);
            Assert.AreEqual (apiAttachmentFile1.Name, sdkAttachmentFile1.Name);
            Assert.AreEqual (apiAttachmentFile1.Preview, sdkAttachmentFile1.Preview);
        }

        private OneSpanSign.API.AttachmentFile createApiAttachmentFile ()
        {
            OneSpanSign.API.AttachmentFile apiAttachmentFile = new OneSpanSign.API.AttachmentFile ();
            apiAttachmentFile.Id = 1;
            apiAttachmentFile.SetInsertDate (NOW.Ticks);
            apiAttachmentFile.Name = "apiAttachmentFile.jpeg";
            apiAttachmentFile.Preview = true;
            return apiAttachmentFile;
        }

        private OneSpanSign.Sdk.AttachmentFile createSdkAttachmentFile ()
        {
            OneSpanSign.Sdk.AttachmentFile sdkAttachmentFile = new OneSpanSign.Sdk.AttachmentFile ();
            sdkAttachmentFile.Id = 1;
            sdkAttachmentFile.InsertDate = NOW;
            sdkAttachmentFile.Name = "apiAttachmentFile.jpeg";
            sdkAttachmentFile.Preview = true;
            return sdkAttachmentFile;
        }



    }
}
