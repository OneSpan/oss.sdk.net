using System;
using NUnit.Framework;
using Silanis.ESL.SDK;
namespace SDK.Tests
{
    [TestFixture ()]
    public class TransactionRetentionConverterTest
    {

        private Silanis.ESL.SDK.TransactionRetention sdkTransactionRetention1 = null;
        private Silanis.ESL.SDK.TransactionRetention sdkTransactionRetention2 = null;
        private Silanis.ESL.API.TransactionRetention apiTransactionRetention1 = null;
        private Silanis.ESL.API.TransactionRetention apiTransactionRetention2 = null;
        private TransactionRetentionConverter converter = null;

        [Test ()]
        public void convertNullSDKToAPI ()
        {
            sdkTransactionRetention1 = null;
            converter = new TransactionRetentionConverter (sdkTransactionRetention1);
            Assert.IsNull (converter.ToAPITransactionRetention ());

        }

        [Test ()]
        public void convertNullAPIToSDK ()
        {
            apiTransactionRetention1 = null;
            converter = new TransactionRetentionConverter (apiTransactionRetention1);
            Assert.IsNull (converter.ToSDKTransactionRetention ());

        }

        [Test ()]
        public void convertNullSDKToSDK ()
        {
            sdkTransactionRetention1 = null;
            converter = new TransactionRetentionConverter (sdkTransactionRetention1);
            Assert.IsNull (converter.ToSDKTransactionRetention ());

        }

        [Test ()]
        public void convertNullAPIToAPI ()
        {
            apiTransactionRetention1 = null;
            converter = new TransactionRetentionConverter (apiTransactionRetention1);
            Assert.IsNull (converter.ToAPITransactionRetention ());

        }

        [Test ()]
        public void convertSDKToSDK ()
        {
            sdkTransactionRetention1 = new TransactionRetention ();
            sdkTransactionRetention2 = new TransactionRetentionConverter (sdkTransactionRetention1).ToSDKTransactionRetention ();
            Assert.IsNotNull (sdkTransactionRetention2);
            Assert.AreEqual (sdkTransactionRetention2, sdkTransactionRetention1);
        }

        [Test ()]
        public void convertAPIToAPI ()
        {
            apiTransactionRetention1 = new Silanis.ESL.API.TransactionRetention ();
            apiTransactionRetention2 = new TransactionRetentionConverter (apiTransactionRetention1).ToAPITransactionRetention ();

            Assert.IsNotNull (apiTransactionRetention2);
            Assert.AreEqual (apiTransactionRetention2, apiTransactionRetention1);

        }

        [Test ()]
        public void convertAPIToSDK ()
        {
            apiTransactionRetention1 = buildApiTransactionRetention ();
            sdkTransactionRetention1 = new TransactionRetentionConverter (apiTransactionRetention1).ToSDKTransactionRetention ();

            Assert.IsNotNull (sdkTransactionRetention1);
            Assert.AreEqual (sdkTransactionRetention1.Expired, 10);
            Assert.AreEqual (sdkTransactionRetention1.Sent, 20);
            Assert.AreEqual (sdkTransactionRetention1.Archived, 30);
            Assert.AreEqual (sdkTransactionRetention1.Completed, 40);
            Assert.AreEqual (sdkTransactionRetention1.Declined, 50);
            Assert.AreEqual (sdkTransactionRetention1.Draft, 60);
            Assert.AreEqual (sdkTransactionRetention1.OptedOut, 70);
        }

        [Test ()]
        public void convertSDKToAPI ()
        {
            sdkTransactionRetention1 = buildSdkTransactionRetention ();
            apiTransactionRetention1 = new TransactionRetentionConverter (sdkTransactionRetention1).ToAPITransactionRetention ();

            Assert.IsNotNull (apiTransactionRetention1);
            Assert.AreEqual (apiTransactionRetention1.Expired, 10);
            Assert.AreEqual (apiTransactionRetention1.Sent, 20);
            Assert.AreEqual (apiTransactionRetention1.Archived, 30);
            Assert.AreEqual (apiTransactionRetention1.Completed, 40);
            Assert.AreEqual (apiTransactionRetention1.Declined, 50);
            Assert.AreEqual (apiTransactionRetention1.Draft, 60);
            Assert.AreEqual (apiTransactionRetention1.OptedOut, 70);
        }

        private TransactionRetention buildSdkTransactionRetention ()
        {
            TransactionRetention result = new TransactionRetention ();
            result.Expired = 10;
            result.Sent = 20;
            result.Archived = 30;
            result.Completed = 40;
            result.Declined = 50;
            result.Draft = 60;
            result.OptedOut = 70;
            return result;
        }


        private Silanis.ESL.API.TransactionRetention buildApiTransactionRetention ()
        {
            Silanis.ESL.API.TransactionRetention result = new Silanis.ESL.API.TransactionRetention ();
            result.Expired = 10;
            result.Sent = 20;
            result.Archived = 30;
            result.Completed = 40;
            result.Declined = 50;
            result.Draft = 60;
            result.OptedOut = 70;
            return result;
        }
    }
}
