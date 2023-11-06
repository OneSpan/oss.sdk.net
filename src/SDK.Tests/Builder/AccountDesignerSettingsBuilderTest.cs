using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Tests
{
    [TestFixture]
    public class AccountDesignerSettingsBuilderTest
    {
        public AccountDesignerSettingsBuilderTest()
        {
        }
        
        [Test]
        public void buildWithSpecifiedValues()
        {
            AccountDesignerSettings accountDesignerSettings = AccountDesignerSettingsBuilder.NewAccountDesignerSettings()
                .WithSend()
                .WithDone()
                .WithoutSettings()
                .WithoutDocumentVisibility()
                .WithAddDocument()
                .WithEditDocument()
                .WithoutDeleteDocument()
                .WithoutAddSigner()
                .WithEditRecipient()
                .WithRolePickerSender()
                .WithoutSaveLayout()
                .WithoutApplyLayout()
                .WithShowSharedLayouts()
                .WithDefaultSignatureType("capture")
                .Build();
            
            Assert.IsTrue(accountDesignerSettings.Send);
            Assert.IsTrue(accountDesignerSettings.Done);
            Assert.IsFalse(accountDesignerSettings.Settings);
            Assert.IsFalse(accountDesignerSettings.DocumentVisibility);
            Assert.IsTrue(accountDesignerSettings.AddDocument);
            Assert.IsTrue(accountDesignerSettings.EditDocument);
            Assert.IsFalse(accountDesignerSettings.DeleteDocument);
            Assert.IsFalse(accountDesignerSettings.AddSigner);
            Assert.IsTrue(accountDesignerSettings.EditRecipient);
            Assert.IsTrue(accountDesignerSettings.RolePickerSender);
            Assert.IsFalse(accountDesignerSettings.SaveLayout);
            Assert.IsFalse(accountDesignerSettings.ApplyLayout);
            Assert.IsTrue(accountDesignerSettings.ShowSharedLayouts);
            Assert.AreEqual("capture",accountDesignerSettings.DefaultSignatureType);
        }
        
    }
}