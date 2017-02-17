using System.IO;
using Example.TextEditor.Application.SystemIO;
using Example.TextEditor.Application.SystemIO.Contracts;
using Example.TextEditor.ViewModel.Document;
using Example.TextEditor.ViewModel.Parsing.XML;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TextEditorTests.UnitTests
{
	[TestClass]
	public class DocumentViewModelTests
	{
		#region Create
		[TestMethod]
		[TestCategory("Document")]
		public void WhenDocumentIsCreated_ItShouldContainAEmptyString()
		{
			//Arrange
			DocumentViewModel document;
			string expected = string.Empty;

			//Act
			document = new DocumentViewModel(new SystemIOFacade(), new XMLParserViewModel());

			//Assert
			Assert.AreEqual(expected, document.Text);
		}

		[TestMethod]
		[TestCategory("Document")]
		public void WhenDocumentIsCreated_ItShouldSetHasChangesToTrue()
		{
			//Arrange
			DocumentViewModel document;
			bool expected = true;

			//Act
			document = new DocumentViewModel(new SystemIOFacade(), new XMLParserViewModel());

			//Assert
			Assert.AreEqual(expected, document.HasChanges);
		}

		[TestMethod]
		[TestCategory("Document")]
		public void WhenDocumentIsCreated_ItShouldHaveADefaultName()
		{
			//Arrange
			DocumentViewModel document;

			//Act
			document = new DocumentViewModel(new SystemIOFacade(), new XMLParserViewModel());

			//Assert
			Assert.IsFalse(string.IsNullOrEmpty(document.Name));
			Assert.IsFalse(string.IsNullOrWhiteSpace(document.Name));
		}
		#endregion

		#region open
		[TestMethod]
		[TestCategory("Document")]
		public void WhenDocumentIsOpened_ItShouldSetHasChangesToFalse()
		{
			//Arrange
			DocumentViewModel document = new DocumentViewModel(Mock.Of<ISystemIOFacade>(), new XMLParserViewModel());
			string vaLidPath = "c:\\SomeValidPath";
			bool expected = false;

			//Act
			document.OpenDocument(vaLidPath);

			//Assert
			Assert.AreEqual(expected, document.HasChanges);
		}

		[TestMethod]
		[TestCategory("Document")]
		public void WhenDocumentIsOpened_ItShouldHaveTheTextLoaded()
		{
			//Arrange
			string validPath = "c:\\SomeValidPath";
			Mock<ISystemIOFacade> fileManagerMock = new Mock<ISystemIOFacade>();
			fileManagerMock.Setup(dm => dm.ReadFile(validPath)).Returns("Hola Mundo");
			DocumentViewModel document = new DocumentViewModel(fileManagerMock.Object, new XMLParserViewModel());
			string expected = "Hola Mundo";

			//Act
			document.OpenDocument(validPath);

			//Assert
			Assert.AreEqual(expected, document.Text);
		}

		[TestMethod]
		[TestCategory("Document")]
		public void WhenDocumentIsWellOpened_ItShouldSetThePath()
		{
			//Arrange
			string validPath = "c:\\SomeValidPath";
			Mock<ISystemIOFacade> fileManagerMock = new Mock<ISystemIOFacade>();
			DocumentViewModel document = new DocumentViewModel(fileManagerMock.Object, new XMLParserViewModel());
			string expected = validPath;

			//Act
			document.OpenDocument(validPath);

			//Assert
			Assert.AreEqual(expected, document.DocumentPath);
		}

		[TestMethod]
		[TestCategory("Document")]
        [ExpectedException(typeof(IOException))]
		public void WhenAInvalidDocumentIsOpened_ItShouldThrowAnException()
		{
			//Arrange
			string unValidPath = @"m:\SomeInvalidPath\SomeUneistingFile.txt";
			DocumentViewModel document = new DocumentViewModel(new SystemIOFacade(), new XMLParserViewModel());

			//Act
				document.OpenDocument(unValidPath);
			Assert.Fail("No right exception launched");
		}

		[TestMethod]
		[TestCategory("Document")]
		public void WhenAInvalidDocumentIsOpened_ItShouldNotChangeThePath()
		{
			//Arrange
			string unValidPath = @"m:\SomeInvalidPath\SomeUneistingFile.txt";
			DocumentViewModel document = new DocumentViewModel(new SystemIOFacade(), new XMLParserViewModel());

			//Act
			try
			{
				document.OpenDocument(unValidPath);
			}
			catch { }

			//Assert
			Assert.AreEqual(unValidPath, document.DocumentPath);
		}
		#endregion

		#region saveAs
		[TestMethod]
		[TestCategory("Document")]
		public void WhenDocumentIsSavedAndIfItCanNotWrite_ItShouldThrowAnException()
		{
			//Arrange
			DocumentViewModel document = new DocumentViewModel(new SystemIOFacade(), new XMLParserViewModel());
			string invalidPath = "m:\\SomeInvalidPath";

			//Act
			try
			{
				document.SaveAsDocument(invalidPath);
			}
			catch (IOException)
			{ return; }
			Assert.Fail("No right exception launched");
		}

		[TestMethod]
		[TestCategory("Document")]
		public void WhenDocumentIsSavedAs_ItShouldSetHasChangesToFalse()
		{
			//Arrange
			DocumentViewModel document = new DocumentViewModel(Mock.Of<ISystemIOFacade>(), new XMLParserViewModel());
			string vaLidPath = "c:\\SomeValidPath";
			bool expected = false;

			//Act
			document.SaveAsDocument(vaLidPath);

			//Assert
			Assert.AreEqual(expected, document.HasChanges);
		}

		[TestMethod]
		[TestCategory("Document")]
		public void WhenDocumentIsSavedAs_ItShouldSetTheNewPathToDocumentPathProperty()
		{
			//Arrange
			DocumentViewModel document = new DocumentViewModel(Mock.Of<ISystemIOFacade>(), new XMLParserViewModel());
			string vaLidPath = @"c:\SomeValidPath\validTxTFile.txt";
			string expected = vaLidPath;

			//Act
			document.SaveAsDocument(vaLidPath);

			//Assert
			Assert.AreEqual(expected, document.DocumentPath);
		}

		#endregion

		#region save
		[TestMethod]
		[TestCategory("Document")]
		public void WhenDocumentIsSaved_ItShouldSetHasChangesToFalse()
		{
			//Arrange
			DocumentViewModel document = new DocumentViewModel(Mock.Of<ISystemIOFacade>(), new XMLParserViewModel());
			bool expected = false;

			//Act
			document.SaveDocument();

			//Assert
			Assert.AreEqual(expected, document.HasChanges);
		}
		#endregion

		#region properties
		[TestMethod]
		[TestCategory("Document")]
		public void WhenTextChanges_ItShouldSetHasChangesToTrue()
		{
			//Arrange
			DocumentViewModel document = new DocumentViewModel(new SystemIOFacade(), new XMLParserViewModel());
			bool expected = true;

			//Act
			document.Text = "Hola mundo";

			//Assert
			Assert.AreEqual(expected, document.HasChanges);
		}


		#endregion
	}
}
