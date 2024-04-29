using System;

namespace OneSpanSign.Sdk
{
    public class GroupMember
    {
		private string email;
		private string firstName;
		private string lastName;
		private GroupMemberType groupMemberType;
		private string userId;

        public GroupMember()
        {
        }

		public string Email
		{
			get
			{
				return email;
			}
			set
			{
				email = value;
			}
		}

		public string FirstName
		{
			get
			{
				return firstName;
			}
			set
			{
				firstName = value;
			}
		}

		public string LastName
		{
			get
			{
				return lastName;
			}
			set
			{
				lastName = value;
			}
		}

		public GroupMemberType GroupMemberType
		{
			get
			{
				return groupMemberType;
			}
			set
			{
				groupMemberType = value;
			}
		}

		public string UserId
		{
			get
			{
				return userId;
			}
			set
			{
				userId = value;
			}
		}
    }
}

