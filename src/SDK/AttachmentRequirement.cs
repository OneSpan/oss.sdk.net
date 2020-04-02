using System;
using System.Collections;
using OneSpanSign.API;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    public class AttachmentRequirement
    {
		private string name;
		private string senderComment;
		private IDictionary<string, object> data;
		private	string description;
		private string id;
		private bool required;
		private OneSpanSign.Sdk.RequirementStatus status;
        private IList<AttachmentFile> files = new List<AttachmentFile> ();

        public AttachmentRequirement(string name)
        {
			Name = name;
		}

		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
			}
		}

		public string SenderComment
		{
			get
			{
				return senderComment;
			}
			set
			{
				senderComment = value;
			}
		}

		public IDictionary<string, object> Data
		{
			get
			{
				return data;
			}
			set
			{
				data = value;
			}
		}

		public string Description
		{
			get
			{
				return description;
			}
			set
			{
				description = value;
			}
		}

		public string Id
		{
			get
			{
				return id;
			}
			set
			{
				id = value;
			}
		}

		public bool Required
		{
			get
			{
				return required;
			}
			set
			{
				required = value;
			}
		}

		public OneSpanSign.Sdk.RequirementStatus Status
		{
			get
			{
				return status;
			}
			set
			{
				status = value;
			}
		}

        public IList<AttachmentFile> Files 
        {
            get 
            {
                return files;
            }
            set 
            {
                files = value;
            }
        }
    }
}

