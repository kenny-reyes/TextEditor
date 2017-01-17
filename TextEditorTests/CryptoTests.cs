using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sedecal.CryptoText;
using Moq;

namespace CryptoTextTests
{
	[TestClass]
	public class CryptoTests
	{
		[TestMethod]
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
