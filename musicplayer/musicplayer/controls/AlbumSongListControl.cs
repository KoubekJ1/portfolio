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
	/// UserControl that displays information about an album as well as its songs
	/// </summary>
	public partial class AlbumSongListControl : UserControl
	{
		private Album _album;
		private Control _artistDisplayControl;

		/// <summary>
		/// Constructs a new AlbumSongListControl with the given album
		/// </summary>
		/// <param name="album">album</param>
		/// <param name="artistDisplayControl">control containing the album list</param>
		public AlbumSongListControl(Album album, Control artistDisplayControl)
		{
			InitializeComponent();
			_album = album;
			_artistDisplayControl = artistDisplayControl;

			lAlbumName.Text = album.Name;
			lArtistName.Text = album.Artist != null ? album.Artist.Name : "Unknown artist";
			pbAlbumImage.Image = album.Image != null ? IconImage.ResizeImage(album.Image.Image, pbAlbumImage.Width, pbAlbumImage.Height) : null;

			if (album.Songs.Count <= 0 && album.Id != null)
			{
				album.Songs = new SongDAO().GetSongsFromAlbum((int)album.Id);
			}

			foreach (Song song in album.Songs)
			{
				song.Album = _album;
				SongControl songControl = new SongControl(song, artistDisplayControl);
				flpSongs.Controls.Add(songControl);
			}

			_artistDisplayControl = artistDisplayControl;
		}

		/// <summary>
		/// EventHandler that opens the edit album dialog
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bEdit_Click(object sender, EventArgs e)
		{
			var EditAlbumForm = new AddAlbumForm(_album);
			EditAlbumForm.ShowDialog();
			_artistDisplayControl.Controls.Clear();
		}

		/// <summary>
		/// EventHandler that deletes the album from the database
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bDelete_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure you wish to delete \"" + _album.Name + "\" from the database?", "Delete", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
			if (_album.Id == null)
			{
				MessageBox.Show("Album has not been uploaded to the database!", "Error");
				return;
			}
			try
			{
				AlbumDAO albumDAO = new AlbumDAO();
				albumDAO.Remove((int)_album.Id);
				MessageBox.Show("Successfully removed \"" + _album.Name + "\" from the database.");
				_artistDisplayControl.Controls.Clear();
			}
			catch (Exception ex)
			{
				ErrorHandler.HandleException(ex, "Error", "Could not remove album.");
			}
		}
	}
}
