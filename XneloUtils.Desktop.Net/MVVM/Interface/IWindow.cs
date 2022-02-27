#region Copyright (c) 2022 Spencer Hoffa
// \file IWindow.cs
// \author Spencer Hoffa
// \copyright \link LICENSE.md MIT License\endlink 2022 Spencer Hoffa 
#endregion

namespace XneloUtils.Desktop.Net.MVVM.Interface
{
	/// <summary>
	/// This class should be applied to the code behind of a Window. The window
	/// must already inherit from the 'Window' class or you will have to implement
	/// the interface.
	/// </summary>
	public interface IWindow
	{
		public object DataContext { get; set; }

		public void Show();
		public bool? ShowDialog();
	}
}
