namespace OneSpanSign.Sdk.Builder
{
    public class IDVAuthenticationBuilder : AuthenticationBuilder
    {
        private string phoneNumber;
        private IdvWorkflow idvWorkflow;

        public IDVAuthenticationBuilder(IdvWorkflow idvWorkflow)
        {
            this.idvWorkflow = idvWorkflow;
        }

        public IDVAuthenticationBuilder(string phoneNumber, IdvWorkflow idvWorkflow)
        {
            this.phoneNumber = phoneNumber;
            this.idvWorkflow = idvWorkflow;
        }

        public override Authentication Build()
        {
            return new Authentication(AuthenticationMethod.IDV, phoneNumber, idvWorkflow);
        }
    }
}