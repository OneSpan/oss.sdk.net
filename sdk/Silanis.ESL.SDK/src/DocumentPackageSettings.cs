using System;
using Silanis.ESL.API;
using System.Collections.Generic;

namespace Silanis.ESL.SDK
{
	public class DocumentPackageSettings
	{
		private Nullable<bool> enableInPerson = null;

		public Nullable<bool> EnableInPerson {
			get {
				return enableInPerson;
			}
			set {
				enableInPerson = value;
			}
		}

		private Nullable<bool> enableOptOut = null;

		public Nullable<bool> EnableOptOut {
			get {
				return enableOptOut;
			}
			set {
				enableOptOut = value;
			}
		}

		private Nullable<bool> enableDecline = null;

		public Nullable<bool> EnableDecline {
			get {
				return enableDecline;
			}
			set {
				enableDecline = value;
			}
		}

		private Nullable<bool> hideWatermark = null;

		public Nullable<bool> HideWatermark {
			get {
				return hideWatermark;
			}
			set {
				hideWatermark = value;
			}
		}

		private Nullable<bool> hideCaptureText = null;

		public Nullable<bool> HideCaptureText {
			get {
				return hideCaptureText;
			}
			set {
				hideCaptureText = value;
			}
		}

		private List<string> optOutReasons = new List<string>();

		public List<string> OptOutReasons {
			get {
				return optOutReasons;
			}
			set {
				optOutReasons = value;
			}
		}

		private Nullable<int> maxAuthAttempts = null;

		public Nullable<int> MaxAuthAttempts {
			get {
				return maxAuthAttempts;
			}
			set {
				maxAuthAttempts = value;
			}
		}

		private Nullable<bool> showDownloadButton = true;

		public Nullable<bool> ShowDownloadButton {
			get {
				return showDownloadButton;
			}
			set {
				showDownloadButton = value;
			}
		}

		private Nullable<bool> showDialogOnComplete = null;

		public Nullable<bool> ShowDialogOnComplete {
			get {
				return showDialogOnComplete;
			}
			set {
				showDialogOnComplete = value;
			}
		}

		private string linkText;

		public string LinkText {
			get {
				return linkText;
			}
			set {
				linkText = value;
			}
		}

		private string linkTooltip;

		public string LinkTooltip {
			get {
				return linkTooltip;
			}
			set {
				linkTooltip = value;
			}
		}

		private string linkHref;

		public string LinkHref {
			get {
				return linkHref;
			}
			set {
				linkHref = value;
			}
		}

		private CeremonyLayoutSettings ceremonyLayoutSettings = null;

		public CeremonyLayoutSettings CeremonyLayoutSettings {
			get {
				return ceremonyLayoutSettings;
			}
			set {
				ceremonyLayoutSettings = value;
			}
		}

		internal PackageSettings toAPIPackageSettings() {

			CeremonySettings ceremonySettings = new CeremonySettings();

			if ( enableInPerson != null )
				ceremonySettings.InPerson = enableInPerson.Value;

			if ( enableOptOut != null )
				ceremonySettings.OptOutButton = enableOptOut.Value;	

			if ( enableDecline != null )
			    ceremonySettings.DeclineButton = enableDecline.Value;

            if ( hideWatermark != null )
			    ceremonySettings.HideWatermark = hideWatermark.Value;

            if ( hideCaptureText != null )
			    ceremonySettings.HideCaptureText = hideCaptureText.Value;

			foreach ( string reason in optOutReasons )
				ceremonySettings.OptOutReasons.Add( reason );

            if ( maxAuthAttempts != null )
			    ceremonySettings.MaxAuthFailsAllowed = maxAuthAttempts.Value;

			if ( linkHref != null ) {
				Link link = new Link();
				link.Href = linkHref;
				link.Text = ( linkText == null ? linkHref : linkText );
				link.Title = ( linkTooltip == null ? linkHref : linkTooltip );
				ceremonySettings.HandOver = link;
			}

			if ( showDialogOnComplete != null ) {
				CeremonyEvents ceremonyEvents = new CeremonyEvents();
				CeremonyEventComplete ceremonyEventComplete = new CeremonyEventComplete();
				if ( showDialogOnComplete != null )
					ceremonyEventComplete.Dialog = showDialogOnComplete.Value;

				ceremonyEvents.Complete = ceremonyEventComplete;
			}

			if ( showDownloadButton != null ) {
				DocumentToolbarOptions documentToolbarOptions = new DocumentToolbarOptions();
			    if ( showDownloadButton != null ) 
					documentToolbarOptions.DownloadButton = showDownloadButton.Value;
				ceremonySettings.DocumentToolbarOptions = documentToolbarOptions;
			}

			if ( ceremonyLayoutSettings != null ) {
				ceremonySettings.Layout = ceremonyLayoutSettings.toAPILayoutOptions();
			}

			PackageSettings result = new PackageSettings();
			result.Ceremony = ceremonySettings;

			return result;
		}
	}
}

