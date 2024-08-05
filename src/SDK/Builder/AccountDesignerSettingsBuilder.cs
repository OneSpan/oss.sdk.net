using System;

namespace OneSpanSign.Sdk
{
    public class AccountDesignerSettingsBuilder
    {


        private Nullable<bool> _send;
        private Nullable<bool> _done;
        private Nullable<bool> _settings;
        private Nullable<bool> _documentVisibility;
        private Nullable<bool> _addDocument;
        private Nullable<bool> _editDocument;
        private Nullable<bool> _deleteDocument;
        private Nullable<bool> _addSigner;
        private Nullable<bool> _editRecipient;
        private Nullable<bool> _rolePickerSender;
        private Nullable<bool> _saveLayout;
        private Nullable<bool> _applyLayout;
        private Nullable<bool> _showSharedLayouts;
        private string _defaultSignatureType;


        private AccountDesignerSettingsBuilder()
        {
        }

        public static AccountDesignerSettingsBuilder NewAccountDesignerSettings()
        {
            return new AccountDesignerSettingsBuilder();
        }
        
        public AccountDesignerSettingsBuilder WithSend()
        {
            this._send = true;
            return this;
        }

        public AccountDesignerSettingsBuilder WithoutSend()
        {
            this._send = false;
            return this;
        }

        public AccountDesignerSettingsBuilder WithDone()
        {
            this._done = true;
            return this;
        }

        public AccountDesignerSettingsBuilder WithoutDone()
        {
            this._done = false;
            return this;
        }

        public AccountDesignerSettingsBuilder WithSettings()
        {
            this._settings = true;
            return this;
        }

        public AccountDesignerSettingsBuilder WithoutSettings()
        {
            this._settings = false;
            return this;
        }

        public AccountDesignerSettingsBuilder WithDocumentVisibility()
        {
            this._documentVisibility = true;
            return this;
        }

        public AccountDesignerSettingsBuilder WithoutDocumentVisibility()
        {
            this._documentVisibility = false;
            return this;
        }

        public AccountDesignerSettingsBuilder WithAddDocument()
        {
            this._addDocument = true;
            return this;
        }

        public AccountDesignerSettingsBuilder WithoutAddDocument()
        {
            this._addDocument = false;
            return this;
        }

        public AccountDesignerSettingsBuilder WithEditDocument()
        {
            this._editDocument = true;
            return this;
        }

        public AccountDesignerSettingsBuilder WithoutEditDocument()
        {
            this._editDocument = false;
            return this;
        }

        public AccountDesignerSettingsBuilder WithDeleteDocument()
        {
            this._deleteDocument = true;
            return this;
        }

        public AccountDesignerSettingsBuilder WithoutDeleteDocument()
        {
            this._deleteDocument = false;
            return this;
        }

        public AccountDesignerSettingsBuilder WithAddSigner()
        {
            this._addSigner = true;
            return this;
        }

        public AccountDesignerSettingsBuilder WithoutAddSigner()
        {
            this._addSigner = false;
            return this;
        }

        public AccountDesignerSettingsBuilder WithEditRecipient()
        {
            this._editRecipient = true;
            return this;
        }

        public AccountDesignerSettingsBuilder WithoutEditRecipient()
        {
            this._editRecipient = false;
            return this;
        }

        public AccountDesignerSettingsBuilder WithRolePickerSender()
        {
            this._rolePickerSender = true;
            return this;
        }

        public AccountDesignerSettingsBuilder WithoutRolePickerSender()
        {
            this._rolePickerSender = false;
            return this;
        }

        public AccountDesignerSettingsBuilder WithSaveLayout()
        {
            this._saveLayout = true;
            return this;
        }

        public AccountDesignerSettingsBuilder WithoutSaveLayout()
        {
            this._saveLayout = false;
            return this;
        }

        public AccountDesignerSettingsBuilder WithApplyLayout()
        {
            this._applyLayout = true;
            return this;
        }

        public AccountDesignerSettingsBuilder WithoutApplyLayout()
        {
            this._applyLayout = false;
            return this;
        }

        public AccountDesignerSettingsBuilder WithShowSharedLayouts()
        {
            this._showSharedLayouts = true;
            return this;
        }

        public AccountDesignerSettingsBuilder WithoutShowSharedLayouts()
        {
            this._showSharedLayouts = false;
            return this;
        }

        public AccountDesignerSettingsBuilder WithDefaultSignatureType(string signatureType)
        {
            this._defaultSignatureType = signatureType;
            return this;
        }

        public AccountDesignerSettings Build()
        {
            AccountDesignerSettings result = new AccountDesignerSettings();
            result.Send = _send;
            result.Done = _done;
            result.Settings = _settings;
            result.DocumentVisibility = _documentVisibility;
            result.AddDocument = _addDocument;
            result.EditDocument = _editDocument;
            result.DeleteDocument = _deleteDocument;
            result.AddSigner = _addSigner;
            result.EditRecipient = _editRecipient;
            result.RolePickerSender = _rolePickerSender;
            result.SaveLayout = _saveLayout;
            result.ApplyLayout = _applyLayout;
            result.ShowSharedLayouts = _showSharedLayouts;
            result.DefaultSignatureType = _defaultSignatureType;

            return result;
        }

    }
}