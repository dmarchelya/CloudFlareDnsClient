using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bespoke.CloudFlareDnsClient
{
	public enum ErrorCode
	{
		/// <summary>
		/// Error Code that is not defined here.
		/// </summary>
		Unknown = -1,

		/// <summary>
		/// Authentication could not be completed.
		/// </summary>
		[StringValue("E_UNAUTH")]
		AuthenticationFailed = 1,

		/// <summary>
		/// Some other input was not valid.
		/// </summary>
		[StringValue("E_INVLDINPUT")]
		InvalidInput = 2,

		/// <summary>
		/// You have exceeded your allowed number of API calls.
		/// </summary>
		[StringValue("E_MAXAPI")]
		ApiCallAllowanceExceeded = 3
	}
}
