using System;
using System.Windows.Input;

namespace Mt.Common.UiCore.MvvmCore
{
	/// <summary>
	/// An <see cref="ICommand"/> with a parameter.
	/// </summary>
	/// <typeparam name="T">The command parameter type.</typeparam>
	public sealed class DelegateCommand<T> : DelegateCommandBase
	{
		private readonly Predicate<T> _canExecute;
		private readonly Action<T> _execute;

		public DelegateCommand(Action<T> execute)
			: this(execute, null)
		{
		}

		public DelegateCommand(Action<T> execute, Predicate<T> canExecute)
		{
			_execute = execute;
			_canExecute = canExecute;
		}

		protected override bool CanExecuteOverride(object parameter)
		{
			return parameter != null && (_canExecute == null || _canExecute((T)parameter));
		}

		protected override void ExecuteOverride(object parameter)
		{
			_execute((T)parameter);
		}
	}

	/// <summary>
	/// An <see cref="ICommand"/> without any parameters.
	/// </summary>
	public sealed class DelegateCommand : DelegateCommandBase
	{
		private readonly Action _execute;
		private readonly Func<bool> _canExecute;

		public DelegateCommand(Action execute)
			: this(execute, null)
		{
		}

		public DelegateCommand(Action execute, Func<bool> canExecute)
		{
			_execute = execute;
			_canExecute = canExecute;
		}

		protected override bool CanExecuteOverride(object parameter)
		{
			return _canExecute == null || _canExecute();
		}

		protected override void ExecuteOverride(object parameter)
		{
			_execute();
		}
	}
}
