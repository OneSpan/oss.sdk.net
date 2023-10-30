using NUnit.Framework;

namespace SDK.Examples
{
    
    [TestFixture ()]
    public class AccountDesignerSettingsExampleTest
    {
        [Test ()]
        public void VerifyResult ()
        {
            AccountDesignerSettingsExample example = new AccountDesignerSettingsExample();
            example.Run ();
            
            Assert.IsNotNull(example.defaultAccountDesignerSettings.Send);
            Assert.IsNotNull(example.defaultAccountDesignerSettings.Done);
            Assert.IsNotNull(example.defaultAccountDesignerSettings.Settings);
            Assert.IsNotNull(example.defaultAccountDesignerSettings.ApplyLayout);
            Assert.IsNotNull(example.defaultAccountDesignerSettings.AddSigner);
            Assert.IsNotNull(example.defaultAccountDesignerSettings.DocumentVisibility);
            
            Assert.IsTrue(example.patchedAccountDesignerSettings.Send);
            Assert.IsTrue(example.patchedAccountDesignerSettings.Done);
            Assert.IsFalse(example.patchedAccountDesignerSettings.Settings);
            Assert.IsFalse(example.patchedAccountDesignerSettings.DocumentVisibility);
            Assert.IsFalse(example.patchedAccountDesignerSettings.ApplyLayout);
            Assert.IsFalse(example.patchedAccountDesignerSettings.AddSigner);
            Assert.IsTrue(example.patchedAccountDesignerSettings.AddDocument);
            Assert.IsTrue(example.patchedAccountDesignerSettings.DefaultSignatureType.Equals("capture"));
        }
    }
}