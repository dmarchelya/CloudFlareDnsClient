using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Bespoke.CloudFlareDnsClient.Tests
{
	[TestFixture]
	public class ClientTests
	{
		private const string apiKey = "";
		private const string email = "";

		[Ignore("Needs API Key / Email")]
		[Test]
		public void CanRetrieveDnsRecord()
		{
			string domain = "";

			var client = new Client(apiKey, email);

			client.RetrieveDnsRecords(domain);
		}

		[Ignore("Needs API Key / Email / Parameters")]
		[Test]
		public void CanEditDnsRecord()
		{
			string recordId = "";
			string domain = "";
			string recordName = "";
			string ipAddress = "127.0.0.1";

			var client = new Client(apiKey, email);

			client.EditDnsRecord(recordId, domain, recordName, DnsRecordType.A, ipAddress, enableCloudFront:false);
		}
	}
}
