using System;
using System.Collections.Generic;

namespace Silanis.ESL.SDK
{
	public class Signature
	{
		private List<Field> fields = new List<Field>();

		public Signature (string signerEmail, int page, double x, double y)
		{
			SignerEmail = signerEmail;
			Page = page;
			X = x;
			Y = y;
		}

		public string SignerEmail 
		{
			get;
			private set;
		}

		public int Page 
		{
			get;
			private set;
		}

		public double X 
		{
			get;
			private set;
		}

		public double Y
		{
			get;
			private set;
		}

		public double Height {
			get;
			set;
		}

		public double Width {
			get;
			set;
		}

		public SignatureStyle Style {
			get;
			set;
		}

		public void AddFields (IList<Field> fields)
		{
			this.fields.AddRange (fields);
		}

		public List<Field> Fields
		{
			get
			{
				return fields;
			}
		}
	}
}