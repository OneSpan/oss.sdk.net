using System;

namespace OneSpanSign.Sdk.Builder
{
    public class AccountDesignerSettingsBuilder
    {


        private Nullable<bool> send;
        private Nullable<bool> done;
        private Nullable<bool> settings;
        private Nullable<bool> documentVisibility;
        private Nullable<bool> addDocument;
        private Nullable<bool> editDocument;
        private Nullable<bool> deleteDocument;
        private Nullable<bool> addSigner;
        private Nullable<bool> editRecipient;
        private Nullable<bool> rolePickerSender;
        private Nullable<bool> saveLayout;
        private Nullable<bool> applyLayout;
        private Nullable<bool> showSharedLayouts;
        private String defaultSignatureType;


        private AccountDesignerSettingsBuilder()
        {
        }

        public static AccountDesignerSettingsBuilder NewAccountDesignerSettings()
        {
            return new AccountDesignerSettingsBuilder();
        }
        
        public AccountDesignerSettingsBuilder WithSend()
        {
            this.send = true;
            return this;
        }

        public AccountDesignerSettingsBuilder WithoutSend()
        {
            this.send = false;
            return this;
        }

        public AccountDesignerSettingsBuilder WithDone()
        {
            this.done = true;
            return this;
        }

        public AccountDesignerSettingsBuilder WithoutDone()
        {
            this.done = false;
            return this;
        }

        public AccountDesignerSettingsBuilder WithSettings()
        {
            this.settings = true;
            return this;
        }

        public AccountDesignerSettingsBuilder WithoutSettings()
        {
            this.settings = false;
            return this;
        }

        public AccountDesignerSettingsBuilder WithDocumentVisibility()
        {
            this.documentVisibility = true;
            return this;
        }

        public AccountDesignerSettingsBuilder WithoutDocumentVisibility()
        {
            this.documentVisibility = false;
            return this;
        }

        public AccountDesignerSettingsBuilder WithAddDocument()
        {
            this.addDocument = true;
            return this;
        }

        public AccountDesignerSettingsBuilder WithoutAddDocument()
        {
            this.addDocument = false;
            return this;
        }

        public AccountDesignerSettingsBuilder WithEditDocument()
        {
            this.editDocument = true;
            return this;
        }

        public AccountDesignerSettingsBuilder WithoutEditDocument()
        {
            this.editDocument = false;
            return this;
        }

        public AccountDesignerSettingsBuilder WithDeleteDocument()
        {
            this.deleteDocument = true;
            return this;
        }

        public AccountDesignerSettingsBuilder WithoutDeleteDocument()
        {
            this.deleteDocument = false;
            return this;
        }

        public AccountDesignerSettingsBuilder WithAddSigner()
        {
            this.addSigner = true;
            return this;
        }

        public AccountDesignerSettingsBuilder WithoutAddSigner()
        {
            this.addSigner = false;
            return this;
        }

        public AccountDesignerSettingsBuilder WithEditRecipient()
        {
            this.editRecipient = true;
            return this;
        }

        public AccountDesignerSettingsBuilder WithoutEditRecipient()
        {
            this.editRecipient = false;
            return this;
        }

        public AccountDesignerSettingsBuilder WithRolePickerSender()
        {
            this.rolePickerSender = true;
            return this;
        }

        public AccountDesignerSettingsBuilder WithoutRolePickerSender()
        {
            this.rolePickerSender = false;
            return this;
        }

        public AccountDesignerSettingsBuilder WithSaveLayout()
        {
            this.saveLayout = true;
            return this;
        }

        public AccountDesignerSettingsBuilder WithoutSaveLayout()
        {
            this.saveLayout = false;
            return this;
        }

        public AccountDesignerSettingsBuilder WithApplyLayout()
        {
            this.applyLayout = true;
            return this;
        }

        public AccountDesignerSettingsBuilder WithoutApplyLayout()
        {
            this.applyLayout = false;
            return this;
        }

        public AccountDesignerSettingsBuilder WithShowSharedLayouts()
        {
            this.showSharedLayouts = true;
            return this;
        }

        public AccountDesignerSettingsBuilder WithoutShowSharedLayouts()
        {
            this.showSharedLayouts = false;
            return this;
        }

        public AccountDesignerSettingsBuilder WithDefaultSignatureType(String signatureType)
        {
            this.defaultSignatureType = signatureType;
            return this;
        }

        public AccountDesignerSettings Build()
        {
            AccountDesignerSettings result = new AccountDesignerSettings();
            result.Send = send;
            result.Done = done;
            result.Settings = settings;
            result.DocumentVisibility = documentVisibility;
            result.AddDocument = addDocument;
            result.EditDocument = editDocument;
            result.DeleteDocument = deleteDocument;
            result.AddSigner = addSigner;
            result.EditRecipient = editRecipient;
            result.RolePickerSender = rolePickerSender;
            result.SaveLayout = saveLayout;
            result.ApplyLayout = applyLayout;
            result.ShowSharedLayouts = showSharedLayouts;
            result.DefaultSignatureType = defaultSignatureType;

            return result;
        }

    }
}