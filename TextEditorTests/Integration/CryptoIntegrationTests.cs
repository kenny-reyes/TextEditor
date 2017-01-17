using Example.TextEditor.Model.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TextEditorTests.Integration
{
	[TestClass]
	public class CryptoIntegrationTests
	{
		[TestMethod]
		[TestCategory("integration")]
		public void WhenEncryptDecryptTextItShouldReturnTheSameText()
		{
			// Arrange
			Crypto crypto = new Crypto();
			string expected = "Hola Mundo";
			string actualResult;
			// Act
			var crypted = crypto.EncryptText(expected);
			actualResult = crypto.DecryptText(crypted);
			// Assert
			Assert.AreEqual(expected, actualResult);
		}
	}
}
