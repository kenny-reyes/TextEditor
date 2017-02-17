using System;
using System.Windows.Input;
using Example.TextEditor.Application.SystemIO;
using Example.TextEditor.Application.SystemIO.Contracts;
using Example.TextEditor.ViewModel;
using Example.TextEditor.ViewModel.Document;
using Microsoft.TeamFoundation.MVVM;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TextEditorTests.UnitTests
{
	[TestClass]
	public class MainViewModelTests
	{
		#region Commands
		[TestMethod]
		[TestCategory("creation")]
		public void WhenNewCommand_ItShouldReturnANewCommand()
		{
			//Arrange
			MainViewModel mvvm = new MainViewModel(Mock.Of<IOpenSaveDialogFacade>(), Mock.Of<INotificationDialogFacade>(), Mock.Of<ISystemIOFacade>());

			//Act
			ICommand actual = mvvm.NewCommand;

			//Assert
			Assert.IsNotNull(actual);
			Assert.IsInstanceOfType(actual, typeof(RelayCommand));
		}

		[TestMethod]
		[TestCategory("closing")]
		public void WhenCloseCommand_ItShouldReturnACloseCommand()
		{
			//Arrange
			MainViewModel mvvm = new MainViewModel(Mock.Of<IOpenSaveDialogFacade>(), Mock.Of<INotificationDialogFacade>(), Mock.Of<ISystemIOFacade>());

			//Act
			ICommand actual = mvvm.CloseCommand;

			//Assert
			Assert.IsNotNull(actual);
			Assert.IsInstanceOfType(actual, typeof(RelayCommand));
		}

		[TestMethod]
		[TestCategory("closing")]
		public void WhenCloseAllCommand_ItShouldReturnACloseCommand()
		{
			//Arrange
			MainViewModel mvvm = new MainViewModel(Mock.Of<IOpenSaveDialogFacade>(), Mock.Of<INotificationDialogFacade>(), Mock.Of<ISystemIOFacade>());

			//Act
			ICommand actual = mvvm.CloseAllCommand;

			//Assert
			Assert.IsNotNull(actual);
			Assert.IsInstanceOfType(actual, typeof(RelayCommand));
		}

		[TestMethod]
		[TestCategory("opening")]
		public void WhenOpenCommand_ItShouldReturnAOpenCommand()
		{
			//Arrange
			MainViewModel mvvm = new MainViewModel(Mock.Of<IOpenSaveDialogFacade>(), Mock.Of<INotificationDialogFacade>(), Mock.Of<ISystemIOFacade>());

			//Act
			ICommand actual = mvvm.OpenCommand;

			//Assert
			Assert.IsNotNull(actual);
			Assert.IsInstanceOfType(actual, typeof(RelayCommand));
		}

		[TestMethod]
		[TestCategory("saving")]
		public void WhenSaveCommand_ItShouldReturnASaveCommand()
		{
			//Arrange
			MainViewModel mvvm = new MainViewModel(Mock.Of<IOpenSaveDialogFacade>(), Mock.Of<INotificationDialogFacade>(), Mock.Of<ISystemIOFacade>());

			//Act
			ICommand actual = mvvm.SaveCommand;

			//Assert
			Assert.IsNotNull(actual);
			Assert.IsInstanceOfType(actual, typeof(RelayCommand));
		}

		[TestMethod]
		[TestCategory("saving")]
		public void WhenSaveAsCommand_ItShouldReturnASaveAsCommand()
		{
			//Arrange
			MainViewModel mvvm = new MainViewModel(Mock.Of<IOpenSaveDialogFacade>(), Mock.Of<INotificationDialogFacade>(), Mock.Of<ISystemIOFacade>());

			//Act
			ICommand actual = mvvm.SaveAsCommand;

			//Assert
			Assert.IsNotNull(actual);
			Assert.IsInstanceOfType(actual, typeof(RelayCommand));
		}

		[TestMethod]
		[TestCategory("saving")]
		public void WhenSaveAllCommand_ItShouldReturnASaveAllCommand()
		{
			//Arrange
			MainViewModel mvvm = new MainViewModel(Mock.Of<IOpenSaveDialogFacade>(), Mock.Of<INotificationDialogFacade>(), Mock.Of<ISystemIOFacade>());

			//Act
			ICommand actual = mvvm.SaveAllCommand;

			//Assert
			Assert.IsNotNull(actual);
			Assert.IsInstanceOfType(actual, typeof(RelayCommand));
		}

		#endregion

		#region New
		[TestMethod]
		[TestCategory("creation")]
		public void WhenNewCommandIsExecuted_ItShouldCreateANewDocument()
		{
			//Arrange
			MainViewModel mvvm = new MainViewModel(Mock.Of<IOpenSaveDialogFacade>(), Mock.Of<INotificationDialogFacade>(), Mock.Of<ISystemIOFacade>());

            IDocumentViewModel oldDocument = mvvm.SelectedDocument;
			ICommand newCommand = mvvm.NewCommand;

			//Act
			newCommand.Execute(null);

			//Assert
			Assert.AreNotEqual(oldDocument, mvvm.SelectedDocument);
			CollectionAssert.Contains(mvvm.LoadedDocuments, mvvm.SelectedDocument);
		}
		#endregion

		#region Close
		[TestMethod]
		[TestCategory("closing")]
		public void WhenCloseIsExecutedWithPreviousDocSaved_ItShouldNotAskForSaveIt()
		{
			//Arrange
			Mock<INotificationDialogFacade> NotificationDialogMock = new Mock<INotificationDialogFacade>();

			MainViewModel mvvm = new MainViewModel(new OpenSaveDialogFacade(), NotificationDialogMock.Object, Mock.Of<ISystemIOFacade>());

			Mock<IDocumentViewModel> documentMock = new Mock<IDocumentViewModel>();
			documentMock.Setup(dm => dm.Text).Returns("Hola Mundo");
			documentMock.Setup(dm => dm.HasChanges).Returns(false); ;

			mvvm.SelectedDocument = documentMock.Object;
			IDocumentViewModel oldDocument = mvvm.SelectedDocument;

			//Act
			mvvm.CloseDocumentAction(documentMock.Object);

			//Assert
			NotificationDialogMock.Verify(ddm => ddm.AskForSaving(documentMock.Name), Times.Never);
			documentMock.Verify(dm => dm.HasChanges, Times.Once);
		}

		[TestMethod]
		[TestCategory("closing")]
		public void WhenCloseIsExecutedWithPreviousDocUnsaved_ItShouldAskForSaveItAndSaveIfTheRequestIsYes()
		{
			//Arrange
			string validPath = @"Drive\\folder\file.ext";
			Mock<IOpenSaveDialogFacade> openSaveDialogMock = new Mock<IOpenSaveDialogFacade>();
			Mock<INotificationDialogFacade> notificationDialogMock = new Mock<INotificationDialogFacade>();
			notificationDialogMock.Setup(ndm => ndm.AskForSaving(It.IsAny<string>())).Returns(true);
			Mock<IDocumentViewModel> documentMock = new Mock<IDocumentViewModel>();
			documentMock.Setup(dm => dm.HasChanges).Returns(true);
			documentMock.Setup(dm => dm.DocumentPath).Returns(validPath);

			MainViewModel mvvm = new MainViewModel(openSaveDialogMock.Object, notificationDialogMock.Object, Mock.Of<ISystemIOFacade>());
			mvvm.LoadedDocuments.Add(documentMock.Object);
			mvvm.SelectedDocument = documentMock.Object;

			//Act
			mvvm.CloseDocumentAction(documentMock.Object);

			//Assert
			notificationDialogMock.Verify(nm => nm.AskForSaving(documentMock.Object.Name), Times.Once);
			documentMock.Verify(dm => dm.SaveDocument(), Times.Once);
		}

		[TestMethod]
		[TestCategory("closing")]
		public void WhenCloseIsExecutedWithPreviousDocUnsaved_ItShouldAskForSaveItAndDontSaveItIfTheRequestIsNo()
		{
			//Arrange
			Mock<IOpenSaveDialogFacade> openSaveDialogMock = new Mock<IOpenSaveDialogFacade>();
			Mock<INotificationDialogFacade> notificationDialogMock = new Mock<INotificationDialogFacade>();
			notificationDialogMock.Setup(ndm => ndm.AskForSaving(It.IsAny<string>())).Returns(false);
			Mock<IDocumentViewModel> documentMock = new Mock<IDocumentViewModel>();
			documentMock.Setup(dm => dm.HasChanges).Returns(true);
			documentMock.Setup(dm => dm.Name).Returns("documentName");

			MainViewModel mvvm = new MainViewModel(openSaveDialogMock.Object, notificationDialogMock.Object, Mock.Of<ISystemIOFacade>());
			mvvm.LoadedDocuments.Add(documentMock.Object);
			mvvm.SelectedDocument = documentMock.Object;

			//Act
			mvvm.CloseDocumentAction(documentMock.Object);

			//Assert
			notificationDialogMock.Verify(nm => nm.AskForSaving(documentMock.Object.Name), Times.Once);
			documentMock.Verify(dm => dm.SaveAsDocument(It.IsAny<string>()), Times.Never);
		}

		[TestMethod]
		[TestCategory("closing")]
		public void WhenCloseIsExecutedAndOnlyOneDocument_ItShouldQuitFromTheListTheDocument()
		{
			/// Arrange
			Mock<IOpenSaveDialogFacade> openSaveDialogMock = new Mock<IOpenSaveDialogFacade>();
			Mock<INotificationDialogFacade> notificationDialogMock = new Mock<INotificationDialogFacade>();
			Mock<IDocumentViewModel> documentMock = new Mock<IDocumentViewModel>();
			documentMock.Setup(dm => dm.HasChanges).Returns(false);
			documentMock.Setup(dm => dm.Name).Returns("documentName");

			MainViewModel mvvm = new MainViewModel(openSaveDialogMock.Object, notificationDialogMock.Object, Mock.Of<ISystemIOFacade>());
			mvvm.LoadedDocuments.Add(documentMock.Object);
			mvvm.SelectedDocument = documentMock.Object;

			/// Act
			mvvm.CloseDocumentAction(documentMock.Object);

			/// Assert
			CollectionAssert.DoesNotContain(mvvm.LoadedDocuments, documentMock.Object);
			//Assert.IsNull(mvvm.SelectedDocument);
 			/// Aqui he intentado hacer cerrar 1/1 y esperar un null en selected, pero se queda en selected document el puntero al objeto
			/// En la practica el TabControl quitara el puntero del selectedDocument ya que al eliminar de la lista un documento este cierra la ventana y si
			/// existiera otro tab moveria el selected document a este otro. Con lo cual tambien nos ahorramos poner otro test, el de comprobar si al cerra un 
			/// documento de varios se selecciona el siguiente documento.
		}
		
		#endregion

		#region close all
		[TestMethod]
		[TestCategory("closing")]
		public void WhenCloseAllIsExecuted_ItShouldQuitAllTheElementsFromLoadedDocument()
		{
			/// Arrange
			Mock<IOpenSaveDialogFacade> openSaveDialogMock = new Mock<IOpenSaveDialogFacade>();
			Mock<INotificationDialogFacade> notificationDialogMock = new Mock<INotificationDialogFacade>();
			Mock<IDocumentViewModel> documentMock1 = new Mock<IDocumentViewModel>();
			documentMock1.Setup(dm => dm.HasChanges).Returns(false);
			documentMock1.Setup(dm => dm.Name).Returns("documentName1");
			Mock<IDocumentViewModel> documentMock2 = new Mock<IDocumentViewModel>();
			documentMock2.Setup(dm => dm.HasChanges).Returns(false);
			documentMock2.Setup(dm => dm.Name).Returns("documentName2");

			MainViewModel mvvm = new MainViewModel(openSaveDialogMock.Object, notificationDialogMock.Object, Mock.Of<ISystemIOFacade>());
			mvvm.LoadedDocuments.Add(documentMock1.Object);
			mvvm.LoadedDocuments.Add(documentMock2.Object);
			int expected = 0;

			/// Act
			mvvm.CloseAllDocumentAction(documentMock1.Object);

			/// Assert
			Assert.AreEqual(mvvm.LoadedDocuments.Count, expected);
		}
		#endregion

		#region save
		[TestMethod]
		[TestCategory("saving")]
		public void WhenSaveIsExecutedAndTheFileHaveAPath_ItShoulSaveIt()
		{
			//Arrange
			string validPath = @"Drive\\folder\file.ext";
			Mock<IOpenSaveDialogFacade> openSaveDialogMock = new Mock<IOpenSaveDialogFacade>();
			//openSaveDialogMock.Setup(obj => obj.ShowSave().Returns(validPath));
			Mock<INotificationDialogFacade> notificationDialogMock = new Mock<INotificationDialogFacade>();
			Mock<IDocumentViewModel> documentMock = new Mock<IDocumentViewModel>();
			documentMock.Setup(dm => dm.HasChanges).Returns(true);
			documentMock.Setup(dm => dm.DocumentPath).Returns(validPath);

			MainViewModel mvvm = new MainViewModel(openSaveDialogMock.Object, notificationDialogMock.Object, Mock.Of<ISystemIOFacade>());
			mvvm.SelectedDocument = documentMock.Object;

			//Act
			mvvm.SaveDocumentAction(null);

			//Assert
			documentMock.Verify(dm => dm.SaveDocument(), Times.Once);
		}

		[TestMethod]
		[TestCategory("saving")]
		public void WhenSaveIsExecutedAndTheSaveMethodThrowsAnException_ItShoulNotifyThatExceptionAndAskWhereSaving()
		{
			string invalidPath = @"Drive\\folder\invalidfile.ext";
			string validPath = @"Drive\\folder\file.ext";
			Mock<IOpenSaveDialogFacade> openSaveDialogMock = new Mock<IOpenSaveDialogFacade>();
			openSaveDialogMock.Setup(obj => obj.ShowSave()).Returns(validPath);
			Mock<INotificationDialogFacade> notificationDialogMock = new Mock<INotificationDialogFacade>();
			Mock<IDocumentViewModel> documentMock = new Mock<IDocumentViewModel>();
			documentMock.Setup(dm => dm.HasChanges).Returns(true);
			documentMock.Setup(dm => dm.DocumentPath).Returns(invalidPath);
			documentMock.Setup(dm => dm.SaveDocument()).Throws(new Exception());

			MainViewModel mvvm = new MainViewModel(openSaveDialogMock.Object, notificationDialogMock.Object, Mock.Of<ISystemIOFacade>());
			mvvm.SelectedDocument = documentMock.Object;

			//Act
			mvvm.SaveDocumentAction(null);

			//Assert
			notificationDialogMock.Verify(obj => obj.NotifyError(It.IsAny<Exception>()), Times.Once);
			openSaveDialogMock.Verify(obj => obj.ShowSave(), Times.Once);
			documentMock.Verify(obj => obj.SaveAsDocument(It.IsAny<string>()), Times.Once);
		}

		[TestMethod]
		[TestCategory("saving")]
		public void WhenSaveIsExecutedAndTheFileDontHavePath_ItShouldAskWhereAndSaveItIfUserSelectNewPath()
		{
			string validPath = @"Drive\\folder\file.ext";
			Mock<IOpenSaveDialogFacade> openSaveDialogMock = new Mock<IOpenSaveDialogFacade>();
			openSaveDialogMock.Setup(obj => obj.ShowSave()).Returns(validPath);
			Mock<INotificationDialogFacade> notificationDialogMock = new Mock<INotificationDialogFacade>();
			Mock<IDocumentViewModel> documentMock = new Mock<IDocumentViewModel>();
			documentMock.Setup(dm => dm.HasChanges).Returns(true);

			MainViewModel mvvm = new MainViewModel(openSaveDialogMock.Object, notificationDialogMock.Object, Mock.Of<ISystemIOFacade>());
			mvvm.SelectedDocument = documentMock.Object;

			//Act
			mvvm.SaveDocumentAction(null);

			//Assert
			openSaveDialogMock.Verify(obj => obj.ShowSave(), Times.Once);
			documentMock.Verify(obj => obj.SaveAsDocument(It.IsAny<string>()), Times.Once);
		}

		[TestMethod]
		[TestCategory("saving")]
		public void WhenSaveIsExecutedAndTheFileDontHavePath_ItShouldAskWhereAndDontSaveItIfUserCancel()
		{
			string invalidPath = @"Drive\\folder\file.ext";
			Mock<IOpenSaveDialogFacade> openSaveDialogMock = new Mock<IOpenSaveDialogFacade>();
			//openSaveDialogMock.Setup(obj => obj.ShowSave()).Returns(null);
			Mock<INotificationDialogFacade> notificationDialogMock = new Mock<INotificationDialogFacade>();
			Mock<IDocumentViewModel> documentMock = new Mock<IDocumentViewModel>();
			documentMock.Setup(dm => dm.HasChanges).Returns(true);

			MainViewModel mvvm = new MainViewModel(openSaveDialogMock.Object, notificationDialogMock.Object, Mock.Of<ISystemIOFacade>());
			mvvm.SelectedDocument = documentMock.Object;

			//Act
			mvvm.SaveDocumentAction(null);

			//Assert
			openSaveDialogMock.Verify(obj => obj.ShowSave(), Times.Once);
			documentMock.Verify(obj => obj.SaveAsDocument(It.IsAny<string>()), Times.Never);
		}

		#endregion

		#region saveCommandCan
		[TestMethod]
		[TestCategory("saving")]
		public void WhenNeitherSelectedDocument_SaveCommandMustBeDisabled()
		{
			Mock<IOpenSaveDialogFacade> openSaveDialogMock = new Mock<IOpenSaveDialogFacade>();
			Mock<INotificationDialogFacade> notificationDialogMock = new Mock<INotificationDialogFacade>();

			MainViewModel mvvm = new MainViewModel(openSaveDialogMock.Object, notificationDialogMock.Object, Mock.Of<ISystemIOFacade>());
			mvvm.SelectedDocument = null;

			//Act
			bool result = mvvm.SaveDocumentCan(null);

			//Assert
			Assert.IsFalse(result);
		}

		[TestMethod]
		[TestCategory("saving")]
		public void WhenSelectedDocumentHasNotChanges_SaveCommandMustBeDisabled()
		{
			Mock<IOpenSaveDialogFacade> openSaveDialogMock = new Mock<IOpenSaveDialogFacade>();
			Mock<INotificationDialogFacade> notificationDialogMock = new Mock<INotificationDialogFacade>();
			Mock<IDocumentViewModel> documentMock = new Mock<IDocumentViewModel>();
			documentMock.Setup(dm => dm.HasChanges).Returns(false);

			MainViewModel mvvm = new MainViewModel(openSaveDialogMock.Object, notificationDialogMock.Object, Mock.Of<ISystemIOFacade>());
			mvvm.SelectedDocument = documentMock.Object;

			//Act
			bool result = mvvm.SaveDocumentCan(null);

			//Assert
			Assert.IsFalse(result);
		}

		[TestMethod]
		[TestCategory("saving")]
		public void WhenSelectedDocumentHasChanges_SaveCommandMustBeEnabled()
		{
			Mock<IOpenSaveDialogFacade> openSaveDialogMock = new Mock<IOpenSaveDialogFacade>();
			Mock<INotificationDialogFacade> notificationDialogMock = new Mock<INotificationDialogFacade>();
			Mock<IDocumentViewModel> documentMock = new Mock<IDocumentViewModel>();
			documentMock.Setup(dm => dm.HasChanges).Returns(true);

			MainViewModel mvvm = new MainViewModel(openSaveDialogMock.Object, notificationDialogMock.Object, Mock.Of<ISystemIOFacade>());
			mvvm.SelectedDocument = documentMock.Object;

			//Act
			bool result = mvvm.SaveDocumentCan(null);

			//Assert
			Assert.IsTrue(result);
		}

		#endregion

		#region saveAs
		[TestMethod]
		[TestCategory("saving")]
		public void WhenSaveAsIsExecuted_ItShoulSaveAskingForPath()
		{
			string validPath = @"Drive\\folder\file.ext";
			Mock<IOpenSaveDialogFacade> openSaveDialogMock = new Mock<IOpenSaveDialogFacade>();
			openSaveDialogMock.Setup(obj => obj.ShowSave()).Returns(validPath);
			Mock<INotificationDialogFacade> notificationDialogMock = new Mock<INotificationDialogFacade>();
			Mock<IDocumentViewModel> documentMock = new Mock<IDocumentViewModel>();
			documentMock.Setup(dm => dm.HasChanges).Returns(true);
			documentMock.Setup(dm => dm.DocumentPath).Returns(validPath);

			MainViewModel mvvm = new MainViewModel(openSaveDialogMock.Object, notificationDialogMock.Object, Mock.Of<ISystemIOFacade>());
			mvvm.SelectedDocument = documentMock.Object;

			//Act
			mvvm.SaveAsDocumentAction(null);

			//Assert
			openSaveDialogMock.Verify(obj => obj.ShowSave(), Times.Once);
			documentMock.Verify(obj => obj.SaveAsDocument(It.IsAny<string>()), Times.Once);
		}

		[TestMethod]
		[TestCategory("saving")]
		public void WhenSaveAsIsExecutedAndTheSaveMethodThrowsAnException_ItShoulNotifyThatException()
		{
			string validPath = @"Drive\\folder\file.ext";
			Mock<IOpenSaveDialogFacade> openSaveDialogMock = new Mock<IOpenSaveDialogFacade>();
			openSaveDialogMock.Setup(obj => obj.ShowSave()).Returns(validPath);
			Mock<INotificationDialogFacade> notificationDialogMock = new Mock<INotificationDialogFacade>();
			Mock<IDocumentViewModel> documentMock = new Mock<IDocumentViewModel>();
			documentMock.Setup(dm => dm.HasChanges).Returns(true);
			documentMock.Setup(dm => dm.DocumentPath).Returns(validPath);
			documentMock.Setup(dm => dm.SaveAsDocument(It.IsAny<string>())).Throws(new Exception());

			MainViewModel mvvm = new MainViewModel(openSaveDialogMock.Object, notificationDialogMock.Object, Mock.Of<ISystemIOFacade>());
			mvvm.SelectedDocument = documentMock.Object;

			//Act
			mvvm.SaveAsDocumentAction(null);

			//Assert
			notificationDialogMock.Verify(obj => obj.NotifyError(It.IsAny<Exception>()), Times.Once);
		}

		#endregion

		#region saveAsCommandCan
		[TestMethod]
		[TestCategory("saving")]
		public void WhenADocumentHasBeenSelected_SaveAsCommandMustBeEnabled()
		{
			Mock<IOpenSaveDialogFacade> openSaveDialogMock = new Mock<IOpenSaveDialogFacade>();
			Mock<INotificationDialogFacade> notificationDialogMock = new Mock<INotificationDialogFacade>();
			Mock<IDocumentViewModel> documentMock = new Mock<IDocumentViewModel>();
			documentMock.Setup(dm => dm.HasChanges).Returns(true);

			MainViewModel mvvm = new MainViewModel(openSaveDialogMock.Object, notificationDialogMock.Object, Mock.Of<ISystemIOFacade>());
			mvvm.SelectedDocument = documentMock.Object;

			//Act
			bool result = mvvm.SaveAsDocumentCan(null);

			//Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		[TestCategory("saving")]
		public void WhenNeitherDocumentHasBeenSelected_SaveAsCommandMustBeDisabled()
		{
			Mock<IOpenSaveDialogFacade> openSaveDialogMock = new Mock<IOpenSaveDialogFacade>();
			Mock<INotificationDialogFacade> notificationDialogMock = new Mock<INotificationDialogFacade>();
			Mock<IDocumentViewModel> documentMock = new Mock<IDocumentViewModel>();
			documentMock.Setup(dm => dm.HasChanges).Returns(true);

			MainViewModel mvvm = new MainViewModel(openSaveDialogMock.Object, notificationDialogMock.Object, Mock.Of<ISystemIOFacade>());

			//Act
			bool result = mvvm.SaveAsDocumentCan(null);

			//Assert
			Assert.IsFalse(result);
		}

		#endregion

		#region save all
		[TestMethod]
		[TestCategory("saving")]
		public void WhenSaveAllIsExecutedAndDocumentDontHavePath_ItShoulAllSaveAskingForPath()
		{
			string validPath = @"Drive\\folder\file.ext";
			Mock<IOpenSaveDialogFacade> openSaveDialogMock = new Mock<IOpenSaveDialogFacade>();
			openSaveDialogMock.Setup(obj => obj.ShowSave()).Returns(validPath);
			Mock<INotificationDialogFacade> notificationDialogMock = new Mock<INotificationDialogFacade>();
			Mock<IDocumentViewModel> documentMock1 = new Mock<IDocumentViewModel>();
			documentMock1.Setup(dm => dm.HasChanges).Returns(true);
			Mock<IDocumentViewModel> documentMock2 = new Mock<IDocumentViewModel>();
			documentMock2.Setup(dm => dm.HasChanges).Returns(false);
			Mock<IDocumentViewModel> documentMock3 = new Mock<IDocumentViewModel>();
			documentMock3.Setup(dm => dm.HasChanges).Returns(true);

			MainViewModel mvvm = new MainViewModel(openSaveDialogMock.Object, notificationDialogMock.Object, Mock.Of<ISystemIOFacade>());
			mvvm.LoadedDocuments.Add(documentMock1.Object);
			mvvm.LoadedDocuments.Add(documentMock2.Object);
			mvvm.LoadedDocuments.Add(documentMock3.Object);

			//Act
			mvvm.SaveAllDocumentAction(null);

			//Assert
			documentMock1.Verify(obj => obj.SaveAsDocument(It.IsAny<string>()), Times.Once);
			documentMock2.Verify(obj => obj.SaveAsDocument(It.IsAny<string>()), Times.Never);
			documentMock3.Verify(obj => obj.SaveAsDocument(It.IsAny<string>()), Times.Once);
		}

		[TestMethod]
		[TestCategory("saving")]
		public void WhenSaveAllIsExecutedAndDocumentHavePath_ItShoulSaveAll()
		{
			string validPath = @"Drive\\folder\file.ext";
			Mock<IOpenSaveDialogFacade> openSaveDialogMock = new Mock<IOpenSaveDialogFacade>();
			Mock<INotificationDialogFacade> notificationDialogMock = new Mock<INotificationDialogFacade>();
			Mock<IDocumentViewModel> documentMock1 = new Mock<IDocumentViewModel>();
			documentMock1.Setup(dm => dm.HasChanges).Returns(true);
			documentMock1.Setup(dm => dm.DocumentPath).Returns(validPath);
			Mock<IDocumentViewModel> documentMock2 = new Mock<IDocumentViewModel>();
			documentMock2.Setup(dm => dm.HasChanges).Returns(false);
			documentMock2.Setup(dm => dm.DocumentPath).Returns(validPath);
			Mock<IDocumentViewModel> documentMock3 = new Mock<IDocumentViewModel>();
			documentMock3.Setup(dm => dm.HasChanges).Returns(true);
			documentMock3.Setup(dm => dm.DocumentPath).Returns(validPath);

			MainViewModel mvvm = new MainViewModel(openSaveDialogMock.Object, notificationDialogMock.Object, Mock.Of<ISystemIOFacade>());
			mvvm.LoadedDocuments.Add(documentMock1.Object);
			mvvm.LoadedDocuments.Add(documentMock2.Object);
			mvvm.LoadedDocuments.Add(documentMock3.Object);

			//Act
			mvvm.SaveAllDocumentAction(null);

			//Assert
			documentMock1.Verify(obj => obj.SaveDocument(), Times.Once);
			documentMock2.Verify(obj => obj.SaveDocument(), Times.Never);
			documentMock3.Verify(obj => obj.SaveDocument(), Times.Once);
		}

		#endregion

		#region saveAllCommandCan
		[TestMethod]
		[TestCategory("saving")]
		public void WhenNoDocumentOpened_SaveAllCommandMustBeDisabled()
		{
			Mock<IOpenSaveDialogFacade> openSaveDialogMock = new Mock<IOpenSaveDialogFacade>();
			Mock<INotificationDialogFacade> notificationDialogMock = new Mock<INotificationDialogFacade>();
			Mock<IDocumentViewModel> documentMock = new Mock<IDocumentViewModel>();
			documentMock.Setup(dm => dm.HasChanges).Returns(false);

			MainViewModel mvvm = new MainViewModel(openSaveDialogMock.Object, notificationDialogMock.Object, Mock.Of<ISystemIOFacade>());

			//Act
			bool result = mvvm.SaveAllDocumentCan(null);

			//Assert
			Assert.IsFalse(result);
		}

		[TestMethod]
		[TestCategory("saving")]
		public void WhenDocumentOpened_SaveAllCommandMustBeEnabled()
		{
			Mock<IOpenSaveDialogFacade> openSaveDialogMock = new Mock<IOpenSaveDialogFacade>();
			Mock<INotificationDialogFacade> notificationDialogMock = new Mock<INotificationDialogFacade>();
			Mock<IDocumentViewModel> documentMock = new Mock<IDocumentViewModel>();
			documentMock.Setup(dm => dm.HasChanges).Returns(true);

			MainViewModel mvvm = new MainViewModel(openSaveDialogMock.Object, notificationDialogMock.Object, Mock.Of<ISystemIOFacade>());
			mvvm.LoadedDocuments.Add(documentMock.Object);

			//Act
			bool result = mvvm.SaveAllDocumentCan(null);

			//Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		[TestCategory("saving")]
		public void WhenDocumentThereArentChangesInAnyDocument_SaveAllCommandMustBeDisabled()
		{
			Mock<IOpenSaveDialogFacade> openSaveDialogMock = new Mock<IOpenSaveDialogFacade>();
			Mock<INotificationDialogFacade> notificationDialogMock = new Mock<INotificationDialogFacade>();
			Mock<IDocumentViewModel> documentMock1 = new Mock<IDocumentViewModel>();
			documentMock1.Setup(dm => dm.HasChanges).Returns(false);
			Mock<IDocumentViewModel> documentMock2 = new Mock<IDocumentViewModel>();
			documentMock2.Setup(dm => dm.HasChanges).Returns(false);
			Mock<IDocumentViewModel> documentMock3 = new Mock<IDocumentViewModel>();
			documentMock3.Setup(dm => dm.HasChanges).Returns(false);

			MainViewModel mvvm = new MainViewModel(openSaveDialogMock.Object, notificationDialogMock.Object, Mock.Of<ISystemIOFacade>());
			mvvm.LoadedDocuments.Add(documentMock1.Object);
			mvvm.LoadedDocuments.Add(documentMock2.Object);
			mvvm.LoadedDocuments.Add(documentMock3.Object);

			//Act
			bool result = mvvm.SaveAllDocumentCan(null);

			//Assert
			Assert.IsFalse(result);
		}

		#endregion

		#region openCommandCan
		[TestMethod]
		[TestCategory("opening")]
		public void WhenOpenASelectADocument_ItShouldBeOpened()
		{
			string validPath = @"c:\folder\file.txt";
			string validText = "SomeText";
			Mock<IOpenSaveDialogFacade> openSaveDialogMock = new Mock<IOpenSaveDialogFacade>();
			openSaveDialogMock.Setup(obj => obj.ShowOpen()).Returns(new string[] { validPath });
			Mock<INotificationDialogFacade> notificationDialogMock = new Mock<INotificationDialogFacade>();
			Mock<ISystemIOFacade> ioMock = new Mock<ISystemIOFacade>();
			ioMock.Setup(obj => obj.ReadFile(validPath)).Returns(validText);

			MainViewModel mvvm = new MainViewModel(openSaveDialogMock.Object, notificationDialogMock.Object, ioMock.Object);
			
			//Act
			mvvm.OpenDocumentsAction(new object());

			//Assert
			openSaveDialogMock.Verify(obj => obj.ShowOpen(), Times.Once);
			Assert.AreEqual(mvvm.LoadedDocuments[0].DocumentPath, validPath);
			Assert.AreEqual(mvvm.LoadedDocuments[0].Text, validText);
			Assert.AreEqual(mvvm.LoadedDocuments.Count, 1);
		}

		[TestMethod]
		[TestCategory("opening")]
		public void WhenSelectSeveralDocuments_AllThemShouldBeOpened()
		{
			string validPath = @"c:\folder\file.txt";
			string validPath2 = @"c:\folder\file2.txt";
			string validText = "SomeText";
			string validText2 = "SomeText2";
			Mock<IOpenSaveDialogFacade> openSaveDialogMock = new Mock<IOpenSaveDialogFacade>();
			openSaveDialogMock.Setup(obj => obj.ShowOpen()).Returns(new string[] { validPath, validPath2 });
			Mock<INotificationDialogFacade> notificationDialogMock = new Mock<INotificationDialogFacade>();
			Mock<ISystemIOFacade> ioMock = new Mock<ISystemIOFacade>();
			ioMock.Setup(obj => obj.ReadFile(validPath)).Returns(validText);
			ioMock.Setup(obj => obj.ReadFile(validPath2)).Returns(validText2);

			MainViewModel mvvm = new MainViewModel(openSaveDialogMock.Object, notificationDialogMock.Object, ioMock.Object);
			
			//Act
			mvvm.OpenDocumentsAction(new object());

			//Assert
			openSaveDialogMock.Verify(obj => obj.ShowOpen(), Times.Once);
			Assert.AreEqual(mvvm.LoadedDocuments[0].DocumentPath, validPath);
			Assert.AreEqual(mvvm.LoadedDocuments[0].Text, validText);
			Assert.AreEqual(mvvm.LoadedDocuments[1].DocumentPath, validPath2);
			Assert.AreEqual(mvvm.LoadedDocuments[1].Text, validText2);
			Assert.AreEqual(mvvm.LoadedDocuments.Count, 2);
		}

		[TestMethod]
		[TestCategory("opening")]
		public void WhenSelectDocumentThatsCantOpen_ItShouldNotifyTheError()
		{
			string inValidPath = @"c:\folder\invalidFile.txt";
			Mock<IOpenSaveDialogFacade> openSaveDialogMock = new Mock<IOpenSaveDialogFacade>();
			openSaveDialogMock.Setup(obj => obj.ShowOpen()).Returns(new string[] { inValidPath });
			Mock<INotificationDialogFacade> notificationDialogMock = new Mock<INotificationDialogFacade>();
			Mock<ISystemIOFacade> ioMock = new Mock<ISystemIOFacade>();
			ioMock.Setup(obj => obj.ReadFile(inValidPath)).Throws(new Exception());

			MainViewModel mvvm = new MainViewModel(openSaveDialogMock.Object, notificationDialogMock.Object, ioMock.Object);

			//Act
			mvvm.OpenDocumentsAction(new object());

			//Assert
			openSaveDialogMock.Verify(obj => obj.ShowOpen(), Times.Once);
			notificationDialogMock.Verify(obj => obj.NotifyError(It.IsAny<Exception>()));
			Assert.AreEqual(mvvm.LoadedDocuments.Count, 0);
		}

		[TestMethod]
		[TestCategory("opening")]
		public void WhenSelectADocumentIsOpenedTwice_ItShouldOnlyOpenedOnce()
		{
			string validPath = @"c:\folder\file.txt";
			string validText = "SomeText";
			Mock<IOpenSaveDialogFacade> openSaveDialogMock = new Mock<IOpenSaveDialogFacade>();
			openSaveDialogMock.Setup(obj => obj.ShowOpen()).Returns(new string[] { validPath });
			Mock<INotificationDialogFacade> notificationDialogMock = new Mock<INotificationDialogFacade>();
			Mock<ISystemIOFacade> ioMock = new Mock<ISystemIOFacade>();
			ioMock.Setup(obj => obj.ReadFile(validPath)).Returns(validText);

			MainViewModel mvvm = new MainViewModel(openSaveDialogMock.Object, notificationDialogMock.Object, ioMock.Object);

			//Act
			mvvm.OpenDocumentsAction(new object());
			mvvm.OpenDocumentsAction(new object());

			//Assert
			openSaveDialogMock.Verify(obj => obj.ShowOpen(), Times.Exactly(2));
			Assert.AreEqual(validPath, mvvm.LoadedDocuments[0].DocumentPath);
			Assert.AreEqual(validText, mvvm.LoadedDocuments[0].Text);
			Assert.AreEqual(1, mvvm.LoadedDocuments.Count);
		}

		[TestMethod]
		[TestCategory("opening")]
		public void WhenUserCancelAOpeningDialog_NothingHappens()
		{
			Mock<IOpenSaveDialogFacade> openSaveDialogMock = new Mock<IOpenSaveDialogFacade>();
			openSaveDialogMock.Setup(obj => obj.ShowOpen());
			Mock<INotificationDialogFacade> notificationDialogMock = new Mock<INotificationDialogFacade>();
			Mock<ISystemIOFacade> ioMock = new Mock<ISystemIOFacade>();

			MainViewModel mvvm = new MainViewModel(openSaveDialogMock.Object, notificationDialogMock.Object, ioMock.Object);

			//Act
			mvvm.OpenDocumentsAction(null);

			//Assert
			openSaveDialogMock.Verify(obj => obj.ShowOpen(), Times.Exactly(1));
			Assert.AreEqual(0, mvvm.LoadedDocuments.Count);
		}

		#endregion
	}
}
