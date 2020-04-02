using System;
using System.Net;
using System.IO;
using OneSpanSign.API;
using Newtonsoft.Json;

namespace OneSpanSign.Sdk
{
    public class OssServerException:OssException
    {

        public ServerError ServerError
        {
            get; set;
        }
    
        public OssServerException(string message, ServerError serverError, OssServerException cause):base(message, cause)
        {
            this.ServerError = serverError;
        }

        public OssServerException(string message, string errorDetails, WebException cause):base(message, cause)
        {
            Error e = JsonConvert.DeserializeObject<Error>(errorDetails);
            this.ServerError = new ErrorConverter(e).ToServerError();
        }
        
    }
}
