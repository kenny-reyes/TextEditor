namespace Example.TextEditor.Model.SystemIO
{
	public interface IOpenSaveDialogFacade
	{
		string[] ShowOpen();
		string ShowSave();
	}
}
