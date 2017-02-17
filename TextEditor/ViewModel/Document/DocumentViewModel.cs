using System.IO;
using Example.TextEditor.Application.SystemIO.Contracts;
using Example.TextEditor.ViewModel.Base;
using Example.TextEditor.ViewModel.Parsing;
using Example.TextEditor.ViewModel.Parsing.Elements;

namespace Example.TextEditor.ViewModel.Document
{
	public class DocumentViewModel : ViewModelBase, IDocumentViewModel
	{
		private readonly ISystemIOFacade _ioManager;
		private readonly IParserViewModel _parser;


        #region constructors
        public DocumentViewModel(ISystemIOFacade ioManager, IParserViewModel iparser)
        {
            _ioManager = ioManager;
            Name = "New";
            _parser = iparser;
        }
        #endregion

		#region Properties
		private string _text = string.Empty;
		public string Text
		{
			get { return _text; }
			set
			{
				_text = value;
				RaisePropertyChanged("Text");
				HasChanges = true;
				_parser.UpdateStructure(value);
			}
		}

		bool _hasChanges = true;
		public bool HasChanges
		{
			get { return _hasChanges; }
			private set
			{
				_hasChanges = value;
				RaisePropertyChanged("HasChanges");
			}
		}

		private string _name = "new";
		public string Name
		{
			get { return _name; }
			private set
			{
				_name = value;
				RaisePropertyChanged("Name");
			}
		}

		private string _documentPath = string.Empty;
		public string DocumentPath
		{
			get { return _documentPath; }
			private set
			{
				_documentPath = value;
				RaisePropertyChanged("DocumentPath");
				Name = Path.GetFileName(value);
			}
		}

		public RootVM Elements
		{
			get { return _parser.Root; }
		}
		#endregion

		#region Public Methods
		public void OpenDocument(string filePath)
		{
			Text = _ioManager.ReadFile(filePath);
			DocumentPath = filePath;
			HasChanges = false;
			//ParseXMLDocument();
		}

		public void SaveDocument()
		{
			SaveAsDocument(DocumentPath);
		}

		public void SaveAsDocument(string filePath)
		{
			_ioManager.WriteFile(filePath, Text);
			DocumentPath = filePath;
			HasChanges = false;
		}
		#endregion

	}
}
