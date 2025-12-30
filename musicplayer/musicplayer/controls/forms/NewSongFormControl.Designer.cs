namespace musicplayer.controls.forms
{
	partial class NewSongFormControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			bAdd = new Button();
			bFile = new Button();
			lFile = new Label();
			tbName = new TextBox();
			label1 = new Label();
			SuspendLayout();
			// 
			// bAdd
			// 
			bAdd.Location = new Point(2, 118);
			bAdd.Margin = new Padding(2);
			bAdd.Name = "bAdd";
			bAdd.Size = new Size(92, 29);
			bAdd.TabIndex = 9;
			bAdd.Text = "Add";
			bAdd.UseVisualStyleBackColor = true;
			bAdd.Click += this.bAdd_Click_1;
			// 
			// bFile
			// 
			bFile.Location = new Point(2, 70);
			bFile.Margin = new Padding(2);
			bFile.Name = "bFile";
			bFile.Size = new Size(92, 29);
			bFile.TabIndex = 8;
			bFile.Text = "Select file";
			bFile.UseVisualStyleBackColor = true;
			bFile.Click += this.bFile_Click_1;
			// 
			// lFile
			// 
			lFile.AutoSize = true;
			lFile.Location = new Point(2, 48);
			lFile.Margin = new Padding(2, 0, 2, 0);
			lFile.Name = "lFile";
			lFile.Size = new Size(123, 20);
			lFile.TabIndex = 7;
			lFile.Text = "(No file selected)";
			// 
			// tbName
			// 
			tbName.Location = new Point(2, 22);
			tbName.Margin = new Padding(2);
			tbName.Name = "tbName";
			tbName.Size = new Size(285, 27);
			tbName.TabIndex = 6;
			tbName.TextChanged += this.tbName_TextChanged_1;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(2, 0);
			label1.Margin = new Padding(2, 0, 2, 0);
			label1.Name = "label1";
			label1.Size = new Size(49, 20);
			label1.TabIndex = 5;
			label1.Text = "Name";
			// 
			// NewSongFormControl
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(bAdd);
			Controls.Add(bFile);
			Controls.Add(lFile);
			Controls.Add(tbName);
			Controls.Add(label1);
			Name = "NewSongFormControl";
			Size = new Size(293, 149);
			Load += NewSongForm_Load;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button bAdd;
		private Button bFile;
		private Label lFile;
		private TextBox tbName;
		private Label label1;
	}
}
