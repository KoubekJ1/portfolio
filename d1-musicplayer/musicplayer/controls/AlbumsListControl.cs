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

namespace musicplayer
{
	/// <summary>
	/// UserControl used for listing albums in an organised manner
	/// </summary>
	public partial class AlbumsListControl : UserControl
	{
		private Control _artistContentControl;
		private IEnumerable<Album> _albums;

		/// <summary>
		/// Constructs a new AlbumsListControl instance with the given albums
		/// </summary>
		/// <param name="albums">albums</param>
		/// <param name="artistContentControl">control containing the album list</param>
		public AlbumsListControl(IEnumerable<Album> albums, Control artistContentControl)
		{
			InitializeComponent();
			_albums = albums;
			_artistContentControl = artistContentControl;
			SetAlbums(albums, artistContentControl);			
		}

		/// <summary>
		/// Sets the albums that are to be displayed
		/// </summary>
		/// <param name="albums">albums</param>
		/// <param name="artistContentControl">control containing the album list</param>
		public void SetAlbums(IEnumerable<Album> albums, Control artistContentControl)
		{
			flpAlbs.Controls.Clear();
			foreach (Album album in albums)
			{
				flpAlbs.Controls.Add(new AlbumDisplayControl(album, artistContentControl));
			}
		}

		/// <summary>
		/// EventHandler that filters albums based on their name and the text inputed into the search bar
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tbSearch_TextChanged(object sender, EventArgs e)
		{
			SetAlbums(_albums.Where(alb => alb.Name.IndexOf(tbSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0), _artistContentControl);
		}
	}
}
