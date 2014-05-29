using System;
using NUnit.Framework;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    public class DocumentOperationsExampleTest
    {
        [Test]
        public void verify() {
            DocumentOperationsExample example = new DocumentOperationsExample(Props.GetInstance());
            example.Run();
            
            Document document = getAddedDocument(example.RetrievedPackageWithNewDocument);
            Assert.IsNotNull(document);
            Assert.IsNotEmpty(document.Signatures);            
            Assert.AreEqual( 1, document.Signatures.Count );
            Assert.AreEqual( DocumentOperationsExample.OriginalDocumentName, document.Name );
            Assert.AreEqual( DocumentOperationsExample.OriginalDocumentDescription, document.Description );
            
            document = getAddedDocument(example.RetrievedPackageWithUpdatedDocument);
            Assert.IsNotNull(document);
            Assert.IsNotEmpty(document.Signatures);
            Assert.AreEqual( 1, document.Signatures.Count );
            Assert.AreEqual( DocumentOperationsExample.UpdatedDocumentName, document.Name );
            Assert.AreEqual( DocumentOperationsExample.UpdatedDocumentDescription, document.Description );
            
            
        }
        
        private Document getAddedDocument(DocumentPackage documentPackage)
        {
            foreach (Document document in documentPackage.Documents.Values)
            {
                if ( document.Name.Equals(DocumentOperationsExample.OriginalDocumentName) || document.Name.Equals(DocumentOperationsExample.UpdatedDocumentName ) )
                {
                    return document;
                }
            }
            return null;
        }         
    }
}

