using System;
using OneSpanSign.Sdk.Internal;
using Newtonsoft.Json;
using OneSpanSign.API;
using System.Web;

namespace OneSpanSign.Sdk
{
    internal class LayoutApiClient
    {
        private RestClient restClient;
        private JsonSerializerSettings settings;
        private string baseUrl;

        public LayoutApiClient(RestClient restClient, string baseUrl, JsonSerializerSettings settings)
        {
            this.restClient = restClient;
            this.settings = settings;
            this.baseUrl = baseUrl;
        }

        public string CreateLayout(Package layoutPackage, String packageId)
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.LAYOUT_PATH)
                .Build();

            string packageJson = JsonConvert.SerializeObject(layoutPackage, settings);
            Template apiTemplate = JsonConvert.DeserializeObject<OneSpanSign.API.Template>(packageJson, settings);
            apiTemplate.Id = packageId;
            String templateJson = JsonConvert.SerializeObject(apiTemplate, settings);

            try
            {
                string response = restClient.Post(path, templateJson);
                Package aPackage = JsonConvert.DeserializeObject<OneSpanSign.API.Package>(response, settings);
                return aPackage.Id;
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not create layout." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not create layout." + " Exception: " + e.Message, e);
            }
        }

        public Package CreateAndGetLayout(Package layoutPackage, string packageId)
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.LAYOUT_PATH)
                .Build();

            string packageJson = JsonConvert.SerializeObject(layoutPackage, settings);
            Template apiTemplate = JsonConvert.DeserializeObject<OneSpanSign.API.Template>(packageJson, settings);
            apiTemplate.Id = packageId;
            String templateJson = JsonConvert.SerializeObject(apiTemplate, settings);

            try
            {
                string response = restClient.Post(path, templateJson);
                return JsonConvert.DeserializeObject<OneSpanSign.API.Package>(response, settings);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not create layout." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not create layout." + " Exception: " + e.Message, e);
            }
        }

        public Result<Package> GetLayouts(Direction direction, PageRequest request)
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.LAYOUT_LIST_PATH)
                .Replace("{dir}", DirectionUtility.getDirection(direction))
                .Replace("{from}", request.From.ToString())
                .Replace("{to}", request.To.ToString())
                .Build();

            try
            {
                string response = restClient.Get(path);
                return JsonConvert.DeserializeObject<Result<OneSpanSign.API.Package>>(response, settings);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get list of layouts." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get list of layouts." + " Exception: " + e.Message, e);
            }
        }

        public void ApplyLayout(string packageId, string documentId, string layoutId)
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.APPLY_LAYOUT_PATH)
                .Replace("{packageId}", packageId)
                .Replace("{documentId}", documentId)
                .Replace("{layoutId}", layoutId)
                .Build();

            try
            {
                restClient.Post(path, "");
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not apply layout." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not apply layout." + " Exception: " + e.Message, e);
            }
        }

        public void ApplyLayoutByName(string packageId, string documentId, string layoutName)
        {
            string path = new UrlTemplate(baseUrl).UrlFor(UrlTemplate.APPLY_LAYOUT_BY_NAME_PATH)
                .Replace("{packageId}", packageId)
                .Replace("{documentId}", documentId)
                .Replace("{layoutName}", HttpUtility.UrlEncode(layoutName))
                .Build();

            try
            {
                restClient.Post(path, "");
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not apply layout." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not apply layout." + " Exception: " + e.Message, e);
            }
        }

    }
}

