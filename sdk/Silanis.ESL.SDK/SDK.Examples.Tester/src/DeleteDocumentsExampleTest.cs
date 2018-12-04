using System.Linq;
using NUnit.Framework;

namespace SDK.Examples
{
    public class DeleteDocumentsExampleTest
    {
        [Test]
        public void verify ()
        {
            DeleteDocumentsExample example = new DeleteDocumentsExample ();
            example.Run ();

            // Verify if the document was uploaded correctly.
            Assert.IsNotNull (example.RetrievedPackage.Documents.First (x => x.Name.Equals (DeleteDocumentsExample.DOCUMENT1_NAME)));
            Assert.IsNotNull (example.RetrievedPackage.Documents.First (x => x.Name.Equals (DeleteDocumentsExample.DOCUMENT2_NAME)));
            Assert.AreEqual (example.RetrievedPackage.Documents.Count, 3);

            // Verify if the document was deleted correctly.
            Assert.IsNull (example.RetrievedPackageWithDeletedDocuments.Documents.FirstOrDefault (x => x.Name.Equals (DeleteDocumentsExample.DOCUMENT1_NAME)));
            Assert.IsNull (example.RetrievedPackageWithDeletedDocuments.Documents.FirstOrDefault (x => x.Name.Equals (DeleteDocumentsExample.DOCUMENT2_NAME)));
            Assert.AreEqual (example.RetrievedPackageWithDeletedDocuments.Documents.Count, 1);
        }

    }
}
