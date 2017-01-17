using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CryptoText
{
	public partial class Form1 : Form
	{
		Crypto crypt;
		string path;

		public Form1()
		{
			InitializeComponent();
		}

		private void LoadButton_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				if (string.IsNullOrEmpty(toolStripTextBox1.Text) && string.IsNullOrEmpty(toolStripTextBox2.Text))
					crypt = new Crypto();
				else
					crypt = new Crypto(toolStripTextBox1.Text, toolStripTextBox2.Text);

				StreamReader sr = new StreamReader(openFileDialog1.FileName);
				try
				{
					string texto = sr.ReadToEnd();
					string xmltxt = crypt.DecryptText(texto);
					richTextBox1.Text = xmltxt;
					path = openFileDialog1.FileName;
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
				finally
				{
					sr.Close();
				}
			}
		}

		private void NewButton_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(path))
			{
				SaveAsButton_Click(null, null);
			}
			richTextBox1.Text = "";
		}

		private void SaveButton_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(path))
				SaveAsButton_Click(null, null);
			else
				Save();
		}

		private void SaveAsButton_Click(object sender, EventArgs e)
		{
			if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				path = saveFileDialog1.FileName;
				Save();
			}
		}

		void Save()
		{
			if (string.IsNullOrEmpty(toolStripTextBox1.Text) && string.IsNullOrEmpty(toolStripTextBox2.Text))
				crypt = new Crypto();
			else
				crypt = new Crypto(toolStripTextBox1.Text, toolStripTextBox2.Text);

			StreamWriter sw = new StreamWriter(path);
			try
			{
				string texto = richTextBox1.Text;
				texto = crypt.EncryptText(texto);
				sw.Write(texto);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				sw.Close();
			}
		}


		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			About a = new About();
			a.ShowDialog();
		}
	}
}
