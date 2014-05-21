using System;
using System.Collections;
using Silanis.ESL.API;

namespace Silanis.ESL.SDK
{
    public class AttachmentRequirement
    {
		public AttachmentRequirement(string name)
        {
			Name = name;
		}

		public string SenderComment
		{
			get;
			set;
    	}

    	public IDictionary Data
		{
			get;
			set;
    	}

    	public string Description
		{
			get;
			set;
    	}

    	public string Id
		{
			get;
			set;
    	}

    	public string Name
		{
			get;
			private set;
    	}

		public bool Required
		{
			get;
			set;
		}

		public RequirementStatus Status
		{
			get;
			set;
		}
    }
}

