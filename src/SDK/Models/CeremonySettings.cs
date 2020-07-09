//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace OneSpanSign.API
{


    internal class CeremonySettings
    {

        // Fields
        private IList<String> _declineReasons = new List<String>();
        private IList<String> _optOutReasons = new List<String>();

        // Accessors

        [JsonProperty("declineButton")]
        public Nullable<bool> DeclineButton
        {
            get; set;
        }
        
        [JsonProperty("leftMenuExpand")]
        public Nullable<bool> LeftMenuExpand
        {
            get; set;
        }


        [JsonProperty("declineReasons")]
        public IList<String> DeclineReasons
        {
            get
            {
                return _declineReasons;
            }
            set {
                _declineReasons = value;
            }
        }
        public CeremonySettings AddDeclineReason(String value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("Argument cannot be null");
            }

            _declineReasons.Add(value);
            return this;
        }


        [JsonProperty("disableDeclineOther")]
        public Nullable<bool> DisableDeclineOther
        {
            get; set;
        }


        [JsonProperty("disableDownloadForUncompletedPackage")]
        public Nullable<bool> DisableDownloadForUncompletedPackage
        {
            get; set;
        }


        [JsonProperty("disableFirstInPersonAffidavit")]
        public Nullable<bool> DisableFirstInPersonAffidavit
        {
            get; set;
        }


        [JsonProperty("disableInPersonAffidavit")]
        public Nullable<bool> DisableInPersonAffidavit
        {
            get; set;
        }


        [JsonProperty("disableOptOutOther")]
        public Nullable<bool> DisableOptOutOther
        {
            get; set;
        }


        [JsonProperty("disableSecondInPersonAffidavit")]
        public Nullable<bool> DisableSecondInPersonAffidavit
        {
            get; set;
        }


        [JsonProperty("ada")]
        public Nullable<bool> Ada
        {
            get; set;
        }


        [JsonProperty ("fontSize")]
        public Nullable<Int32> FontSize {
            get; set;
        }

        [JsonProperty ("defaultTimeBasedExpiry")]
        public Nullable<bool> DefaultTimeBasedExpiry {
            get; set;
        }

        [JsonProperty ("remainingDays")]
        public Nullable<Int32> RemainingDays {
            get; set;
        }

        [JsonProperty ("enforceCaptureSignature")]
        public Nullable<bool> EnforceCaptureSignature {
            get; set;
        }


        [JsonProperty("documentToolbarOptions")]
        public DocumentToolbarOptions DocumentToolbarOptions
        {
            get; set;
        }


        [JsonProperty("events")]
        public CeremonyEvents Events
        {
            get; set;
        }


        [JsonProperty("handOver")]
        public Link HandOver
        {
            get; set;
        }


        [JsonProperty("hideCaptureText")]
        public Nullable<bool> HideCaptureText
        {
            get; set;
        }


        [JsonProperty("hideLanguageDropdown")]
        public Nullable<bool> HideLanguageDropdown
        {
            get; set;
        }


        [JsonProperty("hidePackageOwnerInPerson")]
        public Nullable<bool> HidePackageOwnerInPerson
        {
            get; set;
        }


        [JsonProperty("hideWatermark")]
        public Nullable<bool> HideWatermark
        {
            get; set;
        }


        [JsonProperty("inPerson")]
        public Nullable<bool> InPerson
        {
            get; set;
        }


        [JsonProperty("layout")]
        public LayoutOptions Layout
        {
            get; set;
        }


        [JsonProperty("maxAuthFailsAllowed")]
        public Nullable<int> MaxAuthFailsAllowed
        {
            get; set;
        }


        [JsonProperty("optOutButton")]
        public Nullable<bool> OptOutButton
        {
            get; set;
        }


        [JsonProperty("optOutReasons")]
        public IList<String> OptOutReasons
        {
            get
            {
                return _optOutReasons;
            }
            set {
                _optOutReasons = value;
            }
        }
        public CeremonySettings AddOptOutReason(String value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("Argument cannot be null");
            }

            _optOutReasons.Add(value);
            return this;
        }


        [JsonProperty("style")]
        public LayoutStyle Style
        {
            get; set;
        }



    }
}