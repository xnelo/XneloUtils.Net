#region Copyright (c) 2022 Spencer Hoffa
// \file IWindow.cs
// \author Spencer Hoffa
// \copyright \link LICENSE.md MIT License\endlink 2022 Spencer Hoffa 
#endregion


using System;

namespace XneloUtils.Desktop.Interface
{
	/// <summary>
	/// This class should be applied to the code behind of a Window. The window
	/// must already inherit from the 'Window' class or you will have to implement
	/// the interface.
	/// </summary>
	public interface IWindow
	{
		void Close();

		object DataContext { get; set; }

		event EventHandler Closed;

		void Show();
		bool? ShowDialog();
	}
}
