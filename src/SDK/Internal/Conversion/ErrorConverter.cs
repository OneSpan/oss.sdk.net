using System;
using OneSpanSign.API;

namespace OneSpanSign.Sdk
{
    internal class ErrorConverter
    {
        private Error apiError;

        public ErrorConverter(Error apiError)
        {
            this.apiError = apiError;
        }

        internal ServerError ToServerError()
        {
            if (apiError == null)
            {
                return null;
            }

            ServerError result = new ServerError();

            result.Code = apiError.Code;
            result.Message = apiError.Message;
            result.MessageKey = apiError.MessageKey;
            result.Name = apiError.Name;
            result.Technical = apiError.Technical;

            return result;
        }
    }
}

