using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Bespoke.CloudFlareDnsClient.Model
{
	public class DnsRecordsResponse
	{
		[JsonProperty(PropertyName = "recs")]		
		public Records DnsRecords { get; set; }

		public class Records
		{
			[JsonProperty(PropertyName = "has_more")]
			public bool HasMore { get; set; }

			public int Count { get; set; }

			[JsonProperty(PropertyName = "objs")]
			public List<DnsRecord> DnsRecordsObject { get; set; }
		}
	}
}
