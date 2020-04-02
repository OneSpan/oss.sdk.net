using System;
using NUnit.Framework;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Tests
{
    public class DocumentPackageAttributesBuilderTest
    {
        public DocumentPackageAttributesBuilderTest()
        {
        }

        [Test]
        public void buildWithSpecifiedValues()
        {

            DocumentPackageAttributesBuilder documentPackageAttributesBuilder = new DocumentPackageAttributesBuilder()
                                        .WithAttribute("First Name", "Adam")
                                        .WithAttribute("Last Name", "Smith");
			DocumentPackageAttributes documentPackageAttributes = documentPackageAttributesBuilder.Build();

            Assert.AreEqual("Adam", documentPackageAttributes.Contents["First Name"]);
            Assert.AreEqual("Smith", documentPackageAttributes.Contents["Last Name"]);

        }

    }
}

