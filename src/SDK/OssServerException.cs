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

        internal int? StatusCode { get; private set; }

        internal string ErrorDetails { get; private set; }

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
        public OssServerException(string message, string errorDetails, Exception cause)
            : this(message, errorDetails, null, null, cause)
        {
        }
#else
        public OssServerException(string message, string errorDetails, WebException cause)
            : this(message, errorDetails, (int?)(cause?.Response as HttpWebResponse)?.StatusCode,
                (cause?.Response as HttpWebResponse)?.StatusDescription, cause)
        {
        }
#endif

        internal OssServerException(string message, string errorDetails, int? statusCode, string reasonPhrase,
            Exception cause) : base(message, cause)
        {
            StatusCode = statusCode;
            ErrorDetails = errorDetails;
            Error error = null;
            try
            {
                error = JsonConvert.DeserializeObject<Error>(errorDetails);
            }
            catch (JsonException)
            {
                // An empty or non-JSON body, e.g. from a gateway, is still an HTTP error rather than a parse failure.
            }
            ServerError = error != null
                ? new ErrorConverter(error).ToServerError()
                : new ServerError { Code = statusCode, Message = reasonPhrase };
        }
    }
}
