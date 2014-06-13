using System;
using System.Net;
using System.IO;
using System.Collections.Generic;

namespace Silanis.ESL.SDK.Internal
{
	/// <summary>
	/// For internal use.
	/// </summary>
	public class HttpMethods
	{
		public static byte[] PostHttp (string apiToken, string path, byte[] content)
		{
            try {
				WebRequest request = WebRequest.Create (path);
				request.Method = "POST";
				request.ContentType = "application/json";
				request.ContentLength = content.Length;
				request.Headers.Add ("Authorization", "Basic " + apiToken);

				using (Stream dataStream = request.GetRequestStream ()) {
					dataStream.Write (content, 0, content.Length);
				}

				WebResponse response = request.GetResponse ();

				using (Stream responseStream = response.GetResponseStream()) {
					var memoryStream = new MemoryStream ();
					CopyTo (responseStream, memoryStream);
	          
					byte[] result = memoryStream.ToArray();
					return result;
				}
            }
            catch (WebException e){
                using (var stream = e.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    string errorDetails = reader.ReadToEnd();
                    throw new EslServerException(String.Format("{0} HTTP {1} on URI {2}. Optional details: {3}", e.Message, 
                                                               ((HttpWebResponse)e.Response).Method, e.Response.ResponseUri, errorDetails),
                                                                errorDetails, e);
                }
            }
            catch (Exception e) {
                throw new EslException("Error communicating with esl server. " + e.Message, e);
            }
		}

		public static byte[] PutHttp (string apiToken, string path, byte[] content)
		{
			try {
				WebRequest request = WebRequest.Create (path);
				request.Method = "PUT";
				request.ContentType = "application/json";
				request.ContentLength = content.Length;
				request.Headers.Add ("Authorization", "Basic " + apiToken);

				using (Stream dataStream = request.GetRequestStream ()) {
					dataStream.Write (content, 0, content.Length);
				}

				WebResponse response = request.GetResponse ();

				using (Stream responseStream = response.GetResponseStream()) {
					var memoryStream = new MemoryStream ();
					CopyTo (responseStream, memoryStream);

					byte[] result = memoryStream.ToArray();

					return result;
				}
			}
            catch (WebException e){
                using (var stream = e.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    string errorDetails = reader.ReadToEnd();
                    throw new EslServerException(String.Format("{0} HTTP {1} on URI {2}. Optional details: {3}", e.Message, 
                                                               ((HttpWebResponse)e.Response).Method, e.Response.ResponseUri, errorDetails),
                                                                errorDetails, e);
                }
            }
			catch (Exception e) {
				throw new EslException("Error communicating with esl server. " + e.Message,e);
			}
		}

        /// <summary>
        /// Can only be called for unauthenticated path such as /auth
        /// Gets the http.
        /// </summary>
        public static byte[] GetHttp (string path)
        {
            try {
                WebRequest request = WebRequest.Create (path);
                request.Method = "GET";

                WebResponse response = request.GetResponse ();

                using (Stream responseStream = response.GetResponseStream()) {
                    var memoryStream = new MemoryStream ();
                    CopyTo (responseStream, memoryStream);
                    byte[] result = memoryStream.ToArray();

                    return result;
                }
            }
            catch (WebException e){
                using (var stream = e.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    string errorDetails = reader.ReadToEnd();
                    throw new EslServerException(String.Format("{0} HTTP {1} on URI {2}. Optional details: {3}", e.Message, 
                                                               ((HttpWebResponse)e.Response).Method, e.Response.ResponseUri, errorDetails),
                                                                errorDetails, e);
                }
            }
            catch (Exception e) {
                throw new EslException("Error communicating with esl server. " + e.Message,e);
            }
        }

		public static byte[] GetHttpJson (string apiToken, string path, string acceptType)
		{
			try {
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create (path);
				request.Method = "GET";
				request.Headers.Add ("Authorization", "Basic " + apiToken);
				request.Accept = acceptType;

				WebResponse response = request.GetResponse ();

				using (Stream responseStream = response.GetResponseStream()) {
					var memoryStream = new MemoryStream ();
					CopyTo (responseStream, memoryStream);
					byte[] result = memoryStream.ToArray();

					return result;
				}
			}
            catch (WebException e){
                using (var stream = e.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    string errorDetails = reader.ReadToEnd();
                    throw new EslServerException(String.Format("{0} HTTP {1} on URI {2}. Optional details: {3}", e.Message, 
                                                               ((HttpWebResponse)e.Response).Method, e.Response.ResponseUri, errorDetails),
                                                                errorDetails, e);
                }
            }
			catch (Exception e) {
				throw new EslException("Error communicating with esl server. " + e.Message,e);
			}
		}

		public static byte[] GetHttp (string apiToken, string path)
		{
			try {
				WebRequest request = WebRequest.Create (path);
				request.Method = "GET";
				request.Headers.Add ("Authorization", "Basic " + apiToken);

				WebResponse response = request.GetResponse ();

				using (Stream responseStream = response.GetResponseStream()) {
					var memoryStream = new MemoryStream ();
					CopyTo (responseStream, memoryStream);
					byte[] result = memoryStream.ToArray();

					return result;
				}
			}
            catch (WebException e){
                using (var stream = e.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    string errorDetails = reader.ReadToEnd();
                    throw new EslServerException(String.Format("{0} HTTP {1} on URI {2}. Optional details: {3}", e.Message, 
                                                               ((HttpWebResponse)e.Response).Method, e.Response.ResponseUri, errorDetails),
                                                                errorDetails, e);
                }
            }
			catch (Exception e) {
				throw new EslException("Error communicating with esl server. " + e.Message,e);
			}
		}

		public static byte[] DeleteHttp (string apiToken, string path)
		{
			try {
				WebRequest request = WebRequest.Create (path);
				request.Method = "DELETE";
				request.Headers.Add ("Authorization", "Basic " + apiToken);

				WebResponse response = request.GetResponse ();

				using (Stream responseStream = response.GetResponseStream()) {
					var memoryStream = new MemoryStream ();
					CopyTo (responseStream, memoryStream);
					byte[] result = memoryStream.ToArray();

					return result;
				}
			}
            catch (WebException e){
                using (var stream = e.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    string errorDetails = reader.ReadToEnd();
                    throw new EslServerException(String.Format("{0} HTTP {1} on URI {2}. Optional details: {3}", e.Message, 
                                                               ((HttpWebResponse)e.Response).Method, e.Response.ResponseUri, errorDetails),
                                                                errorDetails, e);
                }
            }
			catch (Exception e) {
				throw new EslException("Error communicating with esl server. " + e.Message,e);
			}
		}

		public static void AddAuthorizationHeader(WebRequest request, AuthHeaderGenerator authHeaderGen)
		{
			request.Headers.Add(authHeaderGen.Name, authHeaderGen.Value);
		}

		public static byte[] MultipartPostHttp (string apiToken, string path, byte[] content, string boundary, AuthHeaderGenerator authHeaderGen)
		{
			WebRequest request = WebRequest.Create (path);
			try {
				request.Method = "POST";
				request.ContentType = string.Format ("multipart/form-data; boundary={0}", boundary);
				request.ContentLength = content.Length;
				AddAuthorizationHeader(request, authHeaderGen);

				using (Stream dataStream = request.GetRequestStream ()) {
					dataStream.Write (content, 0, content.Length);
				}

				WebResponse response = request.GetResponse ();

				using (Stream responseStream = response.GetResponseStream()) {
					var memoryStream = new MemoryStream ();
					CopyTo (responseStream, memoryStream);
					byte[] result = memoryStream.ToArray();

					return result;
				}
			}
            catch (WebException e){
                using (var stream = e.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    string errorDetails = reader.ReadToEnd();
                    throw new EslServerException(String.Format("{0} HTTP {1} on URI {2}. Optional details: {3}", e.Message, 
                                                               ((HttpWebResponse)e.Response).Method, e.Response.ResponseUri, errorDetails),
                                                                errorDetails, e);
                }
            }
			catch (Exception e) {
				throw new EslException("Error communicating with esl server. " + e.Message,e);
			}
		}

		private static void CopyTo (Stream input, Stream output)
		{
			byte[] buffer = new byte[16 * 1024]; // Fairly arbitrary size
			int bytesRead;

			while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0) {
				output.Write (buffer, 0, bytesRead);
			}
		}

	}
}

