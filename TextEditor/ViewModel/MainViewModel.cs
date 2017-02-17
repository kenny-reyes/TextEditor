using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using Example.TextEditor.Application.SystemIO.Contracts;
using Example.TextEditor.ViewModel.Base;
using Example.TextEditor.ViewModel.Document;
using Example.TextEditor.ViewModel.Parsing.XML;

namespace Example.TextEditor.ViewModel
{
	internal class MainViewModel : ViewModelBase
	{
		private readonly IOpenSaveDialogFacade _openSaveFileDialog;
		private readonly INotificationDialogFacade _notificationDialog;
		private readonly ISystemIOFacade _systemIOFacadeInstance;


		internal MainViewModel(IOpenSaveDialogFacade openSaveDialogFacade, INotificationDialogFacade notificationDialogFacade, ISystemIOFacade systemIOFacade)
		{
			_openSaveFileDialog = openSaveDialogFacade;
			_notificationDialog = notificationDialogFacade;
			_systemIOFacadeInstance = systemIOFacade;
			LoadedDocuments = new ObservableCollection<IDocumentViewModel>();
		}

		#region Properties
		/// <summary>
		/// Document acually focused in the list
		/// </summary>
		private IDocumentViewModel _selectedDocument;
		public IDocumentViewModel SelectedDocument
		{
			get { return _selectedDocument; }
			set
			{
				_selectedDocument = value;
				RaisePropertyChanged("SelectedDocument");
			}
		}

		/// <summary>
		/// List of opened documents
		/// </summary>
		public ObservableCollection<IDocumentViewModel> LoadedDocuments { get; private set; }
		#endregion

		#region Commands
		public ICommand NewCommand { get { return new RelayCommand(NewDocumentAction); } }
		public ICommand OpenCommand { get { return new RelayCommand(OpenDocumentsAction); } }
		public ICommand CloseCommand { get { return new RelayCommand(CloseDocumentAction); } }
		public ICommand CloseAllCommand { get { return new RelayCommand(CloseAllDocumentAction); } }
		public ICommand SaveCommand { get { return new RelayCommand(SaveDocumentAction, SaveDocumentCan); } }
		public ICommand SaveAsCommand { get { return new RelayCommand(SaveAsDocumentAction, SaveAsDocumentCan); } }
		public ICommand SaveAllCommand { get { return new RelayCommand(SaveAllDocumentAction, SaveAllDocumentCan); } }
		#endregion

		#region Actions
		/// <summary>
		/// Reference a new DocumentViewModel and include it in the LoadedDocuments list
		/// </summary>
		/// <param name="param">document object form the binding command</param>
		public void NewDocumentAction(object o)
		{
			DocumentViewModel newDocument = new DocumentViewModel(_systemIOFacadeInstance, new XMLParserViewModel());
			LoadedDocuments.Add(newDocument);
			SelectedDocument = newDocument;
		}

		/// <summary>
		/// Open a list of documents
		/// </summary>
		/// <param name="param">normally this will be null</param>
		public void OpenDocumentsAction(object o)
		{
			string[] pathsToOpen = _openSaveFileDialog.ShowOpen();
			if (pathsToOpen != null)
				foreach (var documentPath in pathsToOpen)
				{
					try
					{
						IDocumentViewModel stayDocument = LoadedDocuments.FirstOrDefault(doc => doc.DocumentPath == documentPath);
						if (stayDocument == null)
						{
							IDocumentViewModel document = new DocumentViewModel(_systemIOFacadeInstance, new XMLParserViewModel());
							document.OpenDocument(documentPath);
							LoadedDocuments.Add(document);
							SelectedDocument = document;
						}
						else SelectedDocument = stayDocument;
					}
					catch (Exception ex)
					{
						_notificationDialog.NotifyError(ex);
					}
				}
		}


		/// <summary>
		/// Close the document asking for save if the document is not saved
		/// </summary>
		/// <param name="param">document object form the binding command</param>
		public void CloseDocumentAction(object param)
		{
			IDocumentViewModel document = param != null ? (IDocumentViewModel)param : _selectedDocument;
			if (document.HasChanges)
			{
				if (_notificationDialog.AskForSaving(document.Name))
					SaveDocumentAction(document);
			}
			LoadedDocuments.Remove(document);
		}

		/// <summary>
		/// Close all document in LoadedDocument list
		/// </summary>
		/// <param name="param">document object form the binding command</param>
		public void CloseAllDocumentAction(object param)
		{
			while (LoadedDocuments.Count > 0)
			{
				CloseDocumentAction(LoadedDocuments[0]);
			}
		}

		/// <summary>
		/// Save using the documentPath property
		/// </summary>
		/// <param name="param">document object form the binding command</param>
		public void SaveDocumentAction(object param)
		{
			IDocumentViewModel document = param != null ? (IDocumentViewModel)param : _selectedDocument;
			if (!string.IsNullOrEmpty(document.DocumentPath))
			{
				try
				{
					document.SaveDocument();
				}
				catch (Exception ex)
				{
					_notificationDialog.NotifyError(ex);
					SaveAsDocumentAction(document);
				}
			}
			else SaveAsDocumentAction(document);
		}

		/// <summary>
		/// Save asking for a path
		/// </summary>
		/// <param name="param">document object form the binding command</param>
		public void SaveAsDocumentAction(object param)
		{
			IDocumentViewModel document = param != null ? (IDocumentViewModel)param : _selectedDocument;
			try
			{
				string pathToSave = _openSaveFileDialog.ShowSave();
				if (pathToSave != null) document.SaveAsDocument(pathToSave);
			}
			catch (Exception ex)
			{
				_notificationDialog.NotifyError(ex);
			}
		}

		/// <summary>
		/// Save all document in LoadedDocuments list
		/// </summary>
		/// <param name="param">document object form the binding command</param>
		public void SaveAllDocumentAction(object param)
		{
			foreach (IDocumentViewModel document in LoadedDocuments)
			{
				if (document.HasChanges) SaveDocumentAction(document);
			}
		}

		#endregion

		#region CanDo
		public bool SaveDocumentCan(object param)
		{
		    if (param != null)
				return ((IDocumentViewModel)param).HasChanges ? true : false;
		    return (_selectedDocument != null) && (SelectedDocument.HasChanges ? true : false);
		}

	    public bool SaveAsDocumentCan(object param)
		{
			return (_selectedDocument != null);
		}

		public bool SaveAllDocumentCan(object param)
		{
			return LoadedDocuments.Count(item => item.HasChanges) != 0;
		}

		#endregion
	}
}
