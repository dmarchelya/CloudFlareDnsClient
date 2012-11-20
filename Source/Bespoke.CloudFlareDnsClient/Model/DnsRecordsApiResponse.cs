using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bespoke.CloudFlareDnsClient.Model
{
	public class DnsRecordsApiResponse : CloudFlareApiResponseBase
	{
		public Request Request { get; set; }

		[JsonProperty(PropertyName = "response")]
		public DnsRecordsResponse Response { get; set; }
	}
}
