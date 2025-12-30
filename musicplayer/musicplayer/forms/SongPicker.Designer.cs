namespace musicplayer.forms
{
	partial class SongPicker
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
			bSelect = new Button();
			tbSearch = new TextBox();
			lSearch = new Label();
			lbSongs = new ListBox();
			lSongs = new Label();
			bNew = new Button();
			SuspendLayout();
			// 
			// bSelect
			// 
			bSelect.Location = new Point(12, 315);
			bSelect.Name = "bSelect";
			bSelect.Size = new Size(94, 29);
			bSelect.TabIndex = 9;
			bSelect.Text = "Select";
			bSelect.UseVisualStyleBackColor = true;
			bSelect.Click += bSelect_Click;
			// 
			// tbSearch
			// 
			tbSearch.Location = new Point(12, 282);
			tbSearch.Name = "tbSearch";
			tbSearch.Size = new Size(300, 27);
			tbSearch.TabIndex = 8;
			tbSearch.TextChanged += tbSearch_TextChanged;
			// 
			// lSearch
			// 
			lSearch.AutoSize = true;
			lSearch.Location = new Point(12, 259);
			lSearch.Name = "lSearch";
			lSearch.Size = new Size(53, 20);
			lSearch.TabIndex = 7;
			lSearch.Text = "Search";
			// 
			// lbSongs
			// 
			lbSongs.FormattingEnabled = true;
			lbSongs.Location = new Point(12, 32);
			lbSongs.Name = "lbSongs";
			lbSongs.Size = new Size(300, 224);
			lbSongs.TabIndex = 6;
			// 
			// lSongs
			// 
			lSongs.AutoSize = true;
			lSongs.Location = new Point(12, 9);
			lSongs.Name = "lSongs";
			lSongs.Size = new Size(49, 20);
			lSongs.TabIndex = 5;
			lSongs.Text = "Songs";
			// 
			// bNew
			// 
			bNew.Location = new Point(112, 315);
			bNew.Name = "bNew";
			bNew.Size = new Size(94, 29);
			bNew.TabIndex = 10;
			bNew.Text = "New";
			bNew.UseVisualStyleBackColor = true;
			bNew.Click += bNew_Click;
			// 
			// SongPicker
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(333, 354);
			Controls.Add(bNew);
			Controls.Add(bSelect);
			Controls.Add(tbSearch);
			Controls.Add(lSearch);
			Controls.Add(lbSongs);
			Controls.Add(lSongs);
			MaximizeBox = false;
			Name = "SongPicker";
			Text = "Song Picker";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button bSelect;
		private TextBox tbSearch;
		private Label lSearch;
		private ListBox lbSongs;
		private Label lSongs;
		private Button bNew;
	}
}