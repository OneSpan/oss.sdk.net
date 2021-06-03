using System;
using NUnit.Framework;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Tests
{
    [TestFixture]
    public class VirtualRoomBuilderTest
    {
        [Test]
        public void BuildTest()
        {
            DateTime startDateTime = DateTime.Now;
            VirtualRoomBuilder builder = VirtualRoomBuilder.NewVirtualRoom()
                    .WithVideo(true)
                    .WithVideoRecording(true)
                    .WithStartDateTime(startDateTime)
                    .WithHostUid("hostUid");

            VirtualRoom result = builder.Build();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Video, true);
            Assert.AreEqual(result.VideoRecording, true);
            Assert.AreEqual(result.StartDatetime, startDateTime);
            Assert.AreEqual(result.HostUid, "hostUid");
        }
    }
}
