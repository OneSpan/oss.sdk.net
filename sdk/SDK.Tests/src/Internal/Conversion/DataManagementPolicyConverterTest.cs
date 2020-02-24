using System;
using NUnit.Framework;
using Silanis.ESL.SDK;
namespace SDK.Tests
{
    [TestFixture ()]
    public class DataManagementPolicyConverterTest
    {

        private Silanis.ESL.SDK.DataManagementPolicy sdkDataManagementPolicy1 = null;
        private Silanis.ESL.SDK.DataManagementPolicy sdkDataManagementPolicy2 = null;
        private Silanis.ESL.API.DataManagementPolicy apiDataManagementPolicy1 = null;
        private Silanis.ESL.API.DataManagementPolicy apiDataManagementPolicy2 = null;
        private DataManagementPolicyConverter converter = null;

        [Test ()]
        public void convertNullSDKToAPI ()
        {
            sdkDataManagementPolicy1 = null;
            converter = new DataManagementPolicyConverter (sdkDataManagementPolicy1);
            Assert.IsNull (converter.ToAPIDataManagementPolicy ());

        }

        [Test ()]
        public void convertNullAPIToSDK ()
        {
            apiDataManagementPolicy1 = null;
            converter = new DataManagementPolicyConverter (apiDataManagementPolicy1);
            Assert.IsNull (converter.ToSDKDataManagementPolicy ());

        }

        [Test ()]
        public void convertNullSDKToSDK ()
        {
            sdkDataManagementPolicy1 = null;
            converter = new DataManagementPolicyConverter (sdkDataManagementPolicy1);
            Assert.IsNull (converter.ToSDKDataManagementPolicy ());

        }

        [Test ()]
        public void convertNullAPIToAPI ()
        {
            apiDataManagementPolicy1 = null;
            converter = new DataManagementPolicyConverter (apiDataManagementPolicy1);
            Assert.IsNull (converter.ToAPIDataManagementPolicy ());

        }

        [Test ()]
        public void convertSDKToSDK ()
        {
            sdkDataManagementPolicy1 = new DataManagementPolicy ();
            sdkDataManagementPolicy2 = new DataManagementPolicyConverter (sdkDataManagementPolicy1).ToSDKDataManagementPolicy ();
            Assert.IsNotNull (sdkDataManagementPolicy2);
            Assert.AreEqual (sdkDataManagementPolicy2, sdkDataManagementPolicy1);
        }

        [Test ()]
        public void convertAPIToAPI ()
        {
            apiDataManagementPolicy1 = new Silanis.ESL.API.DataManagementPolicy ();
            apiDataManagementPolicy2 = new DataManagementPolicyConverter (apiDataManagementPolicy1).ToAPIDataManagementPolicy ();

            Assert.IsNotNull (apiDataManagementPolicy2);
            Assert.AreEqual (apiDataManagementPolicy2, apiDataManagementPolicy1);

        }

        [Test ()]
        public void convertAPIToSDK ()
        {
            apiDataManagementPolicy1 = new Silanis.ESL.API.DataManagementPolicy ();
            apiDataManagementPolicy1.TransactionRetention = buildApiTransactionRetention ();
            sdkDataManagementPolicy1 = new DataManagementPolicyConverter (apiDataManagementPolicy1).ToSDKDataManagementPolicy ();

            Assert.IsNotNull (sdkDataManagementPolicy1);
            Assert.IsNotNull (sdkDataManagementPolicy1.TransactionRetention);
            Assert.AreEqual (sdkDataManagementPolicy1.TransactionRetention.Expired, 10);
            Assert.AreEqual (sdkDataManagementPolicy1.TransactionRetention.Sent, 20);
            Assert.AreEqual (sdkDataManagementPolicy1.TransactionRetention.Archived, 30);
            Assert.AreEqual (sdkDataManagementPolicy1.TransactionRetention.Completed, 40);
            Assert.AreEqual (sdkDataManagementPolicy1.TransactionRetention.Declined, 50);
            Assert.AreEqual (sdkDataManagementPolicy1.TransactionRetention.Draft, 60);
            Assert.AreEqual (sdkDataManagementPolicy1.TransactionRetention.OptedOut, 70);
        }

        [Test ()]
        public void convertSDKToAPI ()
        {
            sdkDataManagementPolicy1 = new DataManagementPolicy ();
            sdkDataManagementPolicy1.TransactionRetention = buildSdkTransactionRetention ();
            apiDataManagementPolicy1 = new DataManagementPolicyConverter (sdkDataManagementPolicy1).ToAPIDataManagementPolicy ();

            Assert.IsNotNull (apiDataManagementPolicy1);
            Assert.IsNotNull (apiDataManagementPolicy1.TransactionRetention);
            Assert.AreEqual (apiDataManagementPolicy1.TransactionRetention.Expired, 10);
            Assert.AreEqual (apiDataManagementPolicy1.TransactionRetention.Sent, 20);
            Assert.AreEqual (apiDataManagementPolicy1.TransactionRetention.Archived, 30);
            Assert.AreEqual (apiDataManagementPolicy1.TransactionRetention.Completed, 40);
            Assert.AreEqual (apiDataManagementPolicy1.TransactionRetention.Declined, 50);
            Assert.AreEqual (apiDataManagementPolicy1.TransactionRetention.Draft, 60);
            Assert.AreEqual (apiDataManagementPolicy1.TransactionRetention.OptedOut, 70);
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
