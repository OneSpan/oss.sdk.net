using NUnit.Framework;
using System;
using Silanis.ESL.SDK;
using System.Collections.Generic;

namespace SDK.Examples
{
	[TestFixture()]
	public class EventNotificationRegistrationExampleTest
	{
		[Test()]
		public void VerifyResult()
		{
			EventNotificationRegistrationExample example = new EventNotificationRegistrationExample(Props.GetInstance());
			example.Run();

			EventNotificationConfig eventNotificationConfig = example.EventNotificationConfig;

			Assert.IsNotNull(eventNotificationConfig);
			Assert.AreEqual(eventNotificationConfig.Url, EventNotificationRegistrationExample.URL);
			Assert.AreEqual(eventNotificationConfig.NotificationEvents.Count, 3);
			Assert.AreEqual(eventNotificationConfig.NotificationEvents[0].ToString(), EventNotificationRegistrationExample.EVENT1.ToString());
			Assert.AreEqual(eventNotificationConfig.NotificationEvents[1].ToString(), EventNotificationRegistrationExample.EVENT2.ToString());
			Assert.AreEqual(eventNotificationConfig.NotificationEvents[2].ToString(), EventNotificationRegistrationExample.EVENT3.ToString());
		}
	}
}

