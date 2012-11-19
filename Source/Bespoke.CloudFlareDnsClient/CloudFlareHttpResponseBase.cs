using Newtonsoft.Json;

namespace Bespoke.CloudFlareDnsClient
{
	public class CloudFlareHttpResponseBase
	{
		public string Result { get; internal set; }

		[JsonProperty(PropertyName = "msg")]
		public string Message { get; internal set; }

		[JsonProperty(PropertyName = "err_code")]
		public string ErrorCode { get; internal set; }

		#region Bespoke Properties

		public bool Success
		{
			get
			{
				return Result != null && Result.ToLower() == "success";
			}
		}

		public string ResponseXmlString { get; internal set; }

		public ErrorCode? ErrorCodeType { get; internal set; }

		#endregion Bespoke Properties
	}
}
