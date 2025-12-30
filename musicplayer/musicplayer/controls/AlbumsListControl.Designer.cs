namespace musicplayer
{
	partial class AlbumsListControl
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
			flpAlbs = new FlowLayoutPanel();
			lSearch = new Label();
			tbSearch = new TextBox();
			panel1 = new Panel();
			panel1.SuspendLayout();
			SuspendLayout();
			// 
			// flpAlbs
			// 
			flpAlbs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
			flpAlbs.AutoScroll = true;
			flpAlbs.Location = new Point(0, 56);
			flpAlbs.Name = "flpAlbs";
			flpAlbs.Size = new Size(830, 382);
			flpAlbs.TabIndex = 0;
			// 
			// lSearch
			// 
			lSearch.AutoSize = true;
			lSearch.Location = new Point(3, 0);
			lSearch.Name = "lSearch";
			lSearch.Size = new Size(53, 20);
			lSearch.TabIndex = 1;
			lSearch.Text = "Search";
			// 
			// tbSearch
			// 
			tbSearch.Location = new Point(3, 23);
			tbSearch.Name = "tbSearch";
			tbSearch.Size = new Size(500, 27);
			tbSearch.TabIndex = 2;
			tbSearch.TextChanged += tbSearch_TextChanged;
			// 
			// panel1
			// 
			panel1.Controls.Add(tbSearch);
			panel1.Controls.Add(lSearch);
			panel1.Dock = DockStyle.Top;
			panel1.Location = new Point(0, 0);
			panel1.Name = "panel1";
			panel1.Size = new Size(830, 50);
			panel1.TabIndex = 0;
			// 
			// AlbumsListControl
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(panel1);
			Controls.Add(flpAlbs);
			Name = "AlbumsListControl";
			Size = new Size(830, 438);
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private FlowLayoutPanel flpAlbs;
		private Label lSearch;
		private TextBox tbSearch;
		private Panel panel1;
	}
}
