namespace musicplayer.forms
{
	partial class AboutForm
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
			lName = new Label();
			label1 = new Label();
			label2 = new Label();
			SuspendLayout();
			// 
			// lName
			// 
			lName.AutoSize = true;
			lName.Location = new Point(12, 9);
			lName.Name = "lName";
			lName.Size = new Size(87, 20);
			lName.TabIndex = 0;
			lName.Text = "MusicPlayer";
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(12, 29);
			label1.Name = "label1";
			label1.Size = new Size(120, 20);
			label1.TabIndex = 1;
			label1.Text = "Jan Koubek 2025";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(12, 49);
			label2.Name = "label2";
			label2.Size = new Size(196, 20);
			label2.TabIndex = 2;
			label2.Text = "Email: koubek@spsejecna.cz";
			// 
			// AboutForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(386, 88);
			Controls.Add(label2);
			Controls.Add(label1);
			Controls.Add(lName);
			MaximizeBox = false;
			Name = "AboutForm";
			Text = "About";
			Load += AboutForm_Load;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label lName;
		private Label label1;
		private Label label2;
	}
}