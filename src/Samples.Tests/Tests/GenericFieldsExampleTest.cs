using NUnit.Framework;
using System;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    [TestFixture()]
    public class GenericFieldsExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            GenericFieldsExample example = new GenericFieldsExample();
            example.Run();

            DocumentPackage documentPackage = example.RetrievedPackage;

            foreach (Signature signature in documentPackage.GetDocument(GenericFieldsExample.DOCUMENT_NAME).Signatures)
            {
                foreach (Field field in signature.Fields)
                {
                    // Textfield
                    if (field.Id == GenericFieldsExample.TEXTFIELD_ID)
                    {
                        Assert.AreEqual(FieldStyle.UNBOUND_TEXT_FIELD, field.Style);
                        Assert.AreEqual(GenericFieldsExample.TEXTFIELD_PAGE, field.Page);
                    }
                    // Checkbox
                    if (field.Id == GenericFieldsExample.CHECKBOX_ID)
                    {
                        Assert.AreEqual(FieldStyle.UNBOUND_CHECK_BOX, field.Style);
                        Assert.AreEqual(FieldBuilder.CHECKBOX_CHECKED, field.Value);
                        Assert.AreEqual(GenericFieldsExample.CHECKBOX_PAGE, field.Page);
                    }
                    // Grouped Checkbox
                    if (field.Id == GenericFieldsExample.GROUPED_CHECKBOX_ID_1)
                    {
                        Assert.AreEqual(FieldStyle.UNBOUND_CHECK_BOX, field.Style);
                        Assert.AreEqual(GenericFieldsExample.GROUPED_CHECKBOX_PAGE, field.Page);
                        Assert.AreEqual(GenericFieldsExample.INDIVIDUAL_CHECKBOX_TOOLTIP_MESSAGE, field.Tooltip);
                        Assert.IsNotNull(field.Validator);
                        Assert.AreEqual(GenericFieldsExample.GROUPED_CHECKBOX_GROUP, field.Validator.Group);
                        Assert.AreEqual(GenericFieldsExample.GROUPED_CHECKBOX_MINIMAL_REQUIRED, field.Validator.MinimumRequired);
                        Assert.AreEqual(GenericFieldsExample.GROUPED_CHECKBOX_MAXIMAL_REQUIRED, field.Validator.MaximumRequired);
                        Assert.AreEqual(GenericFieldsExample.CHECKBOX_GROUP_TOOLTIP_MESSAGE, field.Validator.GroupTooltip);
                    }
                    // Radio Button 1
                    if (field.Id == GenericFieldsExample.RADIO_ID_1)
                    {
                        Assert.AreEqual(FieldStyle.UNBOUND_RADIO_BUTTON, field.Style);
                        Assert.AreEqual(GenericFieldsExample.RADIO_PAGE,field.Page);
                        Assert.IsNotNull(field.Validator);
                        Assert.AreEqual(GenericFieldsExample.RADIO_GROUP_1, field.Validator.Options[0]);
                        Assert.AreEqual("", field.Value);
                        Assert.AreEqual(GenericFieldsExample.INDIVIDUAL_RADIOBUTTON_TOOLTIP_MESSAGE_1, field.Tooltip);
                        Assert.AreEqual(GenericFieldsExample.RADIO_BUTTON_GROUP_TOOLTIP_MESSAGE, field.Validator.GroupTooltip);
                    }
                    // Radio Button 2
                    if (field.Id == GenericFieldsExample.RADIO_ID_2)
                    {
                        Assert.AreEqual(FieldStyle.UNBOUND_RADIO_BUTTON, field.Style);
                        Assert.AreEqual(GenericFieldsExample.RADIO_PAGE,field.Page);
                        Assert.IsNotNull(field.Validator);
                        Assert.AreEqual(GenericFieldsExample.RADIO_GROUP_1, field.Validator.Options[0]);
                        Assert.AreEqual(FieldBuilder.RADIO_SELECTED, field.Value);
                        Assert.AreEqual(GenericFieldsExample.INDIVIDUAL_RADIOBUTTON_TOOLTIP_MESSAGE_2, field.Tooltip);
                        Assert.AreEqual(GenericFieldsExample.RADIO_BUTTON_GROUP_TOOLTIP_MESSAGE, field.Validator.GroupTooltip);
                    }
                    // Radio Button 3
                    if (field.Id == GenericFieldsExample.RADIO_ID_3)
                    {
                        Assert.AreEqual(FieldStyle.UNBOUND_RADIO_BUTTON, field.Style);
                        Assert.AreEqual(GenericFieldsExample.RADIO_PAGE,field.Page);
                        Assert.IsNotNull(field.Validator);
                        Assert.AreEqual(GenericFieldsExample.RADIO_GROUP_2, field.Validator.Options[0]);
                        Assert.AreEqual(FieldBuilder.RADIO_SELECTED, field.Value);
                    }
                    // Radio Button 4
                    if (field.Id == GenericFieldsExample.RADIO_ID_4)
                    {
                        Assert.AreEqual(FieldStyle.UNBOUND_RADIO_BUTTON, field.Style);
                        Assert.AreEqual(GenericFieldsExample.RADIO_PAGE,field.Page);
                        Assert.IsNotNull(field.Validator);
                        Assert.AreEqual(GenericFieldsExample.RADIO_GROUP_2, field.Validator.Options[0]);
                        Assert.AreEqual("", field.Value);
                    }
                    // Drop List
                    if (field.Id == GenericFieldsExample.DROP_LIST_ID) 
                    {
                        Assert.AreEqual(GenericFieldsExample.DROP_LIST_PAGE, field.Page);
                        Assert.AreEqual(FieldStyle.DROP_LIST, field.Style);
                        Assert.AreEqual(GenericFieldsExample.DROP_LIST_OPTION1, field.Validator.Options[0]);
                        Assert.AreEqual(GenericFieldsExample.DROP_LIST_OPTION2, field.Validator.Options[1]);
                        Assert.AreEqual(GenericFieldsExample.DROP_LIST_OPTION3, field.Validator.Options[2]);
                        Assert.AreEqual(GenericFieldsExample.DROP_LIST_OPTION2, field.Value);
                    }
                    // Text Area
                    if (field.Id == GenericFieldsExample.TEXT_AREA_ID) 
                    {
                        Assert.AreEqual(GenericFieldsExample.TEXT_AREA_PAGE, field.Page);
                        Assert.AreEqual(FieldStyle.TEXT_AREA, field.Style);
                        Assert.AreEqual(GenericFieldsExample.TEXT_AREA_VALUE, field.Value);
                        Assert.IsTrue(field.Validator.Disabled);
                    }
                    // Label Field
                    if (field.Id == GenericFieldsExample.LABEL_ID) 
                    {
                        Assert.AreEqual(GenericFieldsExample.LABEL_PAGE, field.Page);
                        Assert.AreEqual(FieldStyle.LABEL, field.Style);
                        Assert.AreEqual(GenericFieldsExample.LABEL_VALUE, field.Value);
                    }
                    // Datepicker Field
                    if (field.Id == GenericFieldsExample.DATEPICKER_ID) 
                    {
                        Assert.AreEqual(GenericFieldsExample.DATEPICKER_PAGE, field.Page);
                        Assert.AreEqual(FieldStyle.DATEPICKER, field.Style);
                        Assert.AreEqual(GenericFieldsExample.DATEPICKER_VALUE, field.Value);
                        Assert.IsTrue(field.Validator.Required);
                        Assert.AreEqual(GenericFieldsExample.DATEPICKER_FORMAT, field.Validator.Regex);
                        Assert.AreEqual(field.Validator.Message, FieldValidatorBuilder.DATEPICKER_ERROR_MESSAGE);
                    }
                }
            }
        }
    }
}