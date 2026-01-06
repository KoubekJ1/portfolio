namespace musicplayer
{
	partial class MusicPlayerWindow
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
			panelContent = new Panel();
			menuStrip1 = new MenuStrip();
			addToolStripMenuItem = new ToolStripMenuItem();
			artistToolStripMenuItem = new ToolStripMenuItem();
			albumToolStripMenuItem = new ToolStripMenuItem();
			songToolStripMenuItem = new ToolStripMenuItem();
			importExportToolStripMenuItem = new ToolStripMenuItem();
			toolStripMenuItem1 = new ToolStripMenuItem();
			createReportToolStripMenuItem = new ToolStripMenuItem();
			helpToolStripMenuItem = new ToolStripMenuItem();
			aboutToolStripMenuItem = new ToolStripMenuItem();
			panelMenu = new Panel();
			bSongs = new Button();
			bAlbums = new Button();
			bArtists = new Button();
			importJSONToolStripMenuItem = new ToolStripMenuItem();
			menuStrip1.SuspendLayout();
			panelMenu.SuspendLayout();
			SuspendLayout();
			// 
			// panelContent
			// 
			panelContent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			panelContent.Location = new Point(341, 248);
			panelContent.Name = "panelContent";
			panelContent.Size = new Size(1854, 1051);
			panelContent.TabIndex = 2;
			// 
			// menuStrip1
			// 
			menuStrip1.ImageScalingSize = new Size(32, 32);
			menuStrip1.Items.AddRange(new ToolStripItem[] { addToolStripMenuItem, importExportToolStripMenuItem, toolStripMenuItem1, helpToolStripMenuItem });
			menuStrip1.Location = new Point(0, 0);
			menuStrip1.Name = "menuStrip1";
			menuStrip1.Padding = new Padding(6, 3, 0, 3);
			menuStrip1.Size = new Size(2213, 44);
			menuStrip1.TabIndex = 3;
			menuStrip1.Text = "menuStrip1";
			// 
			// addToolStripMenuItem
			// 
			addToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { artistToolStripMenuItem, albumToolStripMenuItem, songToolStripMenuItem });
			addToolStripMenuItem.Name = "addToolStripMenuItem";
			addToolStripMenuItem.Size = new Size(77, 38);
			addToolStripMenuItem.Text = "Add";
			// 
			// artistToolStripMenuItem
			// 
			artistToolStripMenuItem.Name = "artistToolStripMenuItem";
			artistToolStripMenuItem.Size = new Size(217, 44);
			artistToolStripMenuItem.Text = "Artist";
			artistToolStripMenuItem.Click += artistToolStripMenuItem_Click;
			// 
			// albumToolStripMenuItem
			// 
			albumToolStripMenuItem.Name = "albumToolStripMenuItem";
			albumToolStripMenuItem.Size = new Size(217, 44);
			albumToolStripMenuItem.Text = "Album";
			albumToolStripMenuItem.Click += albumToolStripMenuItem_Click;
			// 
			// songToolStripMenuItem
			// 
			songToolStripMenuItem.Name = "songToolStripMenuItem";
			songToolStripMenuItem.Size = new Size(217, 44);
			songToolStripMenuItem.Text = "Song";
			songToolStripMenuItem.Click += songToolStripMenuItem_Click;
			// 
			// importExportToolStripMenuItem
			// 
			importExportToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { importJSONToolStripMenuItem });
			importExportToolStripMenuItem.Name = "importExportToolStripMenuItem";
			importExportToolStripMenuItem.Size = new Size(105, 38);
			importExportToolStripMenuItem.Text = "Import";
			// 
			// toolStripMenuItem1
			// 
			toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { createReportToolStripMenuItem });
			toolStripMenuItem1.Name = "toolStripMenuItem1";
			toolStripMenuItem1.Size = new Size(104, 38);
			toolStripMenuItem1.Text = "Report";
			// 
			// createReportToolStripMenuItem
			// 
			createReportToolStripMenuItem.Name = "createReportToolStripMenuItem";
			createReportToolStripMenuItem.Size = new Size(288, 44);
			createReportToolStripMenuItem.Text = "Create report";
			createReportToolStripMenuItem.Click += createReportToolStripMenuItem_Click;
			// 
			// helpToolStripMenuItem
			// 
			helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aboutToolStripMenuItem });
			helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			helpToolStripMenuItem.Size = new Size(84, 38);
			helpToolStripMenuItem.Text = "Help";
			// 
			// aboutToolStripMenuItem
			// 
			aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			aboutToolStripMenuItem.Size = new Size(212, 44);
			aboutToolStripMenuItem.Text = "About";
			aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
			// 
			// panelMenu
			// 
			panelMenu.Controls.Add(bSongs);
			panelMenu.Controls.Add(bAlbums);
			panelMenu.Controls.Add(bArtists);
			panelMenu.Location = new Point(13, 248);
			panelMenu.Name = "panelMenu";
			panelMenu.Size = new Size(325, 1051);
			panelMenu.TabIndex = 4;
			// 
			// bSongs
			// 
			bSongs.Location = new Point(3, 208);
			bSongs.Name = "bSongs";
			bSongs.Size = new Size(318, 96);
			bSongs.TabIndex = 2;
			bSongs.Text = "Songs";
			bSongs.UseVisualStyleBackColor = true;
			bSongs.Click += bSongs_Click;
			// 
			// bAlbums
			// 
			bAlbums.Location = new Point(3, 106);
			bAlbums.Name = "bAlbums";
			bAlbums.Size = new Size(318, 96);
			bAlbums.TabIndex = 1;
			bAlbums.Text = "Albums";
			bAlbums.UseVisualStyleBackColor = true;
			bAlbums.Click += bAlbums_Click;
			// 
			// bArtists
			// 
			bArtists.Location = new Point(3, 3);
			bArtists.Name = "bArtists";
			bArtists.Size = new Size(318, 96);
			bArtists.TabIndex = 0;
			bArtists.Text = "Artists";
			bArtists.UseVisualStyleBackColor = true;
			bArtists.Click += buttonArtists_Click;
			// 
			// importJSONToolStripMenuItem
			// 
			importJSONToolStripMenuItem.Name = "importJSONToolStripMenuItem";
			importJSONToolStripMenuItem.Size = new Size(359, 44);
			importJSONToolStripMenuItem.Text = "Import JSON";
			importJSONToolStripMenuItem.Click += importJSONToolStripMenuItem_Click;
			// 
			// MusicPlayerWindow
			// 
			AutoScaleDimensions = new SizeF(13F, 32F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(2213, 1315);
			Controls.Add(panelMenu);
			Controls.Add(panelContent);
			Controls.Add(menuStrip1);
			MainMenuStrip = menuStrip1;
			Margin = new Padding(3, 6, 3, 6);
			Name = "MusicPlayerWindow";
			Text = "Music Player";
			Load += MusicPlayerWindow_Load;
			menuStrip1.ResumeLayout(false);
			menuStrip1.PerformLayout();
			panelMenu.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Button bArtists;
        private ToolStripMenuItem artistToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
		private Button bSongs;
		private Button bAlbums;
		private ToolStripMenuItem albumToolStripMenuItem;
		private ToolStripMenuItem songToolStripMenuItem;
		private ToolStripMenuItem importExportToolStripMenuItem;
		private ToolStripMenuItem toolStripMenuItem1;
		private ToolStripMenuItem createReportToolStripMenuItem;
		private ToolStripMenuItem importJSONToolStripMenuItem;
	}
}

