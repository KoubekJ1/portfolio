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

namespace musicplayer.controls.forms
{
	public partial class NewSongFormControl : UserControl
	{
		private Album? _album;

		private Song _song;

		public Action<Song>? _onCreate;

		public Song Song { get => _song; }

		/// <summary>
		/// Constructs a new add song form
		/// </summary>
		public NewSongFormControl(Action<Song>? onCreate = null)
		{
			InitializeComponent();
			_song = new Song("");
			_onCreate = onCreate;
		}

		/// <summary>
		/// Constructs a new add song form with the given song to edit
		/// </summary>
		/// <param name="song">Song</param>
		public NewSongFormControl(Song song, Action<Song>? onCreate = null)
		{
			InitializeComponent();
			_song = song;
			_onCreate = onCreate;

			this.bAdd.Text = "Save";

			tbName.Text = song.Name;
			lFile.Text = "Original song data";
		}

		public void Clear()
		{
			this._album = null;
			this._song = new Song("");
		}

		/// <summary>
		/// Opens the file chooser dialog for the user to choose a file containing the song data
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bFile_Click_1(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filter = "MP3 Files (*.mp3)|*.mp3";
			if (dialog.ShowDialog() != DialogResult.OK) return;
			try
			{
				_song.Data = File.ReadAllBytes(dialog.FileName);
				_song.DataID = null;
				lFile.Text = dialog.FileName;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Unable to load song: " + dialog.FileName, "Error");
				return;
			}
		}

		/// <summary>
		/// Adds the song to the database
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bAdd_Click_1(object sender, EventArgs e)
		{
			SongDAO dao = new SongDAO();
			if (_song.Data == null)
			{
				if (_song.DataID != null)
				{
					try
					{
						_song.Data = dao.GetSongData((int)_song.DataID);
					}
					catch (Exception ex)
					{
						ErrorHandler.HandleException(ex, "Error", "Unable to load song data from the database!");
						return;
					}
				}
				else
				{
					MessageBox.Show("Please load an MP3 file containing the song data.", "No song data");
					return;
				}
			}

			_song.Length = (int)AudioPlayerManager.GetDuration(_song.Data);

			_song.Id = dao.Upload(_song);

			if (_song.Id == null)
			{
				MessageBox.Show("Upload failed due to an error.", "Error");
			}
			else
			{
				MessageBox.Show("Successfully uploaded \"" + _song.Name + "\" to the database.", "Add Song");
				if (_onCreate != null) _onCreate(_song);
			}
		}

		/// <summary>
		/// Sets the song's name to the textbox value
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tbName_TextChanged_1(object sender, EventArgs e)
		{
			_song.Name = tbName.Text;
		}

		private void NewSongForm_Load(object sender, EventArgs e)
		{

		}
	}
}
