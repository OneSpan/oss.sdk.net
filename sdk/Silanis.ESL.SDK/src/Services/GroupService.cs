using System;
using Silanis.ESL.SDK.Internal;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Silanis.ESL.SDK.Services
{
    public class GroupService
    {
        private UrlTemplate template;
        private JsonSerializerSettings settings;
        private RestClient restClient;

        public GroupService(RestClient restClient, string baseUrl, JsonSerializerSettings settings)
        {
            this.restClient = restClient;
            template = new UrlTemplate (baseUrl);
            this.settings = settings;
        }

        public List<Group> GetMyGroups() {
            string path = template.UrlFor (UrlTemplate.GROUPS_PATH)
                    .Build ();

            try {
                string response = restClient.Get(path);
				Silanis.ESL.API.Result<Silanis.ESL.API.Group> apiResponse = JsonConvert.DeserializeObject<Silanis.ESL.API.Result<Silanis.ESL.API.Group>> (response, settings );

                List<Group> result = new List<Group>();
				foreach ( Silanis.ESL.API.Group apiGroup in apiResponse.Results ) {
                    result.Add( new GroupConverter( apiGroup ).ToSDKGroup() );
                }
                return result;
            }
            catch (EslServerException e) {
                throw new EslServerException ("Failed to retrieve group list." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new EslException ("Failed to retrieve group list." + " Exception: " + e.Message, e);
            }
        }

        public Group GetGroup( GroupId groupId ) {
            string path = template.UrlFor (UrlTemplate.GROUPS_ID_PATH)
                .Replace ("{groupId}", groupId.Id)
                    .Build ();

            try {
                string response = restClient.Get(path);
                Silanis.ESL.API.Group apiGroup = JsonConvert.DeserializeObject<Silanis.ESL.API.Group> (response, settings);
                Group sdkGroup = new GroupConverter( apiGroup ).ToSDKGroup();
                return sdkGroup;
            } 
            catch (EslServerException e) {
                throw new EslServerException ("Failed to retrieve group." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new EslException ("Failed to retrieve group." + " Exception: " + e.Message, e);
            }
        }

        public Group CreateGroup( Group group ) {
            string path = template.UrlFor (UrlTemplate.GROUPS_PATH).Build ();
			Silanis.ESL.API.Group apiGroup = new GroupConverter( group ).ToAPIGroupWithoutMembers();
            try {
                string json = JsonConvert.SerializeObject (apiGroup, settings);
                string response = restClient.Post(path, json);              
                Silanis.ESL.API.Group apiResponse = JsonConvert.DeserializeObject<Silanis.ESL.API.Group> (response);
                Group sdkGroup = new GroupConverter( apiResponse ).ToSDKGroup();
				foreach ( GroupMember groupMember in group.Members ) {
					InviteMember( sdkGroup.Id, groupMember );
				}
                return sdkGroup;
            } 
            catch (EslServerException e) {
                throw new EslServerException ("Failed to create new group." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new EslException ("Failed to create new group." + " Exception: " + e.Message, e);
            }
        }

        public GroupMember InviteMember( GroupId groupId, GroupMember groupMember ) {
            string path = template.UrlFor (UrlTemplate.GROUPS_MEMBER_PATH)
				.Replace("{groupId}", groupId.Id )
                .Build ();
			Silanis.ESL.API.GroupMember apiGroupMember = new GroupMemberConverter(groupMember).ToAPIGroupMember();
            try {
				string json = JsonConvert.SerializeObject (apiGroupMember, settings);
                string response = restClient.Post(path, json);              
				Silanis.ESL.API.GroupMember apiResponse = JsonConvert.DeserializeObject<Silanis.ESL.API.GroupMember> (response);
				return new GroupMemberConverter( apiResponse ).ToSDKGroupMember();
            }
            catch (EslServerException e) {
                throw new EslServerException ("Could not create a new package." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new EslException ("Could not create a new package." + " Exception: " + e.Message, e);
            }
        }

        public void DeleteGroup( GroupId groupId ) {
            string path = template.UrlFor (UrlTemplate.GROUPS_ID_PATH)
                .Replace ("{groupId}", groupId.Id)
                    .Build ();

            try {
                restClient.Delete(path);
            } 
            catch (EslServerException e) {
                throw new EslServerException ("Failed to delete group." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new EslException ("Failed to delete group." + " Exception: " + e.Message, e);
            }
        }

        public List<string> GetGroupMemberEmails( GroupId groupId ) {
            List<string> result = null;
            Group group = GetGroup(groupId);

            if (group != null)
            {
                result = new List<string>();

                foreach (GroupMember groupMember in group.Members)
                {
                    result.Add(groupMember.Email);
                }
            }

            return result;
        }

        public List<GroupMember> GetGroupMembers( GroupId groupId ) {
            List<GroupMember> result = null;

            Group group = GetGroup(groupId);
            if (group != null)
            {
                result = group.Members;
            }
            return result;
        }
    }
}

