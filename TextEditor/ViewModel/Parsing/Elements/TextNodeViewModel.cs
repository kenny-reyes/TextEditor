using Example.TextEditor.ViewModel.Elements;

namespace Example.TextEditor.ViewModel.Parsing.Elements
{
	public class TextNodeViewModel : NodeViewModel
	{
		string _text;
		public string Text
		{
			get { return _text; }
			set 
			{ 
				_text = value;
				RaisePropertyChanged("Text");
			}
		}
	}
}
