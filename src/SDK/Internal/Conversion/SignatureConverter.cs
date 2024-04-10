using System;
using OneSpanSign.Sdk.Builder;
using OneSpanSign.Sdk.src.Internal.Conversion;

namespace OneSpanSign.Sdk
{
    internal class SignatureConverter
    {
        private Signature sdkSignature;
        private OneSpanSign.API.Approval apiApproval;
        private OneSpanSign.API.Package package;

        public SignatureConverter(Signature sdkSignature)
        {
            this.sdkSignature = sdkSignature;
        }

        public SignatureConverter(OneSpanSign.API.Approval apiApproval, OneSpanSign.API.Package package)
        {
            this.apiApproval = apiApproval;
            this.package = package;
        }

        private static bool isPlaceHolder(OneSpanSign.API.Role role)
        {
            return role.Signers.Count == 0;
        }

        private static bool isGroupRole(OneSpanSign.API.Role role)
        {
            return role.Signers.Count == 1 && role.Signers[0].Group != null;
        }

        public Signature ToSDKSignature()
        {

            if(package == null || apiApproval == null)
            {
                return sdkSignature;
            }

            SignatureBuilder signatureBuilder = null;
            foreach (OneSpanSign.API.Role role in package.Roles)
            {
                if (role.Id.Equals(apiApproval.Role))
                {
                    if (isPlaceHolder(role))
                    {
                        signatureBuilder = SignatureBuilder.SignatureFor(new Placeholder(role.Id));
                    }
                    else if (isGroupRole(role))
                    {
                        signatureBuilder = SignatureBuilder.SignatureFor(new GroupId(role.Signers[0].Group.Id));
                    }
                    else
                    {
                        signatureBuilder = SignatureBuilder.SignatureFor(role.Signers[0].Email);
                    }
                }
            }

            if (apiApproval.Id != null)
            {
                signatureBuilder.WithId(new SignatureId(apiApproval.Id));
            }

            signatureBuilder.WithName(apiApproval.Name);

            OneSpanSign.API.Field apiSignatureField = null;
            foreach (OneSpanSign.API.Field apiField in apiApproval.Fields)
            {
                if (FieldType.SIGNATURE.getApiValue().Equals(apiField.Type))
                {
                    apiSignatureField = apiField;
                }
                else
                {
                    Field field = new FieldConverter(apiField).ToSDKField();
                    signatureBuilder.WithField(field);
                }

            }

            if (apiSignatureField == null)
            {
                signatureBuilder.WithStyle(SignatureStyle.ACCEPTANCE);
                signatureBuilder.WithSize(0, 0);
            }
            else
            {
                signatureBuilder.WithStyle(new SignatureStyleConverter(apiSignatureField.Subtype).ToSDKSignatureStyle())
                    .OnPage(apiSignatureField.Page.Value)
                        .AtPosition(apiSignatureField.Left.Value, apiSignatureField.Top.Value)
                        .WithSize(apiSignatureField.Width.Value, apiSignatureField.Height.Value);

                if (apiSignatureField.Extract.HasValue)
                {
                    if (apiSignatureField.Extract.Value)
                        signatureBuilder.WithPositionExtracted(); 
                }
                if (apiSignatureField.FontSize != null)
                {
                    signatureBuilder.WithFontSize(apiSignatureField.FontSize.Value);
                }
            }

            if (apiApproval.Optional)
            {
                signatureBuilder.MakeOptional();
            }

            if (apiApproval.Disabled)
            {
                signatureBuilder.Disabled();
            }

            if (apiApproval.EnforceCaptureSignature)
            {
                signatureBuilder.EnableEnforceCaptureSignature();
            }

            signatureBuilder.SetFromFile(apiApproval.FromFile);
            signatureBuilder.WithTooltip(apiApproval.Tooltip);

            Signature signature = signatureBuilder.Build();
            if (null != apiApproval.Accepted)
            {
                signature.Accepted = apiApproval.Accepted;
            }
            if (null != apiApproval.Signed)
            {
                signature.Signed = apiApproval.Signed;
            }

            return signature;
        }

        public OneSpanSign.API.Approval ToAPIApproval()
        {
            if (sdkSignature == null)
            {
                return apiApproval;
            }

            OneSpanSign.API.Approval result = new OneSpanSign.API.Approval();

            result.AddField(ToField(sdkSignature));

            if (sdkSignature.Id != null)
            {
                result.Id = sdkSignature.Id.Id;
            }

            result.Optional = sdkSignature.Optional;
            result.Disabled = sdkSignature.Disabled;
            result.EnforceCaptureSignature = sdkSignature.EnforceCaptureSignature;
            result.FromFile = sdkSignature.FromFile;
            result.Tooltip = sdkSignature.Tooltip;

            foreach (Field field in sdkSignature.Fields)
            {
                result.AddField(new FieldConverter(field).ToAPIField());
            }

            return result;
        }

        private OneSpanSign.API.Field ToField(Signature signature)
        {
            OneSpanSign.API.Field result = new OneSpanSign.API.Field();

            if (sdkSignature.Id != null)
            {
                result.Id = sdkSignature.Id.Id;
            }

            result.Page = signature.Page;
            result.Name = signature.Name;
            result.Extract = signature.Extract;
            result.FontSize = signature.FontSize;
            result.Tooltip = signature.Tooltip;

            if (!signature.Extract)
            {
                result.Top = signature.Y;
                result.Left = signature.X;
                result.Width = signature.Width;
                result.Height = signature.Height;
            }

            if (signature.TextAnchor != null)
            {
                result.ExtractAnchor = new TextAnchorConverter(signature.TextAnchor).ToAPIExtractAnchor();
            }

            result.Type = FieldType.SIGNATURE.getApiValue();
            result.Subtype = signature.Style.getApiValue();
            return result;
        }
    }
}

