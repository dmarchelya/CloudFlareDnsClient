using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using Bespoke.CloudFlareDnsClient.Model;
using NLog;
using Newtonsoft.Json;

namespace Bespoke.CloudFlareDnsClient
{
	//http://www.cloudflare.com/docs/client-api.html

	public class Client : ClientBase
	{
		private Logger logger = LogManager.GetCurrentClassLogger();
		private CloudFlareCredentials credentials;

		#region ctors

		private Client()
		{
		}

		public Client(CloudFlareCredentials credentials)
		{
			this.credentials = credentials;
		}

		public Client(string apiKey, string emailAddress)
		{
			this.credentials = new CloudFlareCredentials(apiKey, emailAddress);
		}

		#endregion ctors

		/// <summary>
		/// rec_load_all
		/// </summary>
		/// <param name="domainName"></param>
		/// <returns></returns>
		public DnsRecordsApiResponse RetrieveAllDnsRecords(string domainName)
		{
			try
			{
				var postData = new HttpPostDataCollection()
			               	{
			               		{ApiParameter.DomainName, domainName}
			               	};

				var request = CreatePostHttpWebRequest(credentials, ApiAction.RetrieveDnsRecords, postData);

				var response = GetResponse<DnsRecordsApiResponse>(request);

				return response;
			}
			catch (Exception ex)
			{
				logger.Error(ex);
				return null;
			}
		}

		public CloudFlareApiResponseBase EditDnsRecord(string domainName, string dnsRecordName, DnsRecordType dnsRecordType,
								  string dnsRecordContent, string ttl = "1", bool enableCloudFront = true)
		{
			return EditDnsRecord(null, domainName, dnsRecordName, dnsRecordType, dnsRecordContent);
		}

		/// <summary>
		/// rec_edit
		/// </summary>
		/// <param name="dnsRecordId"></param>
		/// <param name="domainName"></param>
		/// <param name="dnsRecordName"></param>
		/// <param name="dnsRecordType"></param>
		/// <param name="dnsRecordContent"></param>
		/// <param name="ttl">1 = auto</param>
		/// <param name="enableCloudFront"></param>
		/// <returns></returns>
		public CloudFlareApiResponseBase EditDnsRecord(string dnsRecordId, string domainName, string dnsRecordName, DnsRecordType dnsRecordType,
								  string dnsRecordContent, string ttl = "1", bool enableCloudFront = true)
		{
			try
			{
				if(string.IsNullOrWhiteSpace(dnsRecordId))
				{
					dnsRecordId = GetDnsRecordId(domainName, dnsRecordName, dnsRecordType);
				}

				var postData = new HttpPostDataCollection()
			               	{
			               		{ApiParameter.DnsRecordId, dnsRecordId},
			               		{ApiParameter.DomainName, domainName},
			               		{ApiParameter.DnsRecordName, dnsRecordName},
			               		{ApiParameter.DnsRecordType, EnumerationUtility.GetStringValue(dnsRecordType)},
			               		{ApiParameter.DnsRecordContent, dnsRecordContent},
			               		{ApiParameter.Ttl, ttl},
			               		{ApiParameter.ServiceMode, enableCloudFront ? "1" : "0"}
			               	};

				var request = CreatePostHttpWebRequest(credentials, ApiAction.EditDnsRecord, postData);

				var response = GetResponse<DnsRecordApiResponse>(request);

				return response;
			}
			catch (Exception ex)
			{
				logger.Error(ex);
				return null;
			}
		}

		private string GetDnsRecordId(string domainName, string dnsRecordName, DnsRecordType recordType)
		{
			var apiResponse = RetrieveAllDnsRecords(domainName);

			foreach (var record in apiResponse.Response.DnsRecords.DnsRecordsObject)
			{
				if (record.RecordName == dnsRecordName && record.RecordType == EnumerationUtility.GetStringValue(recordType))
				{
					return record.RecordId.ToString();
				}
			}

			return null; //No record found
		}
	}
}