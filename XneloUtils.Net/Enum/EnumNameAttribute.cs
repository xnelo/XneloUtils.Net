#region Copyright (c) 2021 Spencer Hoffa
// \file EnumNameAttribute.cs
// \author Spencer Hoffa
// \copyright \link LICENSE.md MIT License\endlink 2021 Spencer Hoffa 
#endregion

using System;

namespace XneloUtils.Net.Enum
{
	[AttributeUsage(AttributeTargets.Field)]
	public class EnumNameAttribute: Attribute
	{
		public EnumNameAttribute(string name, string description = null)
		{
			Name = name;
			Description = description;
		}

		public string Name { get; private set; }
		public string Description { get; private set; }
	}
}
