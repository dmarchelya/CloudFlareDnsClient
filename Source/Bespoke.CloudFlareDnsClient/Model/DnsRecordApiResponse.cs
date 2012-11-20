using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bespoke.CloudFlareDnsClient.Model
{
	public class DnsRecordApiResponse : CloudFlareApiResponseBase
	{
		public Request Request { get; set; }

		[JsonProperty(PropertyName = "response")]
		public DnsRecordResponse Response { get; set; }
	}
}
