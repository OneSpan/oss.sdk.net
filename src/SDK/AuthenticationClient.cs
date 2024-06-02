using System;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    public class AuthenticationClient
    {
        private AuthenticationService authenticationService;

        public AuthenticationClient(string webpageUrl) {
            authenticationService = new AuthenticationService(webpageUrl);
        }

        public string GetSessionIdForUserAuthenticationToken(string userAuthenticationToken) {
            return authenticationService.GetSessionIdForUserAuthenticationToken(userAuthenticationToken);
        }

        public string BuildRedirectToDesignerForUserAuthenticationToken(string userAuthenticationToken, PackageId packageId, string profile = null) {
            return authenticationService.BuildRedirectToDesignerForUserAuthenticationToken(userAuthenticationToken, packageId, profile);
        }

        public string GetSessionIdForSenderAuthenticationToken(string senderAuthenticationToken) {
            return authenticationService.GetSessionIdForSenderAuthenticationToken(senderAuthenticationToken);
        }

        public string BuildRedirectToDesignerForSender(string senderAuthenticationToken, PackageId packageId, string profile = null) {
            return authenticationService.BuildRedirectToDesignerForSender(senderAuthenticationToken, packageId, profile);
        }

        public string BuildRedirectToPackageViewForSender(string userAuthenticationToken, PackageId packageId) {
            return authenticationService.BuildRedirectToPackageViewForSender(userAuthenticationToken, packageId);
        }

        public string GetSessionIdForSignerAuthenticationToken(string signerAuthenticationToken) {
            return authenticationService.GetSessionIdForSignerAuthenticationToken(signerAuthenticationToken);
        }

        public string BuildRedirectToSigningForSigner(string signerAuthenticationToken, PackageId packageId) {
            return authenticationService.BuildRedirectToSigningForSigner(signerAuthenticationToken, packageId);
        }
    }
}

