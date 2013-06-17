using System;
using System.Collections.Generic;
using Silanis.ESL.SDK.Internal.Conversion;

namespace Silanis.ESL.SDK
{
	public class Document
	{
		private List<Signature> signatures = new List<Signature>();

		public string Name {
			get;
			set;
		}

		public string Id {
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

		public int Index {
			get;
			set;
		}

		public bool Extract {
			get;
			set;
		}

		public void AddSignatures (IList<Signature> signatures)
		{
			this.signatures.AddRange (signatures);
		}

		internal Silanis.ESL.API.Document ToAPIDocument (Silanis.ESL.API.Package createdPackage)
		{
			Silanis.ESL.API.Document doc = new Silanis.ESL.API.Document ();

			doc.Name = Name;
			doc.Id = Id;
			doc.Index = Index;
			doc.Extract = Extract;

			SignatureConverter converter = new SignatureConverter ();
			foreach (Signature signature in signatures)
			{
				Silanis.ESL.API.Approval approval = converter.ConvertToApproval (signature);

				approval.Role = FindRoleIdForSignature (signature.SignerEmail, createdPackage);
				doc.AddApproval (approval);
			}

			return doc;
		}

		private string FindRoleIdForSignature (string signerEmail, Silanis.ESL.API.Package createdPackage)
		{
			foreach (Silanis.ESL.API.Role role in createdPackage.Roles)
			{
				if (signerEmail.Equals (role.Signers[0].Email))
				{
					return role.Id;
				}
			}

			throw new EslException(String.Format ("No Role found for signer email {0}", signerEmail));
		}
	}
}