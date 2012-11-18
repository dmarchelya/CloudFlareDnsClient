using System;
using System.Linq;
using System.Reflection;

namespace Bespoke.CloudFlareDnsClient
{
	internal static class EnumerationUtility
	{
		public static string GetStringValue(Enum enumeration)
		{
			Type type = enumeration.GetType();

			MemberInfo[] memberInfo = type.GetMember(enumeration.ToString());

			if (memberInfo.Length == 1)
			{
				object[] attrs = memberInfo.Single().GetCustomAttributes(typeof(StringValueAttribute), false);

				if (attrs.Length == 1)
				{
					return ((StringValueAttribute)attrs.Single()).Value;
				}
			}

			return enumeration.ToString();
		}
	}
}
