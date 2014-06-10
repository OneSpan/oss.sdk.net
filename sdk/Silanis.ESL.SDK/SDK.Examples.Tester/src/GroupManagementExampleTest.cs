using System;
using NUnit.Framework;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    public class GroupManagementExampleTest
    {
        [Test]
        public void verify() {
            GroupManagementExample example = new GroupManagementExample(Props.GetInstance());
            example.Run();
        }
    }
}