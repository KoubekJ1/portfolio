namespace musicplayer.controls
{
	partial class AlbumSongListControl
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
			flpSongs = new FlowLayoutPanel();
			pbAlbumImage = new PictureBox();
			lAlbumName = new Label();
			lArtistName = new Label();
			bEdit = new Button();
			bDelete = new Button();
			((System.ComponentModel.ISupportInitialize)pbAlbumImage).BeginInit();
			SuspendLayout();
			// 
			// flpSongs
			// 
			flpSongs.AutoScroll = true;
			flpSongs.Dock = DockStyle.Bottom;
			flpSongs.FlowDirection = FlowDirection.TopDown;
			flpSongs.Location = new Point(0, 209);
			flpSongs.Name = "flpSongs";
			flpSongs.Size = new Size(840, 441);
			flpSongs.TabIndex = 0;
			flpSongs.WrapContents = false;
			// 
			// pbAlbumImage
			// 
			pbAlbumImage.Location = new Point(3, 3);
			pbAlbumImage.Name = "pbAlbumImage";
			pbAlbumImage.Size = new Size(200, 200);
			pbAlbumImage.TabIndex = 1;
			pbAlbumImage.TabStop = false;
			// 
			// lAlbumName
			// 
			lAlbumName.AutoSize = true;
			lAlbumName.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, 238);
			lAlbumName.Location = new Point(209, 3);
			lAlbumName.Name = "lAlbumName";
			lAlbumName.Size = new Size(210, 46);
			lAlbumName.TabIndex = 2;
			lAlbumName.Text = "Album name";
			// 
			// lArtistName
			// 
			lArtistName.AutoSize = true;
			lArtistName.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
			lArtistName.Location = new Point(209, 49);
			lArtistName.Name = "lArtistName";
			lArtistName.Size = new Size(132, 31);
			lArtistName.TabIndex = 3;
			lArtistName.Text = "Artist name";
			// 
			// bEdit
			// 
			bEdit.Location = new Point(209, 139);
			bEdit.Name = "bEdit";
			bEdit.Size = new Size(94, 29);
			bEdit.TabIndex = 4;
			bEdit.Text = "Edit";
			bEdit.UseVisualStyleBackColor = true;
			bEdit.Click += bEdit_Click;
			// 
			// bDelete
			// 
			bDelete.Location = new Point(209, 174);
			bDelete.Name = "bDelete";
			bDelete.Size = new Size(94, 29);
			bDelete.TabIndex = 5;
			bDelete.Text = "Delete";
			bDelete.UseVisualStyleBackColor = true;
			bDelete.Click += bDelete_Click;
			// 
			// AlbumSongListControl
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(bDelete);
			Controls.Add(bEdit);
			Controls.Add(lArtistName);
			Controls.Add(lAlbumName);
			Controls.Add(pbAlbumImage);
			Controls.Add(flpSongs);
			Name = "AlbumSongListControl";
			Size = new Size(840, 650);
			((System.ComponentModel.ISupportInitialize)pbAlbumImage).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private FlowLayoutPanel flpSongs;
		private PictureBox pbAlbumImage;
		private Label lAlbumName;
		private Label lArtistName;
		private Button bEdit;
		private Button bDelete;
	}
}
