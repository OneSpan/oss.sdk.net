using System;
using NUnit.Framework;
using OneSpanSign.Sdk;
using System.Collections.Generic;

namespace SDK.Examples
{
    public class GroupManagementExampleTest
    {
        [Test]
        public void verify() {
            GroupManagementExample example = new GroupManagementExample();
            example.Run();

            Assert.AreEqual(example.createdGroup1.Id.Id, example.retrievedGroup1.Id.Id);
            Assert.AreEqual(example.createdGroup2.Id.Id, example.retrievedGroup2.Id.Id);
            Assert.AreEqual(example.createdGroup3.Id.Id, example.retrievedGroup3.Id.Id);
            Assert.AreEqual (example.createdGroup1.Name, example.retrievedGroupByName1[0].Name);
            Assert.That (example.retrievedByNamePrefix[1].Name.StartsWith(GroupManagementExample.GROUP_NAME_PREFIX));

            Assert.Contains(example.email2, example.groupMemberEmailsAfterUpdate);
            Assert.Contains(example.email3, example.groupMemberEmailsAfterUpdate);
            Assert.Contains(example.email4, example.groupMemberEmailsAfterUpdate);

            Assert.AreEqual(example.createdGroup3Updated.Name, example.updatedGroupName3, "Group Name was not updated");
            Assert.AreEqual(example.createdGroup3Updated.Email, example.UPDATED_EMAIL, "Group Email was not updated");
        }

        private List<string> GetGroupsId(List<Group> groups)
        {
            List<string> groupsId = new List<string>();
            foreach(Group group in groups)
            {
                groupsId.Add(group.Id.Id);
            }
            return groupsId;
        }
    }
}