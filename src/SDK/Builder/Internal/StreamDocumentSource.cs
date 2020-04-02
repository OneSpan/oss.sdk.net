using System;
using System.IO;
using OneSpanSign.Sdk.Internal;

namespace OneSpanSign.Sdk.Builder.Internal
{
	public class StreamDocumentSource : DocumentSource
	{
		private readonly Stream input;

		public StreamDocumentSource (Stream input)
		{
			this.input = input;
		}

		public byte[] Content()
		{
			return Streams.ToByteArray (input);
		}
	}
}