namespace OneSpanSign.Sdk
{
    public class ChooseSignatureSettingsBuilder
    {

        private ChooseSignatureOptions chooseSignatureOptions;
        
        private ChooseSignatureSettingsBuilder()
        {
        }

        public static ChooseSignatureSettingsBuilder NewChooseSignatureSettings() {
            return new ChooseSignatureSettingsBuilder();
        }

        public ChooseSignatureSettingsBuilder WithCompleteSummaryOptions(ChooseSignatureOptions chooseSignatureOptions) {
            this.chooseSignatureOptions = chooseSignatureOptions;
            return this;
        }
        
        public ChooseSignatureSettings Build() {
            ChooseSignatureSettings result = new ChooseSignatureSettings();
            result.Signature = chooseSignatureOptions;
            
            return result;
        }
    }
}