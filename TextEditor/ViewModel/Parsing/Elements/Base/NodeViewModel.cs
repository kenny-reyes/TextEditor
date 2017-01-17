using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Microsoft.TeamFoundation.MVVM;


namespace Example.TextEditor.ViewModel.Elements
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
