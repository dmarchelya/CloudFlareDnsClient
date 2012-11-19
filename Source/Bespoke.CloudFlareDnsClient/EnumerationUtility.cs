using System;
using System.Linq;
using System.Reflection;

namespace Bespoke.CloudFlareDnsClient
{
	internal static class EnumerationUtility
	{
		/// <summary>
		/// Returns the value from the StringValue attribute decorating an enum member, if present.
		/// Otherwise, returns the enum member name.
		/// </summary>
		/// <param name="enumeration"></param>
		/// <returns></returns>
		public static string GetStringValue(Enum enumeration)
		{
			Type type = enumeration.GetType();

			MemberInfo[] memberInfo = type.GetMember(enumeration.ToString());

			if (memberInfo.Length == 1)
			{
				//Get the StringValueAttribute, if it exists, and then return its value.
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
