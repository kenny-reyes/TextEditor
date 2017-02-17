using Example.TextEditor.Application.SystemIO.Contracts;
using Microsoft.Win32;

namespace Example.TextEditor.Application.SystemIO
{
	public class OpenSaveDialogFacade : IOpenSaveDialogFacade
	{
		private OpenFileDialog openFileDialog = new OpenFileDialog();
		private SaveFileDialog saveFileDialog = new SaveFileDialog();

		public OpenSaveDialogFacade()
		{
			openFileDialog.Filter = "Encrypted preferences files|*.pre|Encrypted Text|*.txt|All Files|*.*";
			saveFileDialog.Filter = "Encrypted preferences files|*.pre|Encrypted Text|*.txt|All Files|*.*";
			openFileDialog.Multiselect = true;
		}

		public string[] ShowOpen()
		{
			return openFileDialog.ShowDialog() == true ? openFileDialog.FileNames : null;
		}

		public string ShowSave()
		{
			return saveFileDialog.ShowDialog() == true ? saveFileDialog.FileName : null;
		}
	}
}
