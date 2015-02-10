/**
 * Created by whou on 05/12/14.
 */
namespace Silanis.ESL.SDK
{
	public class ProxyConfiguration {

		private string httpHost;
		private int httpPort;
		private string httpsHost;
		private int httpsPort;
		private string userName;
		private string password;
		private Scheme scheme;
		private bool credentials;

		public ProxyConfiguration(){
			httpHost = null;
			httpPort = 0;
			httpsHost = null;
			httpsPort = 0;
			userName = null;
			password = null;
			scheme = 0;
			credentials = false;
		}

		public string GetHttpHost() {
			return httpHost;
		}
		public void SetHttpHost(string httpHost) {
			this.httpHost = httpHost;
		}

		public int GetHttpPort() {
			return httpPort;
		}
		public void SetHttpPort(int httpPort) {
			this.httpPort = httpPort;
		}

		public string GetHttpsHost() {
			return httpsHost;
		}
		public void SetHttpsHost(string httpsHost) {
			this.httpsHost = httpsHost;
		}

		public int GetHttpsPort() {
			return httpsPort;
		}
		public void SetHttpsPort(int httpsPort) {
			this.httpsPort = httpsPort;
		}

		public string GetUserName() {
			return userName;
		}
		public void SetUserName(string userName) {
			this.userName = userName;
		}

		public string GetPassword() {
			return password;
		}
		public void SetPassword(string password) {
			this.password = password;
		}

		public string GetScheme() {
            if (scheme == Scheme.http)
                return "http";
            else
                return "https";
		}

		public bool HasCredentials() {
			return credentials;
		}
		public void SetCredentials(bool credentials) {
			this.credentials = credentials;
		}

		public string GetHost() {
			if(IsHttp()){
				return httpHost;
			}
			else{
				return httpsHost;
			}
		}

		public int GetPort() {
			if(IsHttp()){
				return httpPort;
			}
			else{
				return httpsPort;
			}
		}

		private enum Scheme{
			http, https
		}

		public void SetHttpScheme() {
			scheme = Scheme.http;
		}

		public void SetHttpsScheme() {
			scheme = Scheme.https;
		}

		private bool IsHttp() {
			return scheme == Scheme.http;
		}
	}
}