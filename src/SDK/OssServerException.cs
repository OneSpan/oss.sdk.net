using System;
using OneSpanSign.API;
using Newtonsoft.Json;
#if !NET5_0_OR_GREATER
using System.Net;
#endif

namespace OneSpanSign.Sdk
{
    public class OssServerException : OssException
    {
        public ServerError ServerError { get; set; }

        public OssServerException(string message, OssServerException cause) : base(message, cause)
        {
            this.ServerError = cause.ServerError;
        }

        public OssServerException(string message, ServerError serverError, OssServerException cause) : base(message, cause)
        {
            this.ServerError = serverError;
        }

        // net5+: accepts any Exception (HttpClient throws HttpRequestException, not WebException)
        // netstandard2.0: accepts WebException (thrown by HttpWebRequest.GetResponse())
#if NET5_0_OR_GREATER
        public OssServerException(string message, string errorDetails, Exception cause) : base(message, cause)
#else
        public OssServerException(string message, string errorDetails, WebException cause) : base(message, cause)
#endif
        {
            Error e = JsonConvert.DeserializeObject<Error>(errorDetails);
            this.ServerError = new ErrorConverter(e).ToServerError();
        }
    }
}
