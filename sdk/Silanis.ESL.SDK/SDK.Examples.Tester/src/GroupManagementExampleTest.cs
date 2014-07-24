using System;
using NUnit.Framework;
using Silanis.ESL.SDK;
using System.Collections.Generic;

namespace SDK.Examples
{
    public class GroupManagementExampleTest
    {
        [Test]
        public void verify() {
            GroupManagementExample example = new GroupManagementExample(Props.GetInstance());
            example.Run();

            Assert.Contains(example.createdGroup1.Id.Id, GetGroupsId(example.allGroupsBeforeDelete));
            Assert.Contains(example.createdGroup2.Id.Id, GetGroupsId(example.allGroupsBeforeDelete));
            Assert.Contains(example.createdGroup3.Id.Id, GetGroupsId(example.allGroupsBeforeDelete));

            Assert.Contains(example.createdGroup1.Id.Id, GetGroupsId(example.allGroupsAfterDelete));
            Assert.IsFalse(GetGroupsId(example.allGroupsAfterDelete).Contains(example.createdGroup2.Id.Id));
            Assert.Contains(example.createdGroup3.Id.Id, GetGroupsId(example.allGroupsAfterDelete));

            Assert.Contains(example.email2, example.groupMemberEmailsAfterUpdate);
            Assert.Contains(example.email3, example.groupMemberEmailsAfterUpdate);
            Assert.Contains(example.email4, example.groupMemberEmailsAfterUpdate);
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