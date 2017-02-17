using System;
using System.Windows;
using Example.TextEditor.Application.SystemIO.Contracts;

namespace Example.TextEditor.Application.SystemIO
{
	public class NotificationDialogFacade : INotificationDialogFacade
	{
		public bool AskForSaving(string fileName)
		{
			return MessageBox.Show(string.Format("The document '{0}' is unsaved. Do you want to save it?", fileName), "Unsaved file", 
				MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes ? true : false;
		}

		public void NotifyError(Exception ex)
		{
			MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
		}
	}
}
