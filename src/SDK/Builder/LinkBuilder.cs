using System;
using System.Collections.Generic;
using OneSpanSign.API;

namespace OneSpanSign.Sdk
{
	public class LinkBuilder
	{
		private string href;
		private string text;
		private string title;
		private Nullable<bool> autoRedirect;
		private HashSet<string> parameters;

		private LinkBuilder ( string href )
		{
			href = href;
			text = null;
			title = null;
			autoRedirect = null;
			parameters = new HashSet<string>(new string[] {
				PARAMETETS.PACKAGE.ToString(),PARAMETETS.SIGNER.ToString(),PARAMETETS.STATUS.ToString()
			});
		}

		public static LinkBuilder newLink( String href )
		{
			return new LinkBuilder (href);
		}

		public LinkBuilder withText( string text )
		{
			this.text = text;
			return this;
		}

		public LinkBuilder withTitle( string title )
		{
			this.title = title;
			return this;
		}
		
		

		internal Link build()
		{
			Link link = new Link ();
			link.Href = href;
			link.Text = (text == null ? href : text );
			link.Title = (title == null ? href : title);
			link.AutoRedirect = (autoRedirect == null ? false : autoRedirect);
			link.Parameters = parameters;
			return link;
		}

	}
}

