using NUnit.Framework;
using System;
using OneSpanSign.Sdk;
using System.Collections.Generic;
using OneSpanSign.API;

namespace SDK.Tests
{
    [TestFixture()]
    public class DocumentPackageSettingsConverterTest
    {
        private OneSpanSign.Sdk.DocumentPackageSettings sdkPackageSettings1 = null;
        private OneSpanSign.API.PackageSettings apiPackageSettings1 = null;
        private OneSpanSign.API.PackageSettings apiPackageSettings2 = null;
        private DocumentPackageSettingsConverter converter = null;

        [Test()]
        public void ConvertNullSDKToAPI()
        {
            sdkPackageSettings1 = null;
            converter = new DocumentPackageSettingsConverter(sdkPackageSettings1);
            Assert.IsNull(converter.toAPIPackageSettings());
        }

        [Test()]
        public void ConvertNullAPIToAPI()
        {
            apiPackageSettings1 = null;
            converter = new DocumentPackageSettingsConverter(apiPackageSettings1);
            Assert.IsNull(converter.toAPIPackageSettings());
        }

        [Test()]
        public void ConvertAPIToAPI()
        {
            apiPackageSettings1 = CreateTypicalAPIPackageSettings();
            converter = new DocumentPackageSettingsConverter(apiPackageSettings1);
            apiPackageSettings2 = converter.toAPIPackageSettings();

            Assert.IsNotNull(apiPackageSettings2);
            Assert.AreEqual(apiPackageSettings2, apiPackageSettings1);
        }

        [Test()]
        public void ConvertAPIToSDK()
        {
            apiPackageSettings1 = CreateTypicalAPIPackageSettings();
            sdkPackageSettings1 = new DocumentPackageSettingsConverter(apiPackageSettings1).toSDKDocumentPackageSettings();

            Assert.IsNotNull(sdkPackageSettings1);
            Assert.AreEqual(apiPackageSettings1.Ceremony.InPerson, sdkPackageSettings1.EnableInPerson);
            Assert.AreEqual(apiPackageSettings1.Ceremony.Ada, sdkPackageSettings1.Ada);
            Assert.AreEqual(apiPackageSettings1.Ceremony.FontSize, sdkPackageSettings1.FontSize);
            Assert.AreEqual(apiPackageSettings1.Ceremony.EnforceCaptureSignature, sdkPackageSettings1.EnforceCaptureSignature);
            Assert.AreEqual(apiPackageSettings1.Ceremony.DeclineButton, sdkPackageSettings1.EnableDecline);
            Assert.AreEqual(apiPackageSettings1.Ceremony.LeftMenuExpand, sdkPackageSettings1.ExpandLeftMenu);
            Assert.AreEqual(apiPackageSettings1.Ceremony.OptOutButton, sdkPackageSettings1.EnableOptOut);
            Assert.AreEqual(apiPackageSettings1.Ceremony.DeclineReasons[0], sdkPackageSettings1.DeclineReasons[0]);
            Assert.AreEqual(apiPackageSettings1.Ceremony.DeclineReasons[1], sdkPackageSettings1.DeclineReasons[1]);
            Assert.AreEqual(apiPackageSettings1.Ceremony.DeclineReasons[2], sdkPackageSettings1.DeclineReasons[2]);
            Assert.AreEqual(apiPackageSettings1.Ceremony.DisableDeclineOther, sdkPackageSettings1.DisableDeclineOther);
            Assert.AreEqual(apiPackageSettings1.Ceremony.OptOutReasons[0], sdkPackageSettings1.OptOutReasons[0]);
            Assert.AreEqual(apiPackageSettings1.Ceremony.OptOutReasons[1], sdkPackageSettings1.OptOutReasons[1]);
            Assert.AreEqual(apiPackageSettings1.Ceremony.OptOutReasons[2], sdkPackageSettings1.OptOutReasons[2]);
            Assert.AreEqual(apiPackageSettings1.Ceremony.DisableOptOutOther, sdkPackageSettings1.DisableOptOutOther);
            Assert.AreEqual(apiPackageSettings1.Ceremony.HandOver.Href, sdkPackageSettings1.LinkHref);
            Assert.AreEqual(apiPackageSettings1.Ceremony.HandOver.Text, sdkPackageSettings1.LinkText);
            Assert.AreEqual(apiPackageSettings1.Ceremony.HandOver.Title, sdkPackageSettings1.LinkTooltip);
            Assert.AreEqual(apiPackageSettings1.Ceremony.HideCaptureText, sdkPackageSettings1.HideCaptureText);
            Assert.AreEqual(apiPackageSettings1.Ceremony.HideWatermark, sdkPackageSettings1.HideWatermark);
            Assert.AreEqual(apiPackageSettings1.Ceremony.MaxAuthFailsAllowed, sdkPackageSettings1.MaxAuthAttempts);
            Assert.AreEqual(apiPackageSettings1.Ceremony.Layout.Header.GlobalActions.Download, sdkPackageSettings1.CeremonyLayoutSettings.ShowGlobalDownloadButton);
            Assert.AreEqual(apiPackageSettings1.Ceremony.Layout.Header.GlobalActions.Confirm, sdkPackageSettings1.CeremonyLayoutSettings.ShowGlobalConfirmButton);
            Assert.AreEqual(apiPackageSettings1.Ceremony.Layout.Header.GlobalActions.SaveAsLayout, sdkPackageSettings1.CeremonyLayoutSettings.ShowGlobalSaveAsLayoutButton);
            Assert.AreEqual(apiPackageSettings1.Ceremony.HideLanguageDropdown, !sdkPackageSettings1.ShowLanguageDropDown);
            Assert.AreEqual(apiPackageSettings1.Ceremony.HidePackageOwnerInPerson, !sdkPackageSettings1.ShowOwnerInPersonDropDown);
            Assert.AreEqual(apiPackageSettings1.Ceremony.DisableFirstInPersonAffidavit, !sdkPackageSettings1.EnableFirstAffidavit);
            Assert.AreEqual(apiPackageSettings1.Ceremony.DisableSecondInPersonAffidavit, !sdkPackageSettings1.EnableSecondAffidavit);
            Assert.AreEqual(apiPackageSettings1.Ceremony.DefaultTimeBasedExpiry, sdkPackageSettings1.DefaultTimeBasedExpiry);
            Assert.AreEqual(apiPackageSettings1.Ceremony.RemainingDays, sdkPackageSettings1.RemainingDays);
            Assert.AreEqual(apiPackageSettings1.Ceremony.MaxAttachmentFiles, sdkPackageSettings1.MaxAttachmentFiles);
            Assert.AreEqual(apiPackageSettings1.IntegrationFrameworkWorkflows[0].Name, sdkPackageSettings1.IntegrationFrameworkWorkflows[0].Name);
            Assert.AreEqual(apiPackageSettings1.IntegrationFrameworkWorkflows[0].Connector.ToString(), sdkPackageSettings1.IntegrationFrameworkWorkflows[0].Connector.ToString());
        }

        [Test()]
        public void ConvertSDKToAPI()
        {
            sdkPackageSettings1 = CreateTypicalSDKDocumentPackageSettings();
            apiPackageSettings1 = new DocumentPackageSettingsConverter(sdkPackageSettings1).toAPIPackageSettings();

            Assert.IsNotNull(apiPackageSettings1);
            Assert.AreEqual(apiPackageSettings1.Ceremony.InPerson, sdkPackageSettings1.EnableInPerson);
            Assert.AreEqual(apiPackageSettings1.Ceremony.Ada, sdkPackageSettings1.Ada);
            Assert.AreEqual(apiPackageSettings1.Ceremony.FontSize, sdkPackageSettings1.FontSize);
            Assert.AreEqual(apiPackageSettings1.Ceremony.EnforceCaptureSignature, sdkPackageSettings1.EnforceCaptureSignature);
            Assert.AreEqual(apiPackageSettings1.Ceremony.DeclineButton, sdkPackageSettings1.EnableDecline);
            Assert.AreEqual(apiPackageSettings1.Ceremony.LeftMenuExpand, sdkPackageSettings1.ExpandLeftMenu);
            Assert.AreEqual(apiPackageSettings1.Ceremony.OptOutButton, sdkPackageSettings1.EnableOptOut);
            Assert.AreEqual(apiPackageSettings1.Ceremony.DeclineReasons[0], sdkPackageSettings1.DeclineReasons[0]);
            Assert.AreEqual(apiPackageSettings1.Ceremony.DeclineReasons[1], sdkPackageSettings1.DeclineReasons[1]);
            Assert.AreEqual(apiPackageSettings1.Ceremony.DeclineReasons[2], sdkPackageSettings1.DeclineReasons[2]);
            Assert.AreEqual(apiPackageSettings1.Ceremony.DisableDeclineOther, sdkPackageSettings1.DisableDeclineOther);
            Assert.AreEqual(apiPackageSettings1.Ceremony.OptOutReasons[0], sdkPackageSettings1.OptOutReasons[0]);
            Assert.AreEqual(apiPackageSettings1.Ceremony.OptOutReasons[1], sdkPackageSettings1.OptOutReasons[1]);
            Assert.AreEqual(apiPackageSettings1.Ceremony.OptOutReasons[2], sdkPackageSettings1.OptOutReasons[2]);
            Assert.AreEqual(apiPackageSettings1.Ceremony.DisableOptOutOther, sdkPackageSettings1.DisableOptOutOther);
            Assert.AreEqual(apiPackageSettings1.Ceremony.HandOver.Href, sdkPackageSettings1.LinkHref);
            Assert.AreEqual(apiPackageSettings1.Ceremony.HandOver.Text, sdkPackageSettings1.LinkText);
            Assert.AreEqual(apiPackageSettings1.Ceremony.HandOver.Title, sdkPackageSettings1.LinkTooltip);
            Assert.AreEqual(apiPackageSettings1.Ceremony.HideCaptureText, sdkPackageSettings1.HideCaptureText);
            Assert.AreEqual(apiPackageSettings1.Ceremony.HideWatermark, sdkPackageSettings1.HideWatermark);
            Assert.AreEqual(apiPackageSettings1.Ceremony.MaxAuthFailsAllowed, sdkPackageSettings1.MaxAuthAttempts);
            Assert.AreEqual(apiPackageSettings1.Ceremony.Layout.Header.GlobalActions.Download, sdkPackageSettings1.CeremonyLayoutSettings.ShowGlobalDownloadButton);
            Assert.AreEqual(apiPackageSettings1.Ceremony.Layout.Header.GlobalActions.Confirm, sdkPackageSettings1.CeremonyLayoutSettings.ShowGlobalConfirmButton);
            Assert.AreEqual(apiPackageSettings1.Ceremony.Layout.Header.GlobalActions.SaveAsLayout, sdkPackageSettings1.CeremonyLayoutSettings.ShowGlobalSaveAsLayoutButton);
            Assert.AreEqual(apiPackageSettings1.Ceremony.HideLanguageDropdown, !sdkPackageSettings1.ShowLanguageDropDown);
            Assert.AreEqual(apiPackageSettings1.Ceremony.HidePackageOwnerInPerson, !sdkPackageSettings1.ShowOwnerInPersonDropDown);
            Assert.AreEqual(apiPackageSettings1.Ceremony.DisableFirstInPersonAffidavit, !sdkPackageSettings1.EnableFirstAffidavit);
            Assert.AreEqual(apiPackageSettings1.Ceremony.DisableSecondInPersonAffidavit, !sdkPackageSettings1.EnableSecondAffidavit);
            Assert.AreEqual(apiPackageSettings1.Ceremony.DefaultTimeBasedExpiry, sdkPackageSettings1.DefaultTimeBasedExpiry);
            Assert.AreEqual(apiPackageSettings1.Ceremony.RemainingDays, sdkPackageSettings1.RemainingDays);
            Assert.AreEqual(apiPackageSettings1.Ceremony.MaxAttachmentFiles, sdkPackageSettings1.MaxAttachmentFiles);
            Assert.AreEqual(apiPackageSettings1.IntegrationFrameworkWorkflows[0].Name, sdkPackageSettings1.IntegrationFrameworkWorkflows[0].Name);
            Assert.AreEqual(apiPackageSettings1.IntegrationFrameworkWorkflows[0].Connector.ToString(), sdkPackageSettings1.IntegrationFrameworkWorkflows[0].Connector.ToString());
        }

        private OneSpanSign.Sdk.DocumentPackageSettings CreateTypicalSDKDocumentPackageSettings()
        {
            OneSpanSign.Sdk.DocumentPackageSettings sdkDocumentPackageSettings = DocumentPackageSettingsBuilder.NewDocumentPackageSettings()
                    .WithInPerson()
                    .WithAda()
                    .WithFontSize(36)
                    .WithEnforceCaptureSignature()
                    .WithoutDecline()
                    .WithOptOut()
                    .WithoutWatermark()
                    .WithoutCaptureText()
                    .WithoutLeftMenuExpand()
                    .DisableFirstAffidavit()
                    .DisableSecondAffidavit()
                    .HideOwnerInPersonDropDown()
                    .WithoutLanguageDropDown()
                    .WithDeclineReason("Decline reason One")
                    .WithDeclineReason( "Decline reason Two" )
                    .WithDeclineReason( "Decline reason Three" )
                    .WithoutDeclineOther()
                    .WithOptOutReason("Reason One")
                    .WithOptOutReason( "Reason Two" )
                    .WithOptOutReason( "Reason Three" )
                    .WithoutOptOutOther()
                    .WithHandOverLinkHref("http://www.google.ca")
                    .WithHandOverLinkText( "click here" )
                    .WithHandOverLinkTooltip( "link tooltip" )
                    .WithDialogOnComplete()
                    .WithDefaultTimeBasedExpiry ()
                    .WithRemainingDays(14)
                    .WithMaxAttachmentFiles(2)
                    .WithCeremonyLayoutSettings(CeremonyLayoutSettingsBuilder.NewCeremonyLayoutSettings()
                        .WithoutGlobalDownloadButton()
                        .WithoutGlobalConfirmButton()
                        .WithoutGlobalSaveAsLayoutButton()
                        )
                    .WithIfWorkflow(IntegrationFrameworkWorkflowBuilder.NewIntegrationFrameworkWorkflow()
                        .WithName("SFTP")
                        .WithConnector(OneSpanSign.Sdk.Connector.SFTP)
                        .Build())
                    .Build();

            return sdkDocumentPackageSettings;
        }

        private OneSpanSign.API.PackageSettings CreateTypicalAPIPackageSettings()
        {
            OneSpanSign.API.CeremonySettings apiCeremonySettings = new OneSpanSign.API.CeremonySettings();

            apiCeremonySettings.InPerson = false;
            apiCeremonySettings.Ada = true;
            apiCeremonySettings.FontSize = 9;
            apiCeremonySettings.DeclineButton = true;
            apiCeremonySettings.OptOutButton = true;

            apiCeremonySettings.AddDeclineReason("Decline reason one");
            apiCeremonySettings.AddDeclineReason("Decline reason two");
            apiCeremonySettings.AddDeclineReason("Decline reason three");

            apiCeremonySettings.AddOptOutReason("Opt out reason one");
            apiCeremonySettings.AddOptOutReason("Opt out reason two");
            apiCeremonySettings.AddOptOutReason("Opt out reason three");

            OneSpanSign.API.Link link = new OneSpanSign.API.Link();
            link.Href = "http://www.google.ca";
            link.Text = "click here";
            apiCeremonySettings.HandOver = link;

            apiCeremonySettings.HideCaptureText = true;
            apiCeremonySettings.HideWatermark = true;
            apiCeremonySettings.MaxAuthFailsAllowed = 3;

            apiCeremonySettings.DisableFirstInPersonAffidavit = true;
            apiCeremonySettings.DisableSecondInPersonAffidavit = true;
            apiCeremonySettings.HideLanguageDropdown = true;
            apiCeremonySettings.HidePackageOwnerInPerson = true;
            apiCeremonySettings.EnforceCaptureSignature = true;
            apiCeremonySettings.LeftMenuExpand = true;
            apiCeremonySettings.MaxAttachmentFiles = 2;

            Style style = new Style();
            style.BackgroundColor = "white";
            style.Color = "blue";

            LayoutStyle layoutStyle = new LayoutStyle();
            layoutStyle.Dialog = style;

            apiCeremonySettings.Style = layoutStyle;

            LayoutOptions layoutOptions = new LayoutOptions();
            layoutOptions.Iframe = false;
            apiCeremonySettings.Layout = layoutOptions;

            apiCeremonySettings.DefaultTimeBasedExpiry = true;
            apiCeremonySettings.RemainingDays = 9;

            HeaderOptions headerOptions = new HeaderOptions();
            headerOptions.Breadcrumbs = true;
            headerOptions.Feedback = true;

            GlobalActionsOptions globalActionsOptions = new GlobalActionsOptions();
            globalActionsOptions.Confirm = true;
            globalActionsOptions.Download = true;
            globalActionsOptions.SaveAsLayout = true;

            headerOptions.GlobalActions = globalActionsOptions;
            layoutOptions.Header = headerOptions;
            OneSpanSign.API.IntegrationFrameworkWorkflow ifWorkflow = new OneSpanSign.API.IntegrationFrameworkWorkflow();
            ifWorkflow.Name = "SFTP";
            ifWorkflow.Connector = OneSpanSign.API.Connector.SFTP;
            List<OneSpanSign.API.IntegrationFrameworkWorkflow> apiIntegrationFrameworkWorkflows = new List<OneSpanSign.API.IntegrationFrameworkWorkflow>();
            apiIntegrationFrameworkWorkflows.Add(ifWorkflow);

            OneSpanSign.API.PackageSettings apiPackageSettings = new OneSpanSign.API.PackageSettings();
            apiPackageSettings.Ceremony = apiCeremonySettings;
            apiPackageSettings.IntegrationFrameworkWorkflows = apiIntegrationFrameworkWorkflows;

            return apiPackageSettings;
        }
    }
}

