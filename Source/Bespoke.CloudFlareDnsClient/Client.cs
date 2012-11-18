using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using NLog;

namespace Bespoke.CloudFlareDnsClient
{
	//http://www.cloudflare.com/docs/client-api.html

	public class Client
	{
		private const string cloudFlareApiUrl = "https://www.cloudflare.com/api_json.html";
		private const string formUrlEndodedContentType = "application/x-www-form-urlencoded";

		private Logger logger = LogManager.GetCurrentClassLogger();
		private CloudFlareCredentials credentials;
	
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

		public bool RetrieveDnsRecords(string domainName)
		{
			try
			{
				var postData = new HttpPostDataCollection()
				               	{
				               		{ ApiParameter.DomainName, domainName }
								};

				var request = CreatePostHttpWebRequest(credentials, ApiAction.RetrieveDnsRecords, postData);

				using (var response = request.GetResponse())
				{
					string responseBody = string.Empty;
					using (var streamReader = new StreamReader(response.GetResponseStream()))
					{
						responseBody = streamReader.ReadToEnd();

						logger.Info(responseBody);

						//TODO: Build out model from results
						//TODO: Inspect for error and throw appropriate exception.

						return true;
					}
				}
			}
			catch (Exception ex)
			{
				logger.Error(ex);

				return false;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dnsRecordId"></param>
		/// <param name="domainName"></param>
		/// <param name="dnsRecordName"></param>
		/// <param name="dnsRecordType"></param>
		/// <param name="dnsRecordContent"></param>
		/// <param name="ttl">1 = auto</param>
		/// <param name="enableCloudFront"></param>
		/// <returns></returns>
		public bool EditDnsRecord(string dnsRecordId, string domainName, string dnsRecordName, DnsRecordType dnsRecordType, string dnsRecordContent, string ttl = "1", bool enableCloudFront = true)
		{
			try
			{
				var postData = new HttpPostDataCollection()
				               	{
				               		{ ApiParameter.DnsRecordId, dnsRecordId },
				               		{ ApiParameter.DomainName, domainName },
				               		{ ApiParameter.DnsRecordName, dnsRecordName },
									{ ApiParameter.DnsRecordType, EnumerationUtility.GetStringValue(dnsRecordType) },
									{ ApiParameter.DnsRecordContent, dnsRecordContent},
									{ ApiParameter.Ttl, ttl },
									{ ApiParameter.ServiceMode, enableCloudFront ? "1" : "0"}
								};

				var request = CreatePostHttpWebRequest(credentials, ApiAction.EditDnsRecord, postData);

				using(var response = request.GetResponse())
				{
					string responseBody = string.Empty;
					using (var streamReader = new StreamReader(response.GetResponseStream()))
					{
						responseBody = streamReader.ReadToEnd();
						
						logger.Info(responseBody);

						//TODO: Inspect response for success
						//TODO: Inspect for error and throw appropriate exception.

						return true;
					}	
				}
			}
			catch (Exception ex)
			{
				logger.Error(ex);

				return false;
			}
		}

		private HttpWebRequest CreatePostHttpWebRequest(CloudFlareCredentials credentials, ApiAction action, HttpPostDataCollection postDataCollection)
		{
			var request = (HttpWebRequest) HttpWebRequest.Create(cloudFlareApiUrl);
			request.ContentType = formUrlEndodedContentType;
			request.Method = WebRequestMethods.Http.Post;

			if (postDataCollection != null && postDataCollection.Count > 0)
			{
				postDataCollection = AppendApiActionToPostDataCollection(postDataCollection, action);
				postDataCollection = AppendCredentialsToPostDataCollection(postDataCollection, credentials);
				
				var postData = postDataCollection.ToString();
				var postDataStream = Encoding.UTF8.GetBytes(postData);

				request.ContentLength = postData.Length;

				using (var requestStream = request.GetRequestStream())
				{
					requestStream.Write(postDataStream, 0, postDataStream.Length);
				}
			}

			return request;
		}

		private HttpPostDataCollection AppendApiActionToPostDataCollection(HttpPostDataCollection postDataCollection, ApiAction action)
		{
			postDataCollection.Add("a", EnumerationUtility.GetStringValue(action));
			return postDataCollection;
		}

		private HttpPostDataCollection AppendCredentialsToPostDataCollection(HttpPostDataCollection postDataCollection, CloudFlareCredentials credentials)
		{
			postDataCollection.Add("tkn", credentials.ApiKey);
			postDataCollection.Add("email", credentials.EmailAddress);

			return postDataCollection;
		}
	}
}
