#region Copyright (c) 2022 Spencer Hoffa
// \file WindowManager.cs
// \author Spencer Hoffa
// \copyright \link LICENSE.md MIT License\endlink 2022 Spencer Hoffa 
#endregion

using System;
using System.Collections.Generic;
using XneloUtils.Desktop.Net.MVVM.Interface;

namespace XneloUtils.Desktop.Net.MVVM
{
	public class WindowManager: IWindowManager
	{
		private Dictionary<Type, Type> m_VmToViewMap = new();

		public void RegisterWindow<TViewModel, TView>()
			where TViewModel : IViewModel
			where TView : IWindow
		{
			m_VmToViewMap[typeof(TViewModel)] = typeof(TView);
		}

		public void ShowWindow<TViewModel>(TViewModel vm, bool asModal)
		{
			if (!m_VmToViewMap.TryGetValue(typeof(TViewModel), out Type view))
			{
				throw new NullReferenceException($"View model of type {typeof(TViewModel)} does not have a view mapped.");
			}

			var w = Activator.CreateInstance(view) as IWindow;
			w.DataContext = vm;

			if (asModal)
			{
				w.ShowDialog();
			}
			else
			{
				w.Show();
			}
		}
	}
}
