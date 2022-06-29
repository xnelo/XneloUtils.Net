#region Copyright (c) 2022 Spencer Hoffa
// \file RelayCommand.cs
// \author Spencer Hoffa
// \copyright \link LICENSE.md MIT License\endlink 2022 Spencer Hoffa 
#endregion

using System;
using System.Diagnostics;
using System.Windows.Input;

namespace XneloUtils.Desktop.MVVM.Commands
{
	public class RelayCommand : ICommand
	{
		#region Fields
		private readonly Action<object> _execute;
		private readonly Predicate<object> _canExecute;
		private bool m_CanExecute;
		#endregion // Fields

		#region Constructors
		public RelayCommand(Action<object> execute)
		: this(execute, null)
		{
		}

		public RelayCommand(Action<object> execute, Predicate<object> canExecute)
		{
			if (execute == null)
				throw new ArgumentNullException("execute");

			_execute = execute;
			_canExecute = canExecute;
		}
		#endregion // Constructors

		#region ICommand Members
		[DebuggerStepThrough]
		public bool CanExecute(object parameter)
		{
			bool retval = _canExecute == null ? true : _canExecute(parameter);
			if (m_CanExecute != retval)
			{
				m_CanExecute = retval;
				CanExecuteChanged?.Invoke(this, new EventArgs());
			}

			return retval;
		}

		public event EventHandler CanExecuteChanged;

		public void Execute(object parameter)
		{
			_execute(parameter);
		}
		#endregion // ICommand Members
	}
}
