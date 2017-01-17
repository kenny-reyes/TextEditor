namespace CryptoText
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
			this.toolStripTextBox2 = new System.Windows.Forms.ToolStripTextBox();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.NewButton = new System.Windows.Forms.ToolStripButton();
			this.LoadButton = new System.Windows.Forms.ToolStripButton();
			this.SaveButton = new System.Windows.Forms.ToolStripButton();
			this.SaveAsButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.statusStrip1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
			this.statusStrip1.Location = new System.Drawing.Point(0, 392);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(566, 22);
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(39, 17);
			this.toolStripStatusLabel1.Text = "Ready";
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewButton,
            this.LoadButton,
            this.SaveButton,
            this.SaveAsButton,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.toolStripTextBox1,
            this.toolStripSeparator2,
            this.toolStripLabel2,
            this.toolStripTextBox2,
            this.toolStripButton1});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(566, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(26, 22);
			this.toolStripLabel1.Text = "Key";
			// 
			// toolStripTextBox1
			// 
			this.toolStripTextBox1.Name = "toolStripTextBox1";
			this.toolStripTextBox1.Size = new System.Drawing.Size(100, 25);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripLabel2
			// 
			this.toolStripLabel2.Name = "toolStripLabel2";
			this.toolStripLabel2.Size = new System.Drawing.Size(41, 22);
			this.toolStripLabel2.Text = "Vector";
			// 
			// toolStripTextBox2
			// 
			this.toolStripTextBox2.Name = "toolStripTextBox2";
			this.toolStripTextBox2.Size = new System.Drawing.Size(100, 25);
			// 
			// richTextBox1
			// 
			this.richTextBox1.Location = new System.Drawing.Point(0, 29);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(566, 360);
			this.richTextBox1.TabIndex = 2;
			this.richTextBox1.Text = "";
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// NewButton
			// 
			this.NewButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.NewButton.Image = ((System.Drawing.Image)(resources.GetObject("NewButton.Image")));
			this.NewButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.NewButton.Name = "NewButton";
			this.NewButton.Size = new System.Drawing.Size(23, 22);
			this.NewButton.Text = "New Document";
			this.NewButton.Click += new System.EventHandler(this.NewButton_Click);
			// 
			// LoadButton
			// 
			this.LoadButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.LoadButton.Image = global::CryptoText.Properties.Resources._48x48_open;
			this.LoadButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.LoadButton.Name = "LoadButton";
			this.LoadButton.Size = new System.Drawing.Size(23, 22);
			this.LoadButton.Text = "toolStripButton1";
			this.LoadButton.ToolTipText = "Open File";
			this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
			// 
			// SaveButton
			// 
			this.SaveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.SaveButton.Image = global::CryptoText.Properties.Resources._48x48_save;
			this.SaveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.SaveButton.Name = "SaveButton";
			this.SaveButton.Size = new System.Drawing.Size(23, 22);
			this.SaveButton.Text = "SaveButton";
			this.SaveButton.ToolTipText = "Save File";
			this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
			// 
			// SaveAsButton
			// 
			this.SaveAsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.SaveAsButton.Image = global::CryptoText.Properties.Resources._48x48_save_as;
			this.SaveAsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.SaveAsButton.Name = "SaveAsButton";
			this.SaveAsButton.Size = new System.Drawing.Size(23, 22);
			this.SaveAsButton.Text = "SaveAs";
			this.SaveAsButton.ToolTipText = "Save As File";
			this.SaveAsButton.Click += new System.EventHandler(this.SaveAsButton_Click);
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton1.Image = global::CryptoText.Properties.Resources.help;
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton1.Text = "toolStripButton1";
			this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(566, 414);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.statusStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Form1";
			this.Text = "CryptoText 1.0";
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton LoadButton;
		private System.Windows.Forms.ToolStripButton SaveButton;
		private System.Windows.Forms.ToolStripButton SaveAsButton;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripLabel toolStripLabel2;
		private System.Windows.Forms.ToolStripTextBox toolStripTextBox2;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.ToolStripButton NewButton;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
	}
}

