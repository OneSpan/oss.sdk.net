using NUnit.Framework;

namespace SDK.Examples
{
    public class AdHocGroupSignerExampleTest
    {
        [Test]
        public void verify() {
            AdHocGroupSignerExample example = new AdHocGroupSignerExample();
            example.Run();
        }
    }
}