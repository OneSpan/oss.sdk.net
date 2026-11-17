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
                .WithSigningDateDefaultFormat("MMMM d, yyyy")
                .WithSigningDateAllowSenderChange(true)
                .WithSigningDateAllowCustomPattern(false)
                .Build();


            Assert.IsTrue(accountSystemSettingProperties.SenderLoginMaxFailedAttempts.Equals(120000));
            Assert.IsTrue(accountSystemSettingProperties.LoginSessionTimeout == 20);
            Assert.IsTrue(accountSystemSettingProperties.SessionTimeoutWarning == 300000);
            Assert.AreEqual("MMMM d, yyyy", accountSystemSettingProperties.SigningDateDefaultFormat);
            Assert.IsTrue(accountSystemSettingProperties.SigningDateAllowSenderChange == true);
            Assert.IsTrue(accountSystemSettingProperties.SigningDateAllowCustomPattern == false);
        }

        [Test]
        public void buildWithoutSigningDateValuesLeavesThemNull()
        {
            AccountSystemSettingProperties accountSystemSettingProperties = AccountSystemSettingPropertiesBuilder
                .NewAccountSystemSettingPropertiesBuilder()
                .WithLoginSessionTimeout(20)
                .Build();

            Assert.IsNull(accountSystemSettingProperties.SigningDateDefaultFormat);
            Assert.IsNull(accountSystemSettingProperties.SigningDateAllowSenderChange);
            Assert.IsNull(accountSystemSettingProperties.SigningDateAllowCustomPattern);

        }
    }
}