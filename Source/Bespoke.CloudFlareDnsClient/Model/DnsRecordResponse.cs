using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Bespoke.CloudFlareDnsClient.Model
{
	public class DnsRecordResponse
	{
		[JsonProperty(PropertyName = "rec")]
		public Record DnsRecord { get; set; }

		public class Record
		{
			[JsonProperty(PropertyName = "obj")]
			public DnsRecord DnsRecordObject { get; set; }
		}
	}
}
