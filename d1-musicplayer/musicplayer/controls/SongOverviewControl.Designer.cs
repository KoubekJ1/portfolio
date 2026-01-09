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
			bBack = new Button();
			bNext = new Button();
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
			flpSongs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			flpSongs.Location = new Point(3, 56);
			flpSongs.Name = "flpSongs";
			flpSongs.Size = new Size(1135, 563);
			flpSongs.TabIndex = 2;
			// 
			// bBack
			// 
			bBack.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			bBack.Enabled = false;
			bBack.Location = new Point(3, 625);
			bBack.Name = "bBack";
			bBack.Size = new Size(94, 29);
			bBack.TabIndex = 3;
			bBack.Text = "<";
			bBack.UseVisualStyleBackColor = true;
			bBack.Click += bBack_Click;
			// 
			// bNext
			// 
			bNext.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			bNext.Location = new Point(103, 625);
			bNext.Name = "bNext";
			bNext.Size = new Size(94, 29);
			bNext.TabIndex = 4;
			bNext.Text = ">";
			bNext.UseVisualStyleBackColor = true;
			bNext.Click += bNext_Click;
			// 
			// SongOverviewControl
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(bNext);
			Controls.Add(bBack);
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
		private Button bBack;
		private Button bNext;
	}
}
