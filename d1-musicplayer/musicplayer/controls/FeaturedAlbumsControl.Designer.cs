namespace musicplayer.controls
{
	partial class FeaturedAlbumsControl
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
			lFeaturedLabel = new Label();
			SuspendLayout();
			// 
			// flpAlbs
			// 
			flpAlbs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			flpAlbs.Location = new Point(0, 53);
			flpAlbs.Name = "flpAlbs";
			flpAlbs.Size = new Size(873, 605);
			flpAlbs.TabIndex = 0;
			// 
			// lFeaturedLabel
			// 
			lFeaturedLabel.AutoSize = true;
			lFeaturedLabel.Dock = DockStyle.Top;
			lFeaturedLabel.Font = new Font("Segoe UI", 22.2F, FontStyle.Regular, GraphicsUnit.Point, 238);
			lFeaturedLabel.Location = new Point(0, 0);
			lFeaturedLabel.Name = "lFeaturedLabel";
			lFeaturedLabel.Size = new Size(166, 50);
			lFeaturedLabel.TabIndex = 1;
			lFeaturedLabel.Text = "Featured";
			// 
			// FeaturedAlbumsControl
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(lFeaturedLabel);
			Controls.Add(flpAlbs);
			Name = "FeaturedAlbumsControl";
			Size = new Size(873, 658);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private FlowLayoutPanel flpAlbs;
		private Label lFeaturedLabel;
	}
}
