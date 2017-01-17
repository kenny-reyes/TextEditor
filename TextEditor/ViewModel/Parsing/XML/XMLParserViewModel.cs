using System.Xml;
using Example.TextEditor.ViewModel.Parsing.Elements;
using Microsoft.TeamFoundation.MVVM;

namespace Example.TextEditor.ViewModel.Parsing.XML
{
	public class XMLParserViewModel : ViewModelBase, IParserViewModel
	{
		#region Properties
		private RootVM _root;
		public RootVM Root
		{
			get { return this._root; }
			private set
			{
				this._root = value;
				RaisePropertyChanged("Root");
			}
		}
		#endregion

		#region Public methods

		public void UpdateStructure(string documentText)
		{
			XmlDocument xml = new XmlDocument();
			try
			{
				xml.LoadXml(documentText);
				//Root = GetElements(xml);
			}
			catch
			{
				Root = null;
			}
		}

		#endregion

		#region Private methods

		//RootVM GetElements(XmlDocument xmlDocument)
		//{
		//	RootVM root = new RootVM();
		//	foreach (XmlNode item in xmlDocument.ChildNodes)
		//	{
		//		if (item is XmlElement)
		//		{
		//			NodeViewModel node = GetElementsRecursive((XmlElement)item);
		//			if (node != null)
		//				root.Elements.Add(node);
		//		}
		//	}
		//	return root;
		//}

		//NodeViewModel GetElementsRecursive(XmlElement xmlElement)
		//{
		//	NodeViewModel nodeVM;

		//	if(!xmlElement.HasChildNodes)
		//	{
		//		//return GenerateNodeViewModel()
		//	}
		//	else
		//	{
		//		//nodeVM = new GroupNodeViewModel()
		//		nodeVM.Content = xmlElement.Name;
		//		foreach (XmlNode childNode in xmlElement.ChildNodes)
		//		{
		//			NodeViewModel childNodeVM = GetElementsRecursive((XmlElement)childNode);
		//			if (childNodeVM != null)
		//				nodeVM.Elements.Add(childNodeVM);
		//		}
		//	}
		//	return nodeVM;
		//}

		//private NodeViewModel GenerateNodeViewModel(XmlNode childNode)
		//{
		//	//if (childNode is XmlElement)
		//	//{
		//	//	if (childNodeVM != null)
		//	//		childNodeVM.Elements.Add(nodeVM);
		//	//}
		//	//else if (childNode is XmlText)
		//	//{
		//	//	NodeViewModel childNodeVM = new TextNodeViewModel();
		//	//	childNodeVM.Content = xmlElement.Name;
		//	//}
		//}

		#endregion
	}
}
