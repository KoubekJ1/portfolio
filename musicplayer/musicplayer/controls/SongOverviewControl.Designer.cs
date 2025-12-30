namespace musicplayer.controls
{
	partial class SongOverviewControl
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
			lSearch = new Label();
			tbSearch = new TextBox();
			flpSongs = new FlowLayoutPanel();
			SuspendLayout();
			// 
			// lSearch
			// 
			lSearch.AutoSize = true;
			lSearch.Location = new Point(3, 0);
			lSearch.Name = "lSearch";
			lSearch.Size = new Size(53, 20);
			lSearch.TabIndex = 0;
			lSearch.Text = "Search";
			// 
			// tbSearch
			// 
			tbSearch.Location = new Point(3, 23);
			tbSearch.Name = "tbSearch";
			tbSearch.Size = new Size(500, 27);
			tbSearch.TabIndex = 1;
			tbSearch.TextChanged += tbSearch_TextChanged;
			// 
			// flpSongs
			// 
			flpSongs.Location = new Point(3, 56);
			flpSongs.Name = "flpSongs";
			flpSongs.Size = new Size(1135, 598);
			flpSongs.TabIndex = 2;
			// 
			// SongOverviewControl
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(flpSongs);
			Controls.Add(tbSearch);
			Controls.Add(lSearch);
			Name = "SongOverviewControl";
			Size = new Size(1141, 657);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label lSearch;
		private TextBox tbSearch;
		private FlowLayoutPanel flpSongs;
	}
}
