using Newtonsoft.Json;

namespace Bespoke.CloudFlareDnsClient
{
	public class CloudFlareApiResponseBase
	{
		public string Result { get; set; }

		[JsonProperty(PropertyName = "msg")]
		public string Message { get; set; }

		[JsonProperty(PropertyName = "err_code")]
		public string ErrorCode { get; set; }

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
