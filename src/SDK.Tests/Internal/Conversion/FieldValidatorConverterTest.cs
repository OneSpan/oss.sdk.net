using System;
using NUnit.Framework;
using OneSpanSign.API;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Tests
{
    [TestFixture()]
    public class FieldValidatorConverterTest
    {
        private FieldValidation apiFieldValidation1;
        private FieldValidation apiFieldValidation2;
        private FieldValidator sdkFieldValidator1;
        private FieldValidator sdkFieldValidator2;

        [Test()]
        public void ConvertNullSDKToAPI()
        {
            sdkFieldValidator1 = null;
            FieldValidatorConverter converter = new FieldValidatorConverter(sdkFieldValidator1);
            Assert.IsNull(converter.ToAPIFieldValidation());
        }

        [Test()]
        public void ConvertNullAPIToSDK()
        {
            apiFieldValidation1 = null;
            FieldValidatorConverter converter = new FieldValidatorConverter(apiFieldValidation1);
            Assert.IsNull(converter.ToSDKFieldValidator());
        }

        [Test()]
        public void ConvertNullSDKToSDK()
        {
            sdkFieldValidator1 = null;
            FieldValidatorConverter converter = new FieldValidatorConverter(sdkFieldValidator1);
            Assert.IsNull(converter.ToSDKFieldValidator());
        }

        [Test()]
        public void ConvertNullAPIToAPI()
        {
            apiFieldValidation1 = null;
            FieldValidatorConverter converter = new FieldValidatorConverter(apiFieldValidation1);
            Assert.IsNull(converter.ToAPIFieldValidation());
        }

        [Test()]
        public void ConvertSDKToSDK()
        {
            sdkFieldValidator1 = CreateTypicalSDKValidator();
            FieldValidatorConverter converter = new FieldValidatorConverter(sdkFieldValidator1);
            sdkFieldValidator2 = converter.ToSDKFieldValidator();
            Assert.IsNotNull(sdkFieldValidator2);
            Assert.AreEqual(sdkFieldValidator2, sdkFieldValidator1);
        }

        [Test()]
        public void ConvertAPIToAPI()
        {
            apiFieldValidation1 = CreateTypicalAPIFieldValidation();
            FieldValidatorConverter converter = new FieldValidatorConverter(apiFieldValidation1);
            apiFieldValidation2 = converter.ToAPIFieldValidation();
            Assert.IsNotNull(apiFieldValidation2);
            Assert.AreEqual(apiFieldValidation2, apiFieldValidation1);
        }

        [Test()]
        public void ConvertAPIToSDK()
        {
            apiFieldValidation1 = CreateTypicalAPIFieldValidation();
            sdkFieldValidator1 = new FieldValidatorConverter(apiFieldValidation1).ToSDKFieldValidator();

            Assert.AreEqual(sdkFieldValidator1.Message, apiFieldValidation1.ErrorMessage);
            Assert.AreEqual(sdkFieldValidator1.MaxLength, apiFieldValidation1.MaxLength);
            Assert.AreEqual(sdkFieldValidator1.MinLength, apiFieldValidation1.MinLength);
            Assert.AreEqual(sdkFieldValidator1.Required, apiFieldValidation1.Required);
            Assert.AreEqual(sdkFieldValidator1.Disabled, apiFieldValidation1.Disabled);
            Assert.AreEqual(sdkFieldValidator1.Group, apiFieldValidation1.Group);
            Assert.AreEqual(sdkFieldValidator1.MinimumRequired, apiFieldValidation1.MinimumRequired);
            Assert.AreEqual(sdkFieldValidator1.MaximumRequired, apiFieldValidation1.MaximumRequired);
            Assert.AreEqual(sdkFieldValidator1.GroupTooltip, apiFieldValidation1.GroupTooltip);
            Assert.IsEmpty(sdkFieldValidator1.Options);
        }

        [Test()]
        public void ConvertSDKToAPI()
        {
            sdkFieldValidator1 = CreateTypicalSDKValidator();
            apiFieldValidation1 = new FieldValidatorConverter(sdkFieldValidator1).ToAPIFieldValidation();

            Assert.AreEqual(apiFieldValidation1.ErrorCode, 150);
            Assert.AreEqual(apiFieldValidation1.ErrorMessage, sdkFieldValidator1.Message);
            Assert.AreEqual(apiFieldValidation1.MaxLength, sdkFieldValidator1.MaxLength);
            Assert.AreEqual(apiFieldValidation1.MinLength, sdkFieldValidator1.MinLength);
            Assert.AreEqual(apiFieldValidation1.Required, sdkFieldValidator1.Required);
            Assert.AreEqual(apiFieldValidation1.Disabled, sdkFieldValidator1.Disabled);
            Assert.AreEqual(apiFieldValidation1.Group, sdkFieldValidator1.Group);
            Assert.AreEqual(apiFieldValidation1.MinimumRequired, sdkFieldValidator1.MinimumRequired);
            Assert.AreEqual(apiFieldValidation1.MaximumRequired, sdkFieldValidator1.MaximumRequired);
            Assert.AreEqual(apiFieldValidation1.Pattern, sdkFieldValidator1.Regex);
            Assert.AreEqual(apiFieldValidation1.GroupTooltip, sdkFieldValidator1.GroupTooltip);
        }

        private FieldValidation CreateTypicalAPIFieldValidation()
        {
            FieldValidation apiFieldValidation = new FieldValidation();
            apiFieldValidation.ErrorCode = 100;
            apiFieldValidation.ErrorMessage = "Error message.";
            apiFieldValidation.MaxLength = 30;
            apiFieldValidation.MinLength = 10;
            apiFieldValidation.Pattern = "*pattern*";
            apiFieldValidation.Required = true;
            apiFieldValidation.Disabled = false;
            apiFieldValidation.Group = "group";
            apiFieldValidation.MinimumRequired = 1;
            apiFieldValidation.MaximumRequired = 2;
            apiFieldValidation.GroupTooltip = "Group tooltip message";

            return apiFieldValidation;
        }

        private FieldValidator CreateTypicalSDKValidator()
        {
            FieldValidator sdkFieldValidator = FieldValidatorBuilder.Alphabetic()
                    .MaxLength(15)
                    .MinLength(5)
                    .Required()
                    .WithErrorCode(150)
                    .WithErrorMessage("Error message for validation")
                    .WithGroupTooltip("Tooltip message for group.")
                    .Build();

            return sdkFieldValidator;
        }
    }
}

