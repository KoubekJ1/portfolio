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

		/// <summary>
		/// Constructs a new SongOverviewControl instance with the given songs
		/// </summary>
		/// <param name="songs">songs</param>
		/// <param name="artistDisplayControl">parent control containing this instance</param>
		public SongOverviewControl(IEnumerable<Song> songs, Control artistDisplayControl)
		{
			InitializeComponent();
			_songs = songs;
			_artistDisplayControl = artistDisplayControl;
			SetSongs(songs);
		}

		/// <summary>
		/// Sets the songs that are to be displayed
		/// </summary>
		/// <param name="songs">songs</param>
		private void SetSongs(IEnumerable<Song> songs)
		{
			flpSongs.Controls.Clear();
			foreach (Song song in songs)
			{
				var control = new SongControl(song, _artistDisplayControl);
				flpSongs.Controls.Add(control);
			}
		}

		/// <summary>
		/// EventHandler that filters songs based on value of the search textbox
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tbSearch_TextChanged(object sender, EventArgs e)
		{
			IEnumerable<Song> songs = _songs.Where(song => song.Name.IndexOf (tbSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0);
			SetSongs(songs);
		}
	}
}
