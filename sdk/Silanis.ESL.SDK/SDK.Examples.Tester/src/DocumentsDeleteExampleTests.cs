using System.Linq;
using NUnit.Framework;

namespace SDK.Examples
{
    public class DocumentsDeleteExampleTests
    {
        [Test]
        public void verify ()
        {
            DocumentsDeleteExample example = new DocumentsDeleteExample ();
            example.Run ();

            // Verify if the document was uploaded correctly.
            Assert.IsNotNull (example.RetrievedPackageWithDocuments.Documents.First (x => x.Name.Equals (DocumentsDeleteExample.DocumentName1)));
            Assert.IsNotNull (example.RetrievedPackageWithDocuments.Documents.First (x => x.Name.Equals (DocumentsDeleteExample.DocumentName2)));

            // Verify if the document was deleted correctly.
            Assert.IsNull (example.RetrievedPackageWithDeletedDocuments.Documents.FirstOrDefault (x => x.Name.Equals (DocumentsDeleteExample.DocumentName1)));
            Assert.IsNull (example.RetrievedPackageWithDeletedDocuments.Documents.FirstOrDefault (x => x.Name.Equals (DocumentsDeleteExample.DocumentName2)));
        }

    }
}
