using System;

namespace Bespoke.CloudFlareDnsClient
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	internal class StringValueAttribute : Attribute
	{
		public StringValueAttribute(string value)
		{
			Value = value;
		}

		public string Value { get; set; }
	}
}
