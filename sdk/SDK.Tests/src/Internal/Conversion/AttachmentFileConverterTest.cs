using NUnit.Framework;
using System;
using Silanis.ESL.SDK;

namespace SDK.Tests
{

    [TestFixture]
    public class AttachmentFileConverterTest
    {

        private static readonly DateTime NOW = new DateTime ();

        [Test]
        public void convertNullSDKToAPI ()
        {
            Silanis.ESL.API.AttachmentFile attachmentFile =
                    new AttachmentFileConverter ((Silanis.ESL.SDK.AttachmentFile)null).ToApiAttachmentFile ();

            Assert.IsNull (attachmentFile);
        }

        [Test]
        public void convertNullAPIToSDK ()
        {
            Silanis.ESL.SDK.AttachmentFile attachmentFile =
                    new AttachmentFileConverter ((Silanis.ESL.API.AttachmentFile)null).ToSDKAttachmentFile ();

            Assert.IsNull (attachmentFile);

        }

        [Test]
        public void convertNullSDKToSDK ()
        {
            Silanis.ESL.SDK.AttachmentFile attachmentFile =
                    new AttachmentFileConverter ((Silanis.ESL.SDK.AttachmentFile)null).ToSDKAttachmentFile ();

            Assert.IsNull (attachmentFile);
        }

        [Test]
        public void convertNullAPIToAPI ()
        {
            Silanis.ESL.API.AttachmentFile attachmentFile =
                    new AttachmentFileConverter ((Silanis.ESL.API.AttachmentFile)null).ToApiAttachmentFile ();

            Assert.IsNull (attachmentFile);
        }

        [Test]
        public void convertSDKToSDK ()
        {
            Silanis.ESL.SDK.AttachmentFile sdkAttachmentFile1 = createSdkAttachmentFile ();
            Silanis.ESL.SDK.AttachmentFile sdkAttachmentFile2 = new AttachmentFileConverter (sdkAttachmentFile1).ToSDKAttachmentFile ();

            Assert.IsNotNull (sdkAttachmentFile2);
            Assert.AreEqual (sdkAttachmentFile2, sdkAttachmentFile1);
        }

        [Test]
        public void convertAPIToAPI ()
        {
            Silanis.ESL.API.AttachmentFile apiAttachmentFile1 = createApiAttachmentFile ();
            Silanis.ESL.API.AttachmentFile apiAttachmentFile2 = new AttachmentFileConverter (apiAttachmentFile1).ToApiAttachmentFile ();

            Assert.IsNotNull (apiAttachmentFile2);
            Assert.AreEqual (apiAttachmentFile2, apiAttachmentFile1);
        }

        [Test]
        public void convertAPIToSDK ()
        {
            Silanis.ESL.API.AttachmentFile apiAttachmentFile1 = createApiAttachmentFile ();
            Silanis.ESL.SDK.AttachmentFile sdkAttachmentFile1 = new AttachmentFileConverter (apiAttachmentFile1).ToSDKAttachmentFile ();

            Assert.IsNotNull (sdkAttachmentFile1);
            Assert.AreEqual (sdkAttachmentFile1.Id, apiAttachmentFile1.Id);
            Assert.AreEqual (sdkAttachmentFile1.InsertDate, apiAttachmentFile1.GetInsertDate());
            Assert.AreEqual (sdkAttachmentFile1.Name, apiAttachmentFile1.Name);
            Assert.AreEqual (sdkAttachmentFile1.Preview, apiAttachmentFile1.Preview);
        }

        [Test]
        public void convertSDKToAPI ()
        {
            Silanis.ESL.SDK.AttachmentFile sdkAttachmentFile1 = createSdkAttachmentFile ();
            Silanis.ESL.API.AttachmentFile apiAttachmentFile1 = new AttachmentFileConverter (sdkAttachmentFile1).ToApiAttachmentFile ();

            Assert.IsNotNull (apiAttachmentFile1);
            Assert.AreEqual (apiAttachmentFile1.Id, sdkAttachmentFile1.Id);
            Assert.AreEqual (apiAttachmentFile1.GetInsertDate(), sdkAttachmentFile1.InsertDate);
            Assert.AreEqual (apiAttachmentFile1.Name, sdkAttachmentFile1.Name);
            Assert.AreEqual (apiAttachmentFile1.Preview, sdkAttachmentFile1.Preview);
        }

        private Silanis.ESL.API.AttachmentFile createApiAttachmentFile ()
        {
            Silanis.ESL.API.AttachmentFile apiAttachmentFile = new Silanis.ESL.API.AttachmentFile ();
            apiAttachmentFile.Id = 1;
            apiAttachmentFile.SetInsertDate (NOW.Ticks);
            apiAttachmentFile.Name = "apiAttachmentFile.jpeg";
            apiAttachmentFile.Preview = true;
            return apiAttachmentFile;
        }

        private Silanis.ESL.SDK.AttachmentFile createSdkAttachmentFile ()
        {
            Silanis.ESL.SDK.AttachmentFile sdkAttachmentFile = new Silanis.ESL.SDK.AttachmentFile ();
            sdkAttachmentFile.Id = 1;
            sdkAttachmentFile.InsertDate = NOW;
            sdkAttachmentFile.Name = "apiAttachmentFile.jpeg";
            sdkAttachmentFile.Preview = true;
            return sdkAttachmentFile;
        }



    }
}
