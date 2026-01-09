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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace musicplayer.controls.forms
{
	public partial class NewArtistFormControl : UserControl
	{
		private Artist _artist;
		private bool _isAutofill = false;
		private bool _matched = false;
		private Artist? _matchedArtist = null;
		private IEnumerable<Artist> _potentionalMatches = [];
		private bool _editing = false;

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

			_isAutofill = isAutofill;

			if (isAutofill)
			{
				bAdd.Dispose();

			}
			else
			{
				lbArtists.Visible = false;
			}
		}

		/// <summary>
		/// Constructs a new AddArtistForm with the given artist to edit
		/// </summary>
		/// <param name="artist">Artist</param>
		public NewArtistFormControl(Artist artist, bool isAutofill = false)
		{
			InitializeComponent();

			_editing = true;

			OnCreate = delegate { };

			_artist = artist;

			if (_artist.Image != null) pbImage.Image = _artist.Image.Image;

			_isAutofill = isAutofill;

			if (isAutofill)
			{
				bAdd.Dispose();

			}
			else
			{
				lbArtists.Dispose();
			}

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
			if (!IsValid(true))
			{
				MessageBox.Show("Please fix all validation errors", "Add album");
				return;
			}

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
			lNameValidation.Visible = false;
			var length = tbName.Text.Length;
			if (length < 3 || length > 100)
			{
				lNameValidation.Visible = true;
			}

			if (_matched)
			{
				_matched = false;
				_artist = new Artist(tbName.Text);
				pbImage.Image = null;
			}
			_artist.Name = tbName.Text;
			bChangeImage.Enabled = true;
				try
				{
					var dao = new ArtistDAO();
					_potentionalMatches = dao.GetByName(tbName.Text, 10);
					lbArtists.Items.Clear();
					foreach (var match in _potentionalMatches)
					{
						lbArtists.Items.Add(match);
					if (tbName.Text.ToLower() == match.Name.ToLower())
					{
						var selectionStart = tbName.SelectionStart;
						var selectionLength = tbName.SelectionLength;
						MatchArtist(match);
						tbName.SelectionStart = selectionStart;
						tbName.SelectionLength = selectionLength;
					}
					}
				}
				catch (Exception) { }
		}

		private void lbArtists_SelectedIndexChanged(object sender, EventArgs e)
		{
			bChangeImage.Enabled = true;
			_matched = false;
			_artist = new Artist("");
			pbImage.Image = null;
			if (lbArtists.SelectedItem != null) {
				MatchArtist((Artist)lbArtists.SelectedItem);
			}
		}

		public bool IsValid(bool flag = true)
		{
			var length = tbName.Text.Length;
			bool invalid = length < 3 || length > 100;
			if (invalid && flag) lNameValidation.Visible = true;
			return !invalid;
		}

		private void MatchArtist(Artist artist)
		{
			lNameValidation.Visible = false;
			_artist = artist;
			if (!_editing) bChangeImage.Enabled = false;
			tbName.TextChanged -= tbName_TextChanged;
			tbName.Text = _artist.Name;
			tbName.TextChanged += tbName_TextChanged;
			_matched = true;
			if (_artist.ImageID == null) return;
			try
			{
				var dao = new IconImageDAO();
				var iconImage = dao.GetByID((int)_artist.ImageID);
				if (iconImage == null) return;
				_artist.Image = iconImage;
				pbImage.Image = IconImage.ResizeImage(iconImage.Image, pbImage.Width, pbImage.Height);
				pbImage.SizeMode = PictureBoxSizeMode.StretchImage;
			}
			catch (Exception) { }
		}
	}
}
