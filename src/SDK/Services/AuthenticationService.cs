using System;
using System.Web;
using OneSpanSign.Sdk.Internal;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace OneSpanSign.Sdk
{
    public class AuthenticationService
    {
        private UnauthenticatedRestClient client;
        private string webpageUrl;

        public AuthenticationService(string webpageUrl)
        {
            client = new UnauthenticatedRestClient();
            this.webpageUrl = webpageUrl;
        }

        public AuthenticationService (string webpageUrl, ProxyConfiguration proxyConfiguration)
        {
            client = new UnauthenticatedRestClient (proxyConfiguration);
            this.webpageUrl = webpageUrl;
        }

        public string GetSessionIdForUserAuthenticationToken(string userAuthenticationToken)
        {
            string path = new UrlTemplate(webpageUrl + UrlTemplate.ESL_AUTHENTICATION_PATH)
                                        .UrlFor(UrlTemplate.AUTHENTICATION_PATH_FOR_USER_AUTHENTICATION_TOKEN)
                                        .Replace("{authenticationToken}", userAuthenticationToken)
                                        .Build();
            try {
                string stringResponse = client.GetUnauthenticated(path);
                SessionToken userSessionIdToken = JsonConvert.DeserializeObject<SessionToken> (stringResponse);
                return userSessionIdToken.Token;
            }
            catch (OssServerException e) {
                throw new OssServerException("Could not authenticate using an authentication token."+ " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException("Could not authenticate using an authentication token."+ " Exception: " + e.Message, e);
            }
        }        

        public string BuildRedirectToDesignerForUserAuthenticationToken(string userAuthenticationToken, PackageId packageId)
        {
            try {
                string redirectPath = new UrlTemplate(webpageUrl)
                        .UrlFor(UrlTemplate.DESIGNER_REDIRECT_PATH)
                        .Replace("{packageId}", packageId.Id)
                        .Build();
                string encodedRedirectPath = HttpUtility.UrlEncode(redirectPath);
                string path = new UrlTemplate(webpageUrl + UrlTemplate.ESL_AUTHENTICATION_PATH)
                        .UrlFor(UrlTemplate.AUTHENTICATION_PATH_FOR_USER_AUTHENTICATION_TOKEN_WITH_REDIRECT)
                        .Replace("{authenticationToken}", userAuthenticationToken)
                        .Replace("{redirectUrl}", encodedRedirectPath)
                        .Build();
                return path;
            } catch (Exception e) {
                throw new OssException("Could not authenticate using a user authentication token."+ " Exception: " + e.Message, e);
            }
        }        

        public string GetSessionIdForSenderAuthenticationToken(string senderAuthenticationToken)
        {
            string path = new UrlTemplate(webpageUrl + UrlTemplate.ESL_AUTHENTICATION_PATH)
                                                .UrlFor(UrlTemplate.AUTHENTICATION_PATH_FOR_SENDER_AUTHENTICATION_TOKEN)
                                                .Replace("{senderAuthenticationToken}", senderAuthenticationToken)
                                                .Build();
            try {
                string stringResponse = client.GetUnauthenticated(path);
                SessionToken userSessionIdToken = JsonConvert.DeserializeObject<SessionToken> (stringResponse);
                return userSessionIdToken.Token;
            } 
            catch (OssServerException e) {
                throw new OssServerException("Could not authenticate using a sender authentication token."+ " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException("Could not authenticate using a sender authentication token."+ " Exception: " + e.Message, e);
            }
        }        

        public string BuildRedirectToDesignerForSender(string senderAuthenticationToken, PackageId packageId)
        {
            try {
                string redirectPath = new UrlTemplate(webpageUrl)
                        .UrlFor(UrlTemplate.DESIGNER_REDIRECT_PATH)
                        .Replace("{packageId}", packageId.Id)
                        .Build();
                string encodedRedirectPath = HttpUtility.UrlEncode(redirectPath);
                string path = new UrlTemplate(webpageUrl + UrlTemplate.ESL_AUTHENTICATION_PATH)
                        .UrlFor(UrlTemplate.AUTHENTICATION_PATH_FOR_SENDER_AUTHENTICATION_TOKEN_WITH_REDIRECT)
                        .Replace("{senderAuthenticationToken}", senderAuthenticationToken)
                        .Replace("{redirectUrl}", encodedRedirectPath)
                        .Build();

                return path;
            } catch (Exception e) {
                throw new OssException("Could not create a redirect to designer for a sender."+ " Exception: " + e.Message, e);
            }
        }        

        public string BuildRedirectToPackageViewForSender(string userAuthenticationToken, PackageId packageId)
        {
            try {
                string redirectPath = new UrlTemplate(webpageUrl)
                        .UrlFor(UrlTemplate.PACKAGE_VIEW_REDIRECT_PATH)
                        .Replace("{packageId}", packageId.Id)
                        .Build();
                string encodedRedirectPath = HttpUtility.UrlEncode(redirectPath);
                string path = new UrlTemplate(webpageUrl + UrlTemplate.ESL_AUTHENTICATION_PATH)
                        .UrlFor(UrlTemplate.AUTHENTICATION_PATH_FOR_USER_AUTHENTICATION_TOKEN_WITH_REDIRECT)
                        .Replace("{authenticationToken}", userAuthenticationToken)
                        .Replace("{redirectUrl}", encodedRedirectPath)
                        .Build();

                return path;
            } catch (Exception e) {
                throw new OssException("Could not create a redirect to package view for a sender."+ " Exception: " + e.Message, e);
            }
        }       

        public string GetSessionIdForSignerAuthenticationToken(string signerAuthenticationToken)
        {
            string path = new UrlTemplate(webpageUrl + UrlTemplate.ESL_AUTHENTICATION_PATH)
                    .UrlFor(UrlTemplate.AUTHENTICATION_PATH_FOR_SIGNER_AUTHENTICATION_TOKEN)
                    .Replace("{signerAuthenticationToken}", signerAuthenticationToken)
                    .Build();

            try {
                string stringResponse = client.GetUnauthenticated(path);
                SessionToken userSessionIdToken = JsonConvert.DeserializeObject<SessionToken> (stringResponse);
                return userSessionIdToken.Token;
            } 
            catch (OssServerException e) {
                throw new OssServerException("Could not authenticate using a signer authentication token."+ " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new OssException("Could not authenticate using a signer authentication token."+ " Exception: " + e.Message, e);
            }
        }

        public string BuildRedirectToSigningForSigner(string signerAuthenticationToken, PackageId packageId)
        {
            try {
                string redirectPath = new UrlTemplate(webpageUrl)
                        .UrlFor(UrlTemplate.SIGNING_REDIRECT_PATH)
                        .Replace("{packageId}", packageId.Id)
                        .Build();
                string encodedRedirectPath = HttpUtility.UrlEncode(redirectPath);
                string path = new UrlTemplate(webpageUrl + UrlTemplate.ESL_AUTHENTICATION_PATH)
                        .UrlFor(UrlTemplate.AUTHENTICATION_PATH_FOR_SIGNER_AUTHENTICATION_TOKEN_WITH_REDIRECT)
                        .Replace("{signerAuthenticationToken}", signerAuthenticationToken)
                        .Replace("{redirectUrl}", encodedRedirectPath)
                        .Build();

                return path;
            } catch (Exception e) {
                throw new OssException("Could not authenticate using a user authentication token."+ " Exception: " + e.Message, e);
            }
        }
    }
}

