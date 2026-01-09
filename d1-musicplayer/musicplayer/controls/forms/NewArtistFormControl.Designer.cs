namespace musicplayer.controls.forms
{
	partial class NewArtistFormControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewArtistFormControl));
			bAdd = new Button();
			bChangeImage = new Button();
			pbImage = new PictureBox();
			tbName = new TextBox();
			labelName = new Label();
			lbArtists = new ListBox();
			((System.ComponentModel.ISupportInitialize)pbImage).BeginInit();
			SuspendLayout();
			// 
			// bAdd
			// 
			bAdd.Location = new Point(0, 269);
			bAdd.Margin = new Padding(2);
			bAdd.Name = "bAdd";
			bAdd.Size = new Size(92, 29);
			bAdd.TabIndex = 9;
			bAdd.Text = "Add";
			bAdd.UseVisualStyleBackColor = true;
			bAdd.Click += bAdd_Click;
			// 
			// bChangeImage
			// 
			bChangeImage.Location = new Point(0, 224);
			bChangeImage.Margin = new Padding(2);
			bChangeImage.Name = "bChangeImage";
			bChangeImage.Size = new Size(92, 29);
			bChangeImage.TabIndex = 8;
			bChangeImage.Text = "Change";
			bChangeImage.UseVisualStyleBackColor = true;
			bChangeImage.Click += bChangeImage_Click;
			// 
			// pbImage
			// 
			pbImage.Image = (Image)resources.GetObject("pbImage.Image");
			pbImage.InitialImage = (Image)resources.GetObject("pbImage.InitialImage");
			pbImage.Location = new Point(0, 56);
			pbImage.Margin = new Padding(2);
			pbImage.Name = "pbImage";
			pbImage.Size = new Size(160, 164);
			pbImage.SizeMode = PictureBoxSizeMode.StretchImage;
			pbImage.TabIndex = 7;
			pbImage.TabStop = false;
			// 
			// tbName
			// 
			tbName.Location = new Point(0, 22);
			tbName.Margin = new Padding(2);
			tbName.Name = "tbName";
			tbName.Size = new Size(315, 27);
			tbName.TabIndex = 6;
			tbName.TextChanged += tbName_TextChanged;
			// 
			// labelName
			// 
			labelName.AutoSize = true;
			labelName.Location = new Point(0, 0);
			labelName.Margin = new Padding(2, 0, 2, 0);
			labelName.Name = "labelName";
			labelName.Size = new Size(49, 20);
			labelName.TabIndex = 5;
			labelName.Text = "Name";
			// 
			// lbArtists
			// 
			lbArtists.FormattingEnabled = true;
			lbArtists.Location = new Point(165, 56);
			lbArtists.Name = "lbArtists";
			lbArtists.Size = new Size(150, 164);
			lbArtists.TabIndex = 10;
			lbArtists.SelectedIndexChanged += lbArtists_SelectedIndexChanged;
			// 
			// NewArtistFormControl
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(lbArtists);
			Controls.Add(bAdd);
			Controls.Add(bChangeImage);
			Controls.Add(pbImage);
			Controls.Add(tbName);
			Controls.Add(labelName);
			Name = "NewArtistFormControl";
			Size = new Size(320, 300);
			((System.ComponentModel.ISupportInitialize)pbImage).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button bAdd;
		private Button bChangeImage;
		private PictureBox pbImage;
		private TextBox tbName;
		private Label labelName;
		private ListBox lbArtists;
	}
}
