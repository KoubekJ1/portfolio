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
	public partial class NewArtistFormControl : UserControl
	{
		private Artist _artist;
		private bool _matched = false;
		private IEnumerable<string> ;

		public Artist Artist { get => _artist; }

		public Action<Artist> OnCreate { get; set; }

		/// <summary>
		/// Constructs a new AddArtistForm
		/// </summary>
		public NewArtistFormControl(bool isAutofill = false)
		{
			InitializeComponent();

			_artist = new Artist("");
			OnCreate = delegate { };

			if (isAutofill)
			{
				bAdd.Dispose();
				tbName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
				tbName.AutoCompleteSource = AutoCompleteSource.CustomSource;
				tbName.AutoCompleteCustomSource = 
			}
		}

		/// <summary>
		/// Constructs a new AddArtistForm with the given artist to edit
		/// </summary>
		/// <param name="artist">Artist</param>
		public NewArtistFormControl(Artist artist)
		{
			InitializeComponent();
			OnCreate = delegate { };

			_artist = artist;
			tbName.Text = artist.Name;
			if (_artist.Image != null) pbImage.Image = IconImage.ResizeImage(_artist.Image.Image, pbImage.Width, pbImage.Height);
		}

		/// <summary>
		/// Changes the artist's image
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bChangeImage_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filter = "All files (*.*)|*.*|JPEG (*.jpeg)|*.jpeg|PNG (*.png)|*.png|Bitmap (*.bmp)|*.bmp";
			if (dialog.ShowDialog() != DialogResult.OK) return;
			try
			{
				_artist.Image = new IconImage(new Bitmap(dialog.FileName));
				pbImage.Image = IconImage.ResizeImage(new Bitmap(dialog.FileName), pbImage.Width, pbImage.Height);
				pbImage.SizeMode = PictureBoxSizeMode.StretchImage;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Unable to load image: " + dialog.FileName, "Error");
				return;
			}
		}

		/// <summary>
		/// Adds the artist to the database
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bAdd_Click(object sender, EventArgs e)
		{
			try
			{
				new ArtistDAO().Upload(_artist);
				OnCreate.Invoke(_artist);
			}
			catch (Exception ex)
			{
				ErrorHandler.HandleException(ex, "Unable to add artist", "Artist could not be added to the database due to an internal database error.");
			}
		}

		/// <summary>
		/// Sets the artist's name to the textbox value
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tbName_TextChanged(object sender, EventArgs e)
		{
			_artist.Name = tbName.Text;
		}
	}
}
