﻿using System.Windows;
using System;
using System.Windows.Threading;
using Example.TextEditor.Model.SystemIO;
using Example.TextEditor.View;
using Example.TextEditor.ViewModel;

namespace Example.TextEditor
{
	public class App : Application
	{
		[STAThread]
		public static void Main()
		{
			(new App()).Run();
		}


		public App()
		{
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(this.CurrentDomain_UnhandledException);
			this.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(Application_DispatcherUnhandledException);
		}


		protected override void OnStartup(StartupEventArgs e)
		{
			MainViewModel viewModel = new MainViewModel(new OpenSaveDialogFacade(), new NotificationDialogFacade(), new SystemIOFacade());
			if (e.Args.Length > 0)
			{
				if (!string.IsNullOrEmpty(e.Args[0]))
				{
					//viewModel
				}
			}
			this.MainWindow = new MainWindow();
			this.MainWindow.DataContext = viewModel;
			this.MainWindow.Show();
			base.OnStartup(e);
		}


		private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			MessageBox.Show(e.ExceptionObject.ToString());
		}


		private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			MessageBox.Show(e.Exception.Message);
			e.Handled = true;
		}
	}
}
