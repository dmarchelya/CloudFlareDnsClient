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
		public void CanRetrieveAllDnsRecord()
		{
			string domain = "";

			var client = new Client(apiKey, email);

			var response = client.RetrieveAllDnsRecords(domain);

			Assert.IsTrue(response.Success);
		}

		[Test]
		public void NoCredsReturnsAuthenticationFailedErrorCode()
		{
			string domain = "bogus.domain";

			var client = new Client("", "");

			var response = client.RetrieveAllDnsRecords(domain);

			Assert.AreEqual(ErrorCode.AuthenticationFailed, response.ErrorCodeType);
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

		[Ignore("Needs API Key / Email / Parameters")]
		[Test]
		public void CanCreateDnsRecord()
		{
			string domain = "";
			string recordName = "";
			string ipAddress = "127.0.0.1";

			var client = new Client(apiKey, email);

			var record = client.AddDnsRecord(domain, recordName, DnsRecordType.A, ipAddress, enableCloudFront: false);

			Assert.IsTrue(record.Success);
		}

		[Ignore("Needs API Key / Email / Parameters")]
		[Test]
		public void CanDeleteDnsRecord()
		{
			string domain = "";
			string recordName = "";
			string ipAddress = "127.0.0.1";

			var client = new Client(apiKey, email);

			var id = client.GetDnsRecordId(domain, recordName, DnsRecordType.A);

			var record = client.DeleteDnsRecord(id, domain);

			Assert.IsTrue(record.Success);
		}
	}
}
