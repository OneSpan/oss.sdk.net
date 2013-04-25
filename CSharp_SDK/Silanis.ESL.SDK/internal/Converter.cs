using System;

namespace Silanis.ESL.SDK
{
	/// <summary>
	/// A helper class to convert from string to byte array and vice verse.
	/// </summary>
	public class Converter
	{
		/// <summary>
		/// Convert string to byte array.
		/// </summary>
		/// <returns>The byte array.</returns>
		/// <param name="str">String.</param>
		public static byte[] ToBytes (string str)
		{
			System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding ();
			return encoding.GetBytes (str);
		}

		/// <summary>
		/// Convert byte array to string.
		/// </summary>
		/// <returns>The string.</returns>
		/// <param name="bytes">Byte array.</param>
		public static string ToString (byte[] bytes)
		{
			System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding ();
			return enc.GetString (bytes);
		}
	}
}

