using System;
using System.Windows.Input;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sedecal.CryptoText;
using Sedecal.CryptoText.ViewModel.Base;
using Moq;

namespace CryptoTextTests
{
	[TestClass]
	public class MainViewModelTests
	{
		[TestMethod]
		[TestCategory("new")]
		public void WhenNewCommand_ItShouldReturnANewCommand()
		{
			//Arrange
			MainViewModel mvm = new MainViewModel(Mock.Of<IDocumentDialogFacade>());
			
			//Act
			ICommand actual = mvm.NewCommand;

			//Assert
			Assert.IsNotNull(actual);
			Assert.IsInstanceOfType(actual, typeof(RelayCommand));
		}

		[TestMethod]
		[TestCategory("new")]
		public void WhenNewCommandIsExecuted_ItShouldCreateANewDocument()
		{
			//Arrange
			MainViewModel mvm = new MainViewModel(Mock.Of<IDocumentDialogFacade>());
			Mock<IDocumentViewModel> documentMock = new Mock<IDocumentViewModel>();

			mvm.Document = documentMock.Object;
			IDocumentViewModel oldDocument = mvm.Document;
			ICommand newCommand = mvm.NewCommand;

			//Act
			newCommand.Execute(null);

			//Assert
			Assert.AreNotEqual(oldDocument, mvm.Document);
			Assert.AreEqual(string.Empty, mvm.Document.Text);
			documentMock.Verify(dm => dm.HasChanges, Times.Once);
		}

		[TestMethod]
		[TestCategory("close")]
		public void WhenCloseCommandIsExecutedWithPreviousDocSaved_ItShouldNotAskForSaveIt()
		{
			//Arrange
			Mock<IDocumentDialogFacade> docDialogMock = new Mock<IDocumentDialogFacade>();

			MainViewModel mvm = new MainViewModel(docDialogMock.Object);

			Mock<IDocumentViewModel> documentMock = new Mock<IDocumentViewModel>();
			documentMock.Setup(dm => dm.Text).Returns("Hola Mundo");
			documentMock.Setup(dm => dm.HasChanges).Returns(false); ;

			mvm.Document = documentMock.Object;
			IDocumentViewModel oldDocument = mvm.Document;
			ICommand closeCommand = mvm.CloseCommand;

			//Act
			closeCommand.Execute(null);

			//Assert
			docDialogMock.Verify(ddm => ddm.ShowOpen(), Times.Never);
			documentMock.Verify(dm => dm.HasChanges, Times.Once);
		}

		[TestMethod]
		[TestCategory("close")]
		public void WhenCloseCommandIsExecutedWithPreviousDocUnsaved_ItShouldAskForSaveIt()
		{
			//Arrange
			Mock<IDocumentDialogFacade> docDialogMock = new Mock<IDocumentDialogFacade>();
			docDialogMock.Setup(ddm => ddm.ShowOpen()).Returns(true);

			MainViewModel mvm = new MainViewModel(docDialogMock.Object);

			Mock<IDocumentViewModel> documentMock = new Mock<IDocumentViewModel>();
			documentMock.Setup(dm => dm.Text).Returns("Hola Mundo");
			documentMock.Setup(dm => dm.HasChanges).Returns(true); ;

			mvm.Document = documentMock.Object;
			IDocumentViewModel oldDocument = mvm.Document;
			ICommand closeCommand = mvm.CloseCommand;

			//Act
			closeCommand.Execute(null);

			//Assert
			docDialogMock.Verify(ddm => ddm.ShowOpen(), Times.Once);
			documentMock.Verify(dm => dm.HasChanges, Times.Once);
		}

		[TestMethod]
		[TestCategory("save")]
		public void WhenSaveCommandIsExecutedAndUserSaves_ItShouldAskForSaveIt()
		{
			//Arrange
			Mock<IDocumentDialogFacade> docDialogMock = new Mock<IDocumentDialogFacade>();
			docDialogMock.Setup(ddm => ddm.ShowOpen()).Returns(true);

			MainViewModel mvm = new MainViewModel(docDialogMock.Object);

			Mock<IDocumentViewModel> documentMock = new Mock<IDocumentViewModel>();
			documentMock.Setup(dm => dm.Text).Returns("Hola Mundo");
			documentMock.Setup(dm => dm.HasChanges).Returns(true); ;

			mvm.Document = documentMock.Object;
			IDocumentViewModel oldDocument = mvm.Document;
			ICommand closeCommand = mvm.CloseCommand;

			//Act
			closeCommand.Execute(null);

			//Assert
			docDialogMock.Verify(ddm => ddm.ShowOpen(), Times.Once);
			documentMock.Verify(dm => dm.HasChanges, Times.Once);
		}
	}
}
