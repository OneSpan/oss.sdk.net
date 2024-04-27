using System;

namespace OneSpanSign.Sdk
{
    internal class GroupMemberConverter
    {
        private OneSpanSign.API.GroupMember apiMember;
        private GroupMember sdkMember;

        public GroupMemberConverter( GroupMember sdkMember ) {
            this.sdkMember = sdkMember;
            this.apiMember = null;
        }

        public GroupMemberConverter( OneSpanSign.API.GroupMember apiMember ) {
            this.apiMember = apiMember;
            this.sdkMember = null;
        }

        public GroupMember ToSDKGroupMember() {
            if (sdkMember != null)
            {
                return sdkMember;
            }
            else
            {
                GroupMember result = new GroupMember();
                result.Email = apiMember.Email;
                result.FirstName = apiMember.FirstName;
                result.LastName = apiMember.LastName;
                result.GroupMemberType = new GroupMemberTypeConverter(apiMember.MemberType).ToSDKGroupMemberType();
                result.UserId = apiMember.UserId;
                return result;
            }
        }

        public OneSpanSign.API.GroupMember ToAPIGroupMember() {
            if (apiMember != null)
            {
                return apiMember;
            }
            else
            {
                OneSpanSign.API.GroupMember result = new OneSpanSign.API.GroupMember();

                result.Email = sdkMember.Email;
                result.FirstName = sdkMember.FirstName;
                result.LastName = sdkMember.LastName;
                result.MemberType = new GroupMemberTypeConverter(sdkMember.GroupMemberType).ToAPIMemberType();
                result.UserId = sdkMember.UserId;
                return result;
            }
        }
    }
}

