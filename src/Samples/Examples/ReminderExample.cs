using System;
using System.IO;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
	public class ReminderExample : SDKSample
    {
		public static void Main (string[] args)
		{
			new ReminderExample().Run();
		}

        public ReminderSchedule reminderScheduleToCreate, reminderScheduleToUpdate;
        public ReminderSchedule createdReminderSchedule, updatedReminderSchedule, removedReminderSchedule;

		override public void Execute()
		{
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed(PackageName)
				.WithSigner( SignerBuilder.NewSignerWithEmail( email1 )
					.WithFirstName( "Patty" )
					.WithLastName( "Galant" ) )
				.WithDocument( DocumentBuilder.NewDocumentNamed( "First Document" )
					.FromStream( fileStream1, DocumentType.PDF )
					.WithSignature( SignatureBuilder.SignatureFor( email1 )
						.OnPage( 0 )
						.AtPosition( 100, 100 ) ) )
				.Build();

			packageId = ossClient.CreatePackage( superDuperPackage );

            reminderScheduleToCreate = ReminderScheduleBuilder.ForPackageWithId(packageId)
                .WithDaysUntilFirstReminder(2)
                    .WithDaysBetweenReminders(1)
                    .WithNumberOfRepetitions(5)
                    .Build();

            ossClient.ReminderService.CreateReminderScheduleForPackage(reminderScheduleToCreate);

            ossClient.SendPackage( packageId );

            createdReminderSchedule = ossClient.ReminderService.GetReminderScheduleForPackage(packageId);

            reminderScheduleToUpdate = ReminderScheduleBuilder.ForPackageWithId( packageId )
                .WithDaysUntilFirstReminder( 3 )
                    .WithDaysBetweenReminders( 2 )
                    .WithNumberOfRepetitions( 10 )
                    .Build();

            ossClient.ReminderService.UpdateReminderScheduleForPackage(reminderScheduleToUpdate);
            updatedReminderSchedule = ossClient.ReminderService.GetReminderScheduleForPackage(packageId);

			ossClient.ReminderService.ClearReminderScheduleForPackage(packageId);
            removedReminderSchedule = ossClient.ReminderService.GetReminderScheduleForPackage(packageId);
		}
	}
}

