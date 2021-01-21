namespace OneSpanSign.Sdk
{
    public class SigningUiOptions
    {
        private CompleteSummaryOptions completeSummaryOptions;
        private InpersonWelcomeOptions inpersonWelcomeOptions;
        private InpersonHostThankYouOptions inpersonHostThankYouOptions;
        private NotaryWelcomeOptions notaryWelcomeOptions;
        private NotaryHostThankYouOptions notaryHostThankYouOptions;
        private OverviewOptions overviewOptions;

        public CompleteSummaryOptions CompleteSummaryOptions
        {
            get
            {
                return completeSummaryOptions;
            }
            set
            {
                completeSummaryOptions = value;
            }
        }
        
        public InpersonWelcomeOptions InpersonWelcomeOptions
        {
            get
            {
                return inpersonWelcomeOptions;
            }
            set
            {
                inpersonWelcomeOptions = value;
            }
        }
        
        public InpersonHostThankYouOptions InpersonHostThankYouOptions
        {
            get
            {
                return inpersonHostThankYouOptions;
            }
            set
            {
                inpersonHostThankYouOptions = value;
            }
        }
        
        public NotaryWelcomeOptions NotaryWelcomeOptions
        {
            get
            {
                return notaryWelcomeOptions;
            }
            set
            {
                notaryWelcomeOptions = value;
            }
        }
        
        public NotaryHostThankYouOptions NotaryHostThankYouOptions
        {
            get
            {
                return notaryHostThankYouOptions;
            }
            set
            {
                notaryHostThankYouOptions = value;
            }
        }
        
        public OverviewOptions OverviewOptions
        {
            get
            {
                return overviewOptions;
            }
            set
            {
                overviewOptions = value;
            }
        }
    }
}