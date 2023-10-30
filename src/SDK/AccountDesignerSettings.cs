using System;

namespace OneSpanSign.Sdk
{
    public class AccountDesignerSettings
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

        public Nullable<bool> Send
        {
            get
            {
                return send;
            }
            set
            {
                send = value;
            }
        }
        
        public Nullable<bool> Done
        {
            get
            {
                return done;
            }
            set
            {
                done = value;
            }
        }
        
        public Nullable<bool> Settings
        {
            get
            {
                return settings;
            }
            set
            {
                settings = value;
            }
        }
        
        public Nullable<bool> AddDocument
        {
            get
            {
                return addDocument;
            }
            set
            {
                addDocument = value;
            }
        }
        
        public Nullable<bool> EditDocument
        {
            get
            {
                return editDocument;
            }
            set
            {
                editDocument = value;
            }
        }
        
        public Nullable<bool> DeleteDocument
        {
            get
            {
                return deleteDocument;
            }
            set
            {
                deleteDocument = value;
            }
        }
        
        public Nullable<bool> AddSigner
        {
            get
            {
                return addSigner;
            }
            set
            {
                addSigner = value;
            }
        }
        
        public Nullable<bool> EditRecipient
        {
            get
            {
                return editRecipient;
            }
            set
            {
                editRecipient = value;
            }
        }

        public Nullable<bool> RolePickerSender
        {
            get
            {
                return rolePickerSender;
            }
            set
            {
                rolePickerSender = value;
            }
        }
        
        public Nullable<bool> DocumentVisibility
        {
            get
            {
                return documentVisibility;
            }
            set
            {
                documentVisibility = value;
            }
        }
        
        public Nullable<bool> SaveLayout
        {
            get
            {
                return saveLayout;
            }
            set
            {
                saveLayout = value;
            }
        }
        
        public Nullable<bool> ApplyLayout
        {
            get
            {
                return applyLayout;
            }
            set
            {
                applyLayout = value;
            }
        }

        public Nullable<bool> ShowSharedLayouts
        {
            get
            {
                return showSharedLayouts;
            }
            set
            {
                showSharedLayouts = value;
            }
        }
        
        public String DefaultSignatureType
        {
            get
            {
                return defaultSignatureType;
            }
            set
            {
                defaultSignatureType = value;
            }
        }
    }
}