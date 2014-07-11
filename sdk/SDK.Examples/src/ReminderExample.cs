using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
	public class ReminderExample : SDKSample
    {
		public static void Main (string[] args)
		{
			new ReminderExample(Props.GetInstance()).Run();
		}

		private string email1;
		private Stream fileStream1;
        private ReminderSchedule reminderSchedule;

        public ReminderSchedule ReminderSchedule
        {
            get
            {
                return reminderSchedule;
            }
        }

		public ReminderExample( Props props ) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email")) {
		}

		public ReminderExample( string apiKey, string apiUrl, string email1 ) : base( apiKey, apiUrl ) {
			this.email1 = email1;
			this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
		}

		override public void Execute()
		{
			DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed( "ReminderExample: " + DateTime.Now )
				.WithSigner( SignerBuilder.NewSignerWithEmail( email1 )
					.WithFirstName( "Patty" )
					.WithLastName( "Galant" ) )
				.WithDocument( DocumentBuilder.NewDocumentNamed( "First Document" )
					.FromStream( fileStream1, DocumentType.PDF )
					.WithSignature( SignatureBuilder.SignatureFor( email1 )
						.OnPage( 0 )
						.AtPosition( 100, 100 ) ) )
				.Build();

			packageId = eslClient.CreatePackage( superDuperPackage );

			eslClient.ReminderService.SetReminderScheduleForPackage(
				ReminderScheduleBuilder.ForPackageWithId(packageId)
					.WithDaysUntilFirstReminder(2)
					.WithDaysBetweenReminders(1)
					.WithNumberOfRepetitions(5)
					.Build()
			);

			reminderSchedule = eslClient.ReminderService.GetReminderScheduleForPackage(packageId);

			eslClient.SendPackage( packageId );

			eslClient.ReminderService.ClearReminderScheduleForPackage(packageId);
		}
	}
}

