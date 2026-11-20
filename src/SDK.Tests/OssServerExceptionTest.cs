using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Tests
{
    [TestFixture]
    public class OssServerExceptionTest
    {
        [Test]
        public void AClassicErrorBodyIsParsed()
        {
            ServerError error = new OssServerException("403 Forbidden",
                "{\"code\":403,\"message\":\"Not allowed\",\"messageKey\":\"error.forbidden\",\"technical\":\"details\"}",
                403, "Forbidden", null).ServerError;

            Assert.AreEqual(403, error.Code);
            Assert.AreEqual("Not allowed", error.Message);
            Assert.AreEqual("error.forbidden", error.MessageKey);
            Assert.AreEqual("details", error.Technical);
        }

        [Test]
        public void AnEmptyBodyCarriesTheStatusAndReasonPhrase()
        {
            ServerError error = new OssServerException("401 Unauthorized", "", 401, "Unauthorized", null).ServerError;

            Assert.AreEqual(401, error.Code);
            Assert.AreEqual("Unauthorized", error.Message);
        }

        [Test]
        public void ANonJsonBodyCarriesTheStatusAndReasonPhrase()
        {
            ServerError error = new OssServerException("502 Bad Gateway", "<html><body>Bad gateway</body></html>",
                502, "Bad Gateway", null).ServerError;

            Assert.AreEqual(502, error.Code);
            Assert.AreEqual("Bad Gateway", error.Message);
        }
    }
}
