using System;
using Silanis.ESL.API;
using Silanis.ESL.SDK.Builder;

namespace Silanis.ESL.SDK
{
    internal class FieldValidatorConverter
    {
        private FieldValidator fieldValidator;
        private FieldValidation fieldValidation;

        public FieldValidatorConverter(FieldValidator fieldValidator)
        {
            this.fieldValidator = fieldValidator;
            fieldValidation = null;
        }

        public FieldValidatorConverter(FieldValidation fieldValidation)
        {
            this.fieldValidation = fieldValidation;
            fieldValidator = null;
        }

        public FieldValidation ToAPIFieldValidation()
        {
            if (fieldValidator == null)
            {
                return fieldValidation;
            }

            fieldValidation = new Silanis.ESL.API.FieldValidation();
            fieldValidation.MaxLength = fieldValidator.MaxLength;
            fieldValidation.MinLength = fieldValidator.MinLength;
            fieldValidation.Required = fieldValidator.Required;
            fieldValidation.ErrorMessage = fieldValidator.Message;

            if (!String.IsNullOrEmpty(fieldValidator.Regex)) {
                fieldValidation.Pattern = fieldValidator.Regex;
            }

            if (fieldValidator.Options != null && fieldValidator.Options.Count != 0)
            {
                foreach (String option in fieldValidator.Options)
                {
                    fieldValidation.AddEnum(option);
                }
            }

            return fieldValidation;
        }

        public FieldValidator ToSDKFieldValidator() 
        {
            if (fieldValidation == null) {
                return fieldValidator;
            }

            fieldValidator = new FieldValidator();

            fieldValidator.Message = fieldValidation.ErrorMessage;
            fieldValidator.MaxLength = fieldValidation.MaxLength;
            fieldValidator.MinLength = fieldValidation.MinLength;
            fieldValidator.Regex = fieldValidation.Pattern;
            fieldValidator.Required = fieldValidation.Required;
            if (fieldValidation.Enum != null)
            {
                foreach (string option in fieldValidation.Enum)
                {
                    fieldValidator.AddOption(option);
                }
            }

            return fieldValidator;
        }
    }
}

