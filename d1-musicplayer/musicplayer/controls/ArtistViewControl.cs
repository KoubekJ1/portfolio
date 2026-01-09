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
	/// UserControl used for displaying information about the artists as well as listing their albums
	/// </summary>
	public partial class ArtistViewControl : UserControl
	{
		private Artist _artist;
		private Control _artistsControl;
		private Control _artistContentControl;

		/// <summary>
		/// Constructs a new ArtsistViewControl instance with the given artist
		/// </summary>
		/// <param name="artist">artist</param>
		/// <param name="artistsControl">Control containing the artist list</param>
		/// <param name="artistContentControl">Control containing the artist's information</param>
		public ArtistViewControl(Artist artist, Control artistsControl, Control artistContentControl)
		{
			InitializeComponent();

			_artist = artist;
			_artistsControl = artistsControl;
			_artistContentControl = artistContentControl;

			lArtistName.Text = artist.Name;
			if (_artist.Image != null) pbImage.Image = IconImage.ResizeImage(artist.Image.Image, pbImage.Width, pbImage.Height);

			AlbumsListControl control = new AlbumsListControl(_artist.Albums, artistContentControl);
			pAlbumlist.Controls.Add(control);
		}

		/// <summary>
		/// EventHandler that opens the edit artist dialog
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bEdit_Click(object sender, EventArgs e)
		{
			AddArtistForm addArtistForm = new AddArtistForm(_artist);
			addArtistForm.ShowDialog();
			_artistsControl.Controls.Clear();
		}

		/// <summary>
		/// EventHandler that deletes the artist from the database
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bDelete_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure you wish to delete \"" + _artist.Name + "\" from the database?", "Delete", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
			if (_artist.Id == null)
			{
				MessageBox.Show("Artist has not been uploaded to the database", "Error");
				return;
			}

			ArtistDAO dao = new ArtistDAO();
			try
			{
				dao.Remove((int)_artist.Id);
				MessageBox.Show("Artist \"" + _artist.Name + "\" successfully deleted.", "Delete");
				_artistsControl.Controls.Clear();
			}
			catch (Exception ex)
			{
				ErrorHandler.HandleException(ex, "Error", "Unable to delete artist from the database.");
			}
		}
	}
}
