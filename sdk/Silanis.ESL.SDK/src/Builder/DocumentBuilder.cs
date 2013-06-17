using System;
using System.IO;
using System.Collections.Generic;
using Silanis.ESL.SDK.Builder.Internal;

namespace Silanis.ESL.SDK.Builder
{
	public class DocumentBuilder
	{
		private readonly string name;
		private string id;
		private string fileName;
		private DocumentSource documentSource;
		private IList<Signature> signatures = new List<Signature>();
		private int index;
		private bool extract;

		private DocumentBuilder(string name)
		{
			this.name = name;
		}

		public static DocumentBuilder NewDocumentNamed (string name)
		{
			return new DocumentBuilder (name);
		}

		public DocumentBuilder WithId (string id)
		{
			this.id = id;
			return this;
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

		public DocumentBuilder WithSignature (SignatureBuilder builder)
		{
			return WithSignature (builder.Build ());
		}

		public DocumentBuilder WithSignature (Signature signature)
		{
			signatures.Add (signature);
			return this;
		}

		public DocumentBuilder AtIndex (int index)
		{
			this.index = index;
			return this;
		}

		public DocumentBuilder EnableExtraction ()
		{
			extract = true;
			return this;
		}

		public Document Build ()
		{
			Validate ();

			Document doc = new Document ();

			doc.Name = name;
			doc.Id = id;
			doc.FileName = fileName;
			doc.Content = documentSource.Content ();
			doc.AddSignatures (signatures);
			doc.Index = index;
			doc.Extract = extract;
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