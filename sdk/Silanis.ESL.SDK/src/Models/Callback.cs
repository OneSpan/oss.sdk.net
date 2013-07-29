//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Silanis.ESL.API
{
	internal class Callback
	{
		
		// Fields
		private IList<CallbackEvent> _events = new List<CallbackEvent> ();
		// Accessors
		[JsonProperty("events")]
		public IList<CallbackEvent> Events {
			get {
				return _events;
			}
		}

		public Callback AddEvent (CallbackEvent value)
		{
			_events.Add (value);
			return this;
		}

		[JsonProperty("url")]
		public String Url {
			get;
			set;
		}
	}
}