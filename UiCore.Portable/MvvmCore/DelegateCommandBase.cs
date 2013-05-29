using System;
using System.Windows.Input;

namespace Mt.Common.UiCore.MvvmCore
{
	/// <summary>
	/// Represents an <see cref="ICommand"/> which delegates the execution to a View Model.
	/// </summary>
	public abstract class DelegateCommandBase : ICommand
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DelegateCommandBase" /> class.
		/// </summary>
		protected DelegateCommandBase()
		{
		}

		/// <summary>
		/// Occurs when changes occur that affect whether or not the command should execute.
		/// TODO: check original implementation: it utilizes WeakEventHandlerManager
		/// </summary>
		public event EventHandler CanExecuteChanged;

		/// <summary>
		/// Defines the method that determines whether the command can execute in its current state.
		/// </summary>
		/// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
		/// <returns>
		/// true if this command can be executed; otherwise, false.
		/// </returns>
		bool ICommand.CanExecute(object parameter)
		{
			return CanExecuteOverride(parameter);
		}

		/// <summary>
		/// Defines the method to be called when the command is invoked.
		/// </summary>
		/// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
		void ICommand.Execute(object parameter)
		{
			ExecuteOverride(parameter);
		}

		/// <summary>
		/// When overridden determines if the command can execute with the provided parameter.
		/// </summary>
		/// <param name="parameter">The parameter to use when determining if this command can execute.</param>
		/// <returns>Returns <see langword="true"/> if the command can execute.  <see langword="False"/> otherwise.</returns>
		protected abstract bool CanExecuteOverride(object parameter);

		/// <summary>
		/// When overridden executes the command with the provided parameter.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		protected abstract void ExecuteOverride(object parameter);

		public void RaiseCanExecuteChanged()
		{
			EventHandler handler = CanExecuteChanged;
			if(handler != null)
				handler(this, EventArgs.Empty);
		}
	}
}