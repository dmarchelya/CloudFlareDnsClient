using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bespoke.CloudFlareDnsClient.Model
{
	public class DnsRecordsHttpResponse : CloudFlareHttpResponseBase
	{
		public Request Request { get; set; }

		[JsonProperty(PropertyName = "response")]
		public DnsRecordsResponse Response { get; set; }
	}
}
