using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sedecal.CryptoText;

namespace CryptoTextTests
{
	[TestClass]
	public class CryptoIntegrationTests
	{
		[TestMethod]
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
