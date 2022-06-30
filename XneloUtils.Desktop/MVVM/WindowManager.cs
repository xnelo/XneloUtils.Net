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
using XneloUtils.Bootstrap.Interface;

namespace XneloUtils.Desktop.MVVM
{
	public class WindowManager: IWindowManager, IShutdownEvent
	{
		private Dictionary<Type, Type> m_VmToViewMap = new();
		private Dictionary<IViewModel, IWindow> m_ViewToWindow = new();

		public void RegisterWindow<TViewModel, TView>()
			where TViewModel : IViewModel
			where TView : IWindow
		{
			m_VmToViewMap[typeof(TViewModel)] = typeof(TView);
		}

		public void ShowWindow<TViewModel>(TViewModel vm, bool asModal) where TViewModel : IViewModel
		{
			var w = GetWindow(vm);

			if (asModal)
			{
				w.ShowDialog();
			}
			else
			{
				w.Show();
				w.Closed += OnWindowClose;
				m_ViewToWindow.Add(vm, w);
			}
		}

		private void OnWindowClose(object sender, EventArgs e)
		{
			if (sender is IWindow sw)
			{
				sw.Closed -= OnWindowClose;
				var vm = sw.DataContext as IViewModel;
				m_ViewToWindow.Remove(vm);
				vm.Dispose();
			}
		}

		public void ShowOkDialog(string msg, string caption)
		{
			MessageBox.Show(msg, caption, MessageBoxButton.OK, MessageBoxImage.None);
		}

		public MessageBoxResultEnum ShowYesNoDialog(string msg, string caption)
		{
			return ShowYesNoDialog(msg, caption, false);
		}

		public MessageBoxResultEnum ShowYesNoDialog(string msg, string caption, bool isNoSelected)
		{
			MessageBoxResult selected = MessageBoxResult.Yes;
			if (isNoSelected)
			{
				selected = MessageBoxResult.No;
			}

			MessageBoxResult res = MessageBox.Show(msg, caption, MessageBoxButton.YesNo, MessageBoxImage.Question, selected);

			return res switch
			{
				MessageBoxResult.Yes => MessageBoxResultEnum.Yes,
				_ => MessageBoxResultEnum.No
			};
		}

		public IWindow GetWindow<TViewModel>(TViewModel vm) where TViewModel : IViewModel
		{
			if (!m_VmToViewMap.TryGetValue(typeof(TViewModel), out Type view))
			{
				throw new NullReferenceException($"View model of type {typeof(TViewModel)} does not have a view mapped.");
			}

			var w = Activator.CreateInstance(view) as IWindow;
			w.DataContext = vm;

			return w;
		}

		public void OnShutdown()
		{
			CloseAllWindows();
		}

		public void CloseAllWindows()
		{
			foreach (var wp in m_ViewToWindow)
			{
				var window = wp.Value;
				var vm = wp.Key;

				window.Closed -= OnWindowClose;
				window.Close();

				vm.Dispose();
			}

			m_ViewToWindow.Clear();
		}
	}
}
