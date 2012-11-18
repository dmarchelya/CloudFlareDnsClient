using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bespoke.CloudFlareDnsClient
{
	internal enum ApiAction
	{
		[StringValue("rec_new")]
		AddDnsRecord,

		[StringValue("rec_edit")]
		EditDnsRecord,

		[StringValue("rec_delete")]
		DeleteDnsRecord,

		[StringValue("rec_load_all")]
		RetrieveDnsRecords
	}
}