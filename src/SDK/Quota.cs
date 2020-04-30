using System;

namespace OneSpanSign.Sdk
{
	
	
	public class Quota
	{
		public string Cycle { get; set; }
		public Nullable<Int32> Limit { get; set; }
		public string Scope { get; set; }
		public string Target { get; set; }
	}
}