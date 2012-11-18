using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bespoke.CloudFlareDnsClient
{
	public class CloudFlareCredentials
	{
		public CloudFlareCredentials(string apiKey, string emailAddress)
		{
			ApiKey = apiKey;
			EmailAddress = emailAddress;
		}

		public string ApiKey { get; private set; }

		public string EmailAddress { get; private set; }
	}
}
