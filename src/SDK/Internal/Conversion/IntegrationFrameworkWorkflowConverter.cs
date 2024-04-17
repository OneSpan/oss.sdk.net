using System;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
    internal class IntegrationFrameworkWorkflowConverter
    {
        public static IntegrationFrameworkWorkflow ToSDK(OneSpanSign.API.IntegrationFrameworkWorkflow apiIfWorkflow)
        {
            if (apiIfWorkflow == null)
            {
                return null;
            }

            IntegrationFrameworkWorkflow sdkIfWorkflow = new IntegrationFrameworkWorkflow();
            sdkIfWorkflow.Name = apiIfWorkflow.Name;
            sdkIfWorkflow.Connector = (Connector)Enum.Parse(typeof(Connector), apiIfWorkflow.Connector.ToString());
            return sdkIfWorkflow;
        }

        public static OneSpanSign.API.IntegrationFrameworkWorkflow ToAPI(IntegrationFrameworkWorkflow sdkIfWorkflow)
        {
            if (sdkIfWorkflow == null)
            {
                return null;
            }

            OneSpanSign.API.IntegrationFrameworkWorkflow apiIfWorkflow = new API.IntegrationFrameworkWorkflow();
            apiIfWorkflow.Name = sdkIfWorkflow.Name;
            apiIfWorkflow.Connector = (OneSpanSign.API.Connector) Enum.Parse(typeof(API.Connector), sdkIfWorkflow.Connector.ToString());
            return apiIfWorkflow;
        }

        public static IList<IntegrationFrameworkWorkflow> ToSDKList(IList<OneSpanSign.API.IntegrationFrameworkWorkflow> integrationFrameworkWorkflows)
        {
            List<IntegrationFrameworkWorkflow> sdkIntegrationFrameworkWorkflows = new List<IntegrationFrameworkWorkflow>();
            foreach (OneSpanSign.API.IntegrationFrameworkWorkflow apiIfWorkflow in integrationFrameworkWorkflows)
                sdkIntegrationFrameworkWorkflows.Add (IntegrationFrameworkWorkflowConverter.ToSDK(apiIfWorkflow));
            return sdkIntegrationFrameworkWorkflows;
        }
    }
}