using System;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    public class ChooseSignatureOptions
    {

        private Nullable<bool> allowStyling;
        private Nullable<bool> allowDrawing;
        private Nullable<bool> allowUploading;
        private Nullable<bool> allowMobileSigning;
        private String defaultSignatureType;
        private IDictionary<string, List<string>> fontsPerWritingSystem;
        
        public Nullable<bool> AllowStyling
        {
            get
            {
                return allowStyling;
            }
            set
            {
                allowStyling = value;
            }
        }
        
        public Nullable<bool> AllowDrawing
        {
            get
            {
                return allowDrawing;
            }
            set
            {
                allowDrawing = value;
            }
        }
        
        public Nullable<bool> AllowUploading
        {
            get
            {
                return allowUploading;
            }
            set
            {
                allowUploading = value;
            }
        }
        
        public Nullable<bool> AllowMobileSigning
        {
            get
            {
                return allowMobileSigning;
            }
            set
            {
                allowMobileSigning = value;
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

        public IDictionary<string, List<string>> FontsPerWritingSystem
        {
            get
            {
                return fontsPerWritingSystem;
            }
            set
            {
                fontsPerWritingSystem = value;
            }
        }

    }
}