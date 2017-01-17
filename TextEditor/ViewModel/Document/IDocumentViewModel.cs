using Example.TextEditor.ViewModel.Parsing.Elements;

namespace Example.TextEditor.ViewModel.Document
{
	public interface IDocumentViewModel
	{
        /// <summary>
        /// Indicates the name of the document, that's correspond with the name of the file
        /// </summary>
		string Name { get; }
        /// <summary>
        /// Current text in the document. Could be different to the text saved in the document
        /// </summary>
		string Text { get; set; }
        /// <summary>
        /// Indicates if the actual text if diferent to the saved text
        /// </summary>
		bool HasChanges { get; }
        /// <summary>
        /// Path where the document was openend or saved for last time
        /// </summary>
        string DocumentPath { get; }
        /// <summary>
        /// ViewModel Struct to build the GUI
        /// </summary>
        RootVM Elements { get; }
        /// <summary>
        /// Charge text into the object
        /// </summary>
        void SaveDocument();
        /// <summary>
        /// Save the text into a file indicates by the path
        /// </summary>
		void SaveAsDocument(string filePath);
        /// <summary>
        /// Save the text into a file indicates by the path
        /// </summary>
        /// <param name="filePath">path to save the file</param>
        void OpenDocument(string filePath);
	}
}
