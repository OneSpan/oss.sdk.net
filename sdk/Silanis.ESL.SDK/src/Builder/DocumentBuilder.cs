using System;
using System.IO;
using System.Collections.Generic;
using Silanis.ESL.SDK.Builder.Internal;

namespace Silanis.ESL.SDK.Builder
{
	public class DocumentBuilder
	{
		private readonly string name;
		private string fileName;
		private DocumentSource documentSource;

		private DocumentBuilder(string name)
		{
			this.name = name;
		}

		public static DocumentBuilder NewDocumentNamed (string name)
		{
			return new DocumentBuilder (name);
		}

		public DocumentBuilder FromFile (string fileName)
		{
			this.fileName = fileName;
			documentSource = new FileDocumentSource (fileName);
			return this;
		}

		public DocumentBuilder FromStream (Stream contentStream, DocumentType type)
		{
			documentSource = new StreamDocumentSource (contentStream);
			fileName = type.NormalizeName (name);
			return this;
		}

		public Document Build ()
		{
			Validate ();

			Document doc = new Document ();

			doc.Name = name;
			doc.FileName = fileName;
			doc.Content = documentSource.Content ();
			return doc;
		}

		private void Validate ()
		{
			if (String.IsNullOrEmpty (fileName))
			{
				throw new EslException ("Document fileName must be set");
			}
		}
	}
}