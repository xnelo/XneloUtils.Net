#region Copyright (c) 2022 Spencer Hoffa
// \file BootStrapperConfig.cs
// \author Spencer Hoffa
// \copyright \link LICENSE.md MIT License\endlink 2022 Spencer Hoffa 
#endregion

using System.Collections.Generic;

namespace XneloUtils.Desktop.Bootstrap.Autofac
{
	public class BootStrapperConfig
	{
		public List<string> AssembliesToLoad { get; set; } = new List<string>();
	}
}
