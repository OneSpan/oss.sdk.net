using NUnit.Framework;
using System;
using OneSpanSign.Sdk;
using System.Collections.Generic;

namespace SDK.Tests
{
    [TestFixture()]
    public class DocumentVisibilityConverterTest
    {
        private OneSpanSign.Sdk.DocumentVisibility sdkVisibility1 = null;
        private OneSpanSign.Sdk.DocumentVisibility sdkVisibility2 = null;
        private OneSpanSign.API.DocumentVisibility apiVisibility1 = null;
        private OneSpanSign.API.DocumentVisibility apiVisibility2 = null;
        private DocumentVisibilityConverter converter = null;

        [Test()]
        public void convertNullSDKToAPI()
        {
            sdkVisibility1 = null;
            converter = new DocumentVisibilityConverter(sdkVisibility1);
            Assert.IsNull(converter.ToAPIDocumentVisibility());
        }

        [Test()]
        public void convertNullAPIToSDK()
        {
            apiVisibility1 = null;
            converter = new DocumentVisibilityConverter(apiVisibility1);
            Assert.IsNull(converter.ToSDKDocumentVisibility());
        }

        [Test()]
        public void convertNullSDKToSDK()
        {
            sdkVisibility1 = null;
            converter = new DocumentVisibilityConverter(sdkVisibility1);
            Assert.IsNull(converter.ToSDKDocumentVisibility());
        }

        [Test()]
        public void convertNullAPIToAPI()
        {
            apiVisibility1 = null;
            converter = new DocumentVisibilityConverter(apiVisibility1);
            Assert.IsNull(converter.ToAPIDocumentVisibility());
        }

        [Test()]
        public void convertSDKToSDK()
        {
            sdkVisibility1 = CreateTypicalSDKDocumentVisibility();
            sdkVisibility2 = new DocumentVisibilityConverter(sdkVisibility1).ToSDKDocumentVisibility();
            Assert.IsNotNull(sdkVisibility2);
            Assert.AreEqual(sdkVisibility1, sdkVisibility2);
        }

        [Test()]
        public void convertAPIToAPI()
        {
            apiVisibility1 = CreateTypicalAPIDocumentVisibility();
            apiVisibility2 = new DocumentVisibilityConverter(apiVisibility1).ToAPIDocumentVisibility();

            Assert.IsNotNull(apiVisibility2);
            Assert.AreEqual(apiVisibility1, apiVisibility2);
        }

        [Test()]
        public void convertAPIToSDK()
        {
            apiVisibility1 = CreateTypicalAPIDocumentVisibility();
            sdkVisibility1 = new DocumentVisibilityConverter(apiVisibility1).ToSDKDocumentVisibility();

            Assert.IsNotNull(sdkVisibility1);
            Assert.AreEqual(2, sdkVisibility1.Configurations.Count);
            CollectionAssert.AreEqual(apiVisibility1.Configurations[0].RoleUids, sdkVisibility1.GetConfiguration("doc1Id").SignerIds);
            CollectionAssert.AreEqual(apiVisibility1.Configurations[1].RoleUids, sdkVisibility1.GetConfiguration("doc2Id").SignerIds);
        }

        [Test()]
        public void convertSDKToAPI()
        {
            sdkVisibility1 = CreateTypicalSDKDocumentVisibility();
            apiVisibility1 = new DocumentVisibilityConverter(sdkVisibility1).ToAPIDocumentVisibility();

            Assert.IsNotNull(apiVisibility1);
            Assert.AreEqual(2, apiVisibility1.Configurations.Count);
            CollectionAssert.AreEqual(sdkVisibility1.GetConfiguration("doc1Id").SignerIds, apiVisibility1.Configurations[0].RoleUids);
            CollectionAssert.AreEqual(sdkVisibility1.GetConfiguration("doc2Id").SignerIds, apiVisibility1.Configurations[1].RoleUids);
        }

        private OneSpanSign.Sdk.DocumentVisibility CreateTypicalSDKDocumentVisibility()
        {
            return DocumentVisibilityBuilder.NewDocumentVisibility()
                .AddConfiguration(DocumentVisibilityConfigurationBuilder.NewDocumentVisibilityConfiguration("doc1Id")
                    .WithSignerIds(new List<string>{"signer1Id", "signer2Id"}))
                .AddConfiguration(DocumentVisibilityConfigurationBuilder.NewDocumentVisibilityConfiguration("doc2Id")
                    .WithSignerIds(new List<string>{"signer2Id", "signer3Id"}))
                .Build();
        }

        private OneSpanSign.API.DocumentVisibility CreateTypicalAPIDocumentVisibility()
        {
            OneSpanSign.API.DocumentVisibility visibility = new OneSpanSign.API.DocumentVisibility();
            OneSpanSign.API.DocumentVisibilityConfiguration configuration1 = new OneSpanSign.API.DocumentVisibilityConfiguration();
            configuration1.DocumentUid = "doc1Id";
            configuration1.RoleUids = new List<string>{ "role1Id", "role2Id" };

            OneSpanSign.API.DocumentVisibilityConfiguration configuration2 = new OneSpanSign.API.DocumentVisibilityConfiguration();
            configuration2.DocumentUid = "doc2Id";
            configuration2.RoleUids = new List<string>{ "role2Id", "role3Id" };

            visibility.AddConfiguration(configuration1);
            visibility.AddConfiguration(configuration2);

            return visibility;
        }
    }
}

