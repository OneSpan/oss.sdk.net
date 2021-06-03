using System;
using NUnit.Framework;
namespace SDK.Examples
{
    [TestFixture()]
    public class VirtualRoomExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            VirtualRoomExample example = new VirtualRoomExample();
            example.Run();

            // Verify if the VirtualRoom was updated correctly.
            Assert.IsNotNull(example.VirtualRoomAfterUpdate);
            Assert.AreEqual(example.VirtualRoomAfterUpdate.Video, true);
            Assert.AreEqual(example.VirtualRoomAfterUpdate.VideoRecording, true);
            Assert.AreEqual(example.VirtualRoomAfterUpdate.StartDatetime.ToShortTimeString(), example.StartDateTime.ToShortTimeString());
            Assert.AreEqual(example.VirtualRoomAfterUpdate.StartDatetime.ToShortDateString(), example.StartDateTime.ToShortDateString());
            Assert.AreEqual(example.VirtualRoomAfterUpdate.HostUid, example.HostUid);
        }
    }
}
