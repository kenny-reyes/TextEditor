using System;

namespace Example.TextEditor.Application.SystemIO.Contracts
{
	public interface INotificationDialogFacade
	{
		void NotifyError(Exception ex);
		bool AskForSaving(string fileName);
	}
}
