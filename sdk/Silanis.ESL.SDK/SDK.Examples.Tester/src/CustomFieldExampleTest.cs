using System;
using NUnit.Framework;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    public class CustomFieldExampleTest
    {
		[Test]
		public void verify() {
			CustomFieldExample example = new CustomFieldExample(Props.GetInstance());
			example.Run();
		}
    }
}

