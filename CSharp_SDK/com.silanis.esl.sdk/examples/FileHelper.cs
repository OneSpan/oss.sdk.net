using System;
using System.IO;

namespace CSharp_SDK
{
	public class FileHelper
	{
		public static void WriteToFile (byte[] content, string pathToZip)
		{
			FileStream fileStream = new FileStream (pathToZip, FileMode.Create, FileAccess.ReadWrite);
			fileStream.Write (content, 0, content.Length);
			fileStream.Close ();
		}
	}
}

