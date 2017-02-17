using System.Collections.ObjectModel;
using Example.TextEditor.ViewModel.Base;

namespace Example.TextEditor.ViewModel.Parsing.Elements.Base
{
	public class NodeViewModel : ViewModelBase
	{
		ObservableCollection<NodeViewModel> _elements = new ObservableCollection<NodeViewModel>();
		public ObservableCollection<NodeViewModel> Elements
		{
			get { return _elements; }
		}

		object _content;
		public object Content
		{
			get { return _content; }
			set 
			{ 
				_content = value;
				RaisePropertyChanged("Content");
			}
		}
	}
}
