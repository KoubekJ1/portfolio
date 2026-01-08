namespace musicplayer.controls
{
	partial class PlayerControl
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
			bBack = new Button();
			bPlayPause = new Button();
			bNext = new Button();
			trbProgress = new TrackBar();
			lSongName = new Label();
			trbVolume = new TrackBar();
			lVolume = new Label();
			lArtist = new Label();
			((System.ComponentModel.ISupportInitialize)trbProgress).BeginInit();
			((System.ComponentModel.ISupportInitialize)trbVolume).BeginInit();
			SuspendLayout();
			// 
			// bBack
			// 
			bBack.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
			bBack.Location = new Point(3, 3);
			bBack.Name = "bBack";
			bBack.Size = new Size(82, 94);
			bBack.TabIndex = 0;
			bBack.Text = "<<";
			bBack.UseVisualStyleBackColor = true;
			bBack.Click += bBack_Click;
			// 
			// bPlayPause
			// 
			bPlayPause.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
			bPlayPause.Location = new Point(91, 3);
			bPlayPause.Name = "bPlayPause";
			bPlayPause.Size = new Size(82, 94);
			bPlayPause.TabIndex = 1;
			bPlayPause.Text = "Play";
			bPlayPause.UseVisualStyleBackColor = true;
			bPlayPause.Click += bPlayPause_Click;
			// 
			// bNext
			// 
			bNext.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
			bNext.Location = new Point(179, 3);
			bNext.Name = "bNext";
			bNext.Size = new Size(82, 94);
			bNext.TabIndex = 2;
			bNext.Text = ">>";
			bNext.UseVisualStyleBackColor = true;
			bNext.Click += bNext_Click;
			// 
			// trbProgress
			// 
			trbProgress.Anchor = AnchorStyles.None;
			trbProgress.Location = new Point(300, 50);
			trbProgress.Maximum = 100;
			trbProgress.Name = "trbProgress";
			trbProgress.Size = new Size(600, 56);
			trbProgress.TabIndex = 3;
			trbProgress.TickFrequency = 100;
			trbProgress.Scroll += trbProgress_Scroll;
			// 
			// lSongName
			// 
			lSongName.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
			lSongName.AutoSize = true;
			lSongName.Location = new Point(558, 3);
			lSongName.Name = "lSongName";
			lSongName.Size = new Size(125, 20);
			lSongName.TabIndex = 4;
			lSongName.Text = "(no song playing)";
			lSongName.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// trbVolume
			// 
			trbVolume.Anchor = AnchorStyles.Right;
			trbVolume.Location = new Point(1000, 50);
			trbVolume.Maximum = 100;
			trbVolume.Name = "trbVolume";
			trbVolume.Size = new Size(200, 56);
			trbVolume.TabIndex = 5;
			trbVolume.TickFrequency = 100;
			trbVolume.Value = 100;
			trbVolume.Scroll += trbVolume_Scroll;
			// 
			// lVolume
			// 
			lVolume.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
			lVolume.AutoSize = true;
			lVolume.Location = new Point(1070, 3);
			lVolume.MinimumSize = new Size(60, 0);
			lVolume.Name = "lVolume";
			lVolume.Size = new Size(60, 20);
			lVolume.TabIndex = 6;
			lVolume.Text = "Volume";
			lVolume.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// lArtist
			// 
			lArtist.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
			lArtist.AutoSize = true;
			lArtist.Location = new Point(558, 27);
			lArtist.Name = "lArtist";
			lArtist.Size = new Size(126, 20);
			lArtist.TabIndex = 7;
			lArtist.Text = "(no artist playing)";
			lArtist.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// PlayerControl
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(lArtist);
			Controls.Add(lVolume);
			Controls.Add(trbVolume);
			Controls.Add(lSongName);
			Controls.Add(trbProgress);
			Controls.Add(bNext);
			Controls.Add(bPlayPause);
			Controls.Add(bBack);
			Name = "PlayerControl";
			Size = new Size(1200, 100);
			((System.ComponentModel.ISupportInitialize)trbProgress).EndInit();
			((System.ComponentModel.ISupportInitialize)trbVolume).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button bBack;
		private Button bPlayPause;
		private Button bNext;
		private TrackBar trbProgress;
		private Label lSongName;
		private TrackBar trbVolume;
		private Label lVolume;
		private Label lArtist;
	}
}
