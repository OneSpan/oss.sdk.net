using System;

namespace Silanis.ESL.SDK
{
	public class Field
	{

		public double Width {
			get;
			set;
		}

		public double Height {
			get;
			set;
		}

		public FieldStyle Style {
			get;
			set;
		}

		public double X {
			get;
			set;
		}

		public double Y {
			get;
			set;
		}

		public int Page {
			get;
			set;
		}

		public string Binding
		{
			get
			{
				return Style.Binding ();
			}
		}

		public FieldValidator Validator {
			get;
			set;
		}
	}
}