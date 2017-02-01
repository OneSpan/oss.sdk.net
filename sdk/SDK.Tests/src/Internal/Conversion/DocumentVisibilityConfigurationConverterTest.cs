using NUnit.Framework;
using System;
using Silanis.ESL.SDK;
using System.Collections.Generic;

namespace SDK.Tests
{
    [TestFixture()]
    public class DocumentVisibilityConfigurationConverterTest
    {
        private Silanis.ESL.SDK.DocumentVisibilityConfiguration sdkVisibilityConfiguration1 = null;
        private Silanis.ESL.SDK.DocumentVisibilityConfiguration sdkVisibilityConfiguration2 = null;
        private Silanis.ESL.API.DocumentVisibilityConfiguration apiVisibilityConfiguration1 = null;
        private Silanis.ESL.API.DocumentVisibilityConfiguration apiVisibilityConfiguration2 = null;
        private DocumentVisibilityConfigurationConverter converter = null;

        [Test()]
        public void convertNullSDKToAPI() {
            sdkVisibilityConfiguration1 = null;
            converter = new DocumentVisibilityConfigurationConverter(sdkVisibilityConfiguration1);
            Assert.IsNull(converter.ToAPIVisibilityConfiguration());
        }

        [Test()]
        public void convertNullAPIToSDK() {
            apiVisibilityConfiguration1 = null;
            converter = new DocumentVisibilityConfigurationConverter(apiVisibilityConfiguration1);
            Assert.IsNull(converter.ToSDKVisibilityConfiguration());
        }

        [Test()]
        public void convertNullSDKToSDK() {
            sdkVisibilityConfiguration1 = null;
            converter = new DocumentVisibilityConfigurationConverter(sdkVisibilityConfiguration1);
            Assert.IsNull(converter.ToSDKVisibilityConfiguration());
        }

        [Test()]
        public void convertNullAPIToAPI() {
            apiVisibilityConfiguration1 = null;
            converter = new DocumentVisibilityConfigurationConverter(apiVisibilityConfiguration1);
            Assert.IsNull(converter.ToAPIVisibilityConfiguration());
        }

        [Test()]
        public void convertSDKToSDK() {
            sdkVisibilityConfiguration1 = CreateTypicalSDKDocumentVisibilityConfiguration();
            sdkVisibilityConfiguration2 = new DocumentVisibilityConfigurationConverter(sdkVisibilityConfiguration1).ToSDKVisibilityConfiguration();
            Assert.IsNotNull(sdkVisibilityConfiguration2);
            Assert.AreEqual(sdkVisibilityConfiguration1, sdkVisibilityConfiguration2);
        }

        [Test()]
        public void convertAPIToAPI() {
            apiVisibilityConfiguration1 = CreateTypicalAPIDocumentVisibilityConfiguration();
            apiVisibilityConfiguration2 = new DocumentVisibilityConfigurationConverter(apiVisibilityConfiguration1).ToAPIVisibilityConfiguration();

            Assert.IsNotNull(apiVisibilityConfiguration2);
            Assert.AreEqual(apiVisibilityConfiguration1, apiVisibilityConfiguration2);
        }

        [Test()]
        public void convertAPIToSDK() {
            apiVisibilityConfiguration1 = CreateTypicalAPIDocumentVisibilityConfiguration();
            sdkVisibilityConfiguration1 = new DocumentVisibilityConfigurationConverter(apiVisibilityConfiguration1).ToSDKVisibilityConfiguration();

            Assert.IsNotNull(sdkVisibilityConfiguration1);
            Assert.AreEqual(apiVisibilityConfiguration1.DocumentUid, sdkVisibilityConfiguration1.DocumentUid);
            Assert.AreEqual(2, sdkVisibilityConfiguration1.SignerIds.Count);
            CollectionAssert.AreEqual(apiVisibilityConfiguration1.RoleUids, sdkVisibilityConfiguration1.SignerIds);
        }

        [Test()]
        public void convertSDKToAPI() {
            sdkVisibilityConfiguration1 = CreateTypicalSDKDocumentVisibilityConfiguration();
            apiVisibilityConfiguration1 = new DocumentVisibilityConfigurationConverter(sdkVisibilityConfiguration1).ToAPIVisibilityConfiguration();

            Assert.IsNotNull(apiVisibilityConfiguration1);
            Assert.AreEqual(sdkVisibilityConfiguration1.DocumentUid, apiVisibilityConfiguration1.DocumentUid);
            Assert.AreEqual(2, apiVisibilityConfiguration1.RoleUids.Count);
            CollectionAssert.AreEqual(sdkVisibilityConfiguration1.SignerIds, apiVisibilityConfiguration1.RoleUids);
        }

        private Silanis.ESL.SDK.DocumentVisibilityConfiguration CreateTypicalSDKDocumentVisibilityConfiguration() 
        {
            return DocumentVisibilityConfigurationBuilder.NewDocumentVisibilityConfiguration("docId")
                .WithSignerIds(new List<string>{"signer1Id", "signer2Id"}).Build();
        }

        private Silanis.ESL.API.DocumentVisibilityConfiguration CreateTypicalAPIDocumentVisibilityConfiguration() 
        {
            Silanis.ESL.API.DocumentVisibilityConfiguration configuration = new Silanis.ESL.API.DocumentVisibilityConfiguration();
            configuration.DocumentUid = "docId";
            configuration.RoleUids = new List<string>{"role1Id", "role2Id"};
            return configuration;
        }
    }
}
