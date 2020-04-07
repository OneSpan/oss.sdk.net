using System;

namespace OneSpanSign.Sdk.Builder
{
    public class BuilderException : Exception
    {
        public BuilderException(string message) : base(message)
        {
            string s = message;
        }
    }
}