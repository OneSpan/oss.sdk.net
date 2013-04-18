using System;
using System.Net;
using System.IO;

namespace Silanis.ESL.SDK
{
	public class HttpMethods
	{

		public static byte[] PostHttp (string apiToken, string path, byte[] content)
		{
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
				responseStream.CopyTo (memoryStream);
				return memoryStream.ToArray ();
			}
		}

		public static byte[] PutHttp (string apiToken, string path, byte[] content)
		{
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
				responseStream.CopyTo (memoryStream);
				return memoryStream.ToArray ();
			}
		}

		public static byte[] GetHttp (string apiToken, string path)
		{
			WebRequest request = WebRequest.Create (path);
			request.Method = "GET";
			request.Headers.Add ("Authorization", "Basic " + apiToken);

			WebResponse response = request.GetResponse ();

			using (Stream responseStream = response.GetResponseStream()) {
				var memoryStream = new MemoryStream ();
				responseStream.CopyTo (memoryStream);
				return memoryStream.ToArray ();
			}
		}

		public static byte[] DeleteHttp (string apiToken, string path)
		{
			WebRequest request = WebRequest.Create (path);
			request.Method = "DELETE";
			request.Headers.Add ("Authorization", "Basic " + apiToken);

			WebResponse response = request.GetResponse ();

			using (Stream responseStream = response.GetResponseStream()) {
				var memoryStream = new MemoryStream ();
				responseStream.CopyTo (memoryStream);
				return memoryStream.ToArray ();
			}
		}

		public static byte[] MultipartPostHttp (string apiToken, string path, byte[] content, string boundary)
		{
			WebRequest request = WebRequest.Create (path);
			request.Method = "POST";
			request.ContentType = string.Format ("multipart/form-data; boundary={0}", boundary);
			request.ContentLength = content.Length;
			request.Headers.Add ("Authorization", "Basic " + apiToken);

			using (Stream dataStream = request.GetRequestStream ()) {
				dataStream.Write (content, 0, content.Length);
			}

			WebResponse response = request.GetResponse ();

			using (Stream responseStream = response.GetResponseStream()) {
				var memoryStream = new MemoryStream ();
				responseStream.CopyTo (memoryStream);
				return memoryStream.ToArray ();
			}
		}

	}
}

