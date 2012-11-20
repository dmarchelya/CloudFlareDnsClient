using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Caching;
using System.Text;
using Bespoke.CloudFlareDnsClient.Model;
using NLog;
using Newtonsoft.Json;

namespace Bespoke.CloudFlareDnsClient
{
	public abstract class ClientBase
	{
		private const string cloudFlareApiUrl = "https://www.cloudflare.com/api_json.html";
		private const string formUrlEndodedContentType = "application/x-www-form-urlencoded";

		private Logger logger = LogManager.GetCurrentClassLogger();
		private bool cachingEnabled = true;

		public T GetResponse<T>(HttpWebRequest request)
			where T : CloudFlareApiResponseBase
		{
			try
			{
				using (var response = request.GetResponse())
				{
					var responseStream = response.GetResponseStream();

					using (var streamReader = new StreamReader(responseStream))
					{
						var responseString = streamReader.ReadToEnd();
						logger.Info(responseString);

						var responseObject = BuildResponse<T>(responseString);

						return responseObject;
					}
				}
			}
			catch (Exception ex)
			{
				logger.Error(ex);
				throw;
			}
		}

		private static T BuildResponse<T>(string responseString) where T : CloudFlareApiResponseBase
		{
			var response = JsonConvert.DeserializeObject<T>(responseString);
			response.ResponseXmlString = responseString;

			if (!string.IsNullOrWhiteSpace(response.ErrorCode))
			{
				SetErrorCodeType(response);

				//TODO: possibly throw an exception at this point
			}

			return response;
		}

		private static void SetErrorCodeType<T>(T response)
			where T : CloudFlareApiResponseBase
		{
			//Defaulting to unknown, we override this if a match is found.
			response.ErrorCodeType = ErrorCode.Unknown;

			foreach (ErrorCode errorCode in Enum.GetValues(typeof(ErrorCode)))
			{
				var value = EnumerationUtility.GetStringValue(errorCode);

				if (response.ErrorCode == value)
				{
					response.ErrorCodeType = errorCode;
					break;
				}
			}
		}

		internal HttpWebRequest CreatePostHttpWebRequest(CloudFlareCredentials credentials, ApiAction action, HttpPostDataCollection postDataCollection)
		{
			var request = (HttpWebRequest)HttpWebRequest.Create(cloudFlareApiUrl);
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

		internal HttpPostDataCollection AppendApiActionToPostDataCollection(HttpPostDataCollection postDataCollection, ApiAction action)
		{
			postDataCollection.Add("a", EnumerationUtility.GetStringValue(action));
			return postDataCollection;
		}

		internal HttpPostDataCollection AppendCredentialsToPostDataCollection(HttpPostDataCollection postDataCollection, CloudFlareCredentials credentials)
		{
			postDataCollection.Add("tkn", credentials.ApiKey);
			postDataCollection.Add("email", credentials.EmailAddress);

			return postDataCollection;
		}

		/// <summary>
		/// Default is true.
		/// </summary>
		protected bool CachingEnabled
		{
			get { return cachingEnabled; }
			set { cachingEnabled = value; }
		}
	}
}
