using System;
using NUnit.Framework;
using OneSpanSign.Sdk;
using System.Collections.Generic;

namespace SDK.Tests
{
    [TestFixture()]
    public class CreditCardConverterTest
    {
        private CreditCard sdkCreditCard = null;
        private OneSpanSign.API.CreditCard apiCreditCard = null;
        private CreditCardConverter converter = null;


        private static readonly string CC_CVV = "license_0_transaction_0_creditCard_cvv";
        private static readonly string CC_NAME = "license_0_transaction_0_creditCard_name";
        private static readonly string CC_NUM = "license_0_transaction_0_creditCard_number";
        private static readonly string CC_TYPE = "license_0_transaction_0_creditCard_type";
        private static readonly Int32 CC_EXP_MONTH = 12;
        private static readonly Int32 CC_EXP_YEAR = 12;

        [Test()]
        public void ConvertNullAPIToSDK()
        {
            apiCreditCard = null;
            converter = new CreditCardConverter(apiCreditCard);
            Assert.IsNull(converter.ToSDKCreditCard());
        }

        [Test()]
        public void ConvertNullSDKToSDK()
        {
            sdkCreditCard = null;
            converter = new CreditCardConverter(sdkCreditCard);
            Assert.IsNull(converter.ToSDKCreditCard());
        }

        [Test()]
        public void ConvertSDKToSDK()
        {
            sdkCreditCard = CreateTypicalSDKCreditCard();
            converter = new CreditCardConverter(sdkCreditCard);
            OneSpanSign.Sdk.CreditCard creditCard = converter.ToSDKCreditCard();

            Assert.IsNotNull(creditCard);
            Assert.AreEqual(sdkCreditCard, creditCard);
        }

        [Test()]
        public void ConvertAPIToSDK()
        {
            apiCreditCard = CreateTypicalAPICreditCard();
            converter = new CreditCardConverter(apiCreditCard);
            OneSpanSign.Sdk.CreditCard creditCard = converter.ToSDKCreditCard();

            Assert.IsNotNull(creditCard);
            Assert.AreEqual(CC_NUM, creditCard.Number);
            Assert.AreEqual(CC_EXP_YEAR, creditCard.Expiration.Year);
        }

        [Test()]
        public void ConvertNullSDKToAPI()
        {
            sdkCreditCard = null;
            converter = new CreditCardConverter(sdkCreditCard);

            Assert.IsNull(converter.ToAPICreditCard());
        }

        [Test()]
        public void ConvertNullAPIToAPI()
        {
            apiCreditCard = null;
            converter = new CreditCardConverter(apiCreditCard);

            Assert.IsNull(converter.ToAPICreditCard());
        }

        [Test()]
        public void ConvertAPIToAPI()
        {
            apiCreditCard = CreateTypicalAPICreditCard();
            converter = new CreditCardConverter(apiCreditCard);

            OneSpanSign.API.CreditCard creditCard = converter.ToAPICreditCard();

            Assert.IsNotNull(creditCard);
            Assert.AreEqual(apiCreditCard, creditCard);
        }

        [Test()]
        public void ConvertSDKToAPI()
        {
            sdkCreditCard = CreateTypicalSDKCreditCard();
            converter = new CreditCardConverter(sdkCreditCard);

            OneSpanSign.API.CreditCard creditCard = converter.ToAPICreditCard();

            Assert.IsNotNull(creditCard);
            Assert.AreEqual(CC_CVV, creditCard.Cvv);
            Assert.AreEqual(CC_EXP_YEAR, creditCard.Expiration.Year);
        }

        private CreditCard CreateTypicalSDKCreditCard()
        {
            CreditCard creditCard = new CreditCard();
            creditCard.Cvv = CC_CVV;
            creditCard.Type = CC_TYPE;
            creditCard.Name = CC_NAME;
            creditCard.Number = CC_NUM;
            CcExpiration ccExpiration = new CcExpiration();
            ccExpiration.Month = CC_EXP_MONTH;
            ccExpiration.Year = CC_EXP_YEAR;
            creditCard.Expiration = ccExpiration;

            return creditCard;
        }

        private OneSpanSign.API.CreditCard CreateTypicalAPICreditCard()
        {
            OneSpanSign.API.CreditCard creditCard = new OneSpanSign.API.CreditCard();

            creditCard.Cvv = CC_CVV;
            creditCard.Type = CC_TYPE;
            creditCard.Name = CC_NAME;
            creditCard.Number = CC_NUM;
            OneSpanSign.API.CcExpiration ccExpiration = new OneSpanSign.API.CcExpiration();
            ccExpiration.Month = CC_EXP_MONTH;
            ccExpiration.Year = CC_EXP_YEAR;
            creditCard.Expiration = ccExpiration;

            return creditCard;
        }
    }
}