using System;
using NUnit.Framework;
using OneSpanSign.API;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;
using OneSpanSign.Sdk.src.Internal.Conversion;

namespace SDK.Tests
{
    [TestFixture ()]
    public class FieldConverterTest
    {
        private OneSpanSign.Sdk.Field sdkField1 = null;
        private OneSpanSign.Sdk.Field sdkField2 = null;
        private OneSpanSign.API.Field apiField1 = null;
        private OneSpanSign.API.Field apiField2 = null;
        private FieldConverter converter = null;

        [Test ()]
        public void ConvertNullSDKToAPI ()
        {
            sdkField1 = null;
            converter = new FieldConverter (sdkField1);
            Assert.IsNull (converter.ToAPIField ());
        }

        [Test ()]
        public void ConvertNullAPIToSDK ()
        {
            apiField1 = null;
            converter = new FieldConverter (apiField1);
            Assert.IsNull (converter.ToSDKField ());
        }

        [Test ()]
        public void ConvertNullSDKToSDK ()
        {
            sdkField1 = null;
            converter = new FieldConverter (sdkField1);
            Assert.IsNull (converter.ToSDKField ());
        }

        [Test ()]
        public void ConvertNullAPIToAPI ()
        {
            apiField1 = null;
            converter = new FieldConverter (apiField1);
            Assert.IsNull (converter.ToAPIField ());
        }

        [Test ()]
        public void ConvertSDKToSDK ()
        {
            sdkField1 = CreateTypicalSDKField ();
            converter = new FieldConverter (sdkField1);
            sdkField2 = converter.ToSDKField ();
            Assert.IsNotNull (sdkField2);
            Assert.AreEqual (sdkField2, sdkField1);
        }

        [Test ()]
        public void ConvertAPIToAPI ()
        {
            apiField1 = CreateTypicalAPIField ();
            converter = new FieldConverter (apiField1);
            apiField2 = converter.ToAPIField ();
            Assert.IsNotNull (apiField2);
            Assert.AreEqual (apiField2, apiField1);
        }

        [Test ()]
        public void ConvertAPIToSDK ()
        {
            apiField1 = CreateTypicalAPIField ();
            sdkField1 = new FieldConverter (apiField1).ToSDKField ();

            Assert.IsNotNull (sdkField1);
            Assert.AreEqual (sdkField1.Validator, new FieldValidatorConverter (apiField1.Validation).ToSDKFieldValidator ());
            Assert.AreEqual (sdkField1.Id, apiField1.Id);
            Assert.AreEqual (sdkField1.Name, apiField1.Name);
            Assert.AreEqual (sdkField1.Page, apiField1.Page);
            Assert.AreEqual (sdkField1.Style, new FieldStyleAndSubTypeConverter (apiField1.Subtype, apiField1.Binding).ToSDKFieldStyle ());
            Assert.AreEqual (sdkField1.TextAnchor, new TextAnchorConverter (apiField1.ExtractAnchor).ToSDKTextAnchor ());
            Assert.AreEqual (sdkField1.Value, apiField1.Value);
            Assert.AreEqual (sdkField1.FontSize, apiField1.FontSize);
            Assert.AreEqual (sdkField1.X, apiField1.Left);
            Assert.AreEqual (sdkField1.Y, apiField1.Top);
            Assert.AreEqual (sdkField1.Width, apiField1.Width);
            Assert.AreEqual (sdkField1.Height, apiField1.Height);
            Assert.AreEqual (sdkField1.Tooltip, apiField1.Tooltip);
        }

        [Test ()]
        public void ConvertSDKToAPI ()
        {
            sdkField1 = CreateTypicalSDKField ();
            apiField1 = new FieldConverter (sdkField1).ToAPIField ();

            Assert.IsNotNull (apiField1);
            Assert.AreEqual (sdkField1.Value, apiField1.Value);
            Assert.AreEqual (sdkField1.X, apiField1.Left);
            Assert.AreEqual (sdkField1.Y, apiField1.Top);
            Assert.AreEqual (sdkField1.Width, apiField1.Width);
            Assert.AreEqual (sdkField1.Height, apiField1.Height);
            Assert.AreEqual (sdkField1.Id, apiField1.Id);
            Assert.AreEqual (sdkField1.FontSize, apiField1.FontSize);
            Assert.AreEqual (sdkField1.Name, apiField1.Name);
            Assert.AreEqual (sdkField1.Page, apiField1.Page);
            Assert.AreEqual (sdkField1.Tooltip, apiField1.Tooltip);
        }

        private OneSpanSign.Sdk.Field CreateTypicalSDKField ()
        {
            double x = 1;
            double y = 1;
            int page = 3;
            double width = 4;
            double height = 5;

            OneSpanSign.Sdk.Field sdkField = FieldBuilder.NewField ()
                .WithId ("99")
                    .AtPosition (x, y)
                    .OnPage (page)
                    .WithSize (width, height)
                    .WithFontSize (20)
                    .WithStyle (FieldStyle.BOUND_DATE)
                    .WithName ("Field name")
                    .WithPositionAnchor (TextAnchorBuilder.NewTextAnchor ("Anchor Text")
                                        .AtPosition (TextAnchorPosition.BOTTOMLEFT)
                                        .WithCharacter (65)
                                        .WithOccurrence (2)
                                        .WithOffset (101, 102) //xOffset, yOffset
                                        .WithSize (201, 202)   // width, height
                                        .Build ())
                    .WithValidation (FieldValidatorBuilder.Alphabetic ()
                                    .MaxLength (15)
                                    .MinLength (5)
                                    .Required ()
                                    .WithErrorMessage ("Error message for validation.")
                                    .Build ())
                    .WithValue ("value")
                    .WithTooltip("Tooltip message.")
                    .Build ();

            return sdkField;
        }

        private OneSpanSign.API.Field CreateTypicalAPIField ()
        {
            OneSpanSign.API.Field apiField = new OneSpanSign.API.Field ();

            apiField.Extract = false;
            apiField.Height = 100.0;
            apiField.Left = 10.0;
            apiField.Id = "3";
            apiField.Name = "Field Name";
            apiField.Page = 1;
            apiField.Subtype = FieldStyle.UNBOUND_TEXT_FIELD.getApiValue ();
            apiField.Top = 101.0;
            apiField.Type = "INPUT";
            apiField.Value = "field value";
            apiField.FontSize = 18;
            apiField.Width = 102.0;
            apiField.Tooltip = "Tooltip message";

            return apiField;
        }

    }
}

