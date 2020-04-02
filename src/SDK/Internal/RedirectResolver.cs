using System;
using System.Net;
using System.Web;

namespace OneSpanSign.Sdk
{
    public class RedirectResolver
    {
        public static string ResolveUrlAfterRedirect(string url)
        {
            try 
            {
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create (url);

                request.AllowAutoRedirect = false;
                HttpWebResponse response = (HttpWebResponse) request.GetResponse();
                string locationHeader = response.Headers["Location"];

                return String.IsNullOrEmpty(locationHeader) ? url : locationHeader;
            } 
            catch (WebException) 
            {
                return url;
            } 
            catch (Exception) 
            {
                return url;
            }

        }
    }
}

