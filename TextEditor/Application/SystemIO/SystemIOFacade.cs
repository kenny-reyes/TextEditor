﻿using System.IO;
using Example.TextEditor.Model.Security;

namespace Example.TextEditor.Model.SystemIO
{
	public class SystemIOFacade : ISystemIOFacade
	{
		private Crypto cryptoObject = new Crypto();

		public string ReadFile(string filePath)
		{
			StreamReader sr = new StreamReader(filePath);
			try
			{
				string fileText = sr.ReadToEnd();
				return cryptoObject.DecryptText(fileText);
			}
			finally
			{
				sr.Close();
			}
		}


		public void WriteFile(string filePath, string text)
		{
			StreamWriter sw = new StreamWriter(filePath);
			try
			{
				string encryptedText = cryptoObject.EncryptText(text);
				sw.Write(encryptedText);
			}
			finally
			{
				sw.Close();
			}
		}
	}
}
