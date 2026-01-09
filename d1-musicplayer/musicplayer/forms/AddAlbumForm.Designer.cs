namespace musicplayer
{
	partial class AddAlbumForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddAlbumForm));
			lName = new Label();
			tbName = new TextBox();
			pbImage = new PictureBox();
			bChange = new Button();
			lSongs = new Label();
			bAddSong = new Button();
			bUp = new Button();
			bDown = new Button();
			bRemove = new Button();
			bAddAlbum = new Button();
			lbSongs = new ListBox();
			pNewSongFormContainer = new Panel();
			lNewSongLabel = new Label();
			pArtistContainer = new Panel();
			lArtistLabel = new Label();
			dtpReleaseDate = new DateTimePicker();
			label1 = new Label();
			cbType = new ComboBox();
			lTypeLabel = new Label();
			((System.ComponentModel.ISupportInitialize)pbImage).BeginInit();
			SuspendLayout();
			// 
			// lName
			// 
			lName.AutoSize = true;
			lName.Location = new Point(12, 9);
			lName.Name = "lName";
			lName.Size = new Size(49, 20);
			lName.TabIndex = 0;
			lName.Text = "Name";
			// 
			// tbName
			// 
			tbName.Location = new Point(12, 32);
			tbName.Name = "tbName";
			tbName.Size = new Size(328, 27);
			tbName.TabIndex = 1;
			tbName.TextChanged += tbName_TextChanged;
			// 
			// pbImage
			// 
			pbImage.Image = (Image)resources.GetObject("pbImage.Image");
			pbImage.InitialImage = (Image)resources.GetObject("pbImage.InitialImage");
			pbImage.Location = new Point(12, 65);
			pbImage.Name = "pbImage";
			pbImage.Size = new Size(125, 125);
			pbImage.SizeMode = PictureBoxSizeMode.StretchImage;
			pbImage.TabIndex = 2;
			pbImage.TabStop = false;
			// 
			// bChange
			// 
			bChange.Location = new Point(12, 196);
			bChange.Name = "bChange";
			bChange.Size = new Size(125, 29);
			bChange.TabIndex = 3;
			bChange.Text = "Change";
			bChange.UseVisualStyleBackColor = true;
			bChange.Click += bChange_Click;
			// 
			// lSongs
			// 
			lSongs.AutoSize = true;
			lSongs.Location = new Point(274, 88);
			lSongs.Name = "lSongs";
			lSongs.Size = new Size(49, 20);
			lSongs.TabIndex = 5;
			lSongs.Text = "Songs";
			// 
			// bAddSong
			// 
			bAddSong.Location = new Point(274, 281);
			bAddSong.Name = "bAddSong";
			bAddSong.Size = new Size(151, 29);
			bAddSong.TabIndex = 9;
			bAddSong.Text = "Add existing";
			bAddSong.UseVisualStyleBackColor = true;
			bAddSong.Click += bAddSong_Click;
			// 
			// bUp
			// 
			bUp.Location = new Point(431, 146);
			bUp.Name = "bUp";
			bUp.Size = new Size(43, 29);
			bUp.TabIndex = 11;
			bUp.Text = "↑";
			bUp.UseVisualStyleBackColor = true;
			bUp.Click += bUp_Click;
			// 
			// bDown
			// 
			bDown.Location = new Point(432, 181);
			bDown.Name = "bDown";
			bDown.Size = new Size(43, 29);
			bDown.TabIndex = 12;
			bDown.Text = "↓";
			bDown.UseVisualStyleBackColor = true;
			bDown.Click += bDown_Click;
			// 
			// bRemove
			// 
			bRemove.Location = new Point(274, 316);
			bRemove.Name = "bRemove";
			bRemove.Size = new Size(151, 29);
			bRemove.TabIndex = 13;
			bRemove.Text = "Remove";
			bRemove.UseVisualStyleBackColor = true;
			// 
			// bAddAlbum
			// 
			bAddAlbum.Location = new Point(12, 350);
			bAddAlbum.Name = "bAddAlbum";
			bAddAlbum.Size = new Size(125, 29);
			bAddAlbum.TabIndex = 14;
			bAddAlbum.Text = "Add Album";
			bAddAlbum.UseVisualStyleBackColor = true;
			bAddAlbum.Click += bAddAlbum_Click;
			// 
			// lbSongs
			// 
			lbSongs.FormattingEnabled = true;
			lbSongs.Location = new Point(274, 111);
			lbSongs.Name = "lbSongs";
			lbSongs.Size = new Size(151, 164);
			lbSongs.TabIndex = 16;
			// 
			// pNewSongFormContainer
			// 
			pNewSongFormContainer.Location = new Point(481, 112);
			pNewSongFormContainer.Name = "pNewSongFormContainer";
			pNewSongFormContainer.Size = new Size(296, 149);
			pNewSongFormContainer.TabIndex = 20;
			// 
			// lNewSongLabel
			// 
			lNewSongLabel.AutoSize = true;
			lNewSongLabel.Location = new Point(481, 88);
			lNewSongLabel.Name = "lNewSongLabel";
			lNewSongLabel.Size = new Size(77, 20);
			lNewSongLabel.TabIndex = 21;
			lNewSongLabel.Text = "New Song";
			// 
			// pArtistContainer
			// 
			pArtistContainer.Location = new Point(838, 112);
			pArtistContainer.Name = "pArtistContainer";
			pArtistContainer.Size = new Size(320, 300);
			pArtistContainer.TabIndex = 22;
			// 
			// lArtistLabel
			// 
			lArtistLabel.AutoSize = true;
			lArtistLabel.Location = new Point(838, 88);
			lArtistLabel.Name = "lArtistLabel";
			lArtistLabel.Size = new Size(44, 20);
			lArtistLabel.TabIndex = 23;
			lArtistLabel.Text = "Artist";
			// 
			// dtpReleaseDate
			// 
			dtpReleaseDate.Location = new Point(12, 264);
			dtpReleaseDate.Name = "dtpReleaseDate";
			dtpReleaseDate.Size = new Size(250, 27);
			dtpReleaseDate.TabIndex = 24;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(12, 241);
			label1.Name = "label1";
			label1.Size = new Size(96, 20);
			label1.TabIndex = 25;
			label1.Text = "Release Date";
			label1.Click += label1_Click;
			// 
			// cbType
			// 
			cbType.FormattingEnabled = true;
			cbType.Items.AddRange(new object[] { "LP", "EP", "SP" });
			cbType.Location = new Point(12, 316);
			cbType.Name = "cbType";
			cbType.Size = new Size(250, 28);
			cbType.TabIndex = 26;
			cbType.Text = "LP";
			// 
			// lTypeLabel
			// 
			lTypeLabel.AutoSize = true;
			lTypeLabel.Location = new Point(12, 294);
			lTypeLabel.Name = "lTypeLabel";
			lTypeLabel.Size = new Size(40, 20);
			lTypeLabel.TabIndex = 27;
			lTypeLabel.Text = "Type";
			// 
			// AddAlbumForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1187, 560);
			Controls.Add(lTypeLabel);
			Controls.Add(cbType);
			Controls.Add(label1);
			Controls.Add(dtpReleaseDate);
			Controls.Add(lArtistLabel);
			Controls.Add(pArtistContainer);
			Controls.Add(lNewSongLabel);
			Controls.Add(pNewSongFormContainer);
			Controls.Add(lbSongs);
			Controls.Add(bAddAlbum);
			Controls.Add(bRemove);
			Controls.Add(bDown);
			Controls.Add(bUp);
			Controls.Add(bAddSong);
			Controls.Add(lSongs);
			Controls.Add(bChange);
			Controls.Add(pbImage);
			Controls.Add(tbName);
			Controls.Add(lName);
			MaximizeBox = false;
			Name = "AddAlbumForm";
			Text = "Add Album";
			((System.ComponentModel.ISupportInitialize)pbImage).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label lName;
		private TextBox tbName;
		private PictureBox pbImage;
		private Button bChange;
		private Label lSongs;
		private Button bAddSong;
		private Button bUp;
		private Button bDown;
		private Button bRemove;
		private Button bAddAlbum;
		private ListBox lbSongs;
		private Panel pNewSongFormContainer;
		private Label lNewSongLabel;
		private Panel pArtistContainer;
		private Label lArtistLabel;
		private DateTimePicker dtpReleaseDate;
		private Label label1;
		private ComboBox cbType;
		private Label lTypeLabel;
	}
}