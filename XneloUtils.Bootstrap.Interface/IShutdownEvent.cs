#region Copyright (c) 2022 Spencer Hoffa
// \file IShutdownEvent.cs
// \author Spencer Hoffa
// \copyright \link LICENSE.md MIT License\endlink 2022 Spencer Hoffa 
#endregion

namespace XneloUtils.Bootstrap.Interface
{
	public interface IShutdownEvent
	{
		void OnShutdown();
	}
}
