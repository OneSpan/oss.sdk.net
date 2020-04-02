using NUnit.Framework;
using System;

namespace SDK.Examples
{
    [TestFixture ()]
    public class SigningStyleExampleTest
    {
        [Test ()]
        public void VerifyResult ()
        {
            object actualTheme;
            
            SigningStyleExample example = new SigningStyleExample();
            example.Run ();

            Assert.IsNotNull (example.createdSigningThemes);
            Assert.IsNotNull (example.createdSigningThemes.ContainsKey ("default"));
            example.createdSigningThemes.TryGetValue ("default", out actualTheme);
            StringAssert.Contains ("\"primary\": \"#5940C3\"", actualTheme.ToString());

            Assert.IsNotNull (example.retrievedSigningThemes);
            Assert.IsNotNull (example.retrievedSigningThemes.ContainsKey ("default"));
            example.retrievedSigningThemes.TryGetValue ("default", out actualTheme);
            StringAssert.Contains ("\"primary\": \"#5940C3\"", actualTheme.ToString ());

            Assert.IsNotNull (example.updatedSigningThemes);
            Assert.IsNotNull (example.updatedSigningThemes.ContainsKey ("default"));
            example.updatedSigningThemes.TryGetValue ("default", out actualTheme);
            StringAssert.Contains ("\"secondary\": \"#F31C8B\"", actualTheme.ToString ());

            CollectionAssert.IsEmpty (example.removedSigningThemes); 
        }
    }
}