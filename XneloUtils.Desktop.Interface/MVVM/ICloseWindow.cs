
#region Copyright (c) 2022 Spencer Hoffa
// \file ICloseWindow.cs
// \author Spencer Hoffa
// \copyright \link LICENSE.md MIT License\endlink 2022 Spencer Hoffa 
#endregion


using System;

namespace XneloUtils.Desktop.Interface.MVVM
{
	public interface ICloseWindow
	{
		Action Close { get; set; }
	}
}
