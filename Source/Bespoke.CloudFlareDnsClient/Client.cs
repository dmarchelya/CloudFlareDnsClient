using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
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

				if (CachingEnabled && response != null && response.Response != null && response.Response.DnsRecords != null)
					Cache.DnsRecords = response.Response.DnsRecords.DnsRecordsObject;

				return response;
			}
			catch (Exception ex)
			{
				logger.Error(ex);
				throw;
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
								  string dnsRecordContent, string ttl = Constants.AutomaticTtl, bool enableCloudFront = true)
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
				throw;
			}
		}

		/// <summary>
		/// rec_new
		/// </summary>
		/// <param name="domainName"></param>
		/// <param name="dnsRecordName"></param>
		/// <param name="dnsRecordType"></param>
		/// <param name="dnsRecordContent"></param>
		/// <param name="ttl"></param>
		/// <param name="enableCloudFront"></param>
		/// <returns></returns>
		public CloudFlareApiResponseBase AddDnsRecord(string domainName, string dnsRecordName, DnsRecordType dnsRecordType,
								  string dnsRecordContent, string ttl = "1", bool enableCloudFront = true)
		{
			try
			{
				var postData = new HttpPostDataCollection()
			               	{
			               		{ApiParameter.DomainName, domainName},
			               		{ApiParameter.DnsRecordName, dnsRecordName},
			               		{ApiParameter.DnsRecordType, EnumerationUtility.GetStringValue(dnsRecordType)},
			               		{ApiParameter.DnsRecordContent, dnsRecordContent},
			               		{ApiParameter.Ttl, ttl},
			               		{ApiParameter.ServiceMode, enableCloudFront ? "1" : "0"}
			               	};

				var request = CreatePostHttpWebRequest(credentials, ApiAction.AddDnsRecord, postData);

				var response = GetResponse<DnsRecordApiResponse>(request);

				return response;
			}
			catch (Exception ex)
			{
				logger.Error(ex);
				throw;
			}
		}

		/// <summary>
		/// rec_delete
		/// </summary>
		/// <param name="dnsRecordId"></param>
		/// <param name="domainName"></param>
		/// <returns></returns>
		public CloudFlareApiResponseBase DeleteDnsRecord(string dnsRecordId, string domainName)
		{
			try
			{
				var postData = new HttpPostDataCollection()
			               	{
			               		{ApiParameter.DnsRecordId, dnsRecordId},
			               		{ApiParameter.DomainName, domainName},
			               	};

				var request = CreatePostHttpWebRequest(credentials, ApiAction.DeleteDnsRecord, postData);

				var response = GetResponse<DnsRecordApiResponse>(request);

				return response;
			}
			catch (Exception ex)
			{
				logger.Error(ex);
				throw;
			}
		} 

		/// <summary>
		/// Get the RecordId for the given dns record.
		/// Attempts to use the cached value, if available.
		/// </summary>
		/// <param name="domainName"></param>
		/// <param name="dnsRecordName"></param>
		/// <param name="recordType"></param>
		/// <returns></returns>
		public string GetDnsRecordId(string domainName, string dnsRecordName, DnsRecordType recordType)
		{
			string dnsRecordId = GetDnsRecordId(Cache.DnsRecords, domainName, dnsRecordName, recordType);

			if(dnsRecordId == null)
			{
				var apiResponse = RetrieveAllDnsRecords(domainName);

				dnsRecordId = GetDnsRecordId(apiResponse.Response.DnsRecords.DnsRecordsObject, domainName, dnsRecordName, recordType);
			}

			return dnsRecordId;
		}

		private string GetDnsRecordId(List<DnsRecord> dnsRecords, string domainName, string dnsRecordName, DnsRecordType recordType)
		{
			if(dnsRecords != null && dnsRecords.Count > 0)
			{
				foreach (var record in dnsRecords)
				{
					if (record.RecordName == dnsRecordName && record.RecordType == EnumerationUtility.GetStringValue(recordType))
					{
						return record.RecordId.ToString();
					}
				}	
			}

			return null; //No record found
		}
	}
}