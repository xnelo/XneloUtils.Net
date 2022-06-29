#region Copyright (c) 2022 Spencer Hoffa
// \file IWindowClosed.cs
// \author Spencer Hoffa
// \copyright \link LICENSE.md MIT License\endlink 2022 Spencer Hoffa 
#endregion

using System.ComponentModel;

namespace XneloUtils.Desktop.Interface.MVVM
{
	/// <summary>
	/// This must be applied to a VM that wants to recieve Closing and/or Closed events from a 
	/// window. When the window sends one of these events the VM will be notified.
	/// </summary>
	public interface IWindowClosed
	{
		/// <summary>
		/// The window is closing. This is where you would check for conditions that call for 
		/// canceling the window closing e.g. unsaved files.
		/// </summary>
		/// <param name="sender">The window that is closing.</param>
		/// <param name="e">Event arguments with ability to cancel closing.</param>
		void Closing(object? sender, CancelEventArgs e);

		/// <summary>
		/// This method is called when the window is closed.
		/// </summary>
		void Closed();
	}
}
