using System;
using System.Collections.Generic;
using OneSpanSign.Sdk.Internal;
using Newtonsoft.Json;

namespace OneSpanSign.Sdk
{
    internal class GroupApiClient
    {
        private JsonSerializerSettings settings;
        private RestClient restClient;
        private string baseUrl;

        public GroupApiClient(RestClient restClient, string baseUrl, JsonSerializerSettings settings)
        {
            this.restClient = restClient;
            this.baseUrl = baseUrl;
            this.settings = settings;
        }

        public OneSpanSign.API.Result<OneSpanSign.API.Group> GetMyGroups ()
        {
            string path = new UrlTemplate(baseUrl).UrlFor (UrlTemplate.GROUPS_PATH)
                    .Build ();
            return GetGroups (path);
        }

        public OneSpanSign.API.Result<OneSpanSign.API.Group> GetMyGroups (String groupName)
        {
            string path = new UrlTemplate(baseUrl).UrlFor (UrlTemplate.GROUPS_PATH)
                    .AddParam ("name", groupName)
                    .Build ();
            return GetGroups (path); 
        }

        private OneSpanSign.API.Result<OneSpanSign.API.Group> GetGroups(String path) {
            try {
                string response = restClient.Get(path);
                OneSpanSign.API.Result<OneSpanSign.API.Group> apiResponse = JsonConvert.DeserializeObject<OneSpanSign.API.Result<OneSpanSign.API.Group>> (response, settings );
                return apiResponse;
            }
            catch (OssServerException e) {
                throw new OssServerException ("Failed to retrieve group list." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException ("Failed to retrieve group list." + " Exception: " + e.Message, e);
            }
        }
        
        public OneSpanSign.API.Group GetGroup( string groupId ) {
            string path = new UrlTemplate(baseUrl).UrlFor (UrlTemplate.GROUPS_ID_PATH)
                .Replace ("{groupId}", groupId)
                    .Build ();

            try {
                string response = restClient.Get(path);
                OneSpanSign.API.Group apiGroup = JsonConvert.DeserializeObject<OneSpanSign.API.Group> (response, settings);
                return apiGroup;
            } 
            catch (OssServerException e) {
                throw new OssServerException ("Failed to retrieve group." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException ("Failed to retrieve group." + " Exception: " + e.Message, e);
            }
        }
        
        public OneSpanSign.API.Group CreateGroup( OneSpanSign.API.Group apiGroup ) {
            string path = new UrlTemplate(baseUrl).UrlFor (UrlTemplate.GROUPS_PATH).Build ();
            try {
                string json = JsonConvert.SerializeObject (apiGroup, settings);
                string response = restClient.Post(path, json);              
                OneSpanSign.API.Group apiResponse = JsonConvert.DeserializeObject<OneSpanSign.API.Group> (response);
                return apiResponse;
            } 
            catch (OssServerException e) {
                throw new OssServerException ("Failed to create new group." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException ("Failed to create new group." + " Exception: " + e.Message, e);
            }
        }

        public OneSpanSign.API.Group UpdateGroup( OneSpanSign.API.Group apiGroup, String groupId ) {
            string path = new UrlTemplate(baseUrl).UrlFor (UrlTemplate.GROUPS_ID_PATH)
                .Replace("{groupId}", groupId)
                .Build ();
            try {
                string json = JsonConvert.SerializeObject (apiGroup, settings);
                string response = restClient.Put(path, json);              
                OneSpanSign.API.Group apiResponse = JsonConvert.DeserializeObject<OneSpanSign.API.Group> (response);
                return apiResponse;
            } 
            catch (OssServerException e) {
                throw new OssServerException ("Failed to update group." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException ("Failed to update group." + " Exception: " + e.Message, e);
            }
        }
        
        public OneSpanSign.API.GroupMember AddMember( string groupId, OneSpanSign.API.GroupMember apiGroupMember ) {
            string path = new UrlTemplate(baseUrl).UrlFor (UrlTemplate.GROUPS_MEMBER_PATH)
                .Replace("{groupId}", groupId )
                .Build ();
            try {
                string json = JsonConvert.SerializeObject (apiGroupMember, settings);
                string response = restClient.Post(path, json);              
                OneSpanSign.API.GroupMember apiResponse = JsonConvert.DeserializeObject<OneSpanSign.API.GroupMember> (response);
                return apiResponse;
            }
            catch (OssServerException e) {
                throw new OssServerException ("Failed to add new member." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException ("Failed to add new member." + " Exception: " + e.Message, e);
            }
        }

        public OneSpanSign.API.Group InviteMember( string groupId, OneSpanSign.API.GroupMember apiGroupMember ) {
            string path = new UrlTemplate(baseUrl).UrlFor (UrlTemplate.GROUPS_INVITE_PATH)
                .Replace("{groupId}", groupId )
                    .Build ();
            try {
                string json = JsonConvert.SerializeObject (apiGroupMember, settings);
                string response = restClient.Post(path, json);              
                OneSpanSign.API.Group apiResponse = JsonConvert.DeserializeObject<OneSpanSign.API.Group> (response);
                return apiResponse;
            }
            catch (OssServerException e) {
                throw new OssServerException ("Failed to invite member." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException ("Failed to invite member." + " Exception: " + e.Message, e);
            }
        }
        
        public void DeleteGroup( string groupId ) {
            string path = new UrlTemplate(baseUrl).UrlFor (UrlTemplate.GROUPS_ID_PATH)
                .Replace ("{groupId}", groupId)
                .Build ();

            try {
                restClient.Delete(path);
            } 
            catch (OssServerException e) {
                throw new OssServerException ("Failed to delete group." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException ("Failed to delete group." + " Exception: " + e.Message, e);
            }
        }

        public OneSpanSign.API.Result<OneSpanSign.API.GroupSummary> GetGroupSummaries() {
            string path = new UrlTemplate(baseUrl).UrlFor (UrlTemplate.GROUPS_SUMMARY_PATH)
                .Build ();

            try {
                string response = restClient.Get(path);
                OneSpanSign.API.Result<OneSpanSign.API.GroupSummary> apiResponse = JsonConvert.DeserializeObject<OneSpanSign.API.Result<OneSpanSign.API.GroupSummary>> (response, settings );
                return apiResponse;
            }
            catch (OssServerException e) {
                throw new OssServerException ("Failed to retrieve Group Summary list." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException ("Failed to retrieve Group Summary list." + " Exception: " + e.Message, e);
            }
        }
    }
}

