#region Copyright (c) 2022 Spencer Hoffa
// \file IWindowManager.cs
// \author Spencer Hoffa
// \copyright \link LICENSE.md MIT License\endlink 2022 Spencer Hoffa 
#endregion


namespace XneloUtils.Desktop.Interface.MVVM
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
		void ShowWindow<TViewModel>(TViewModel vm, bool asModal) where TViewModel: IViewModel;

		/// <summary>
		/// Get the window for the passed in view Model.
		/// </summary>
		/// <typeparam name="TViewModel"></typeparam>
		/// <param name="vm">The VM that is the data context for the new window.</param>
		/// <returns>A built window.</returns>
		IWindow GetWindow<TViewModel>(TViewModel vm) where TViewModel : IViewModel;

		/// <summary>
		/// Show a message box with ok only button.
		/// </summary>
		/// <param name="msg">The message to display</param>
		/// <param name="caption">The title to display on the messagebox.</param>
		void ShowOkDialog(string msg, string caption);

		/// <summary>
		/// Close all open windows.
		/// </summary>
		void CloseAllWindows();
	}
}
