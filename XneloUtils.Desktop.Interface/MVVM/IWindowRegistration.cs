#region Copyright (c) 2022 Spencer Hoffa
// \file IWindowRegistration.cs
// \author Spencer Hoffa
// \copyright \link LICENSE.md MIT License\endlink 2022 Spencer Hoffa 
#endregion


namespace XneloUtils.Desktop.Interface.MVVM
{
	public interface IWindowRegistration
	{
		/// <summary>
		/// Register any windows with the manager.
		/// </summary>
		/// <param name="manager"></param>
		void WindowRegistration(IWindowManager manager);
	}
}
