namespace Example.TextEditor.Model.SystemIO
{
	public interface ISystemIOFacade
	{
        /// <summary>
        /// Read from the disc an encrypted text file
        /// </summary>
        /// <param name="filePath">path to read</param>
        /// <returns>text readed</returns>
		string ReadFile(string filePath);

        /// <summary>
        /// Save the file in the disc an encrypted text file
        /// </summary>
        /// <param name="filePath">path to save</param>
        /// <param name="text">text to save</param>
		void WriteFile(string filePath, string Text);
	}
}
