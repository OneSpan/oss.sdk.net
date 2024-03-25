namespace OneSpanSign.Sdk
{
    public class IntegrationFrameworkWorkflow
    {
	    public string Name { get; set; }

        public Connector Connector { get; set; }
	
	}

    public enum Connector
    {
        TRUST_VAULT, SFTP, IPAAS
    }
}