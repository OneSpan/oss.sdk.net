using System;
using System.Collections.Generic;
using Silanis.ESL.API;

namespace Silanis.ESL.SDK
{
	public class Signer
	{
		private readonly Authentication authentication;

		public Signer (string signerEmail, string firstName, string lastName, Authentication authentication)
		{
			Email = signerEmail;
			FirstName = firstName;
			LastName = lastName;
			this.authentication = authentication;
			this.GroupId = null;
		}

		public Signer( GroupId groupId )
		{
			GroupId = groupId;
			Email = null;
			FirstName = null;
			LastName = null;
			authentication = new Authentication(AuthenticationMethod.EMAIL);
		}
        
        public Signer(string roleId)
        {
            GroupId = null;
            FirstName = null;
            LastName = null;
            Email = null;
            authentication = null;
            RoleId = roleId;
            authentication = new Authentication(Silanis.ESL.SDK.AuthenticationMethod.EMAIL);
        }

		public string Id {
			get;
			set;
		}

		public GroupId GroupId
		{
			get;
			private set;
		}

		public string Email {
			get;
			private set;
		}

		public string FirstName {
			get;
			private set;
		}

		public string LastName {
			get;
			private set;
		}

		public string Title {
			get;
			set;
		}

		public string Company {
			get;
			set;
		}

		public bool CanChangeSigner {
			get;
			set;
		}

		public AuthenticationMethod AuthenticationMethod {
			get
			{
				return authentication.Method;
			}
		}

		public IList<Challenge> ChallengeQuestion {
			get
			{
				return authentication.Challenges;
			}
		}

		public string PhoneNumber {
			get
			{
				return authentication.PhoneNumber;
			}
		}

		public bool DeliverSignedDocumentsByEmail {
			get;
			set;
		}

		public int SigningOrder {
			get;
			set;
		}

		public string Message {
			get;
			set;
		}

		public bool Locked {
			get;
			set;
		}

        public string RoleId
        {
            get;
            set;
        }
        
        public bool IsPlaceholderSigner()
        {
            return GroupId == null && Email == null;
        }

		public bool IsGroupSigner()
		{
			return GroupId != null;
		}

		internal Silanis.ESL.API.Signer ToAPISigner()
        {
            if (IsPlaceholderSigner())
            {
                return null;
            }
        
			Silanis.ESL.API.Signer signer = new Silanis.ESL.API.Signer ();

			if (!IsGroupSigner())
			{
				signer.Email = Email;
				signer.FirstName = FirstName;
				signer.LastName = LastName;
				signer.Title = Title;
				signer.Company = Company;
				if (DeliverSignedDocumentsByEmail)
				{
					signer.Delivery = new Silanis.ESL.API.Delivery();
					signer.Delivery.Email = DeliverSignedDocumentsByEmail;
				}
			}
			else
			{
				signer.Group = new Silanis.ESL.API.Group();
				signer.Group.Id = GroupId.Id;
			}

			if (!String.IsNullOrEmpty(Id))
			{
				signer.Id = Id;
			}

			signer.Auth = authentication.ToAPIAuthentication ();

			return signer;
		}
	}
}