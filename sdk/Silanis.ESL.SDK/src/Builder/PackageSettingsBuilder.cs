using System;
using System.Collections.Generic;
using Silanis.ESL.API;

namespace Silanis.ESL.SDK
{
	public class PackageSettingsBuilder
	{
		private Nullable<bool> enableInPerson = null;
		private Nullable<bool> enableOptOut = null;
		private Nullable<bool> enableDecline = null;
		private Nullable<bool> hideWatermark = null;
		private Nullable<bool> hideCaptureText = null;

		private bool showDownloadButton = true;
		private Nullable<bool> showDialogOnComplete = null;
		private Nullable<int> maxAuthAttempts = null;
		private List<string> optOutReasons = new List<string> ();

		Link handOverLink = null;

		LayoutOptions layoutOptions;

		private PackageSettingsBuilder(){}

		public static PackageSettingsBuilder NewPackageSettings() 
		{
			return new PackageSettingsBuilder ();
		}

		public PackageSettingsBuilder EnableInPerson()
		{
			enableInPerson = true;
			return this;
		}

		public PackageSettingsBuilder EnableOptOut()
		{
			enableOptOut = true;
			return this;
		}

		public PackageSettingsBuilder DisableOptOut() 
		{
			enableOptOut = false;
			return this;
		}

		public PackageSettingsBuilder EnableDecline()
		{
			enableDecline = true;
			return this;
		}

		public PackageSettingsBuilder DisableDecline()
		{
			enableDecline = false;
			return this;
		}

		public PackageSettingsBuilder HideWatermark()
		{
			hideWatermark = true;
			return this;
		}

		public PackageSettingsBuilder HideCaptureText()
		{
			hideCaptureText = true;
			return this;
		}

		public PackageSettingsBuilder WithMaxAuthAttempts(int maxAuthAttempts )
		{
			this.maxAuthAttempts = maxAuthAttempts;
			return this;
		}

		public PackageSettingsBuilder HideDownloadButton()
		{
			this.showDownloadButton = true;
			return this;
		}

		public PackageSettingsBuilder WithOptOutReason( string reason )
		{
			optOutReasons.Add (reason);
			return this;
		}

		private PackageSettingsBuilder WithHandOverLink( Link handOverLink )
		{
			this.handOverLink = handOverLink;
			return this;
		}

		public PackageSettingsBuilder WithHandOverLink( LinkBuilder builder )
		{
			this.handOverLink = builder.build ();
			return this;
		}

		private PackageSettingsBuilder WithLayoutOptions( LayoutOptions layoutOptions )
		{
			this.layoutOptions = layoutOptions;
			return this;
		}

		public PackageSettingsBuilder withLayoutOptions( LayoutOptionsBuilder builder )
		{
			return WithLayoutOptions (builder.build ());
		}

		public PackageSettings build()
		{
			PackageSettings result = new PackageSettings ();
			result.Ceremony = BuildCeremonySettings ();
			return result;
		}

		private CeremonySettings BuildCeremonySettings()
		{
			CeremonySettings result = new CeremonySettings ();

			result.InPerson = enableInPerson;
			result.OptOutButton = enableOptOut;
			result.DeclineButton = enableDecline;
			result.HideWatermark = hideWatermark;
			result.HideCaptureText = hideCaptureText;

			result.HandOver = handOverLink;

			result.OptOutReasons = optOutReasons;

			result.MaxAuthFailsAllowed = maxAuthAttempts;

			DocumentToolbarOptions documentToolbarOptions = new DocumentToolbarOptions ();
			documentToolbarOptions.DownloadButton = showDownloadButton;

			result.DocumentToolbarOptions = documentToolbarOptions;

			CeremonyEvents ceremonyEvents = new CeremonyEvents ();
			CeremonyEventComplete ceremonyEventComplete = new CeremonyEventComplete ();
			ceremonyEventComplete.Dialog = showDialogOnComplete;
			ceremonyEvents.Complete = ceremonyEventComplete;
			result.Events = ceremonyEvents;

			result.Layout = layoutOptions;

			return result;
		}
	}
}

