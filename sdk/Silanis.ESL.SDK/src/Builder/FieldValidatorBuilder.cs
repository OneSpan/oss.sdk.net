using System;
using System.Collections.Generic;

namespace Silanis.ESL.SDK.Builder
{
	public class FieldValidatorBuilder
	{
		public static string ALPHABETIC_REGEX = "^[\\sa-zA-Z]+$";
		public static string NUMERIC_REGEX = "^[-+]?[0-9]*\\.?[0-9]*$";
		public static string ALPHANUMERIC_REGEX = "^[\\s0-9a-zA-Z]+$";
		public static string EMAIL_REGEX = "^([a-z0-9_\\.-]+)@([\\da-z\\.-]+)\\.([a-z\\.]{2,6})$";
		public static string URL_REGEX = "^(https?:\\/\\/)?([\\da-z\\.-]+)\\.([a-z\\.]{2,6})([\\/\\w \\.-]*)*\\/?$";
        public static string DEFAULT_REGEX = null;
        public static string DEFAULT_DATEPICKER_FORMAT = "YYYY-MM-dd";

        public static string ALPHABETIC_ERROR_MESSAGE = "Value entered must by alphabetic only.";
        public static string ALPHANUMERIC_ERROR_MESSAGE = "Value entered must be alphanumeric only.";
        public static string NUMERIC_ERROR_MESSAGE = "Value entered must be numeric only.";
        public static string EMAIL_ERROR_MESSAGE = "Value entered must be an email.";
        public static string URL_ERROR_MESSAGE = "Value entered must be a URL.";
        public static string DATEPICKER_ERROR_MESSAGE = "Value entered must be valid DateTimeFormat only.";

		private static int DEFAULT_MAX_LENGTH = Int32.MaxValue;

		private String regex;
		private int maxLength = DEFAULT_MAX_LENGTH;
		private int minLength = 0;
        private int errorCode;
		private string message;
		private bool required;
        private bool disabled;
        private IList<string> options = new List<string>();

		private FieldValidatorBuilder (String regex)
		{
			this.regex = regex;
		}

		public static FieldValidatorBuilder Alphabetic ()
		{
			return new FieldValidatorBuilder (ALPHABETIC_REGEX)
                .WithErrorMessage(ALPHABETIC_ERROR_MESSAGE);
		}

		public static FieldValidatorBuilder Numeric ()
		{
			return new FieldValidatorBuilder (NUMERIC_REGEX)
                .WithErrorMessage(NUMERIC_ERROR_MESSAGE);
		}

		public static FieldValidatorBuilder Alphanumeric ()
		{
            return new FieldValidatorBuilder (ALPHANUMERIC_REGEX)
                .WithErrorMessage(ALPHANUMERIC_ERROR_MESSAGE);
		}

		public static FieldValidatorBuilder Email ()
		{
            return new FieldValidatorBuilder (EMAIL_REGEX)
                .WithErrorMessage(EMAIL_ERROR_MESSAGE);
		}

		public static FieldValidatorBuilder URL ()
		{
            return new FieldValidatorBuilder (URL_REGEX)
                .WithErrorMessage(URL_ERROR_MESSAGE);
		}

        public static FieldValidatorBuilder DatepickerFormat ()
        {
            return new FieldValidatorBuilder (DEFAULT_DATEPICKER_FORMAT)
                .WithErrorMessage(DATEPICKER_ERROR_MESSAGE);
        }

        public static FieldValidatorBuilder DatepickerFormat (string format)
        {
            return new FieldValidatorBuilder (format)
                .WithErrorMessage(DATEPICKER_ERROR_MESSAGE);
        }

		public static FieldValidatorBuilder Regex (string regex)
		{
			return new FieldValidatorBuilder (regex);
		}

		public FieldValidatorBuilder MaxLength (int maxLength)
		{
			this.maxLength = maxLength;
			return this;
		}

		public FieldValidatorBuilder MinLength (int minLength)
		{
			this.minLength = minLength;
			return this;
		}

		public FieldValidatorBuilder Required ()
		{
			required = true;
			return this;
		}

        public FieldValidatorBuilder Disabled ()
        {
            disabled = true;
            return this;
        }

        public static FieldValidatorBuilder Basic() 
        {
            return new FieldValidatorBuilder( DEFAULT_REGEX );
        }

		public FieldValidatorBuilder WithErrorMessage (string message)
		{
			this.message = message;
			return this;
		}

        public FieldValidatorBuilder WithOption( string option )
        {
            options.Add(option);
            return this;
        }

        public FieldValidatorBuilder WithErrorCode(int errorCode)
        {
            this.errorCode = errorCode;
            return this;
        }

		public FieldValidator Build ()
		{
			FieldValidator validator = new FieldValidator ();

			validator.Regex = regex;
			validator.MaxLength = maxLength;
			validator.MinLength = minLength;
            validator.Required = required;
            validator.Disabled = disabled;
            validator.Message = message;
            validator.AddOptions(options);
            validator.ErrorCode = errorCode;

			return validator;
		}
	}
}