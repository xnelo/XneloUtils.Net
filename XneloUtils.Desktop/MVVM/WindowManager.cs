#region Copyright (c) 2022 Spencer Hoffa
// \file WindowManager.cs
// \author Spencer Hoffa
// \copyright \link LICENSE.md MIT License\endlink 2022 Spencer Hoffa 
#endregion

using System;
using System.Windows;
using System.Collections.Generic;
using XneloUtils.Desktop.Interface.MVVM;
using XneloUtils.Desktop.Interface;

namespace XneloUtils.Desktop.MVVM
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
			var w = GetWindow(vm);

			if (asModal)
			{
				w.ShowDialog();
			}
			else
			{
				w.Show();
			}
		}

		public void ShowOkDialog(string msg, string caption)
		{
			MessageBox.Show(msg, caption, MessageBoxButton.OK, MessageBoxImage.None);
		}

		public IWindow GetWindow<TViewModel>(TViewModel vm)
		{
			if (!m_VmToViewMap.TryGetValue(typeof(TViewModel), out Type view))
			{
				throw new NullReferenceException($"View model of type {typeof(TViewModel)} does not have a view mapped.");
			}

			var w = Activator.CreateInstance(view) as IWindow;
			w.DataContext = vm;

			return w;
		}
	}
}
