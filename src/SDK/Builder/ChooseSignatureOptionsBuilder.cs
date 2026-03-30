using System;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    public class ChooseSignatureOptionsBuilder
    {
        private Nullable<bool> allowStyling;
        private Nullable<bool> allowDrawing;
        private Nullable<bool> allowUploading;
        private Nullable<bool> allowMobileSigning;
        private ChooseSignatureStyleType chooseSignatureStyleType = ChooseSignatureStyleType.STYLE;
        private IDictionary<string, List<string>> fontsPerWritingSystem;
        
        private ChooseSignatureOptionsBuilder()
        {
        }

        public static ChooseSignatureOptionsBuilder NewChooseSignatureOptions() {
            return new ChooseSignatureOptionsBuilder();
        }

        public ChooseSignatureOptionsBuilder WithAllowStyling() {
            this.allowStyling = true;
            return this;
        }

        public ChooseSignatureOptionsBuilder WithoutAllowStyling() {
            this.allowStyling = false;
            return this;
        }
        
        public ChooseSignatureOptionsBuilder WithAllowDrawing() {
            this.allowDrawing = true;
            return this;
        }

        public ChooseSignatureOptionsBuilder WithoutAllowDrawing() {
            this.allowDrawing = false;
            return this;
        }
        
        public ChooseSignatureOptionsBuilder WithAllowUploading() {
            this.allowUploading = true;
            return this;
        }

        public ChooseSignatureOptionsBuilder WithoutAllowUploading() {
            this.allowUploading = false;
            return this;
        }
        
        public ChooseSignatureOptionsBuilder WithAllowMobileSigning() {
            this.allowMobileSigning = true;
            return this;
        }

        public ChooseSignatureOptionsBuilder WithoutAllowMobileSigning() {
            this.allowMobileSigning = false;
            return this;
        }

        public ChooseSignatureOptionsBuilder WithChooseSignatureStyleType(
            ChooseSignatureStyleType chooseSignatureStyleType)
        {
            this.chooseSignatureStyleType = chooseSignatureStyleType;
            return this;
        }
        
        public ChooseSignatureOptionsBuilder WithFontsPerWritingSystem(IDictionary<string, List<string>> fontsPerWritingSystem)
        {
            this.fontsPerWritingSystem = fontsPerWritingSystem;
            return this;
        }

        public ChooseSignatureOptions Build() {
            ChooseSignatureOptions result = new ChooseSignatureOptions();
            result.AllowStyling = allowStyling;
            result.AllowDrawing = allowDrawing;
            result.AllowUploading = allowUploading;
            result.AllowMobileSigning = allowMobileSigning;
            result.DefaultSignatureType = chooseSignatureStyleType.ToString();
            result.FontsPerWritingSystem = fontsPerWritingSystem;
            return result;
        }
    }
}