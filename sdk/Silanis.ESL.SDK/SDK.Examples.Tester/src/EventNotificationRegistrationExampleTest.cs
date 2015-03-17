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
            Assert.AreEqual(eventNotificationConfig.Url, example.URL);
			Assert.AreEqual(eventNotificationConfig.NotificationEvents.Count, 18);

            List<NotificationEvent> eventList = new List<NotificationEvent>();
            eventList.Add(example.EVENT1);
            eventList.Add(example.EVENT2);
            eventList.Add(example.EVENT3);
            eventList.Add(example.EVENT4);
            eventList.Add(example.EVENT5);
            eventList.Add(example.EVENT6);
            eventList.Add(example.EVENT7);
            eventList.Add(example.EVENT8);
            eventList.Add(example.EVENT9);
            eventList.Add(example.EVENT10);
            eventList.Add(example.EVENT11);
            eventList.Add(example.EVENT12);
            eventList.Add(example.EVENT13);
            eventList.Add(example.EVENT14);
            eventList.Add(example.EVENT15);
            eventList.Add(example.EVENT16);
            eventList.Add(example.EVENT17);
            eventList.Add(example.EVENT18);

            foreach (NotificationEvent notificationEvent in eventList)
            {
                bool found = false;
                foreach (NotificationEvent receivedEvent in eventNotificationConfig.NotificationEvents) 
                {
                    if (receivedEvent.ToString().Equals(notificationEvent.ToString())) 
                    {
                        found = true;
                        break;
                    }
                }
                Assert.IsTrue(found, "Callback has wrong event for EVENT" + (eventList.IndexOf(notificationEvent) + 1));
            }
		}
	}
}

