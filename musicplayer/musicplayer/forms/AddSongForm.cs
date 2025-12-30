using musicplayer.controls.forms;
using musicplayer.dao;
using musicplayer.dataobjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace musicplayer.forms
{
	/// <summary>
	/// Form used for creating or updating song instances
	/// </summary>
	public partial class AddSongForm : Form
	{
		public Song? Song { get; private set; }

		private Action<Song>? _onCreate;

		private NewSongFormControl _control = new NewSongFormControl();

		/// <summary>
		/// Constructs a new add song form
		/// </summary>
		public AddSongForm(Action<Song>? onCreate = null)
		{
			InitializeComponent();
			this.FormBorderStyle = FormBorderStyle.FixedSingle;
			_control = new NewSongFormControl((song) => {
				Song = song;
				if (_onCreate != null) { _onCreate(song); }
			});
			_control.Dock = DockStyle.Fill;
			this.Controls.Add(_control);
			_onCreate = onCreate;
		}

		/// <summary>
		/// Constructs a new add song form with the given song to edit
		/// </summary>
		/// <param name="song">Song</param>
		public AddSongForm(Song song, Action<Song>? onCreate = null)
		{
			InitializeComponent();
			this.FormBorderStyle = FormBorderStyle.FixedSingle;
			_control = new NewSongFormControl(song, (song) => {
				Song = song;
				if (_onCreate != null) { _onCreate(song); }
			});
			_control.Dock = DockStyle.Fill;
			this.Controls.Add(_control);

			this.Text = "Edit Song";

			_onCreate = onCreate;
		}

		private void AddSongForm_Load(object sender, EventArgs e)
		{

		}
	}
}
