using System;

namespace OneSpanSign.Sdk
{
    public class DateHelper
    {

		public static string dateToIsoUtcFormat(DateTime date)
        {
			string result = date.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");
			Console.WriteLine(result);
			return result;
        }
    }
}

