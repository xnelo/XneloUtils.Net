#region Copyright (c) 2022 Spencer Hoffa
// \file RelayCommand.cs
// \author Spencer Hoffa
// \copyright \link LICENSE.md MIT License\endlink 2022 Spencer Hoffa 
#endregion

using System;
using System.Windows.Input;

namespace XneloUtils.Desktop.Net.MVVM.Commands
{
	public class RelayCommand : ICommand
	{
		#region Fields

		private readonly Action<object> m_Execute;
		private readonly Predicate<object> m_CanExecute;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of <see cref="DelegateCommand{T}"/>.
		/// </summary>
		/// <param name="execute">Delegate to execute when Execute is called on the command.  This can be null to just hook up a CanExecute delegate.</param>
		/// <remarks><seealso cref="CanExecute"/> will always return true.</remarks>
		public RelayCommand(Action<object> execute)
			: this(execute, null)
		{
		}

		/// <summary>
		/// Creates a new command.
		/// </summary>
		/// <param name="execute">The execution logic.</param>
		/// <param name="canExecute">The execution status logic.</param>
		public RelayCommand(Action<object> execute, Predicate<object> canExecute)
		{
			if (execute == null)
				throw new ArgumentNullException("execute");

			m_Execute = execute;
			m_CanExecute = canExecute;
		}
		#endregion

		#region ICommand Members

		///<summary>
		///Defines the method that determines whether the command can execute in its current state.
		///</summary>
		///<param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
		///<returns>
		///true if this command can be executed; otherwise, false.
		///</returns>
		public bool CanExecute(object parameter)
		{
			return m_CanExecute == null || m_CanExecute(parameter);
		}

		/////<summary>
		/////Occurs when changes occur that affect whether or not the command should execute.
		/////</summary>
		//public event EventHandler CanExecuteChanged
		//{
		//	add { CommandManager.RequerySuggested += value; }
		//	remove { CommandManager.RequerySuggested -= value; }
		//}

		// The above is a suggested implementation but 'CommandManager' is not available.
		public event EventHandler CanExecuteChanged;

		///<summary>
		///Defines the method to be called when the command is invoked.
		///</summary>
		///<param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to <see langword="null" />.</param>
		public void Execute(object parameter)
		{
			m_Execute(parameter);
		}

		#endregion
	}
}
