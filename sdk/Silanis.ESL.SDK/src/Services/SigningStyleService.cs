using System;
using Silanis.ESL.SDK.Internal;
using Newtonsoft.Json;
using System.Collections.Generic;
using Silanis.ESL.API;

namespace Silanis.ESL.SDK
{
    public class SigningStyleService
    {
        private UrlTemplate template;
        private RestClient restClient;
        private JsonSerializerSettings settings;

        public SigningStyleService (RestClient restClient, string baseUrl, JsonSerializerSettings settings)
        {
            this.template = new UrlTemplate (baseUrl);
            this.restClient = restClient;
            this.settings = settings;
        }

        public IDictionary<string, object> CreateSigningThemes (string signingThemesString)
        {
            string path = template.UrlFor (UrlTemplate.ACCOUNT_SIGNING_THEME_PATH)
                          .Build ();

            try {
                string response = restClient.Post (path, signingThemesString);
                return JsonConvert.DeserializeObject<IDictionary<string, object>> (response, settings);
            } catch (EslServerException e) {
                throw new EslServerException ("Could not create the signing themes for account." + " Exception: " + e.Message, e.ServerError, e);
            } catch (Exception e) {
                throw new EslException ("Could not create the signing themes for account." + " Exception: " + e.Message, e);
            }
        }

        public IDictionary<string, object> GetSigningThemes ()
        {
            string path = template.UrlFor (UrlTemplate.ACCOUNT_SIGNING_THEME_PATH)
                          .Build ();

            try {
                string response = restClient.Get (path);
                return JsonConvert.DeserializeObject<IDictionary<string, object>> (response, settings);
            } catch (EslServerException e) {
                throw new EslServerException ("Could not get the signing themes from account." + " Exception: " + e.Message, e.ServerError, e);
            } catch (Exception e) {
                throw new EslException ("Could not get the signing themes from account." + " Exception: " + e.Message, e);
            }
        }

        public IDictionary<string, object> UpdateSigningThemes (string signingThemesString)
        {
            string path = template.UrlFor (UrlTemplate.ACCOUNT_SIGNING_THEME_PATH)
                          .Build ();

            try {
                string response = restClient.Put (path, signingThemesString);
                return JsonConvert.DeserializeObject<IDictionary<string, object>> (response, settings);
            } catch (EslServerException e) {
                throw new EslServerException ("Could not update the signing themes to account." + " Exception: " + e.Message, e.ServerError, e);
            } catch (Exception e) {
                throw new EslException ("Could not update the signing themes to account." + " Exception: " + e.Message, e);
            }
        }

        public void DeleteSigningThemes ()
        {
            string path = template.UrlFor (UrlTemplate.ACCOUNT_SIGNING_THEME_PATH)
                          .Build ();

            try {
                string response = restClient.Delete (path);
            } catch (EslServerException e) {
                throw new EslServerException ("Could not delete the signing themes from account." + " Exception: " + e.Message, e.ServerError, e);
            } catch (Exception e) {
                throw new EslException ("Could not delete the signing themes from account." + " Exception: " + e.Message, e);
            }
        }
    }
}