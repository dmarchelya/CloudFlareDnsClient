using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Bespoke.CloudFlareDnsClient
{
	public class HttpPostDataCollection : NameValueCollection
	{
		public override string ToString()
		{
			const string formatString = "{0}={1}&";
			var stringBuilder = new StringBuilder();

			foreach (var key in this.AllKeys)
			{
				//TODO: Url Encode key and value
				stringBuilder.Append(string.Format(formatString, key, this[key]));
			}

			return stringBuilder.ToString().TrimEnd('&');
		}
	}
}
