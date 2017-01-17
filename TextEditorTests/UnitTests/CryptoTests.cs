using Example.TextEditor.Model.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TextEditorTests.UnitTests
{
	[TestClass]
	public class CryptoTests
	{
		[TestMethod]
		[TestCategory("encrypt")]
		public void WhenEncryptTextWithNullStringItShouldReturnEmpty()
		{
			// Arrange
			Crypto crypto = new Crypto();
			string expectedResult = string.Empty;
			string actualResult;
			// Act
			actualResult = crypto.EncryptText(null);
			// Assert
			Assert.AreEqual(expectedResult, actualResult);
		}

		[TestMethod]
		[TestCategory("encrypt")]
		public void WhenEncryptTextWithEmptyStringItShouldReturnEmpty()
		{
			// Arrange
			Crypto crypto = new Crypto();
			string expectedResult = string.Empty;
			string actualResult;
			// Act
			actualResult = crypto.EncryptText(string.Empty);
			// Assert
			Assert.AreEqual(expectedResult, actualResult);
		}

		[TestMethod]
		[TestCategory("encrypt")]
		public void WhenEncryptTextItShouldReturnAnEncryptedText()
		{
			// Arrange
			Crypto crypto = new Crypto();
			string original = "Hola Mundo";
			string actualResult;
			// Act
			actualResult = crypto.EncryptText(original);
			// Assert
			Assert.AreNotEqual(original, actualResult);
		}

		[TestMethod]
		[TestCategory("encrypt")]
		public void WhenDecryptTextItShouldReturnTheRightDecryptedText()
		{
			// Arrange
			Crypto crypto = new Crypto();
			string original = "AQjmVHj77hIo1orX/XnBvg==";
			string expected = "Hola Mundo";
			string actualResult;
			// Act
			actualResult = crypto.DecryptText(original);
			// Assert
			Assert.AreEqual(expected, actualResult);
		}
	}
}
