using System;
using System.Collections.Generic;
using Silanis.ESL.SDK.Internal.Conversion;

namespace Silanis.ESL.SDK
{
	public class Document
	{
		private List<Signature> signatures = new List<Signature>();
		private List<Field> fields = new List<Field> ();

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

        public string Description
        {
            get;
            set;
        }

        public List<Signature> Signatures
        {
            get
            {
                return signatures;
            }
        }       
         
		public void AddSignatures (IList<Signature> signatures)
		{
			this.signatures.AddRange (signatures);
		}

		public void AddFields (IList<Field> fields)
		{
			this.fields.AddRange (fields);
		}

		internal Silanis.ESL.API.Document ToAPIDocument (Silanis.ESL.API.Package apiPackage)
		{
			Silanis.ESL.API.Document doc = new Silanis.ESL.API.Document ();

			doc.Name = Name;
			doc.Index = Index;
			doc.Extract = Extract;

			if ( Id != null )
			{
				doc.Id = Id;
			}

            if (Description != null)
            {
                doc.Description = Description;
            }

			Converter converter = new Converter ();
			foreach (Signature signature in signatures)
			{
				Silanis.ESL.API.Approval approval = converter.ConvertToApproval (signature);

                if (signature.IsPlaceholderSignature())
                {
                    approval.Role = signature.RoleId.Id;
                }
				else if (signature.IsGroupSignature() )
				{
					approval.Role = FindRoleIdForGroup (signature.GroupId, apiPackage);
				}
				else
				{
					approval.Role = FindRoleIdForSigner (signature.SignerEmail, apiPackage);
				}
				doc.AddApproval (approval);
			}

			foreach (Field field in fields)
			{
				doc.AddField (converter.ToAPIField(field));
			}

			return doc;
		}

		private string FindRoleIdForGroup (GroupId groupId, Silanis.ESL.API.Package createdPackage )
		{
			foreach (Silanis.ESL.API.Role role in createdPackage.Roles)
			{
				if (role.Signers.Count > 0 && role.Signers[0].Group != null)
				{
					if (groupId.Id.Equals(role.Signers[0].Group.Id))
					{
						return role.Id;
					}
				}
			}

			throw new EslException(String.Format ("No Role found for group with id {0}", groupId.Id));
		}

		private string FindRoleIdForSigner (string signerEmail, Silanis.ESL.API.Package createdPackage)
		{
			foreach (Silanis.ESL.API.Role role in createdPackage.Roles)
			{
				if (role.Signers.Count > 0 && role.Signers[0].Email != null)
				{
					if (signerEmail.Equals(role.Signers[0].Email))
					{
						return role.Id;
					}
				}
			}

			throw new EslException(String.Format ("No Role found for signer email {0}", signerEmail));
		}
	}
}