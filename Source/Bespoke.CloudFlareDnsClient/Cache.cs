using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using Bespoke.CloudFlareDnsClient.Model;

namespace Bespoke.CloudFlareDnsClient
{
    internal static class Cache
	{
		private static ObjectCache LocalCache
		{
			get { return MemoryCache.Default; }
		}

		internal static List<DnsRecord> DnsRecords
		{
			get { return (List<DnsRecord>)LocalCache["DnsRecords"]; }
			set { LocalCache["DnsRecords"] = value; }
		}
	}
}
