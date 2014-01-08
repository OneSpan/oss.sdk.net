using System;
using NUnit.Framework;

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

