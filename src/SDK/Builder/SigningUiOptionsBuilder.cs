namespace OneSpanSign.Sdk
{
    public class SigningUiOptionsBuilder
    {

        private CompleteSummaryOptions completeSummaryOptions;
        private InpersonHostThankYouOptions inpersonHostThankYouOptions;
        private InpersonWelcomeOptions inpersonWelcomeOptions;
        private NotaryHostThankYouOptions notaryHostThankYouOptions;
        private NotaryWelcomeOptions notaryWelcomeOptions;
        private OverviewOptions overviewOptions;
        
        private SigningUiOptionsBuilder()
        {
        }

        public static SigningUiOptionsBuilder NewSigningUiOptions() {
            return new SigningUiOptionsBuilder();
        }

        public SigningUiOptionsBuilder WithCompleteSummaryOptions(CompleteSummaryOptions completeSummaryOptions) {
            this.completeSummaryOptions = completeSummaryOptions;
            return this;
        }

        public SigningUiOptionsBuilder WithInpersonHostThankYouOptions(InpersonHostThankYouOptions inpersonHostThankYouOptions) {
            this.inpersonHostThankYouOptions = inpersonHostThankYouOptions;
            return this;
        }
        
        public SigningUiOptionsBuilder WithInpersonWelcomeOptions(InpersonWelcomeOptions inpersonWelcomeOptions) {
            this.inpersonWelcomeOptions = inpersonWelcomeOptions;
            return this;
        }
        
        public SigningUiOptionsBuilder WithNotaryHostThankYouOptions(NotaryHostThankYouOptions notaryHostThankYouOptions) {
            this.notaryHostThankYouOptions = notaryHostThankYouOptions;
            return this;
        }
        
        public SigningUiOptionsBuilder WithNotaryWelcomeOptions(NotaryWelcomeOptions notaryWelcomeOptions) {
            this.notaryWelcomeOptions = notaryWelcomeOptions;
            return this;
        }
        
        public SigningUiOptionsBuilder WithOverviewOptions(OverviewOptions overviewOptions) {
            this.overviewOptions = overviewOptions;
            return this;
        }

        public SigningUiOptions Build() {
            SigningUiOptions result = new SigningUiOptions();
            result.CompleteSummaryOptions = completeSummaryOptions;
            result.InpersonHostThankYouOptions = inpersonHostThankYouOptions;
            result.InpersonWelcomeOptions = inpersonWelcomeOptions;
            result.NotaryHostThankYouOptions = notaryHostThankYouOptions;
            result.NotaryWelcomeOptions = notaryWelcomeOptions;
            result.OverviewOptions = overviewOptions;

            return result;
        }
    }
}