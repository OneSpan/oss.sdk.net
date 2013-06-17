using System;
using System.Collections.Generic;

namespace Silanis.ESL.SDK.Builder
{
	public class SignatureBuilder
	{
		public static double DEFAULT_HEIGHT = 50d;
		public static double DEFAULT_WIDTH = 200d;
		public static SignatureStyle DEFAULT_STYLE = SignatureStyle.FULL_NAME;

		private string signerEmail;
		private double width = DEFAULT_WIDTH;
		private double height = DEFAULT_HEIGHT;
		private SignatureStyle style = DEFAULT_STYLE;
		private int page;
		private double x;
		private double y;
		private IList<Field> fields = new List<Field>();
		private bool extract;
		private string name;

		private SignatureBuilder (string signerEmail)
		{
			this.signerEmail = signerEmail;
		}

		public static SignatureBuilder SignatureFor (string signerEmail)
		{
			return new SignatureBuilder (signerEmail);
		}

		public static SignatureBuilder InitialsFor (string signerEmail)
		{
			return new SignatureBuilder (signerEmail).WithStyle (SignatureStyle.INITIALS);
		}

		public static SignatureBuilder CaptureFor (string signerEmail)
		{
			return new SignatureBuilder (signerEmail).WithStyle (SignatureStyle.HAND_DRAWN);
		}

		public SignatureBuilder WithName (string name)
		{
			this.name = name;
			return this;
		}

		public SignatureBuilder EnableExtraction()
		{
			extract = true;
			return this;
		}

		public SignatureBuilder OnPage (int page)
		{
			this.page = page;
			return this;
		}

		public SignatureBuilder AtPosition (double x, double y)
		{
			this.x = x;
			this.y = y;
			return this;
		}

		public SignatureBuilder WithStyle (SignatureStyle style)
		{
			this.style = style;
			return this;
		}

		public SignatureBuilder WithSize (double width, double height)
		{
			this.width = width;
			this.height = height;
			return this;
		}

		public SignatureBuilder WithField (FieldBuilder builder)
		{
			return WithField (builder.Build());
		}

		public SignatureBuilder WithField (Field field)
		{
			fields.Add (field);
			return this;
		}

		public Signature Build()
		{
			Signature signature = new Signature (signerEmail, page, x, y);

			signature.Height = height;
			signature.Width = width;
			signature.Style = style;
			signature.AddFields (fields);
			signature.Name = name;
			signature.Extract = extract;
			return signature;
		}
	}
}