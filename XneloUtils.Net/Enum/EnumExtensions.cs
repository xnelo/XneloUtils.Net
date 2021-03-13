#region Copyright (c) 2021 Spencer Hoffa
// \file EnumExtensions.cs
// \author Spencer Hoffa
// \copyright \link LICENSE.md MIT License\endlink 2021 Spencer Hoffa 
#endregion

using System.Collections.Generic;
using System.Reflection;

namespace XneloUtils.Net.Enum
{
	public static class EnumExtensions
	{
		public static TEnum GetEnum<TEnum>(this string sVal) where TEnum : System.Enum
		{
			var enumType = typeof(TEnum);

			var fields = enumType.GetFields();
			foreach (var field in fields)
			{
				if (field.MemberType == MemberTypes.Field)
				{
					var atts = field.GetCustomAttributes(typeof(EnumNameAttribute), false);
					if (atts.Length > 0)
					{
						var att = (EnumNameAttribute)atts[0];
						if (att.Name == sVal)
						{
							return (TEnum)field.GetRawConstantValue();
						}
					}
				}
			}

			// try normal name
			foreach (var field in fields)
			{
				if (field.MemberType == MemberTypes.Field)
				{
					if (field.Name == sVal)
					{
						return (TEnum)field.GetRawConstantValue();
					}
				}
			}

			return default;
		}

		public static string GetEnumString<TEnum>(this TEnum val) where TEnum : System.Enum
		{
			string enumString = val.ToString();

			var enumType = typeof(TEnum);
			var emem = enumType.GetMember(enumString);
			var atts = emem[0].GetCustomAttributes(typeof(EnumNameAttribute), false);
			if (atts.Length > 0)
			{
				var att = (EnumNameAttribute)atts[0];
				return att.Name;
			}
			return enumString;
		}
	}
}
