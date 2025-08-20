using System;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
	public class Signer
	{
		private readonly Authentication authentication;
		private readonly NotificationMethods notificationMethods;

        public Signer (string signerEmail, string firstName, string lastName, Authentication authentication, NotificationMethods notificationMethods = null)
		{
			Email = signerEmail;
			FirstName = firstName;
			LastName = lastName;
			this.authentication = authentication;
			this.notificationMethods = notificationMethods;
			this.GroupId = null;
            this.KnowledgeBasedAuthentication = null;
		}

		public Signer( GroupId groupId )
		{
			GroupId = groupId;
			Email = null;
			FirstName = null;
			LastName = null;
			authentication = new Authentication(AuthenticationMethod.EMAIL);
            this.KnowledgeBasedAuthentication = null;
		}
        
        public Signer(string id)
        {
            GroupId = null;
            FirstName = null;
            LastName = null;
            Email = null;
            authentication = null;
            this.Id = id;
            authentication = new Authentication(OneSpanSign.Sdk.AuthenticationMethod.EMAIL);
            this.KnowledgeBasedAuthentication = null;
        }
        
        public Signer(string id, string adHocGroupName, string email)
        {
	        GroupId = null;
	        FirstName = adHocGroupName;
	        LastName = null;
	        Email = email;
	        authentication = null;
	        Id = id;
	        authentication = new Authentication(OneSpanSign.Sdk.AuthenticationMethod.EMAIL);
	        KnowledgeBasedAuthentication = null;
        }

		public string Id {
			get;
			set;
		}

        public string PlaceholderName {
            get;
            set;
        }

		public GroupId GroupId
		{
			get;
			private set;
		}

		public Group Group
		{
			get;
			set;
		}
		
        public KnowledgeBasedAuthentication KnowledgeBasedAuthentication
        {
            get; set;
        }

        private string email;
        public string Email
        {
            get{ return email; }
            private set
            { 
                if (value != null)
                {
                    email = value.ToLower();
                }
                else
                {
                    email = null;
                }
            }
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

        public string SignerType {
            get;
            set;
        }

        public string Language {
            get;
            set;
        }

		public bool CanChangeSigner {
			get;
			set;
		}

		public Authentication Authentication
		{
			get
			{
				return authentication;
			}
		}
		
		public NotificationMethods NotificationMethods
		{
			get
			{
				return notificationMethods;
			}
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

		public string AuthPhoneNumber
		{
			get
			{
				return authentication.PhoneNumber;
			}
		}
		
		[Obsolete("This method is deprecated. Use AuthPhoneNumber() instead.")]
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

		public IList<AttachmentRequirement> Attachments
		{
			get;
			set;
		}

        public AttachmentRequirement GetAttachmentRequirement(string attachmentName) 
        {
            foreach(AttachmentRequirement attachment in Attachments) 
            {
                if(attachment.Name.Equals(attachmentName)) 
                {
                    return attachment;
                }
            }
            return null;
        }

        public bool IsPlaceholderSigner()
        {
            return GroupId == null && Email == null;
        }

		public bool IsGroupSigner()
		{
			return GroupId != null;
		}

		public bool IsAdHocGroupSigner()
		{
			return email.EndsWith(SignerUtil.AD_HOC_GROUP_SIGNER_EMAIL_PREFIX);
		}

		public string LocalLanguage 
        {
            get;
            set; 
        }
    }
}