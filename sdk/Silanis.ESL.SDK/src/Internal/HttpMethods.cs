using System;
using System.Net;
using System.IO;

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
                
				return memoryStream.ToArray ();
			}
            }
            catch (Exception e) {
                Console.Error.WriteLine(e.Message);
                Console.Error.WriteLine(e.StackTrace);
            }

            return null;
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
				CopyTo (responseStream, memoryStream);

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
				CopyTo (responseStream, memoryStream);
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
				CopyTo (responseStream, memoryStream);
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
				CopyTo (responseStream, memoryStream);

				return memoryStream.ToArray ();
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

