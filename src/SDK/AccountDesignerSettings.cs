using System;

namespace OneSpanSign.Sdk
{
    public class AccountDesignerSettings
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
        private String _defaultSignatureType;

        public Nullable<bool> Send
        {
            get
            {
                return _send;
            }
            set
            {
                _send = value;
            }
        }
        
        public Nullable<bool> Done
        {
            get
            {
                return _done;
            }
            set
            {
                _done = value;
            }
        }
        
        public Nullable<bool> Settings
        {
            get
            {
                return _settings;
            }
            set
            {
                _settings = value;
            }
        }
        
        public Nullable<bool> AddDocument
        {
            get
            {
                return _addDocument;
            }
            set
            {
                _addDocument = value;
            }
        }
        
        public Nullable<bool> EditDocument
        {
            get
            {
                return _editDocument;
            }
            set
            {
                _editDocument = value;
            }
        }
        
        public Nullable<bool> DeleteDocument
        {
            get
            {
                return _deleteDocument;
            }
            set
            {
                _deleteDocument = value;
            }
        }
        
        public Nullable<bool> AddSigner
        {
            get
            {
                return _addSigner;
            }
            set
            {
                _addSigner = value;
            }
        }
        
        public Nullable<bool> EditRecipient
        {
            get
            {
                return _editRecipient;
            }
            set
            {
                _editRecipient = value;
            }
        }

        public Nullable<bool> RolePickerSender
        {
            get
            {
                return _rolePickerSender;
            }
            set
            {
                _rolePickerSender = value;
            }
        }
        
        public Nullable<bool> DocumentVisibility
        {
            get
            {
                return _documentVisibility;
            }
            set
            {
                _documentVisibility = value;
            }
        }
        
        public Nullable<bool> SaveLayout
        {
            get
            {
                return _saveLayout;
            }
            set
            {
                _saveLayout = value;
            }
        }
        
        public Nullable<bool> ApplyLayout
        {
            get
            {
                return _applyLayout;
            }
            set
            {
                _applyLayout = value;
            }
        }

        public Nullable<bool> ShowSharedLayouts
        {
            get
            {
                return _showSharedLayouts;
            }
            set
            {
                _showSharedLayouts = value;
            }
        }
        
        public String DefaultSignatureType
        {
            get
            {
                return _defaultSignatureType;
            }
            set
            {
                _defaultSignatureType = value;
            }
        }
    }
}