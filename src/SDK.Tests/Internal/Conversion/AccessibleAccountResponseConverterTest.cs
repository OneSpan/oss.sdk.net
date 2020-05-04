using System;
using NUnit.Framework;
using OneSpanSign.Sdk;
using System.Collections.Generic;

namespace SDK.Tests
{
    [TestFixture()]
    public class AccessibleAccountResponseConverterTest
    {
        private AccessibleAccountResponse sdkAccessibleAccountResponse = null;
        private OneSpanSign.API.AccessibleAccountResponse apiAccessibleAccountResponse = null;
        private AccessibleAccountResponseConverter converter = null;

        private static readonly string ACC_UID = "account_uid";
        private static readonly string ACC_NAME = "account_name";

        [Test()]
        public void ConvertNullAPIToSDK()
        {
            apiAccessibleAccountResponse = null;
            converter = new AccessibleAccountResponseConverter(apiAccessibleAccountResponse);
            Assert.IsNull(converter.ToSDKAccessibleAccountResponse());
        }

        [Test()]
        public void ConvertNullSDKToSDK()
        {
            sdkAccessibleAccountResponse = null;
            converter = new AccessibleAccountResponseConverter(sdkAccessibleAccountResponse);
            Assert.IsNull(converter.ToSDKAccessibleAccountResponse());
        }

        [Test()]
        public void ConvertSDKToSDK()
        {
            sdkAccessibleAccountResponse = CreateTypicalSDKAccessibleAccountResponse();
            converter = new AccessibleAccountResponseConverter(sdkAccessibleAccountResponse);
            AccessibleAccountResponse accessibleAccountResponse = converter.ToSDKAccessibleAccountResponse();

            Assert.IsNotNull(accessibleAccountResponse);
            Assert.AreEqual(sdkAccessibleAccountResponse, accessibleAccountResponse);
        }

        [Test()]
        public void ConvertAPIToSDK()
        {
            apiAccessibleAccountResponse = CreateTypicalAPIAccessibleAccountResponse();
            converter = new AccessibleAccountResponseConverter(apiAccessibleAccountResponse);
            AccessibleAccountResponse accessibleAccountResponse = converter.ToSDKAccessibleAccountResponse();

            Assert.IsNotNull(accessibleAccountResponse);
            Assert.AreEqual(ACC_NAME, accessibleAccountResponse.AccountName);
            Assert.AreEqual(ACC_UID, accessibleAccountResponse.AccountUid);
        }

        [Test()]
        public void ConvertNullSDKToAPI()
        {
            sdkAccessibleAccountResponse = null;
            converter = new AccessibleAccountResponseConverter(sdkAccessibleAccountResponse);

            Assert.IsNull(converter.ToAPIAccessibleAccountResponse());
        }

        [Test()]
        public void ConvertNullAPIToAPI()
        {
            apiAccessibleAccountResponse = null;
            converter = new AccessibleAccountResponseConverter(apiAccessibleAccountResponse);

            Assert.IsNull(converter.ToAPIAccessibleAccountResponse());
        }

        [Test()]
        public void ConvertAPIToAPI()
        {
            apiAccessibleAccountResponse = CreateTypicalAPIAccessibleAccountResponse();
            converter = new AccessibleAccountResponseConverter(apiAccessibleAccountResponse);

            OneSpanSign.API.AccessibleAccountResponse accessibleAccountResponse = converter.ToAPIAccessibleAccountResponse();

            Assert.IsNotNull(accessibleAccountResponse);
            Assert.AreEqual(apiAccessibleAccountResponse, accessibleAccountResponse);
        }

        [Test()]
        public void ConvertSDKToAPI()
        {
            sdkAccessibleAccountResponse = CreateTypicalSDKAccessibleAccountResponse();
            converter = new AccessibleAccountResponseConverter(sdkAccessibleAccountResponse);

            OneSpanSign.API.AccessibleAccountResponse accessibleAccountResponse = converter.ToAPIAccessibleAccountResponse();
            ;

            Assert.IsNotNull(accessibleAccountResponse);
            Assert.AreEqual(ACC_NAME, accessibleAccountResponse.AccountName);
            Assert.AreEqual(ACC_UID, accessibleAccountResponse.AccountUid);
        }

        private AccessibleAccountResponse CreateTypicalSDKAccessibleAccountResponse()
        {
            AccessibleAccountResponse accessibleAccountResponse = new AccessibleAccountResponse();
            accessibleAccountResponse.AccountName = ACC_NAME;
            accessibleAccountResponse.AccountUid = ACC_UID;

            return accessibleAccountResponse;
        }

        private OneSpanSign.API.AccessibleAccountResponse CreateTypicalAPIAccessibleAccountResponse()
        {
            OneSpanSign.API.AccessibleAccountResponse accessibleAccountResponse = new OneSpanSign.API.AccessibleAccountResponse();
            accessibleAccountResponse.AccountName = ACC_NAME;
            accessibleAccountResponse.AccountUid = ACC_UID;

            return accessibleAccountResponse;
        }
    }
}