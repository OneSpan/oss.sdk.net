using System;

namespace Silanis.ESL.SDK
{
    public sealed class SenderInfoBuilder
    {
        private string firstName;
        private string lastName;
        private string company;
        private string title;

        private SenderInfoBuilder() {
        }

        public static SenderInfoBuilder NewSenderInfo() {
            SenderInfoBuilder result = new SenderInfoBuilder();
            return result;
        }

        internal SenderInfoBuilder( Silanis.ESL.API.Sender sender ) {
            firstName = sender.FirstName;
            lastName = sender.LastName;
            company = sender.Company;
            title = sender.Title;
        }

        public SenderInfoBuilder WithName( string firstName, string lastName ) {
            this.firstName = firstName;
            this.lastName = lastName;
            return this;
        }

        public SenderInfoBuilder WithCompany( string company ) {
            this.company = company;
            return this;
        }

        public SenderInfoBuilder WithTitle( string title ) {
            this.title = title;
            return this;
        }

        public SenderInfo Build() {
            SenderInfo result = new SenderInfo();

            result.FirstName = firstName;
            result.LastName = lastName;
            result.Company = company;
            result.Title = title;

            return result;
        }
    }
}

