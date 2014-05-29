using System;
using Silanis.ESL.SDK.Internal;
using Silanis.ESL.SDK.Builder;
using Newtonsoft.Json;
using Silanis.ESL.API;

namespace Silanis.ESL.SDK.Services
{
    public class CustomFieldService
    {
        private UrlTemplate template;
        private RestClient client;
        private JsonSerializerSettings settings;
    
        public CustomFieldService(RestClient client, string baseUrl, JsonSerializerSettings settings) {
            template = new UrlTemplate( baseUrl );
            this.client = client;
            this.settings = settings;
        }
    
        ///
         /// Create an account custom field.
         /// If the custom field already existed then update it.
         ///
         /// @param customField
         /// @return the custom field added created or updated
         /// @throws com.silanis.esl.sdk.EslException
         ///
        public CustomField CreateCustomField(CustomField customField ) {
            string path = template.UrlFor(UrlTemplate.ACCOUNT_CUSTOMFIELD_PATH).Build();
            
            CustomField sdkResponse = null;
            Silanis.ESL.API.CustomField apiResponse;
            Silanis.ESL.API.CustomField apiRequest;
    
            try {
                apiRequest = customField.toAPICustomField();
                string stringResponse;
                if ( DoesCustomFieldExist( customField.Id )){
                    stringResponse = client.Put(path,JsonConvert.SerializeObject(apiRequest, settings));
                }else{
                    stringResponse = client.Post(path,JsonConvert.SerializeObject(apiRequest, settings));
                }
                
                apiResponse = JsonConvert.DeserializeObject<Silanis.ESL.API.CustomField>(stringResponse);
                sdkResponse = CustomFieldBuilder.CustomField( apiResponse ).Build();
                return sdkResponse;
            } catch ( Exception e ) {
                throw new EslException( "Could not add/update the custom field to account." + e.Message, e );
            }
        }
    
        ///
         /// Determine if a custom field already existed
         ///
         /// @param id of custom field
         /// @return true if existed otherwise false
         ///
        public bool DoesCustomFieldExist( string id ){
            string path = template.UrlFor(UrlTemplate.ACCOUNT_CUSTOMFIELD_PATH).Build();
            path += "/" + id;
    
            try {
                string stringResponse = client.Get(path);
                if (string.IsNullOrEmpty(stringResponse)) {
                    return false;
                }
                JsonConvert.DeserializeObject<CustomFieldValue>(stringResponse);
                return true;
            } catch ( EslException e ) {
                return false;
            } catch ( Exception e ) {
                throw new EslException( "Could not get the custom field from account." + e.Message, e );
            }
        }
    
        ///
         /// Create an user custom field.
         /// If the custom field already existed then update it.
         ///
         ///
         /// @param customFieldValue
         /// @return user custom field id
         /// @throws com.silanis.esl.sdk.EslException
         ///
        public CustomFieldValue SubmitCustomFieldValue( CustomFieldValue customFieldValue ) {
            string path = template.UrlFor(UrlTemplate.USER_CUSTOMFIELD_PATH).Build();
            string response;
    
            try {
            
                string payload = JsonConvert.SerializeObject(customFieldValue.toAPIUserCustomField(), settings);
                if ( DoesCustomFieldValueExist( customFieldValue.Id )){
                    response = client.Put(path,payload);
                }else{
                    response = client.Post(path, payload);
                }
                UserCustomField result = JsonConvert.DeserializeObject<UserCustomField>(response);
    
                return CustomFieldValueBuilder.CustomFieldValue( result ).build();
            } catch ( Exception e ) {
                throw new EslException( "Could not add/update the custom field to account." + e.Message, e );
            }
        }
    
        ///
         /// Determine if an user custom field already existed
         ///
         /// @param id of user custom field
         /// @return true if existed otherwise false
         ///
        public bool DoesCustomFieldValueExist( string id ){
            string path = template.UrlFor(UrlTemplate.USER_CUSTOMFIELD_PATH).Build();
            path += "/" + id;
    
            try {
                string stringResponse = client.Get(path);
                if (String.IsNullOrEmpty(stringResponse)) {
                    return false;
                }
                JsonConvert.DeserializeObject<UserCustomField>(stringResponse);
                return true;
            } catch ( EslException e ) {
                return false;
            } catch ( Exception e ) {
                throw new EslException( "Could not get the custom field from user." + e.Message, e );
            }
        }

    }
}

