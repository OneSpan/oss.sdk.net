using System;
using Silanis.ESL.SDK.Internal;

namespace Silanis.ESL.SDK.Builder
{
	public class FieldBuilder
	{
		public static double DEFAULT_WIDTH = 200;
		public static double DEFAULT_HEIGHT = 50;
		public static FieldStyle DEFAULT_STYLE = FieldStyle.UNBOUND_TEXT_FIELD;

		private int page;
		private double x;
		private double y;
		private double width = DEFAULT_WIDTH;
		private double height = DEFAULT_HEIGHT;
		private FieldStyle style = DEFAULT_STYLE;
		private FieldValidator validator;
		private bool extract;
		private string name;
		private string value;

		private FieldBuilder ()
		{
		}

		public static FieldBuilder SignerCompany ()
		{
			return new FieldBuilder ().WithStyle (FieldStyle.BOUND_COMPANY);
		}

		public static FieldBuilder SignerTitle ()
		{
			return new FieldBuilder ().WithStyle (FieldStyle.BOUND_TITLE);
		}

		public static FieldBuilder SignerName ()
		{
			return new FieldBuilder ().WithStyle (FieldStyle.BOUND_NAME);
		}

		public static FieldBuilder SignatureDate ()
		{
			return new FieldBuilder ().WithStyle (FieldStyle.BOUND_DATE);
		}

		public static FieldBuilder NewField ()
		{
			return new FieldBuilder ();
		}

		public static FieldBuilder TextField ()
		{
			return new FieldBuilder ().WithStyle (FieldStyle.UNBOUND_TEXT_FIELD);
		}

		public static FieldBuilder Label ()
		{
			return new FieldBuilder ().WithStyle (FieldStyle.LABEL);
		}

		public static FieldBuilder CheckBox ()
		{
			return new FieldBuilder ().WithStyle (FieldStyle.UNBOUND_CHECK_BOX);
		}

		public FieldBuilder WithName (string name)
		{
			this.name = name;
			return this;
		}

		public FieldBuilder WithExtraction ()
		{
			extract = true;
			return this;
		}

		public FieldBuilder OnPage (int page)
		{
			this.page = page;
			return this;
		}

		public FieldBuilder AtPosition (double x, double y)
		{
			this.x = x;
			this.y = y;
			return this;
		}

		public FieldBuilder WithSize (double width, double height)
		{
			this.width = width;
			this.height = height;
			return this;
		}

		public FieldBuilder WithStyle (FieldStyle style)
		{
			this.style = style;
			return this;
		}

		public FieldBuilder WithValidation (FieldValidatorBuilder builder)
		{
			return WithValidation(builder.Build ());
		}

		public FieldBuilder WithValidation (FieldValidator validator)
		{
			this.validator = validator;
			return this;
		}

		public FieldBuilder WithValue (string value)
		{
			this.value = value;
			return this;
		}

		public Field Build ()
		{
			if (!extract && style != FieldStyle.LABEL)
			{
				Asserts.NonZero (x, "x");
				Asserts.NonZero (y, "y");
			}

			Field field = new Field ();

			field.Width = width;
			field.Height = height;
			field.Style = style;
			field.Page = page;
			field.X = x;
			field.Y = y;
			field.Validator = validator;
			field.Name = name;
			field.Extract = extract;
			field.Value = value;
			return field;
		}
	}
}