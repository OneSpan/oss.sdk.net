using System;
#if NET5_0_OR_GREATER
using System.Net.Http;
#else
using System.Net;
#endif

namespace OneSpanSign.Sdk
{
    public class RedirectResolver
    {
        public static string ResolveUrlAfterRedirect(string url)
        {
#if NET5_0_OR_GREATER
            try
            {
                var handler = new HttpClientHandler { AllowAutoRedirect = false };
                using var client = new HttpClient(handler);
                using var request = new HttpRequestMessage(HttpMethod.Get, url);
                using var response = client.Send(request);
                string location = response.Headers.Location?.ToString();
                return string.IsNullOrEmpty(location) ? url : location;
            }
            catch (Exception)
            {
                return url;
            }
#else
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AllowAutoRedirect = false;
                using (WebResponse response = request.GetResponse())
                {
                    string location = response.Headers["Location"];
                    return string.IsNullOrEmpty(location) ? url : location;
                }
            }
            catch (WebException)
            {
                return url;
            }
            catch (Exception)
            {
                return url;
            }
#endif
        }
    }
}
