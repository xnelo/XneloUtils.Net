#region Copyright (c) 2022 Spencer Hoffa
// \file IViewModel.cs
// \author Spencer Hoffa
// \copyright \link LICENSE.md MIT License\endlink 2022 Spencer Hoffa 
#endregion

using System;
using System.ComponentModel;

namespace XneloUtils.Desktop.Net.MVVM.Interface
{
	public interface IViewModel: INotifyPropertyChanged, IDisposable
	{
	}
}
