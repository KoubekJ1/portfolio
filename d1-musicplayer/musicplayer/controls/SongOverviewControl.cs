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

namespace musicplayer.controls
{
	/// <summary>
	/// UserControl used for displaying the given songs independantly of any albums
	/// </summary>
	public partial class SongOverviewControl : UserControl
	{
		private IEnumerable<Song> _songs;
		private Control _artistDisplayControl;

		private int _currentPage = 0;
		private int _lastIndex = 0;
		private Stack<int> _lastIndexHistory = new Stack<int>();

		/// <summary>
		/// Constructs a new SongOverviewControl instance with the given songs
		/// </summary>
		/// <param name="songs">songs</param>
		/// <param name="artistDisplayControl">parent control containing this instance</param>
		public SongOverviewControl(Control artistDisplayControl)
		{
			InitializeComponent();
			_artistDisplayControl = artistDisplayControl;
			Next();
		}

		/// <summary>
		/// Sets the songs that are to be displayed
		/// </summary>
		/// <param name="songs">songs</param>
		private void SetSongs(IEnumerable<Song> songs)
		{
			_songs = songs;
			flpSongs.Controls.Clear();
			foreach (Song song in songs)
			{
				var control = new SongControl(song, _artistDisplayControl);
				flpSongs.Controls.Add(control);
			}
		}

		private void Next()
		{
			MessageBox.Show(_lastIndex.ToString());
			if (_lastIndex > 0) bBack.Enabled = true;

			var songs = new SongDAO().GetRange(_lastIndex + 1);
			SetSongs(songs);
			if (songs.Count() < 10)
				bNext.Enabled = false;
			if (songs.Count() < 1)
			{
				_currentPage++;
				_lastIndexHistory.Push(_lastIndex);
				_lastIndex += 1;
				return;
			}
			var id = songs.Last().Id;
			if (id == null)
			{
				return;
			}
			_currentPage++;
			_lastIndexHistory.Push(_lastIndex);
			_lastIndex = (int)id;
		}

		private void Back()
		{
			MessageBox.Show(_lastIndex.ToString());
			MessageBox.Show(_currentPage.ToString());
			if (_currentPage <= 0)
			{
				bBack.Enabled = false;
			}

			bNext.Enabled = true;

			if (_lastIndexHistory.Count < 1) return;

			_lastIndex = _lastIndexHistory.Pop();

			var songs = new SongDAO().GetRange(_lastIndex + 1);
			
			_currentPage--;
			SetSongs(songs);
		}

		private void LoadSongsByName(string name)
		{
			_lastIndex = 0;
			_currentPage = 0;
			_lastIndexHistory.Clear();
			if (name.Trim() == string.Empty)
			{
					
			}
			var songs = new SongDAO().GetByName(name);
			SetSongs(songs);
		}

		/// <summary>
		/// EventHandler that filters songs based on value of the search textbox
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tbSearch_TextChanged(object sender, EventArgs e)
		{
			LoadSongsByName(tbSearch.Text);
		}

		private void bBack_Click(object sender, EventArgs e)
		{
			Back();
		}

		private void bNext_Click(object sender, EventArgs e)
		{
			Next();
		}
	}
}
