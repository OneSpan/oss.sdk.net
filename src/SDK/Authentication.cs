using System;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
	public class Authentication
	{
		private readonly AuthenticationMethod method;
		private List<Challenge> challenges = new List<Challenge>();

		public Authentication(AuthenticationMethod method)
		{
			this.method = method;
		}

		public Authentication(IList<Challenge> challenges) : this(AuthenticationMethod.CHALLENGE)
		{
			this.challenges.AddRange(challenges);
		}

		public Authentication(AuthenticationMethod method, string phoneNumber) : this(method)
		{
			this.PhoneNumber = phoneNumber;
		}
		
		public Authentication(AuthenticationMethod method, string phoneNumber, IdvWorkflow idvWorkflow) : this(method)
		{
			this.PhoneNumber = phoneNumber;
			this.IdvWorkflow = idvWorkflow;
		}

		public AuthenticationMethod Method
		{
			get
			{
				return method;
			}
		}

		public IList<Challenge> Challenges
		{
			get
			{
				return challenges.AsReadOnly();
			}
		}

		public string PhoneNumber
		{
			get;
			private set;
		}
		
		public IdvWorkflow IdvWorkflow
		{
			get;
			private set;
		}
	}
}