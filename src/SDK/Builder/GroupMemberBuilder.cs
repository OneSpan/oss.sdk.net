using System;

namespace OneSpanSign.Sdk
{
    public class GroupMemberBuilder
    {
        private string email;
        private string firstName;
        private string lastName;
        private GroupMemberType groupMemberType = GroupMemberType.REGULAR;
        private string userId;

        private GroupMemberBuilder( string email )
        {
            this.email = email;
        }

        public GroupMember Build() {
            GroupMember result = new GroupMember();
            result.Email = email;
            result.FirstName = firstName;
            result.LastName = lastName;
            result.GroupMemberType = groupMemberType;
            result.UserId = userId;
            return result;
        }

        public static GroupMemberBuilder NewGroupMember( string email ) {
            return new GroupMemberBuilder(email);
        }

        public GroupMemberBuilder AsMemberType( GroupMemberType groupMemberType ) {
            this.groupMemberType = groupMemberType;
            return this;
        }

        public GroupMemberBuilder WithUserId( string userId ) {
            this.userId = userId;
            return this;
        }

		internal GroupMemberBuilder WithFirstName( string firstName ) {
            this.firstName = firstName;
            return this;
        }

		internal GroupMemberBuilder WithLastName( string lastName ) {
            this.lastName = lastName;
            return this;
        }
    }
}

