using System.Collections.ObjectModel;
using Example.TextEditor.ViewModel.Elements;

namespace Example.TextEditor.ViewModel.Parsing.Elements
{
	public class RootVM : NodeViewModel
	{
	    readonly ObservableCollection<NodeViewModel> _elements = new ObservableCollection<NodeViewModel>();
		public ObservableCollection<NodeViewModel> Elements
		{
			get { return _elements; }
		}
	}
}
