using System.ComponentModel;

namespace Sedecal.CryptoText.ViewModel.Base
{
	public class ViewModelBase : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged Members
		public event PropertyChangedEventHandler PropertyChanged;

		protected void Notify(string propName)
		{
			//lanzar evento
			if (this.PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propName));
			}
		}
		#endregion

		private string id;
		public string Id
		{
			get { return id; }
			set
			{
				id = value;
				Notify("Id");
			}
		}

		private string name;
		public string Name
		{
			get { return name; }
			set
			{
				name = value;
				Notify("Name");
			}
		}
	}
}
