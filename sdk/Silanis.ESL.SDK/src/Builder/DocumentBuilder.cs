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
		private IList<Field> injectedFields = new List<Field> ();
        private string description;

		private DocumentBuilder(string name)
		{
			this.name = name;
		}

		public static DocumentBuilder NewDocumentNamed (string name)
		{
			return new DocumentBuilder (name);
		}

		internal static DocumentBuilder NewDocumentFromAPIDocument (Silanis.ESL.API.Document apiDocument, Silanis.ESL.API.Package package)
		{
            DocumentBuilder documentBuilder = DocumentBuilder.NewDocumentNamed(apiDocument.Name)
    				.WithId(apiDocument.Id)
    				.AtIndex(apiDocument.Index)
                    .WithDescription(apiDocument.Description);

			foreach ( Silanis.ESL.API.Approval apiApproval in apiDocument.Approvals ) {
                Signature signature = new SignatureConverter( apiApproval, package ).ToSDKSignature();
				documentBuilder.WithSignature( signature );
			}

			foreach ( Silanis.ESL.API.Field apiField in apiDocument.Fields ) {
				FieldBuilder fieldBuilder = FieldBuilder.NewFieldFromAPIField( apiField );

				documentBuilder.WithInjectedField( fieldBuilder );
			}

			return documentBuilder;
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
			fileName = DocumentTypeUtility.NormalizeName (type, name);
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

		private void Validate ()
		{
			if (String.IsNullOrEmpty(id) && String.IsNullOrEmpty (fileName))
			{
				throw new EslException ("Document fileName must be set");
			}
		}

		public DocumentBuilder WithInjectedField (FieldBuilder builder)
		{
			return WithInjectedField (builder.Build());
		}

		public DocumentBuilder WithInjectedField (Field field)
		{
			injectedFields.Add (field);
			return this;
		}

        public DocumentBuilder WithDescription( string description ) {
            this.description = description;
            return this;
        }

		public Document Build ()
		{
			Support.LogMethodEntry();
			Validate ();

			Document doc = new Document ();

			doc.Name = name;
			doc.Id = id;
			doc.FileName = fileName;
			doc.Content = documentSource != null ? documentSource.Content () : null;
			doc.AddSignatures (signatures);
			doc.Index = index;
			doc.Extract = extract;
			doc.AddFields (injectedFields);
			doc.Description = description;
			Support.LogMethodExit(doc);
			return doc;
		}
	}
}