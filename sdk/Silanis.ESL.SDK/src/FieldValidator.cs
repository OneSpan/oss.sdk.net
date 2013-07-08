using System;

namespace Silanis.ESL.SDK
{
	public class FieldValidator
	{

		public string Regex {
			get;
			set;
		}

		public int MinLength {
			get;
			set;
		}

		public int MaxLength {
			get;
			set;
		}

		public string Message {
			get;
			set;
		}

		public bool Required {
			get;
			set;
		}
	}
}