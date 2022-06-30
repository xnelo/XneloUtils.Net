#region Copyright (c) 2022 Spencer Hoffa
// \file OkCancelVm.cs
// \author Spencer Hoffa
// \copyright \link LICENSE.md MIT License\endlink 2022 Spencer Hoffa 
#endregion

using System;
using System.Windows.Input;
using XneloUtils.Desktop.Interface.MVVM;
using XneloUtils.Desktop.MVVM.Commands;

namespace XneloUtils.Desktop.MVVM
{
	public class OkCancelVm: ViewModelBase, ICloseWindow
	{
		public Action Close { get; set; }

		/// <summary>
		/// If Success is true then the OK button was clicked.
		/// </summary>
		public bool Success { get; private set; }

		private RelayCommand m_OkCommand;
		public ICommand OkCommand 
		{
			get
			{
				if (m_OkCommand == null)
				{
					m_OkCommand = new RelayCommand(OkCommandExecute);
				}
				return m_OkCommand;
			} 
		}

		protected virtual void OkCommandExecute(object o)
		{
			Success = true;
			Close();
		}

		private RelayCommand m_CancelCommand;
		public ICommand CancelCommand
		{
			get
			{
				if (m_CancelCommand == null)
				{
					m_CancelCommand = new RelayCommand(CancelCommandExecute);
				}
				return m_CancelCommand;
			}
		}

		protected virtual void CancelCommandExecute(object o)
		{
			Close();
		}
	}
}
