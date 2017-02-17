namespace Example.TextEditor.Application.SystemIO.Contracts
{
	public interface IOpenSaveDialogFacade
	{
		string[] ShowOpen();
		string ShowSave();
	}
}
