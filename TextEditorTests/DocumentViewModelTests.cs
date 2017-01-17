using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sedecal.CryptoText;

namespace CryptoTextTests
{
	[TestClass]
	public class DocumentViewModelTests
	{
		[TestMethod]
		public void WhenDocumentIsCreatedWithAText_ItShouldContainTheText()
		{
			//Arrange
			DocumentViewModel document;
			string expected = "Hola mundo";

			//Act
			document = new DocumentViewModel(expected);

			//Assert
			Assert.AreEqual(expected, document.Text);
		}

		[TestMethod]
		public void WhenTextChanges_ItShouldSetHasChangesToTrue()
		{
			//Arrange
			DocumentViewModel document = new DocumentViewModel(string.Empty); ;
			bool expected = true;

			//Act
			document.Text = "Hola mundo";

			//Assert
			Assert.AreEqual(expected, document.HasChanges);
		}
	}
}
