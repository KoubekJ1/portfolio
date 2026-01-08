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
			trbRating = new TrackBar();
			lRatingValue = new Label();
			lRatingLabel = new Label();
			((System.ComponentModel.ISupportInitialize)trbRating).BeginInit();
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
			bAdd.Click += bAdd_Click_1;
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
			bFile.Click += bFile_Click_1;
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
			tbName.TextChanged += tbName_TextChanged_1;
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
			// trbRating
			// 
			trbRating.Location = new Point(157, 104);
			trbRating.Maximum = 50;
			trbRating.Name = "trbRating";
			trbRating.Size = new Size(130, 56);
			trbRating.TabIndex = 10;
			trbRating.Scroll += trackBar1_Scroll;
			// 
			// lRatingValue
			// 
			lRatingValue.AutoSize = true;
			lRatingValue.Location = new Point(157, 79);
			lRatingValue.Margin = new Padding(2, 0, 2, 0);
			lRatingValue.Name = "lRatingValue";
			lRatingValue.Size = new Size(17, 20);
			lRatingValue.TabIndex = 11;
			lRatingValue.Text = "0";
			lRatingValue.Click += lRatingValue_Click;
			// 
			// lRatingLabel
			// 
			lRatingLabel.AutoSize = true;
			lRatingLabel.Location = new Point(157, 59);
			lRatingLabel.Margin = new Padding(2, 0, 2, 0);
			lRatingLabel.Name = "lRatingLabel";
			lRatingLabel.Size = new Size(52, 20);
			lRatingLabel.TabIndex = 12;
			lRatingLabel.Text = "Rating";
			// 
			// NewSongFormControl
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(lRatingLabel);
			Controls.Add(lRatingValue);
			Controls.Add(trbRating);
			Controls.Add(bAdd);
			Controls.Add(bFile);
			Controls.Add(lFile);
			Controls.Add(tbName);
			Controls.Add(label1);
			Name = "NewSongFormControl";
			Size = new Size(296, 149);
			Load += NewSongForm_Load;
			((System.ComponentModel.ISupportInitialize)trbRating).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button bAdd;
		private Button bFile;
		private Label lFile;
		private TextBox tbName;
		private Label label1;
		private TrackBar trbRating;
		private Label lRatingValue;
		private Label lRatingLabel;
	}
}
