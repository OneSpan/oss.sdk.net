using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Tests
{
    [TestFixture]
    public class AccountSystemSettingPropertiesBuilderTest
    {
        public AccountSystemSettingPropertiesBuilderTest()
        {
            
        }
        
        [Test]
        public void buildWithSpecifiedValues()
        {

            AccountSystemSettingProperties accountSystemSettingProperties = AccountSystemSettingPropertiesBuilder
                .NewAccountSystemSettingPropertiesBuilder()
                .WithLoginSessionTimeout(20)
                .WithSessionTimeoutWarning(300000)
                .WithSenderLoginMaxFailedAttempts(120000)
                .Build();

 
            Assert.IsTrue(accountSystemSettingProperties.SenderLoginMaxFailedAttempts.Equals(120000));
            Assert.IsTrue(accountSystemSettingProperties.LoginSessionTimeout == 20);
            Assert.IsTrue(accountSystemSettingProperties.SessionTimeoutWarning == 300000);

        }
    }
}