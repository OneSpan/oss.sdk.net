using NUnit.Framework;
using System;
using System.Collections.Generic;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    [TestFixture()]
    public class FieldOverlapsExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            FieldOverlapsExample example = new FieldOverlapsExample();
            example.Run();

            FieldOverlapValidationResult result = example.FieldOverlaps;

            // The overlapping pair is reported once, anchored on whichever checkbox comes first.
            Assert.AreEqual(1, result.Overlaps.Count);
            FieldOverlap overlap = result.Overlaps[0];
            Assert.AreEqual(FieldOverlapsExample.DOCUMENT_ID, overlap.Field.DocumentId);
            Assert.AreEqual(FieldOverlapsExample.CHECKBOX_PAGE, overlap.Field.Page);
            Assert.AreEqual(1, overlap.ConflictsWith.Count);

            ConflictingField conflict = overlap.ConflictsWith[0];
            Assert.AreEqual(OverlapType.FIELD_VS_FIELD, conflict.OverlapType);

            // The separate checkbox is not reported.
            HashSet<string> reportedIds = new HashSet<string> { overlap.Field.Id, conflict.Field.Id };
            Assert.IsTrue(reportedIds.SetEquals(new[] { FieldOverlapsExample.FIRST_CHECKBOX_ID, FieldOverlapsExample.SECOND_CHECKBOX_ID }));
        }
    }
}
