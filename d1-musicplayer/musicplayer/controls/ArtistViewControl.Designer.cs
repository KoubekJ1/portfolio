namespace musicplayer.controls
{
	partial class ArtistViewControl
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
			pbImage = new PictureBox();
			lArtistName = new Label();
			bDelete = new Button();
			bEdit = new Button();
			pAlbumlist = new Panel();
			((System.ComponentModel.ISupportInitialize)pbImage).BeginInit();
			SuspendLayout();
			// 
			// pbImage
			// 
			pbImage.Location = new Point(3, 3);
			pbImage.Name = "pbImage";
			pbImage.Size = new Size(200, 200);
			pbImage.TabIndex = 0;
			pbImage.TabStop = false;
			// 
			// lArtistName
			// 
			lArtistName.AutoSize = true;
			lArtistName.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, 238);
			lArtistName.Location = new Point(209, 3);
			lArtistName.Name = "lArtistName";
			lArtistName.Size = new Size(198, 46);
			lArtistName.TabIndex = 1;
			lArtistName.Text = "Artist Name";
			// 
			// bDelete
			// 
			bDelete.Location = new Point(209, 174);
			bDelete.Name = "bDelete";
			bDelete.Size = new Size(94, 29);
			bDelete.TabIndex = 2;
			bDelete.Text = "Delete";
			bDelete.UseVisualStyleBackColor = true;
			bDelete.Click += bDelete_Click;
			// 
			// bEdit
			// 
			bEdit.Location = new Point(209, 139);
			bEdit.Name = "bEdit";
			bEdit.Size = new Size(94, 29);
			bEdit.TabIndex = 3;
			bEdit.Text = "Edit";
			bEdit.UseVisualStyleBackColor = true;
			bEdit.Click += bEdit_Click;
			// 
			// pAlbumlist
			// 
			pAlbumlist.Location = new Point(3, 209);
			pAlbumlist.Name = "pAlbumlist";
			pAlbumlist.Size = new Size(834, 438);
			pAlbumlist.TabIndex = 4;
			// 
			// ArtistViewControl
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(pAlbumlist);
			Controls.Add(bEdit);
			Controls.Add(bDelete);
			Controls.Add(lArtistName);
			Controls.Add(pbImage);
			Name = "ArtistViewControl";
			Size = new Size(840, 650);
			((System.ComponentModel.ISupportInitialize)pbImage).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private PictureBox pbImage;
		private Label lArtistName;
		private Button bDelete;
		private Button bEdit;
		private Panel pAlbumlist;
	}
}
