using System;
using System.Collections.Generic;

namespace Silanis.ESL.SDK
{
    class PackageUtil
    {
        public static Package NewPackage()
        {
            Role role = new Role();
            role.Name = "role name";
            role.Type = RoleType.SENDER;
            role.Signers.Add(NewSigner());
            role.Id = "Role1";

            Package package = new Package();
            package.Autocomplete = true;
            package.Name = "My package";
            package.AddRole(role);

            return package;
        }

        public static Signer NewSigner()
        {
            Auth auth = new Auth();
            auth.Scheme = AuthScheme.CHALLENGE;

            Signer signer = new Signer();
            signer.Auth = auth;
            signer.Email = "email@email.com";
            signer.FirstName = "firstName";
            signer.LastName = "lastName";
            signer.Id = "Signer1";
            return signer;
        }

        public static Package UpdatePackage()
        {
            Auth auth = new Auth();
            auth.Scheme = AuthScheme.CHALLENGE;

            Signer signer = new Signer();
            signer.Auth = auth;
            signer.Email = "email@email.com";
            signer.FirstName = "First";
            signer.LastName = "Last";
            signer.Id = "Signer1";

            Role role = new Role();
            role.Name = "Role Name";
            role.Type = RoleType.SENDER;
            role.Signers.Add(signer);
            role.Id = "Role1";

            Package package = new Package();
            package.Autocomplete = true;
            package.Name = "My Modified Package";
            package.AddRole(role);

            return package;
        }

        public static Document CreateDocument()
        {
            Field field = new Field();
            field.Type = FieldType.SIGNATURE;
            field.Subtype = FieldSubtype.FULLNAME;
            field.Left = 500.0;
            field.Top = 100.0;
            field.Height = 50.0;
            field.Width = 200.0;
            field.Name = "Sign Here";

            Field field2 = new Field();
            field2.Type = FieldType.INPUT;
            field2.Subtype = FieldSubtype.TEXTFIELD;
            field2.Left = 500.0;
            field2.Top = 300.0;
            field2.Height = 50.0;
            field2.Width = 100.0;

            Field field3 = new Field();
            field3.Type = FieldType.INPUT;
            field3.Subtype = FieldSubtype.TEXTFIELD;
            field3.Left = 500.0;
            field3.Top = 400.0;
            field3.Height = 50.0;
            field3.Width = 100.0;

            Approval approval = new Approval();
            approval.AddField(field);
            approval.AddField(field2);
            approval.AddField(field3);
            approval.Role = "Role1";

            Document document = new Document();
            document.AddApproval(approval);
            document.Name = "File Name";
            document.Id = "Doc1";

            return document;
        }

        public static Role NewRole()
        {
            Auth auth = new Auth();
            auth.Scheme = AuthScheme.CHALLENGE;

            Signer signer = new Signer();
            signer.Auth = auth;
            signer.Email = "email2@email.com";
            signer.FirstName = "new_firstName";
            signer.LastName = "new_lastName";
            signer.Id = "Signer2";

            Role role = new Role();
            role.Name = "new role name";
            role.Type = RoleType.SIGNER;
            role.AddSigner(signer);
            role.Id = "Role2";

            return role;
        }
    }
}
