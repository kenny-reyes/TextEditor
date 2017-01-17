using Example.TextEditor.ViewModel.Parsing.Elements;

namespace Example.TextEditor.ViewModel.Parsing
{
	public interface IParserViewModel
	{
        /// <summary>
        /// Bindable objects structure
        /// </summary>
		RootVM Root { get; }
        /// <summary>
        /// Parse XML to elements structure
        /// </summary>
        /// <param name="Any text fromat">XML string</param>
        /// <returns>Structure root node</returns>
		void UpdateStructure(string documentText);
	}
}
