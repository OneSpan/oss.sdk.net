using System;
using OneSpanSign.Sdk.Internal;
using Newtonsoft.Json;
using OneSpanSign.API;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    internal class CustomFieldApiClient
    {
        private UrlTemplate template;
        private RestClient client;
        private JsonSerializerSettings settings;

        public CustomFieldApiClient(RestClient client, string baseUrl, JsonSerializerSettings settings)
        {
            template = new UrlTemplate(baseUrl);
            this.client = client;
            this.settings = settings;
        }

        public bool DoesCustomFieldExist(string id)
        {
            string path = template.UrlFor(UrlTemplate.ACCOUNT_CUSTOMFIELD_ID_PATH)
                .Replace("{customFieldId}", id)
                .Build();
    
            try
            {
                string stringResponse = client.Get(path);
                if (string.IsNullOrEmpty(stringResponse))
                {
                    return false;
                }
                JsonConvert.DeserializeObject<CustomFieldValue>(stringResponse);
                return true;
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get the custom field from account." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get the custom field from account." + " Exception: " + e.Message, e);
            }
        }

        public bool DoesCustomFieldValueExist(string id)
        {
            string path = template.UrlFor(UrlTemplate.USER_CUSTOMFIELD_ID_PATH)
                .Replace("{customFieldId}", id)
                .Build();
    
            try
            {
                string stringResponse = client.Get(path);
                if (String.IsNullOrEmpty(stringResponse))
                {
                    return false;
                }
                JsonConvert.DeserializeObject<UserCustomField>(stringResponse);
                return true;
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get the custom field from user." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get the custom field from user." + " Exception: " + e.Message, e);
            }
        }
        
        public OneSpanSign.API.CustomField CreateCustomField( OneSpanSign.API.CustomField apiField )
        {
            string path = template.UrlFor(UrlTemplate.ACCOUNT_CUSTOMFIELD_PATH).Build();
    
            try
            {
                string stringResponse;
                if (DoesCustomFieldExist(apiField.Id))
                {
                    stringResponse = client.Put(path, JsonConvert.SerializeObject(apiField, settings));
                }
                else
                {
                    stringResponse = client.Post(path, JsonConvert.SerializeObject(apiField, settings));
                }
                
                return JsonConvert.DeserializeObject<OneSpanSign.API.CustomField>(stringResponse);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not add/update the custom field to account." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not add/update the custom field to account." + " Exception: " + e.Message, e);
            }
        }

        public OneSpanSign.API.CustomField GetCustomField(string id)
        {
            string path = template.UrlFor(UrlTemplate.ACCOUNT_CUSTOMFIELD_ID_PATH)
                .Replace("{customFieldId}", id)
                .Build();

            try 
            {
                string response = client.Get(path);
                return JsonConvert.DeserializeObject<OneSpanSign.API.CustomField>(response);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get the custom field from account." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get the custom field from account." + " Exception: " + e.Message, e);
            }
        }

        public IList<OneSpanSign.API.CustomField> GetCustomFields(Direction direction, PageRequest request)
        {
            string path = template.UrlFor(UrlTemplate.ACCOUNT_CUSTOMFIELD_LIST_PATH)
                .Replace("{dir}", DirectionUtility.getDirection(direction))
                .Replace("{from}", request.From.ToString())
                .Replace("{to}", request.To.ToString())
                .Build();

            try 
            {
                string response = client.Get(path);
                return JsonConvert.DeserializeObject<IList<OneSpanSign.API.CustomField>> (response, settings);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get the list of custom fields from account." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get the list of custom fields from account." + " Exception: " + e.Message, e);
            }
        }

        public void DeleteCustomField( string id )
        {
            string path = template.UrlFor(UrlTemplate.ACCOUNT_CUSTOMFIELD_ID_PATH)
                .Replace("{customFieldId}", id)
                .Build();

            try
            {
                client.Delete(path);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not delete the custom field from account." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not delete the custom field from account." + " Exception: " + e.Message, e);
            }
        }

        public IList<UserCustomField> GetUserCustomFields()
        {
            string path = template.UrlFor(UrlTemplate.USER_CUSTOMFIELD_PATH).Build();
            string response;

            try 
            {
                response = client.Get(path);
                return JsonConvert.DeserializeObject<IList<OneSpanSign.API.UserCustomField>> (response, settings);
            } 
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get the custom fields for the user." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get the custom fields for the user." + " Exception: " + e.Message, e);
            }
        }

        public UserCustomField GetUserCustomField(string customFieldId)
        {
            string path = template.UrlFor(UrlTemplate.USER_CUSTOMFIELD_ID_PATH)
                .Replace("{customFieldId}", customFieldId)
                .Build();

            string response;
            try 
            {
                response = client.Get(path);
                return JsonConvert.DeserializeObject<OneSpanSign.API.UserCustomField> (response, settings);
            } 
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get the custom field for the user." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get the custom field for the user." + " Exception: " + e.Message, e);
            }
        }
        
        public UserCustomField SubmitCustomFieldValue(UserCustomField apiCustomFieldValue)
        {
            string path = template.UrlFor(UrlTemplate.USER_CUSTOMFIELD_PATH).Build();
            string response;
    
            try
            {
                string payload = JsonConvert.SerializeObject(apiCustomFieldValue, settings);
                if (DoesCustomFieldValueExist(apiCustomFieldValue.Id))
                {
                    response = client.Put(path, payload);
                }
                else
                {
                    response = client.Post(path, payload);
                }
                return JsonConvert.DeserializeObject<UserCustomField>(response);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not add/update the custom field to account." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not add/update the custom field to account." + " Exception: " + e.Message, e);
            }
        }

        public void DeleteUserCustomField(string id) 
        {
            string path = template.UrlFor(UrlTemplate.USER_CUSTOMFIELD_ID_PATH)
                    .Replace("{customFieldId}", id)
                    .Build();
            try 
            {
                client.Delete(path);
            } 
            catch (OssServerException e)
            {
                throw new OssServerException("Could not delete the custom field from user." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not delete the custom field from user." + " Exception: " + e.Message, e);
            }
        }

    }
}

