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

		private int _lastIndex = 0;
		private Stack<int> _cursorHistory = new Stack<int>();

		public SongOverviewControl(Control artistDisplayControl)
		{
			InitializeComponent();
			_artistDisplayControl = artistDisplayControl;

			LoadPage();
		}

		private void SetSongs(IEnumerable<Song> songs)
		{
			_songs = songs;

			while (flpSongs.Controls.Count > 0)
			{
				Control c = flpSongs.Controls[0];
				flpSongs.Controls.Remove(c);
				c.Dispose();
			}

			flpSongs.SuspendLayout();

			foreach (Song song in songs)
			{
				var control = new SongControl(song, _artistDisplayControl);
				flpSongs.Controls.Add(control);
			}

			flpSongs.ResumeLayout();
		}

		private void LoadPage()
		{
			var dao = new SongDAO();
			var songs = dao.GetRange(_lastIndex);

			SetSongs(songs);
			UpdatePaginationState(songs.Count());
		}

		private void UpdatePaginationState(int count)
		{
			bBack.Enabled = _cursorHistory.Count > 0;

			bNext.Enabled = count >= 10;
		}

		private void Next()
		{
			if (_songs == null || !_songs.Any()) return;

			_cursorHistory.Push(_lastIndex);

			int? lastId = _songs.Last().Id;

			if (lastId.HasValue)
			{
				_lastIndex = lastId.Value;
				LoadPage();
			}
		}

		private void Back()
		{
			if (_cursorHistory.Count == 0) return;

			_lastIndex = _cursorHistory.Pop();
			LoadPage();
		}

		private void LoadSongsByName(string name)
		{
			_lastIndex = 0;
			_cursorHistory.Clear();

			if (string.IsNullOrWhiteSpace(name))
			{
				LoadPage();
				return;
			}

			var songs = new SongDAO().GetByName(name);
			SetSongs(songs);

			bBack.Enabled = false;
			bNext.Enabled = false;
		}

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
