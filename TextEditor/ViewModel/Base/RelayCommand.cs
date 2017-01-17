using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Sedecal.CryptoText.ViewModel.Base
{
	public class RelayCommand : ICommand
	{
		private readonly Action<object> execute;
		//private readonly Action execute;
		private readonly Func<bool> canExecute;

		//public RelayCommand(Action execute)
		//    : this(execute, null)
		//{
		//}

		//public RelayCommand(Action execute, Func<bool> canExecute)
		//{
		//    if (execute == null)
		//    {
		//        throw new ArgumentNullException("execute");
		//    }

		//    Action<object> exec = new Action<object>();
		//    exec.
		//    this.execute = execute;
		//    this.canExecute = canExecute;
		//}

		public RelayCommand(Action<object> execute)
			: this(execute, null)
		{
		}

		public RelayCommand(Action<object> execute, Func<bool> canExecute)
		{
			if (execute == null)
			{
				throw new ArgumentNullException("execute");
			}

			this.execute = execute;
			this.canExecute = canExecute;
		}

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		[DebuggerStepThrough]
		public bool CanExecute(object parameter)
		{
			return canExecute == null ? true : canExecute();
		}

		public void Execute(object parameter)
		{
			if (parameter != null)
				if (parameter.ToString() == "noexecute")
					return;
			execute(parameter);
		}
	}
}
