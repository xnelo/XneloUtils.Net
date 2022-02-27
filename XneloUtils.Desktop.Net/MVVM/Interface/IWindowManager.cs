#region Copyright (c) 2022 Spencer Hoffa
// \file IWindowManager.cs
// \author Spencer Hoffa
// \copyright \link LICENSE.md MIT License\endlink 2022 Spencer Hoffa 
#endregion

namespace XneloUtils.Desktop.Net.MVVM.Interface
{
	public interface IWindowManager
	{
		void RegisterWindow<TViewModel, TView>() 
			where TViewModel : IViewModel
			where TView : IWindow;

		/// <summary>
		/// Show a window.
		/// </summary>
		/// <typeparam name="TViewModel"></typeparam>
		/// <param name="vm">The VM that is the data context for the new window.</param>
		/// <param name="asModal">
		/// Should the window be a modal or 'dialog' window that will halt code execution until window is closed.
		/// </param>
		void ShowWindow<TViewModel>(TViewModel vm, bool asModal);
	}
}
