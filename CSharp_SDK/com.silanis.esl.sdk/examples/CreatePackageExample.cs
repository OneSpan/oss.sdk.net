using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace Silanis.ESL.SDK
{
	public class CreatePackageExample
	{
		public static string apiToken = "cW55dmh1T2lTS3ZlOnNlY3JldA==";
		public static string url = "http://localhost:8080/aws/rest/services";

		public static void Main (string[] args)
		{
			EslClient eslClient = new EslClient (apiToken, url);
			Console.WriteLine ("***** start *****");
			Console.WriteLine ();

			//PackageId
			PackageId packageId = new PackageId ("buwNtm3rxC8P2ZTnp96Yse8eqP0N");

//*********************************

//			BeforeSigning (eslClient);
			AfterSigning (eslClient, packageId);

			Console.WriteLine ("***** done *****");
		}

		public static void BeforeSigning (EslClient eslClient)
		{
			byte[] file = File.ReadAllBytes ("/Users/lena/tmp/document.pdf");
			string fileName = System.IO.Path.GetFileName ("/Users/lena/tmp/document.pdf");

			Console.WriteLine ("...testing create package...");
			PackageId packageId = eslClient.PackageService.CreatePackage (NewPackage ());
			Console.WriteLine ("package id : " + packageId.Id);
			Console.WriteLine ();

			Console.WriteLine ("...testing upload document...");
			eslClient.PackageService.UploadDocument (packageId, fileName, file, CreateDocument ());
			Console.WriteLine ("ok");
			Console.WriteLine ();

			Console.WriteLine ("...testing get package...");
			Console.WriteLine (eslClient.PackageService.GetPackage (packageId));
			Console.WriteLine ();

			Console.WriteLine ("...testing get roles...");
			List<Role> roleList = eslClient.PackageService.GetRoles (packageId);
			roleList.ForEach (delegate(Role role) {
				Console.WriteLine (role.Name);
			});
			Console.WriteLine ();

			Console.WriteLine ("...testing add role...");
			Role newRole = NewRole ();
			Role role1 = eslClient.PackageService.AddRole (packageId, newRole);
			Console.WriteLine (role1.Name);
			Console.WriteLine ();

			Console.WriteLine ("...testing delete role...");
			eslClient.PackageService.DeleteRole (packageId, newRole);
			List<Role> roleList2 = eslClient.PackageService.GetRoles (packageId);
			roleList2.ForEach (delegate(Role role) {
				Console.WriteLine (role.Name);
			});
			Console.WriteLine ();

			Console.WriteLine ("...testing update package...");
			eslClient.PackageService.UpdatePackage (packageId, UpdatePackage ());
			Console.WriteLine ("ok");
			Console.WriteLine ();

			Console.WriteLine ("...testing send package...");
			eslClient.PackageService.SendPackage (packageId);
			Console.WriteLine ("sent");
			Console.WriteLine ();

			Console.WriteLine ("...testing create session token...");
			Console.WriteLine ("http://localhost/access?sessionToken=" + eslClient.SessionService.CreateSessionToken (packageId, NewSigner ()).Token);
			Console.WriteLine ();

		}

		public static void AfterSigning (EslClient eslClient, PackageId packageId)
		{
			Console.WriteLine ("...testing get field summary...");
			List<FieldSummary> fieldSummaryList = eslClient.FieldSummaryService.GetFieldSummary (packageId);
			fieldSummaryList.ForEach (delegate(FieldSummary fieldSummary) {
				Console.WriteLine (fieldSummary.FieldValue);
			});
			Console.WriteLine ();

			Console.WriteLine ("...testing get audit...");
			Console.WriteLine (eslClient.AuditService.GetAudit (packageId) [0].type);
			Console.WriteLine ();

			Console.WriteLine ("...testing download document...");
			byte[] doc = eslClient.PackageService.DownloadDocument (packageId, CreateDocument ());
			File.WriteAllBytes ("/Users/lena/tmp/downloadDocument.pdf", doc);
			Console.WriteLine (doc.Length != 0);
			Console.WriteLine ();

			Console.WriteLine ("...testing download zip file...");
			Console.WriteLine (eslClient.PackageService.DownloadZippedDocuments (packageId).Length != 0);
			byte[] bytes = eslClient.PackageService.DownloadZippedDocuments (packageId);
			FileStream fileStream = new FileStream ("/Users/lena/tmp/all.zip", FileMode.Create, FileAccess.ReadWrite);
			fileStream.Write (bytes, 0, bytes.Length);
			fileStream.Close ();
			Console.WriteLine ();

			Console.WriteLine ("...testing download evidence summary...");
			Console.WriteLine (eslClient.PackageService.DownloadEvidenceSummary (packageId).Length != 0);
			byte[] evidence = eslClient.PackageService.DownloadEvidenceSummary (packageId);
			File.WriteAllBytes ("/Users/lena/tmp/evidence.pdf", evidence);
			Console.WriteLine ();

		}

		public static Package NewPackage ()
		{
			Role role = new Role ();
			role.Name = "role name";
			role.Type = RoleType.SENDER;
			role.Signers.Add (NewSigner ());
			role.Id = "Role1";
			
			Package package = new Package ();
			package.Autocomplete = true;
			package.Name = "My package";
			package.AddRole (role);
			
			return package;
		}

		public static Signer NewSigner ()
		{
			Auth auth = new Auth ();
			auth.Scheme = AuthScheme.CHALLENGE;
			
			Signer signer = new Signer ();
			signer.Auth = auth;
			signer.Email = "email@email.com";
			signer.FirstName = "firstName";
			signer.LastName = "lastName";
			signer.Id = "Signer1";
			return signer;
		}

		public static Package UpdatePackage ()
		{
			Auth auth = new Auth ();
			auth.Scheme = AuthScheme.CHALLENGE;
			
			Signer signer = new Signer ();
			signer.Auth = auth;
			signer.Email = "email@email.com";
			signer.FirstName = "First";
			signer.LastName = "Last";
			signer.Id = "Signer1";
			
			Role role = new Role ();
			role.Name = "Role Name";
			role.Type = RoleType.SENDER;
			role.Signers.Add (signer);
			role.Id = "Role1";
			
			Package package = new Package ();
			package.Autocomplete = true;
			package.Name = "My Modified Package";
			package.AddRole (role);
			
			return package;
		}

		public static Document CreateDocument ()
		{
			Field field = new Field ();
			field.Type = FieldType.SIGNATURE;
			field.Subtype = FieldSubtype.FULLNAME;
			field.Left = 500.0;
			field.Top = 100.0;
			field.Height = 50.0;
			field.Width = 200.0;
			field.Name = "Sign Here";
			
			Field field2 = new Field ();
			field2.Type = FieldType.INPUT;
			field2.Subtype = FieldSubtype.TEXTFIELD;
			field2.Left = 500.0;
			field2.Top = 300.0;
			field2.Height = 50.0;
			field2.Width = 100.0;
			
			Field field3 = new Field ();
			field3.Type = FieldType.INPUT;
			field3.Subtype = FieldSubtype.TEXTFIELD;
			field3.Left = 500.0;
			field3.Top = 400.0;
			field3.Height = 50.0;
			field3.Width = 100.0;
			
			Approval approval = new Approval ();
			approval.AddField (field);
			approval.AddField (field2);
			approval.AddField (field3);
			approval.Role = "Role1";
			
			Document document = new Document ();
			document.AddApproval (approval);
			document.Name = "File Name";
			document.Id = "Doc1";
			
			return document;
		}

		public static Role NewRole ()
		{
			Auth auth = new Auth ();
			auth.Scheme = AuthScheme.CHALLENGE;
			
			Signer signer = new Signer ();
			signer.Auth = auth;
			signer.Email = "email2@email.com";
			signer.FirstName = "new_firstName";
			signer.LastName = "new_lastName";
			signer.Id = "Signer2";

			Role role = new Role ();
			role.Name = "new role name";
			role.Type = RoleType.SIGNER;
			role.AddSigner (signer);
			role.Id = "Role2";

			return role;
		}

	}
}

