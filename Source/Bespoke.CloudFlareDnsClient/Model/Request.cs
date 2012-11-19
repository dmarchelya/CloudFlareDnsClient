using Newtonsoft.Json;

namespace Bespoke.CloudFlareDnsClient.Model
{
	public class Request
	{
		[JsonProperty(PropertyName = "act")]
		public string Action { get; set; }

		[JsonProperty(PropertyName = "email")]
		public string EmailAddress { get; set; }

		[JsonProperty(PropertyName = "tkn")]
		public string ApiKey { get; set; }

		[JsonProperty(PropertyName = "z")]
		public string DomainName { get; set; }
	}
}
