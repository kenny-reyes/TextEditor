using System;

namespace Example.TextEditor.Model.SystemIO
{
	public interface INotificationDialogFacade
	{
		void NotifyError(Exception ex);
		bool AskForSaving(string fileName);
	}
}
