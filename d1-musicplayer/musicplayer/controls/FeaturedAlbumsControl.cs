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
	public partial class FeaturedAlbumsControl : UserControl
	{
		private IEnumerable<Album> _albums = [];
		private Control _artistContentControl;
		public FeaturedAlbumsControl(Control artistContentControl)
		{
			InitializeComponent();
			_artistContentControl = artistContentControl;

			try
			{
				_albums = new AlbumDAO().GetFeaturedAlbums();
				SetAlbums(_albums, artistContentControl);
			} catch { }
		}

		private void SetAlbums(IEnumerable<Album> albums, Control artistContentControl)
		{
			flpAlbs.Controls.Clear();
			foreach (Album album in albums)
			{
				flpAlbs.Controls.Add(new AlbumDisplayControl(album, artistContentControl));
			}
		}
	}
}
