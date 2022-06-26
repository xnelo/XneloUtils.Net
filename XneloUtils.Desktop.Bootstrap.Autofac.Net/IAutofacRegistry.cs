#region Copyright (c) 2022 Spencer Hoffa
// \file IAutofacRegistry.cs
// \author Spencer Hoffa
// \copyright \link LICENSE.md MIT License\endlink 2022 Spencer Hoffa 
#endregion

using Autofac;

namespace XneloUtils.Desktop.Bootstrap.Autofac.Net
{
	public interface IAutofacRegistry
	{
		void Register(ContainerBuilder builder);
	}
}
