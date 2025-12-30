namespace musicplayer.forms
{
	partial class AlbumPicker
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
			lbAlbums = new ListBox();
			lAlbums = new Label();
			SuspendLayout();
			// 
			// bSelect
			// 
			bSelect.Location = new Point(12, 315);
			bSelect.Name = "bSelect";
			bSelect.Size = new Size(94, 29);
			bSelect.TabIndex = 14;
			bSelect.Text = "Select";
			bSelect.UseVisualStyleBackColor = true;
			bSelect.Click += bSelect_Click;
			// 
			// tbSearch
			// 
			tbSearch.Location = new Point(12, 282);
			tbSearch.Name = "tbSearch";
			tbSearch.Size = new Size(300, 27);
			tbSearch.TabIndex = 13;
			tbSearch.TextChanged += tbSearch_TextChanged;
			// 
			// lSearch
			// 
			lSearch.AutoSize = true;
			lSearch.Location = new Point(12, 259);
			lSearch.Name = "lSearch";
			lSearch.Size = new Size(53, 20);
			lSearch.TabIndex = 12;
			lSearch.Text = "Search";
			// 
			// lbAlbums
			// 
			lbAlbums.FormattingEnabled = true;
			lbAlbums.Location = new Point(12, 32);
			lbAlbums.Name = "lbAlbums";
			lbAlbums.Size = new Size(300, 224);
			lbAlbums.TabIndex = 11;
			// 
			// lAlbums
			// 
			lAlbums.AutoSize = true;
			lAlbums.Location = new Point(12, 9);
			lAlbums.Name = "lAlbums";
			lAlbums.Size = new Size(59, 20);
			lAlbums.TabIndex = 10;
			lAlbums.Text = "Albums";
			// 
			// AlbumPicker
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(334, 354);
			Controls.Add(bSelect);
			Controls.Add(tbSearch);
			Controls.Add(lSearch);
			Controls.Add(lbAlbums);
			Controls.Add(lAlbums);
			MaximizeBox = false;
			Name = "AlbumPicker";
			Text = "Album Picker";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button bSelect;
		private TextBox tbSearch;
		private Label lSearch;
		private ListBox lbAlbums;
		private Label lAlbums;
	}
}