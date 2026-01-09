namespace musicplayer
{
	partial class AlbumDisplayControl
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
			button = new Button();
			label = new Label();
			SuspendLayout();
			// 
			// button
			// 
			button.Location = new Point(3, 3);
			button.Name = "button";
			button.Size = new Size(184, 184);
			button.TabIndex = 0;
			button.UseVisualStyleBackColor = true;
			button.Click += button_Click;
			// 
			// label
			// 
			label.AutoSize = true;
			label.Location = new Point(3, 190);
			label.MaximumSize = new Size(184, 43);
			label.MinimumSize = new Size(184, 43);
			label.Name = "label";
			label.Size = new Size(184, 43);
			label.TabIndex = 1;
			label.Text = "Artist Name";
			label.TextAlign = ContentAlignment.MiddleCenter;
			label.Click += label_Click;
			// 
			// AlbumDisplayControl
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(label);
			Controls.Add(button);
			Name = "AlbumDisplayControl";
			Size = new Size(190, 237);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button button;
		private Label label;
	}
}
