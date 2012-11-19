using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Bespoke.CloudFlareDnsClient.Model
{
	public class DnsRecord
	{
		[JsonProperty(PropertyName = "rec_id")]		
		public int RecordId { get; set; }

		[JsonProperty(PropertyName = "rec_tag")]		
		public string RecordTag { get; set; }

		/// <summary>
		/// Zone Name
		/// </summary>
		[JsonProperty(PropertyName = "zone_name")]				
		public string DomainName { get; set; }

		[JsonProperty(PropertyName = "name")]
		public string RecordName { get; set; }

		[JsonProperty(PropertyName = "display_name")]		
		public string DisplayName { get; set; }

		[JsonProperty(PropertyName = "type")]		
		public string RecordType { get; set; }

		[JsonProperty(PropertyName = "prio")]		
		public string Priority { get; set; }

		[JsonProperty(PropertyName = "content")]		
		public string RecordContent { get; set; }

		[JsonProperty(PropertyName = "display_content")]		
		public string DisplayContent { get; set; }

		public int Ttl { get; set; }

		[JsonProperty(PropertyName = "ttl_ceil")]		
		public int TtlCeiling { get; set; }

		[JsonProperty(PropertyName = "ssl_id")]		
		public string SslId { get; set; }

		[JsonProperty(PropertyName = "ssl_status")]		
		public string SslStatus { get; set; }

		[JsonProperty(PropertyName = "ssl_expires_on")]		
		public string SslExpiresOn { get; set; }

		[JsonProperty(PropertyName = "auto_ttl")]		
		public bool AutoTtl { get; set; }

		[JsonProperty(PropertyName = "service_mode")]		
		public int ServiceMode { get; set; }

		[JsonProperty(PropertyName = "props")]				
		public DnsRecordProperties Properties { get; set; }

		public class DnsRecordProperties
		{			
			public bool Proxiable { get; set; }

			[JsonProperty(PropertyName = "cloud_on")]		
			public bool CloudOn { get; set; }

			[JsonProperty(PropertyName = "cf_open")]		
			public bool CloudFlareOpen { get; set; }
			
			public bool Ssl { get; set; }

			[JsonProperty(PropertyName = "expired_ssl")]		
			public bool ExpiredSsl { get; set; }

			[JsonProperty(PropertyName = "expiring_ssl")]		
			public bool ExpiringSsl { get; set; }

			[JsonProperty(PropertyName = "pending_ssl")]		
			public bool PendingSsl { get; set; }

			[JsonProperty(PropertyName = "vanity_ssl")]		
			public bool VanityLock { get; set; }
		}
	}
}
