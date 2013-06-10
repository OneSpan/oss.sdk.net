using System;

namespace Silanis.ESL.SDK
{
	public class Document
	{
		public string Name {
			get;
			set;
		}

		public string FileName {
			get;
			set;
		}

		public byte[] Content {
			get;
			set;
		}

		internal Silanis.ESL.API.Document ToAPIDocument (Silanis.ESL.API.Package packageToCreate)
		{
			Silanis.ESL.API.Document doc = new Silanis.ESL.API.Document ();

			doc.Name = Name;
			return doc;
		}
	}
}