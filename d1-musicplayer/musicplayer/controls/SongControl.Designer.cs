namespace musicplayer.controls
{
	partial class SongControl
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
			bPlay = new Button();
			lSongName = new Label();
			lLength = new Label();
			bDelete = new Button();
			bEdit = new Button();
			SuspendLayout();
			// 
			// bPlay
			// 
			bPlay.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
			bPlay.Location = new Point(3, 3);
			bPlay.Name = "bPlay";
			bPlay.Size = new Size(54, 54);
			bPlay.TabIndex = 0;
			bPlay.Text = "Play";
			bPlay.UseVisualStyleBackColor = true;
			bPlay.Click += bPlay_Click;
			// 
			// lSongName
			// 
			lSongName.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
			lSongName.AutoSize = true;
			lSongName.Location = new Point(63, 20);
			lSongName.Name = "lSongName";
			lSongName.Size = new Size(49, 20);
			lSongName.TabIndex = 1;
			lSongName.Text = "Name";
			// 
			// lLength
			// 
			lLength.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
			lLength.AutoSize = true;
			lLength.Location = new Point(743, 20);
			lLength.Name = "lLength";
			lLength.Size = new Size(54, 20);
			lLength.TabIndex = 2;
			lLength.Text = "Length";
			// 
			// bDelete
			// 
			bDelete.Location = new Point(643, 16);
			bDelete.Name = "bDelete";
			bDelete.Size = new Size(94, 29);
			bDelete.TabIndex = 3;
			bDelete.Text = "Delete";
			bDelete.UseVisualStyleBackColor = true;
			bDelete.Click += bDelete_Click;
			// 
			// bEdit
			// 
			bEdit.Location = new Point(543, 16);
			bEdit.Name = "bEdit";
			bEdit.Size = new Size(94, 29);
			bEdit.TabIndex = 4;
			bEdit.Text = "Edit";
			bEdit.UseVisualStyleBackColor = true;
			bEdit.Click += bEdit_Click;
			// 
			// SongControl
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			AutoSize = true;
			BorderStyle = BorderStyle.FixedSingle;
			Controls.Add(bEdit);
			Controls.Add(bDelete);
			Controls.Add(lLength);
			Controls.Add(lSongName);
			Controls.Add(bPlay);
			Name = "SongControl";
			Size = new Size(800, 60);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button bPlay;
		private Label lSongName;
		private Label lLength;
		private Button bDelete;
		private Button bEdit;
	}
}
