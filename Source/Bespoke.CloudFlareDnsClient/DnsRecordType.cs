using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bespoke.CloudFlareDnsClient
{
	public enum DnsRecordType
	{
		[StringValue("A")]
		A,

		[StringValue("CNAME")]
		CName,

		[StringValue("MX")]
		MX,

		[StringValue("TXT")]
		Txt,

		[StringValue("SPF")]
		Spf,

		[StringValue("AAAA")]
		Aaaa,

		[StringValue("NS")]
		NS,

		[StringValue("SRV")]
		Srv,

		[StringValue("LOC")]
		Loc
	}
}
