#region Copyright (c) 2022 Spencer Hoffa
// \file ViewModelBase.cs
// \author Spencer Hoffa
// \copyright \link LICENSE.md MIT License\endlink 2022 Spencer Hoffa 
#endregion

using System.ComponentModel;
using System.Runtime.CompilerServices;
using XneloUtils.Desktop.Interface.MVVM;

namespace XneloUtils.Desktop.MVVM
{
	public class ViewModelBase : IViewModel
	{
		#region Event
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion

		#region Methods
		public virtual void Dispose()
		{ }

		/// <summary>
		/// Fire property changed event.
		/// </summary>
		/// <param name="propertyName">The name of the property data that changed.</param>
		public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		/// <summary>
		/// Fire property changed event.
		/// </summary>
		/// <param name="sender">The object firing the event.</param>
		/// <param name="propertyName">The name of the property data that changed.</param>
		public void NotifyPropertyChanged(object sender, string propertyName)
		{
			PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
		}
		#endregion
	}
}
