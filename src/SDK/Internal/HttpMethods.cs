using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using OneSpanSign.Sdk;
using System.Text;
using System.Security.Authentication;
using Newtonsoft.Json;

namespace OneSpanSign.Sdk.Internal
{
	/// <summary>
	/// For internal use.
	/// </summary>
	public class HttpMethods
	{
        public const string ESL_API_VERSION = "11.32";
        public const string ESL_API_VERSION_HEADER = "esl-api-version=" + ESL_API_VERSION;

        public const string CONTENT_TYPE_APPLICATION_JSON = "application/json";
        public const string ESL_CONTENT_TYPE_APPLICATION_JSON = CONTENT_TYPE_APPLICATION_JSON + "; " + ESL_API_VERSION_HEADER;

        public const string CONTENT_TYPE_APPLICATION_MULTIPART = "multipart/form-data";
        public const string ESL_CONTENT_TYPE_APPLICATION_MULTIPART = CONTENT_TYPE_APPLICATION_MULTIPART + "; " + ESL_API_VERSION_HEADER + "; boundary={0}";

        public const string ACCEPT_TYPE_APPLICATION_JSON = "application/json";
        public const string ACCEPT_TYPE_APPLICATION_OCTET_STREAM = "application/octet-stream";
        public const string ACCEPT_TYPE_APPLICATION = "*/*";
        public const string ESL_ACCEPT_TYPE_APPLICATION_JSON = ACCEPT_TYPE_APPLICATION_JSON + "; " + ESL_API_VERSION_HEADER;
        public const string ESL_ACCEPT_TYPE_APPLICATION_OCTET_STREAM = ACCEPT_TYPE_APPLICATION_OCTET_STREAM + "; " + ESL_API_VERSION_HEADER;
        public const string ESL_ACCEPT_TYPE_APPLICATION = ACCEPT_TYPE_APPLICATION + "; " + ESL_API_VERSION_HEADER;

        public static ProxyConfiguration proxyConfiguration;
        public static ApiTokenConfig apiTokenConfig { set; get; }
        private static ApiToken apiToken = null;

		private static void setupAuthorization(WebRequest request, string apiKey) {
            if (apiTokenConfig != null) {
                //Do we have an api token and is it still valid for at least a minute ?
                TimeSpan timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1);
                long milliseconds = (long)timeSpan.TotalMilliseconds;
                if (apiToken == null || milliseconds > apiToken.ExpiresAt - 60 * 1000) {
                    //We need to fetch a new access token using the clientAppId/Secret
                    string jsonPayload = String.Format("{{\"clientId\":\"{0}\",\"secret\":\"{1}\",\"type\":\"{2}\"", apiTokenConfig.ClientAppId, apiTokenConfig.ClientAppSecret, apiTokenConfig.TokenType.ToString());
                    if (apiTokenConfig.TokenType == ApiTokenType.SENDER) {
                        jsonPayload += String.Format("\"email\":\"{0}\"", apiTokenConfig.SenderEmail);
                    }
                    jsonPayload += "}";
                    byte[] jsonPayloadBytes = Encoding.Unicode.GetBytes(jsonPayload);
                    HttpWebRequest apiTokenRequest = (HttpWebRequest)WebRequest.Create(apiTokenConfig.BaseUrl+"/apitoken/clientApp/accessToken");
                    apiTokenRequest.Method = "POST";
                    apiTokenRequest.ContentType = ESL_CONTENT_TYPE_APPLICATION_JSON;
                    apiTokenRequest.ContentLength = jsonPayloadBytes.Length;
                    apiTokenRequest.Accept = ESL_ACCEPT_TYPE_APPLICATION_JSON;
                    apiTokenRequest.Timeout = 30000; //30 seconds
                    SetProxy(apiTokenRequest);

                    using (Stream dataStream = apiTokenRequest.GetRequestStream())
                    {
                        dataStream.Write(jsonPayloadBytes, 0, jsonPayloadBytes.Length);
                    }

                    var httpResponse = (HttpWebResponse)apiTokenRequest.GetResponse();

                    if (httpResponse.StatusCode != HttpStatusCode.OK) {
                        throw new OssException("Unable to fetch access token for "+apiTokenConfig.ToString()+", response was "+httpResponse.ToString(), null);
                    }

                    string result;
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        result = streamReader.ReadToEnd();
                    }
                    httpResponse.Close();

                    apiToken = JsonConvert.DeserializeObject<ApiToken>(result);
                }
                request.Headers.Add ("Authorization", "Bearer " + apiToken.AccessToken);
            } else {
                request.Headers.Add ("Authorization", "Basic " + apiKey);
            }
        }

		public static byte[] PostHttp (string apiToken, string path, byte[] content)
		{
            return PostHttp (apiToken, path, content, new Dictionary<string, string> ());
		}

        public static byte [] PostHttp (string apiKey, string path, byte [] content, IDictionary<string, string> headers)
        {
            try {
                System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create (path);
                request.Method = "POST";
                request.ContentType = ESL_CONTENT_TYPE_APPLICATION_JSON;
                request.ContentLength = content.Length;
                AddAdditionalHeaders (request, headers);
                setupAuthorization(request, apiKey);
                request.Accept = ESL_ACCEPT_TYPE_APPLICATION_JSON;
                SetProxy (request);

                using (Stream dataStream = request.GetRequestStream ()) {
                    dataStream.Write (content, 0, content.Length);
                }

                WebResponse response = request.GetResponse ();

                using (Stream responseStream = response.GetResponseStream ()) {
                    var memoryStream = new MemoryStream ();
                    CopyTo (responseStream, memoryStream);

                    byte [] result = memoryStream.ToArray ();
                    return result;
                }
            } catch (WebException e) {
                using (var stream = e.Response.GetResponseStream ())
                using (var reader = new StreamReader (stream)) {
                    string errorDetails = reader.ReadToEnd ();
                    throw new OssServerException (String.Format ("{0} HTTP {1} on URI {2}. Optional details: {3}", e.Message,
                                                               ((HttpWebResponse)e.Response).Method, e.Response.ResponseUri, errorDetails),
                                                                errorDetails, e);
                }
            } catch (Exception e) {
                throw new OssException ("Error communicating with oss server. " + e.Message, e);
            }
        }

        public static byte[] PostHttp (AuthHeaderGenerator authHeaderGen, string path, byte[] content)
        {
            return PostHttp (authHeaderGen, path, content, new Dictionary<string, string> ());
        }

        public static byte [] PostHttp (AuthHeaderGenerator authHeaderGen, string path, byte [] content, IDictionary<string, string> headers)
        {
            try {
                System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create (path);
                request.Method = "POST";
                request.ContentType = ESL_CONTENT_TYPE_APPLICATION_JSON;
                request.ContentLength = content.Length;
                AddAdditionalHeaders (request, headers);
                setupAuthorization(request, authHeaderGen.Value);
                request.Accept = ESL_ACCEPT_TYPE_APPLICATION_JSON;
                SetProxy (request);

                using (Stream dataStream = request.GetRequestStream ()) {
                    dataStream.Write (content, 0, content.Length);
                }

                WebResponse response = request.GetResponse ();

                using (Stream responseStream = response.GetResponseStream ()) {
                    var memoryStream = new MemoryStream ();
                    CopyTo (responseStream, memoryStream);

                    byte [] result = memoryStream.ToArray ();
                    return result;
                }
            } catch (WebException e) {
                using (var stream = e.Response.GetResponseStream ())
                using (var reader = new StreamReader (stream)) {
                    string errorDetails = reader.ReadToEnd ();
                    throw new OssServerException (String.Format ("{0} HTTP {1} on URI {2}. Optional details: {3}", e.Message,
                                                               ((HttpWebResponse)e.Response).Method, e.Response.ResponseUri, errorDetails),
                                                 errorDetails, e);
                }
            } catch (Exception e) {
                throw new OssException ("Error communicating with oss server. " + e.Message, e);
            }
        }

		public static byte[] PutHttp (string apiKey, string path, byte[] content)
		{
            return PutHttp (apiKey, path, content, new Dictionary<string, string> ());
		}

        public static byte [] PutHttp (string apiKey, string path, byte [] content, IDictionary<string, string> headers)
        {
            try {
                System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create (path);
                request.Method = "PUT";
                request.ContentType = ESL_CONTENT_TYPE_APPLICATION_JSON;
                request.ContentLength = content.Length;
                AddAdditionalHeaders (request, headers);
                setupAuthorization(request, apiKey);
                request.Accept = ESL_ACCEPT_TYPE_APPLICATION_JSON;
                SetProxy (request);

                using (Stream dataStream = request.GetRequestStream ()) {
                    dataStream.Write (content, 0, content.Length);
                }

                WebResponse response = request.GetResponse ();

                using (Stream responseStream = response.GetResponseStream ()) {
                    var memoryStream = new MemoryStream ();
                    CopyTo (responseStream, memoryStream);

                    byte [] result = memoryStream.ToArray ();

                    return result;
                }
            } catch (WebException e) {
                using (var stream = e.Response.GetResponseStream ())
                using (var reader = new StreamReader (stream)) {
                    string errorDetails = reader.ReadToEnd ();
                    throw new OssServerException (String.Format ("{0} HTTP {1} on URI {2}. Optional details: {3}", e.Message,
                                                               ((HttpWebResponse)e.Response).Method, e.Response.ResponseUri, errorDetails),
                                                                errorDetails, e);
                }
            } catch (Exception e) {
                throw new OssException ("Error communicating with oss server. " + e.Message, e);
            }
        }

        /// <summary>
        /// Can only be called for unauthenticated path such as /auth
        /// Gets the http.
        /// </summary>
        public static byte[] GetHttp (string path)
        {
            return GetHttp (path, new Dictionary<string, string> ());
        }

        public static byte [] GetHttp (string path, IDictionary<string, string> headers)
        {
            string message = "";
            UseUnsafeHeaderParsing (ref message);
            try {
                System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create (path);
                request.Method = "GET";
                AddAdditionalHeaders (request, headers);
                request.Accept = ESL_ACCEPT_TYPE_APPLICATION_JSON;
                SetProxy (request);

                WebResponse response = request.GetResponse ();

                using (Stream responseStream = response.GetResponseStream ()) {
                    var memoryStream = new MemoryStream ();
                    CopyTo (responseStream, memoryStream);
                    byte [] result = memoryStream.ToArray ();

                    return result;
                }
            } catch (WebException e) {
                using (var stream = e.Response.GetResponseStream ())
                using (var reader = new StreamReader (stream)) {
                    string errorDetails = reader.ReadToEnd ();
                    throw new OssServerException (String.Format ("{0} HTTP {1} on URI {2}. Optional details: {3}", e.Message,
                                                               ((HttpWebResponse)e.Response).Method, e.Response.ResponseUri, errorDetails),
                                                                errorDetails, e);
                }
            } catch (Exception e) {
                throw new OssException ("Error communicating with oss server. " + e.Message, e);
            }
        }

        public static bool UseUnsafeHeaderParsing(ref string strError)
        {
            //Assembly assembly = Assembly.GetAssembly(typeof(System.Net.Configuration.SettingsSection));
            //if (null == assembly)
            //{
            //    strError = "Could not access Assembly";
            //    return false;
            //}

            //Type type = assembly.GetType("System.Net.Configuration.SettingsSectionInternal");
            //if (null == type)
            //{
            //    strError = "Could not access internal settings";
            //    return false;
            //}

            //object obj = type.InvokeMember("Section",
            //                               BindingFlags.Static | BindingFlags.GetProperty | BindingFlags.NonPublic,
            //                               null, null, new object[] { });

            //if (null == obj)
            //{
            //    strError = "Could not invoke Section member";
            //    return false;
            //}

            //// If it's not already set, set it.
            //FieldInfo fi = type.GetField("useUnsafeHeaderParsing", BindingFlags.NonPublic | BindingFlags.Instance);
            //if (null == fi)
            //{
            //    strError = "Could not access useUnsafeHeaderParsing field";
            //    return false;
            //}

            //if (!Convert.ToBoolean(fi.GetValue(obj)))
            //{
            //    fi.SetValue(obj, true);
            //}

            return true;
        }

        public static DownloadedFile GetHttpJson (string apiKey, string path, string acceptType)
		{
            return GetHttpJson (apiKey, path, acceptType, new Dictionary<string, string> ());
		}

        public static DownloadedFile GetHttpJson (string apiKey, string path, string acceptType, IDictionary<string, string> headers)
        {
            try {
                System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create (path);
                request.Method = "GET";
                AddAdditionalHeaders (request, headers);
                setupAuthorization(request, apiKey);
                request.Accept = acceptType;
                SetProxy (request);

                WebResponse response = request.GetResponse ();

                using (Stream responseStream = response.GetResponseStream ()) {

                    var memoryStream = new MemoryStream ();
                    CopyTo (responseStream, memoryStream);
                    byte [] result = memoryStream.ToArray ();

                    DownloadedFile downloadedFile = new DownloadedFile ("", result);
                    return downloadedFile;
                }
            } catch (WebException e) {
                using (var stream = e.Response.GetResponseStream ())
                using (var reader = new StreamReader (stream)) {
                    string errorDetails = reader.ReadToEnd ();
                    throw new OssServerException (String.Format ("{0} HTTP {1} on URI {2}. Optional details: {3}", e.Message,
                                                               ((HttpWebResponse)e.Response).Method, e.Response.ResponseUri, errorDetails),
                                                                errorDetails, e);
                }
            } catch (Exception e) {
                throw new OssException ("Error communicating with oss server. " + e.Message, e);
            }
        }

        public static DownloadedFile GetHttp (string apiKey, string path)
		{
            return GetHttp (apiKey, path, new Dictionary<string, string> ());
		}

        public static DownloadedFile GetHttp (string apiKey, string path, IDictionary<string, string> headers)
        {
            try {
                System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create (path);
                request.Method = "GET";
                AddAdditionalHeaders (request, headers);
                setupAuthorization(request, apiKey);
                request.Accept = ESL_ACCEPT_TYPE_APPLICATION;
                SetProxy (request);

                WebResponse response = request.GetResponse ();

                using (Stream responseStream = response.GetResponseStream ()) {
                    var memoryStream = new MemoryStream ();
                    CopyTo (responseStream, memoryStream);
                    byte [] result = memoryStream.ToArray ();

                    string fileName = "";
                    if (!String.IsNullOrEmpty (response.Headers ["Content-Disposition"])) {
                        fileName = GetFilename (response.Headers ["Content-Disposition"].ToString ());
                    }

                    DownloadedFile downloadedFile = new DownloadedFile (fileName, result);

                    return downloadedFile;
                }
            } catch (WebException e) {
                using (var stream = e.Response.GetResponseStream ())
                using (var reader = new StreamReader (stream)) {
                    string errorDetails = reader.ReadToEnd ();
                    throw new OssServerException (String.Format ("{0} HTTP {1} on URI {2}. Optional details: {3}", e.Message,
                                                               ((HttpWebResponse)e.Response).Method, e.Response.ResponseUri, errorDetails),
                                                                errorDetails, e);
                }
            } catch (Exception e) {
                throw new OssException ("Error communicating with oss server. " + e.Message, e);
            }
        }

        private static string GetFilename(string disposition) 
        {
            string fileNameTitle = "filename*=UTF-8''";
            string[] parts = disposition.Split(';');

            foreach(string part in parts) 
            {
                int index = part.IndexOf(fileNameTitle);
                if (index > 0) 
                {
                    return Uri.UnescapeDataString(part.Substring(fileNameTitle.Length+1, part.Length-fileNameTitle.Length-1));
                }
            }

            return "";
        }

        public static DownloadedFile GetHttpAsOctetStream (string apiKey, string path)
        {
            return GetHttpAsOctetStream (apiKey, path, new Dictionary<string, string> ());
        }

        public static DownloadedFile GetHttpAsOctetStream (string apiKey, string path, IDictionary<string, string> headers)
        {
            try {
                System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create (path);
                request.Method = "GET";
                AddAdditionalHeaders (request, headers);
                setupAuthorization(request, apiKey);
                request.Accept = ESL_ACCEPT_TYPE_APPLICATION_OCTET_STREAM;
                SetProxy (request);

                WebResponse response = request.GetResponse ();

                using (Stream responseStream = response.GetResponseStream ()) {
                    var memoryStream = new MemoryStream ();
                    CopyTo (responseStream, memoryStream);
                    byte [] result = memoryStream.ToArray ();

                    DownloadedFile downloadedFile = new DownloadedFile ("", result);

                    return downloadedFile;
                }
            } catch (WebException e) {
                using (var stream = e.Response.GetResponseStream ())
                using (var reader = new StreamReader (stream)) {
                    string errorDetails = reader.ReadToEnd ();
                    throw new OssServerException (String.Format ("{0} HTTP {1} on URI {2}. Optional details: {3}", e.Message,
                                                               ((HttpWebResponse)e.Response).Method, e.Response.ResponseUri, errorDetails),
                                                 errorDetails, e);
                }
            } catch (Exception e) {
                throw new OssException ("Error communicating with oss server. " + e.Message, e);
            }
        }

        public static byte[] DeleteHttp (string apiKey, string path)
		{
            return DeleteHttp (apiKey, path, new Dictionary<string, string> ());
		}

        public static byte [] DeleteHttp (string apiKey, string path, IDictionary<string, string> headers)
        {
            try {
                System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create (path);
                request.Method = "DELETE";
                AddAdditionalHeaders (request, headers);
                setupAuthorization(request, apiKey);
                request.Accept = ESL_ACCEPT_TYPE_APPLICATION_JSON;
                SetProxy (request);

                WebResponse response = request.GetResponse ();

                using (Stream responseStream = response.GetResponseStream ()) {
                    var memoryStream = new MemoryStream ();
                    CopyTo (responseStream, memoryStream);
                    byte [] result = memoryStream.ToArray ();

                    return result;
                }
            } catch (WebException e) {
                using (var stream = e.Response.GetResponseStream ())
                using (var reader = new StreamReader (stream)) {
                    string errorDetails = reader.ReadToEnd ();
                    throw new OssServerException (String.Format ("{0} HTTP {1} on URI {2}. Optional details: {3}", e.Message,
                                                               ((HttpWebResponse)e.Response).Method, e.Response.ResponseUri, errorDetails),
                                                                errorDetails, e);
                }
            } catch (Exception e) {
                throw new OssException ("Error communicating with oss server. " + e.Message, e);
            }
        }

        public static byte [] DeleteHttp (AuthHeaderGenerator authHeader, string path, byte [] content, IDictionary<string, string> headers)
        {
            headers[authHeader.Name]=authHeader.Value;
            return DeleteHttp (path, content, headers, (string)null);
        }

        public static byte [] DeleteHttp (string apiKey, string path, byte [] content, IDictionary<string, string> headers)
        {
            return DeleteHttp (path, content, headers, apiKey);
        }

        private static byte [] DeleteHttp (string path, byte [] content, IDictionary<string, string> headers, string apiKey)
        {
            try {
                System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create (path);
                request.Method = "DELETE";
                request.ContentType = ESL_CONTENT_TYPE_APPLICATION_JSON;
                request.ContentLength = content.Length;
                AddAdditionalHeaders (request, headers);
                setupAuthorization(request, apiKey);

                request.Accept = ESL_ACCEPT_TYPE_APPLICATION_JSON;
                SetProxy (request);

                using (Stream dataStream = request.GetRequestStream ()) {
                    dataStream.Write (content, 0, content.Length);
                }

                WebResponse response = request.GetResponse ();

                using (Stream responseStream = response.GetResponseStream ()) {
                    var memoryStream = new MemoryStream ();
                    CopyTo (responseStream, memoryStream);
                    byte [] result = memoryStream.ToArray ();

                    return result;
                }
            } catch (WebException e) {
                using (var stream = e.Response.GetResponseStream ())
                using (var reader = new StreamReader (stream)) {
                    string errorDetails = reader.ReadToEnd ();
                    throw new OssServerException (String.Format ("{0} HTTP {1} on URI {2}. Optional details: {3}", e.Message,
                                                               ((HttpWebResponse)e.Response).Method, e.Response.ResponseUri, errorDetails),
                                                                errorDetails, e);
                }
            } catch (Exception e) {
                throw new OssException ("Error communicating with oss server. " + e.Message, e);
            }
        }

        private static void AddAdditionalHeaders (WebRequest request, IDictionary<string, string> headers)
        {
            foreach (KeyValuePair<string, string> entry in headers) {
                request.Headers.Add (entry.Key, entry.Value);
            }
        }

		public static void AddAuthorizationHeader(WebRequest request, AuthHeaderGenerator authHeaderGen)
		{
			request.Headers.Add(authHeaderGen.Name, authHeaderGen.Value);
		}

        public static string MultipartPostHttp (string apiKey, string path, byte[] content, string boundary, AuthHeaderGenerator authHeaderGen)
        {
            return MultipartPostHttp (apiKey, path, content, boundary, authHeaderGen, new Dictionary<string, string> ());
        }

        public static string MultipartPostHttp (string apiKey, string path, byte [] content, string boundary, AuthHeaderGenerator authHeaderGen, IDictionary<string, string> headers)
        {
            WebRequest request = WebRequest.Create (path);
            try {
                System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

                request.Method = "POST";
                request.ContentType = string.Format (ESL_CONTENT_TYPE_APPLICATION_MULTIPART, boundary);
                request.ContentLength = content.Length;
                AddAdditionalHeaders (request, headers);
                AddAuthorizationHeader (request, authHeaderGen);
                setupAuthorization(request, apiKey);
                SetProxy (request);

                using (Stream dataStream = request.GetRequestStream ()) {
                    dataStream.Write (content, 0, content.Length);
                }

                WebResponse response = request.GetResponse ();

                using (Stream responseStream = response.GetResponseStream ()) {
                    StreamReader reader = new StreamReader (responseStream, Encoding.UTF8);
                    return reader.ReadToEnd ();
                }
            } catch (WebException e) {
                using (var stream = e.Response.GetResponseStream ())
                using (var reader = new StreamReader (stream)) {
                    string errorDetails = reader.ReadToEnd ();
                    throw new OssServerException (String.Format ("{0} HTTP {1} on URI {2}. Optional details: {3}", e.Message,
                                                               ((HttpWebResponse)e.Response).Method, e.Response.ResponseUri, errorDetails),
                                                 errorDetails, e);
                }
            } catch (Exception e) {
                throw new OssException ("Error communicating with oss server. " + e.Message, e);
            }
        }

		private static void CopyTo (Stream input, Stream output)
		{
			byte[] buffer = new byte[64 * 1024]; // Fairly arbitrary size
			int bytesRead;

			while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0) {
				output.Write (buffer, 0, bytesRead);
			}
		}

        private static void SetProxy (WebRequest request)
        {
            if (proxyConfiguration != null)
            {
                WebProxy webProxy = new WebProxy(new Uri(proxyConfiguration.GetScheme() 
                                                         + "://" 
                                                         + proxyConfiguration.GetHost() 
                                                         + ":" 
                                                         + proxyConfiguration.GetPort()));
                if (proxyConfiguration.HasCredentials())
                {
                    webProxy.Credentials = new NetworkCredential(proxyConfiguration.GetUserName(), 
                                                                 proxyConfiguration.GetPassword());
                }
                request.Proxy = webProxy;
            }
        }
	}
}

