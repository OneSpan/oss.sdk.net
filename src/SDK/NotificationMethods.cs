using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace OneSpanSign.Sdk
{
	public class NotificationMethods
	{
		private readonly ISet<NotificationMethod> primary;

		public NotificationMethods()
		{
			this.primary = new HashSet<NotificationMethod>();
			primary.Add(NotificationMethod.EMAIL);
		}

		public NotificationMethods(ISet<NotificationMethod> methods, string phone)
		{
			this.Phone = phone;
			if (methods != null)
			{
				this.primary = new HashSet<NotificationMethod>();
				this.SetPrimaryMethods(methods.ToArray());
			}
			else
			{
				this.primary = null;
			}
		}

		public ISet<NotificationMethod> Primary => primary;

		public void SetPrimaryMethods(ISet<NotificationMethod> primary)
		{
			this.SetPrimaryMethods(primary.ToArray());
		}
		
		public void SetPrimaryMethods(params NotificationMethod[] methods)
		{
			this.primary.Clear();
			this.primary.Add(NotificationMethod.EMAIL);
			this.AddPrimaryMethods(methods);
		}

		public void AddPrimaryMethods(params NotificationMethod[] methods)
		{
			foreach (var method in methods)
			{
				ValidateSMSRequirements(method);
				this.primary.Add(method);
			}
		}

		private void ValidateSMSRequirements(NotificationMethod method)
		{
			if (method == NotificationMethod.SMS &&
			    (Phone == null || string.IsNullOrWhiteSpace(Phone)))
			{
				throw new OssException("Phone number must be set before enabling SMS notifications", null);
			}
		}

		public string Phone
		{
			get;  set;
		}
	}
}