using System;
using OneSpanSign.Sdk.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using OneSpanSign.API;

namespace OneSpanSign.Sdk
{
    public class SigningStyleService
    {
        private RestClient restClient;
        private JsonSerializerSettings settings;
        private string baseUrl;

        public SigningStyleService (RestClient restClient, string baseUrl, JsonSerializerSettings settings)
        {
            this.restClient = restClient;
            this.settings = settings;
            this.baseUrl = baseUrl;
        }

        public IDictionary<string, object> CreateSigningThemes (string signingThemesString)
        {
            string path = new UrlTemplate(baseUrl).UrlFor (UrlTemplate.ACCOUNT_SIGNING_THEME_PATH)
                          .Build ();

            try
            {
                string response = restClient.Post (path, signingThemesString);
                return JsonConvert.DeserializeObject<IDictionary<string, object>> (response, settings);
            } 
            catch (OssServerException e) 
            {
                throw new OssServerException ("Could not create the signing themes for account." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) 
            {
                throw new OssException ("Could not create the signing themes for account." + " Exception: " + e.Message, e);
            }
        }

        public IDictionary<string, object> GetSigningThemes ()
        {
            string path = new UrlTemplate(baseUrl).UrlFor (UrlTemplate.ACCOUNT_SIGNING_THEME_PATH)
                          .Build ();

            try
            {
                string response = restClient.Get (path);
                return JsonConvert.DeserializeObject<IDictionary<string, object>> (response, settings);
            }
            catch (OssServerException e)
            {
                throw new OssServerException ("Could not get the signing themes from account." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException ("Could not get the signing themes from account." + " Exception: " + e.Message, e);
            }
        }

        public IDictionary<string, object> UpdateSigningThemes (string signingThemesString)
        {
            string path = new UrlTemplate(baseUrl).UrlFor (UrlTemplate.ACCOUNT_SIGNING_THEME_PATH)
                          .Build ();

            try
            {
                string response = restClient.Put (path, signingThemesString);
                return JsonConvert.DeserializeObject<IDictionary<string, object>> (response, settings);
            }
            catch (OssServerException e )
            {
                throw new OssServerException ("Could not update the signing themes to account." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) 
            {
                throw new OssException ("Could not update the signing themes to account." + " Exception: " + e.Message, e);
            }
        }

        public void DeleteSigningThemes ()
        {
            string path = new UrlTemplate(baseUrl).UrlFor (UrlTemplate.ACCOUNT_SIGNING_THEME_PATH)
                          .Build ();

            try 
            {
                string response = restClient.Delete (path);
            }
            catch (OssServerException e)
            {
                throw new OssServerException ("Could not delete the signing themes from account." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException ("Could not delete the signing themes from account." + " Exception: " + e.Message, e);
            }
        }

        public void SaveSigningLogos (List<SigningLogo> signingLogos)
        {
            string path = new UrlTemplate(baseUrl).UrlFor (UrlTemplate.ACCOUNT_SIGNING_LOGO_PATH)
                          .Build ();
            string payload = JsonConvert.SerializeObject (signingLogos, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver (), Formatting = Formatting.Indented });

            try
            {
                restClient.Post (path, payload);
            }
            catch (OssServerException e)
            {
                throw new OssServerException ("Could not save the signing logos for account." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException ("Could not save the signing logos for account." + " Exception: " + e.Message, e);
            }
        }

        public List<SigningLogo> GetSigningLogos ()
        {
            string path = new UrlTemplate(baseUrl).UrlFor (UrlTemplate.ACCOUNT_SIGNING_LOGO_PATH)
                          .Build ();

            try
            {
                string response = restClient.Get (path);
                return JsonConvert.DeserializeObject<List<SigningLogo>> (response, settings);
            }
            catch (OssServerException e)
            {
                throw new OssServerException ("Could not get the signing logos from account." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e )
            {
                throw new OssException ("Could not get the signing logos from account." + " Exception: " + e.Message, e);
            }
        }

        public SigningUiOptions GetSigningUiOptions()
        {
            string path = new UrlTemplate(baseUrl).UrlFor (UrlTemplate.ACCOUNT_SIGNING_UI_OPTIONS_PATH)
                .Build ();

            try
            {
                string response = restClient.Get (path);
                return JsonConvert.DeserializeObject<SigningUiOptions> (response, settings);
            }
            catch (OssServerException e)
            {
                throw new OssServerException ("Could not get the signing ui options from account." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e )
            {
                throw new OssException ("Could not get the signing ui options from account." + " Exception: " + e.Message, e);
            }
        }
        
        public void SaveSigningUiOptions(SigningUiOptions signingUiOptions)
        {
            string path = new UrlTemplate(baseUrl).UrlFor (UrlTemplate.ACCOUNT_SIGNING_UI_OPTIONS_PATH)
                .Build ();
            string payload = JsonConvert.SerializeObject(signingUiOptions, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver (), Formatting = Formatting.Indented ,NullValueHandling = NullValueHandling.Ignore});
            try
            {
                restClient.Patch(path, payload);
            }
            catch (OssServerException e)
            {
                throw new OssServerException ("Could not save the signing logos for account." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException ("Could not save the signing logos for account." + " Exception: " + e.Message, e);
            }
        }
        
        public void DeleteSigningUiOptions ()
        {
            string path = new UrlTemplate(baseUrl).UrlFor (UrlTemplate.ACCOUNT_SIGNING_UI_OPTIONS_PATH)
                .Build ();

            try 
            {
                string response = restClient.Delete (path);
            }
            catch (OssServerException e)
            {
                throw new OssServerException ("Could not delete the signing ui options from account." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException ("Could not delete the signing ui options from account." + " Exception: " + e.Message, e);
            }
        }
        
    }
}