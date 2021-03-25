using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using OneSpanSign.API;
using OneSpanSign.Sdk.Internal;

namespace OneSpanSign.Sdk
{
    internal class AccountConfigClient
    {
        private UrlTemplate template;
        private RestClient restClient;
        private JsonSerializerSettings jsonSettings;

        public AccountConfigClient(RestClient restClient, string apiUrl, JsonSerializerSettings jsonSettings)
        {
            this.restClient = restClient;
            this.template = new UrlTemplate(apiUrl);
            this.jsonSettings = jsonSettings;
        }

        public OneSpanSign.API.Handover GetHandoverUrl(string language)
        {
            string path = template.UrlFor(UrlTemplate.HANDOVER_URL_PATH)
                .Replace("{language}", language)
                .Build();
            try
            {
                string stringResponse = restClient.Get(path);
                OneSpanSign.API.Handover apiResponse = JsonConvert.DeserializeObject<OneSpanSign.API.Handover>(stringResponse, jsonSettings);
                return apiResponse;
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get handover url.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get handover url.", e);
            }
        }

        public OneSpanSign.API.Handover CreateHandoverUrl(string language, OneSpanSign.API.Handover handover)
        {
            string path = template.UrlFor(UrlTemplate.HANDOVER_URL_PATH)
                .Replace("{language}", language)
                .Build();
            try
            {
                string json = JsonConvert.SerializeObject(handover, jsonSettings);
                string stringResponse = restClient.Post(path, json);

                OneSpanSign.API.Handover apiResponse = JsonConvert.DeserializeObject<OneSpanSign.API.Handover>(stringResponse, jsonSettings);
                return apiResponse;
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not create handover url.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not create handover url.", e);
            }
        }

        public OneSpanSign.API.Handover UpdateHandoverUrl(string language, OneSpanSign.API.Handover handover)
        {
            string path = template.UrlFor(UrlTemplate.HANDOVER_URL_PATH)
                .Replace("{language}", language)
                .Build();
            try
            {
                string json = JsonConvert.SerializeObject(handover, jsonSettings);
                string stringResponse = restClient.Put(path, json);

                OneSpanSign.API.Handover apiResponse = JsonConvert.DeserializeObject<OneSpanSign.API.Handover>(stringResponse, jsonSettings);
                return apiResponse;
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not update handover url.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not update handover url.", e);
            }
        }

        public void DeleteHandoverUrl(string language)
        {
            string path = template.UrlFor(UrlTemplate.HANDOVER_URL_PATH)
                .Replace("{language}", language)
                .Build();
            try
            {
                restClient.Delete(path);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not delete handover url.", e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not delete handover url.", e);
            }
        }
        
        public IList<string> CreateDeclineReasons(IList<string> declineReasons, string language)
        {
            string path = template.UrlFor(UrlTemplate.DECLINE_REASONS_PATH).Replace("{language}", language).Build();

            try
            {
                string serializeObject = JsonConvert.SerializeObject(declineReasons);
                string stringResponse = restClient.Post(path, serializeObject);
                return JsonConvert.DeserializeObject<IList<string>>(stringResponse, jsonSettings);
            }
            catch (OssServerException e)
            {
                throw new OssServerException(
                    "Could not create decline reasons for account." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (OssException e)
            {
                throw  new OssException("Could not create decline reasons for account." + " Exception: " + e.Message, e);
            }
        }
        
        public IList<string> UpdateDeclineReasons(IList<string> declineReasons, string language)
        {
            string path = template.UrlFor(UrlTemplate.DECLINE_REASONS_PATH).Replace("{language}", language)
                .Build();

            try
            {
                string serializeObject = JsonConvert.SerializeObject(declineReasons);
                string stringResponse = restClient.Put(path, serializeObject);
                return JsonConvert.DeserializeObject<IList<string>>(stringResponse, jsonSettings);
            }
            catch (OssServerException e)
            {
                throw new OssServerException(
                    "Could not update decline reasons for account." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (OssException e)
            {
                throw  new OssException("Could not update decline reasons for account." + " Exception: " + e.Message, e);
            }
        }

        public IList<string> GetDeclineReasons(string language)
        {
            string path = template.UrlFor(UrlTemplate.DECLINE_REASONS_PATH).Replace("{language}", language)
                .Build();

            try
            {
                string stringResponse = restClient.Get(path);
                return JsonConvert.DeserializeObject<IList<string>>(stringResponse, jsonSettings);
            }
            catch (OssServerException e)
           {
               throw new OssServerException(
                   "Could not get decline reasons for account." + " Exception: " + e.Message, e.ServerError, e);
           }
           catch (OssException e)
           {
               throw  new OssException("Could not get decline reasons for account." + " Exception: " + e.Message, e);
           }
        }
        
        public void DeleteDeclineReasons(string language)
        {
            string path = template.UrlFor(UrlTemplate.DECLINE_REASONS_PATH).Replace("{language}", language)
                .Build();

            try
            {
                string stringResponse = restClient.Delete(path);
            }
            catch (OssServerException e)
            {
                throw new OssServerException(
                    "Could not get decline reasons for account." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (OssException e)
            {
                throw  new OssException("Could not get decline reasons for account." + " Exception: " + e.Message, e);
            }
        }
    }
}