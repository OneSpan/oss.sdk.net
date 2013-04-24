using System;

namespace Silanis.ESL.SDK
{
	public class Converter
	{
		public static byte[] ToBytes (string str)
		{
			System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding ();
			return encoding.GetBytes (str);
		}
		
		public static string ToString (byte[] bytes)
		{
			System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding ();
			return enc.GetString (bytes);
		}
	}
}

