using System;

namespace Silanis.ESL.SDK
{
	public class Signature
	{
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
	}
}