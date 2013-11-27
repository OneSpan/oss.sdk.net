using System;
using Silanis.ESL.API;

namespace Silanis.ESL.SDK
{
    internal class GroupMemberTypeConverter
    {
        private GroupMemberType groupMemberType;
        private MemberType memberType;

        public GroupMemberTypeConverter( GroupMemberType groupMemberType )
        {
            this.groupMemberType = groupMemberType;
        }

        public GroupMemberTypeConverter( MemberType memberType ) {
            this.memberType = memberType;
        }

        internal MemberType ToAPIMemberType() {
            if (groupMemberType == GroupMemberType.MANAGER)
            {
                return MemberType.MANAGER;
            }
            else
            {
                return MemberType.REGULAR;
            }
        }

        internal GroupMemberType ToSDKGroupMemberType() {
            if (memberType == MemberType.MANAGER)
            {
                return GroupMemberType.MANAGER;
            }
            else
            {
                return GroupMemberType.REGULAR;
            }
        }
    }
}

