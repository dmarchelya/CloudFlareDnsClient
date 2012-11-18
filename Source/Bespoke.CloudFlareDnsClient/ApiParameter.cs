using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bespoke.CloudFlareDnsClient
{
	public static class ApiParameter
	{
		/// <summary>
		/// The target domain
		/// </summary>
		public const string DomainName = "z"; //"zone"

		/// <summary>
		/// DNS Record ID. Available by using the rec_load_all call.
		/// </summary>
		public const string DnsRecordId = "id";

		/// <summary>
		/// Type of DNS record. Values include: [A/CNAME/MX/TXT/SPF/AAAA/NS/SRV/LOC]
		/// </summary>
		public const string DnsRecordType = "type";

		/// <summary>
		/// Name of the DNS record.
		/// </summary>
		public const string DnsRecordName = "name";

		/// <summary>
		/// The content of the DNS record, will depend on the the type of record being added.
		/// </summary>
		public const string DnsRecordContent = "content";
		
		/// <summary>
		/// TTL of record in seconds. 1 = Automatic, otherwise, value must in between 120 and 4,294,967,295 seconds.
		/// </summary>
		public const string Ttl = "ttl";

		/// <summary>
		/// Status of CloudFlare Proxy, 1 = orange cloud, 0 = grey cloud. 
		/// </summary>
		public const string ServiceMode = "service_mode";

		/// <summary>
		/// MX record priority.
		/// </summary>
		public const string DnsRecordPriority = "prio";
	}
}
