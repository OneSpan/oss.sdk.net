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
		private string id;
        private TextAnchor textAnchor;

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

		public static FieldBuilder CustomField( String name )
		{
			return new FieldBuilder().WithStyle(FieldStyle.UNBOUND_CUSTOM_FIELD).WithName(name);
		}

		internal static FieldBuilder NewFieldFromAPIField (Silanis.ESL.API.Field apiField)
		{
			FieldBuilder fieldBuilder = new FieldBuilder()
				.OnPage( apiField.Page )
				.AtPosition( apiField.Left, apiField.Top )
				.WithSize( apiField.Width, apiField.Height )
				.WithStyle( GetFieldStyleFromAPIField( apiField ) )
				.WithName( apiField.Name );

			if ( apiField.Id != null ) {
				fieldBuilder.WithId( apiField.Id );
			}

			if ( apiField.Extract ) {
				fieldBuilder.WithPositionExtracted();
			}

			fieldBuilder.WithValue( apiField.Value );
			return fieldBuilder;
		}

		private static FieldStyle GetFieldStyleFromAPIField( Silanis.ESL.API.Field field ) {

			if ( field.Binding == null ) {
				switch ( field.Subtype ) {
				case Silanis.ESL.API.FieldSubtype.TEXTFIELD:
					return FieldStyle.UNBOUND_TEXT_FIELD;
				case Silanis.ESL.API.FieldSubtype.CHECKBOX:
					return FieldStyle.UNBOUND_CHECK_BOX;
				default: 
					throw new EslException( "Unrecognized field style." );				
				}
			} else {
				String binding = field.Binding;
				if ( binding.Equals( FieldStyleUtility.BINDING_DATE ) ) {
					return FieldStyle.BOUND_DATE;
				} else if ( binding.Equals( FieldStyleUtility.BINDING_TITLE ) ) {
					return FieldStyle.BOUND_TITLE;
				} else if ( binding.Equals( FieldStyleUtility.BINDING_NAME ) ) {
					return FieldStyle.BOUND_NAME;
				} else if ( binding.Equals( FieldStyleUtility.BINDING_COMPANY ) ) {
					return FieldStyle.BOUND_COMPANY;
				} else {
					throw new EslException( "Invalid field binding." );
				}
			}
		}

		public FieldBuilder WithId (string id)
		{
			this.id = id;
			return this;
		}

		public FieldBuilder WithName (string name)
		{
			this.name = name;
			return this;
		}

		public FieldBuilder WithPositionExtracted ()
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

        public FieldBuilder WithValue (bool value)
        {
            if (this.style == FieldStyle.UNBOUND_CHECK_BOX)
            {
                this.value = value ? "X" : "";
            }
            return this;
        }

		public FieldBuilder WithValue (string value)
		{
			this.value = value;
			return this;
		}

        public FieldBuilder WithPositionAnchor( TextAnchorBuilder builder ) {
            return WithPositionAnchor( builder.Build() );
        }

        public FieldBuilder WithPositionAnchor( TextAnchor textAnchor ) {
            this.textAnchor = textAnchor;
            return this;
        }

		public Field Build ()
		{
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
			field.Id = id;
            field.TextAnchor = textAnchor;
			return field;
		}
	}
}