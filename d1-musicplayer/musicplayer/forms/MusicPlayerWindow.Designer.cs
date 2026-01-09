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
			importJSONToolStripMenuItem = new ToolStripMenuItem();
			exportJSONToolStripMenuItem = new ToolStripMenuItem();
			toolStripMenuItem1 = new ToolStripMenuItem();
			createReportToolStripMenuItem = new ToolStripMenuItem();
			helpToolStripMenuItem = new ToolStripMenuItem();
			aboutToolStripMenuItem = new ToolStripMenuItem();
			panelMenu = new Panel();
			bSongs = new Button();
			bAlbums = new Button();
			bArtists = new Button();
			bFeatured = new Button();
			menuStrip1.SuspendLayout();
			panelMenu.SuspendLayout();
			SuspendLayout();
			// 
			// panelContent
			// 
			panelContent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			panelContent.Location = new Point(210, 155);
			panelContent.Margin = new Padding(2);
			panelContent.Name = "panelContent";
			panelContent.Size = new Size(1141, 657);
			panelContent.TabIndex = 2;
			// 
			// menuStrip1
			// 
			menuStrip1.ImageScalingSize = new Size(32, 32);
			menuStrip1.Items.AddRange(new ToolStripItem[] { addToolStripMenuItem, importExportToolStripMenuItem, toolStripMenuItem1, helpToolStripMenuItem });
			menuStrip1.Location = new Point(0, 0);
			menuStrip1.Name = "menuStrip1";
			menuStrip1.Padding = new Padding(4, 2, 0, 2);
			menuStrip1.Size = new Size(1362, 28);
			menuStrip1.TabIndex = 3;
			menuStrip1.Text = "menuStrip1";
			// 
			// addToolStripMenuItem
			// 
			addToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { artistToolStripMenuItem, albumToolStripMenuItem, songToolStripMenuItem });
			addToolStripMenuItem.Name = "addToolStripMenuItem";
			addToolStripMenuItem.Size = new Size(51, 24);
			addToolStripMenuItem.Text = "Add";
			// 
			// artistToolStripMenuItem
			// 
			artistToolStripMenuItem.Name = "artistToolStripMenuItem";
			artistToolStripMenuItem.Size = new Size(136, 26);
			artistToolStripMenuItem.Text = "Artist";
			artistToolStripMenuItem.Click += artistToolStripMenuItem_Click;
			// 
			// albumToolStripMenuItem
			// 
			albumToolStripMenuItem.Name = "albumToolStripMenuItem";
			albumToolStripMenuItem.Size = new Size(136, 26);
			albumToolStripMenuItem.Text = "Album";
			albumToolStripMenuItem.Click += albumToolStripMenuItem_Click;
			// 
			// songToolStripMenuItem
			// 
			songToolStripMenuItem.Name = "songToolStripMenuItem";
			songToolStripMenuItem.Size = new Size(136, 26);
			songToolStripMenuItem.Text = "Song";
			songToolStripMenuItem.Click += songToolStripMenuItem_Click;
			// 
			// importExportToolStripMenuItem
			// 
			importExportToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { importJSONToolStripMenuItem, exportJSONToolStripMenuItem });
			importExportToolStripMenuItem.Name = "importExportToolStripMenuItem";
			importExportToolStripMenuItem.Size = new Size(117, 24);
			importExportToolStripMenuItem.Text = "Import/Export";
			// 
			// importJSONToolStripMenuItem
			// 
			importJSONToolStripMenuItem.Name = "importJSONToolStripMenuItem";
			importJSONToolStripMenuItem.Size = new Size(176, 26);
			importJSONToolStripMenuItem.Text = "Import JSON";
			importJSONToolStripMenuItem.Click += importJSONToolStripMenuItem_Click;
			// 
			// exportJSONToolStripMenuItem
			// 
			exportJSONToolStripMenuItem.Name = "exportJSONToolStripMenuItem";
			exportJSONToolStripMenuItem.Size = new Size(176, 26);
			exportJSONToolStripMenuItem.Text = "Export JSON";
			exportJSONToolStripMenuItem.Click += exportJSONToolStripMenuItem_Click;
			// 
			// toolStripMenuItem1
			// 
			toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { createReportToolStripMenuItem });
			toolStripMenuItem1.Name = "toolStripMenuItem1";
			toolStripMenuItem1.Size = new Size(68, 24);
			toolStripMenuItem1.Text = "Report";
			// 
			// createReportToolStripMenuItem
			// 
			createReportToolStripMenuItem.Name = "createReportToolStripMenuItem";
			createReportToolStripMenuItem.Size = new Size(180, 26);
			createReportToolStripMenuItem.Text = "Create report";
			createReportToolStripMenuItem.Click += createReportToolStripMenuItem_Click;
			// 
			// helpToolStripMenuItem
			// 
			helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aboutToolStripMenuItem });
			helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			helpToolStripMenuItem.Size = new Size(55, 24);
			helpToolStripMenuItem.Text = "Help";
			// 
			// aboutToolStripMenuItem
			// 
			aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			aboutToolStripMenuItem.Size = new Size(133, 26);
			aboutToolStripMenuItem.Text = "About";
			aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
			// 
			// panelMenu
			// 
			panelMenu.Controls.Add(bFeatured);
			panelMenu.Controls.Add(bSongs);
			panelMenu.Controls.Add(bAlbums);
			panelMenu.Controls.Add(bArtists);
			panelMenu.Location = new Point(8, 155);
			panelMenu.Margin = new Padding(2);
			panelMenu.Name = "panelMenu";
			panelMenu.Size = new Size(200, 657);
			panelMenu.TabIndex = 4;
			// 
			// bSongs
			// 
			bSongs.Location = new Point(2, 194);
			bSongs.Margin = new Padding(2);
			bSongs.Name = "bSongs";
			bSongs.Size = new Size(196, 60);
			bSongs.TabIndex = 2;
			bSongs.Text = "Songs";
			bSongs.UseVisualStyleBackColor = true;
			bSongs.Click += bSongs_Click;
			// 
			// bAlbums
			// 
			bAlbums.Location = new Point(2, 130);
			bAlbums.Margin = new Padding(2);
			bAlbums.Name = "bAlbums";
			bAlbums.Size = new Size(196, 60);
			bAlbums.TabIndex = 1;
			bAlbums.Text = "Albums";
			bAlbums.UseVisualStyleBackColor = true;
			bAlbums.Click += bAlbums_Click;
			// 
			// bArtists
			// 
			bArtists.Location = new Point(2, 66);
			bArtists.Margin = new Padding(2);
			bArtists.Name = "bArtists";
			bArtists.Size = new Size(196, 60);
			bArtists.TabIndex = 0;
			bArtists.Text = "Artists";
			bArtists.UseVisualStyleBackColor = true;
			bArtists.Click += buttonArtists_Click;
			// 
			// bFeatured
			// 
			bFeatured.Location = new Point(2, 2);
			bFeatured.Margin = new Padding(2);
			bFeatured.Name = "bFeatured";
			bFeatured.Size = new Size(196, 60);
			bFeatured.TabIndex = 3;
			bFeatured.Text = "Featured";
			bFeatured.UseVisualStyleBackColor = true;
			bFeatured.Click += bFeatured_Click;
			// 
			// MusicPlayerWindow
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1362, 822);
			Controls.Add(panelMenu);
			Controls.Add(panelContent);
			Controls.Add(menuStrip1);
			MainMenuStrip = menuStrip1;
			Margin = new Padding(2, 4, 2, 4);
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
		private ToolStripMenuItem exportJSONToolStripMenuItem;
		private Button bFeatured;
	}
}

