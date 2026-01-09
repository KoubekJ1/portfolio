namespace musicplayer.forms
{
	partial class ArtistPicker
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
			lArtists = new Label();
			lbArtists = new ListBox();
			lSearch = new Label();
			tbSearch = new TextBox();
			bSelect = new Button();
			SuspendLayout();
			// 
			// lArtists
			// 
			lArtists.AutoSize = true;
			lArtists.Location = new Point(12, 9);
			lArtists.Name = "lArtists";
			lArtists.Size = new Size(50, 20);
			lArtists.TabIndex = 0;
			lArtists.Text = "Artists";
			// 
			// lbArtists
			// 
			lbArtists.FormattingEnabled = true;
			lbArtists.Location = new Point(12, 32);
			lbArtists.Name = "lbArtists";
			lbArtists.Size = new Size(300, 224);
			lbArtists.TabIndex = 1;
			// 
			// lSearch
			// 
			lSearch.AutoSize = true;
			lSearch.Location = new Point(12, 259);
			lSearch.Name = "lSearch";
			lSearch.Size = new Size(53, 20);
			lSearch.TabIndex = 2;
			lSearch.Text = "Search";
			// 
			// tbSearch
			// 
			tbSearch.Location = new Point(12, 282);
			tbSearch.Name = "tbSearch";
			tbSearch.Size = new Size(300, 27);
			tbSearch.TabIndex = 3;
			tbSearch.TextChanged += tbSearch_TextChanged;
			// 
			// bSelect
			// 
			bSelect.Location = new Point(12, 315);
			bSelect.Name = "bSelect";
			bSelect.Size = new Size(94, 29);
			bSelect.TabIndex = 4;
			bSelect.Text = "Select";
			bSelect.UseVisualStyleBackColor = true;
			bSelect.Click += bSelect_Click;
			// 
			// ArtistPicker
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(334, 366);
			Controls.Add(bSelect);
			Controls.Add(tbSearch);
			Controls.Add(lSearch);
			Controls.Add(lbArtists);
			Controls.Add(lArtists);
			MaximizeBox = false;
			Name = "ArtistPicker";
			Text = "Artist Picker";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label lArtists;
		private ListBox lbArtists;
		private Label lSearch;
		private TextBox tbSearch;
		private Button bSelect;
	}
}