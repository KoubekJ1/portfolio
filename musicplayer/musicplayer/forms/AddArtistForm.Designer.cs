namespace musicplayer
{
    partial class AddArtistForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddArtistForm));
			labelName = new Label();
			tbName = new TextBox();
			pbImage = new PictureBox();
			bChangeImage = new Button();
			bAdd = new Button();
			((System.ComponentModel.ISupportInitialize)pbImage).BeginInit();
			SuspendLayout();
			// 
			// labelName
			// 
			labelName.AutoSize = true;
			labelName.Location = new Point(7, 6);
			labelName.Margin = new Padding(2, 0, 2, 0);
			labelName.Name = "labelName";
			labelName.Size = new Size(49, 20);
			labelName.TabIndex = 0;
			labelName.Text = "Name";
			// 
			// tbName
			// 
			tbName.Location = new Point(7, 28);
			tbName.Margin = new Padding(2);
			tbName.Name = "tbName";
			tbName.Size = new Size(248, 27);
			tbName.TabIndex = 1;
			tbName.TextChanged += tbName_TextChanged;
			// 
			// pbImage
			// 
			pbImage.Image = (Image)resources.GetObject("pbImage.Image");
			pbImage.InitialImage = (Image)resources.GetObject("pbImage.InitialImage");
			pbImage.Location = new Point(7, 62);
			pbImage.Margin = new Padding(2);
			pbImage.Name = "pbImage";
			pbImage.Size = new Size(160, 160);
			pbImage.SizeMode = PictureBoxSizeMode.StretchImage;
			pbImage.TabIndex = 2;
			pbImage.TabStop = false;
			// 
			// bChangeImage
			// 
			bChangeImage.Location = new Point(7, 226);
			bChangeImage.Margin = new Padding(2);
			bChangeImage.Name = "bChangeImage";
			bChangeImage.Size = new Size(92, 29);
			bChangeImage.TabIndex = 3;
			bChangeImage.Text = "Change";
			bChangeImage.UseVisualStyleBackColor = true;
			bChangeImage.Click += bChangeImage_Click;
			// 
			// bAdd
			// 
			bAdd.Location = new Point(7, 274);
			bAdd.Margin = new Padding(2);
			bAdd.Name = "bAdd";
			bAdd.Size = new Size(92, 29);
			bAdd.TabIndex = 4;
			bAdd.Text = "Add";
			bAdd.UseVisualStyleBackColor = true;
			bAdd.Click += bAdd_Click;
			// 
			// AddArtistForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(261, 310);
			Controls.Add(bAdd);
			Controls.Add(bChangeImage);
			Controls.Add(pbImage);
			Controls.Add(tbName);
			Controls.Add(labelName);
			Margin = new Padding(2);
			MaximizeBox = false;
			Name = "AddArtistForm";
			Text = "Add Artist";
			((System.ComponentModel.ISupportInitialize)pbImage).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label labelName;
        private TextBox tbName;
        private PictureBox pbImage;
        private Button bChangeImage;
        private Button bAdd;
    }
}