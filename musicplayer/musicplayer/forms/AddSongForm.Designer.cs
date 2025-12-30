namespace musicplayer.forms
{
    partial class AddSongForm
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
			label1 = new Label();
			tbName = new TextBox();
			lFile = new Label();
			bFile = new Button();
			bAdd = new Button();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(7, 6);
			label1.Margin = new Padding(2, 0, 2, 0);
			label1.Name = "label1";
			label1.Size = new Size(49, 20);
			label1.TabIndex = 0;
			label1.Text = "Name";
			// 
			// tbName
			// 
			tbName.Location = new Point(7, 28);
			tbName.Margin = new Padding(2);
			tbName.Name = "tbName";
			tbName.Size = new Size(285, 27);
			tbName.TabIndex = 1;
			tbName.TextChanged += tbName_TextChanged;
			// 
			// lFile
			// 
			lFile.AutoSize = true;
			lFile.Location = new Point(7, 54);
			lFile.Margin = new Padding(2, 0, 2, 0);
			lFile.Name = "lFile";
			lFile.Size = new Size(123, 20);
			lFile.TabIndex = 2;
			lFile.Text = "(No file selected)";
			// 
			// bFile
			// 
			bFile.Location = new Point(7, 76);
			bFile.Margin = new Padding(2);
			bFile.Name = "bFile";
			bFile.Size = new Size(92, 29);
			bFile.TabIndex = 3;
			bFile.Text = "Select file";
			bFile.UseVisualStyleBackColor = true;
			bFile.Click += bFile_Click;
			// 
			// bAdd
			// 
			bAdd.Location = new Point(7, 124);
			bAdd.Margin = new Padding(2);
			bAdd.Name = "bAdd";
			bAdd.Size = new Size(92, 29);
			bAdd.TabIndex = 4;
			bAdd.Text = "Add";
			bAdd.UseVisualStyleBackColor = true;
			bAdd.Click += bAdd_Click;
			// 
			// AddSongForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(317, 188);
			Controls.Add(bAdd);
			Controls.Add(bFile);
			Controls.Add(lFile);
			Controls.Add(tbName);
			Controls.Add(label1);
			Margin = new Padding(2);
			MaximizeBox = false;
			Name = "AddSongForm";
			Text = "Add Song";
			Load += AddSongForm_Load;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label label1;
        private TextBox tbName;
        private Label lFile;
        private Button bFile;
        private Button bAdd;
	}
}