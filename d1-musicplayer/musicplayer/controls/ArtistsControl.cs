using musicplayer.controls;
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

namespace musicplayer
{
	/// <summary>
	/// UserControl used for displaying individual artist thumbnails as well as information about the selected artist
	/// </summary>
	public partial class ArtistsControl : UserControl
	{
		private Dictionary<Button, Artist> _artistButtons = new Dictionary<Button, Artist>();
		private Control _parentPanel;

		/// <summary>
		/// Constructs a new ArtistsControl instance
		/// </summary>
		/// <param name="parentPanel">Control containing the instance</param>
		public ArtistsControl(Control parentPanel)
		{
			InitializeComponent();
			_artistButtons = new Dictionary<Button, Artist>();
			_parentPanel = parentPanel;

			var artists = new ArtistDAO().GetAll();
			foreach (Artist artist in artists.OrderBy(artist => artist.Name))
			{
				Button button = new Button();
				button.Text = artist.Name;
				button.Width = 250;
				button.Height = 100;
				button.AutoSize = true;
				button.AutoSizeMode = AutoSizeMode.GrowOnly;
				//button.TextAlign = ContentAlignment.MiddleRight;
				button.TextAlign = ContentAlignment.MiddleCenter;
				flpArtists.Controls.Add(button);

				PictureBox pictureBox = new PictureBox();
				pictureBox.Image = artist.Image?.Image;
				//pictureBox.Dock = DockStyle.Left;
				pictureBox.Location = new Point(10, 25);
				pictureBox.Height = 50;
				pictureBox.Width = 50;
				pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
				button.Controls.Add(pictureBox);

				_artistButtons.Add(button, artist);
				button.Click += ButtonClicked;
			}
		}

		/// <summary>
		/// EventHandler that sets the content panel's content to the given artist
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ButtonClicked(object? sender, EventArgs e)
		{
			Button? button = sender as Button;
			if (button == null) return;
			Artist? artist = _artistButtons[button];
			if (artist == null) return;
			pArtistContent.Controls.Clear();

			artist.LoadAlbums();
			
			//pArtistContent.Controls.Add(new AlbumsListControl(artist.Albums, pArtistContent));
			pArtistContent.Controls.Add(new ArtistViewControl(artist, _parentPanel, pArtistContent));
		}
	}
}
