using musicplayer.controls.forms;
using musicplayer.dao;
using musicplayer.dataobjects;
using musicplayer.forms;
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
	/// Form used for creating or updating an album
	/// </summary>
	public partial class AddAlbumForm : Form
	{
		private Album _album;

		private NewArtistFormControl _artistControl;

		public Album? Album { get => _album; }

		/// <summary>
		/// Constructs a new AddAlbumForm with a new album
		/// </summary>
		public AddAlbumForm()
		{
			InitializeComponent();
			this.FormBorderStyle = FormBorderStyle.FixedSingle;
			_album = new Album("");

			cbType.DropDownStyle = ComboBoxStyle.DropDownList;

			AddNewSongForm();

			this.KeyPreview = true;

			_artistControl = new NewArtistFormControl(true);
			_artistControl.Dock = DockStyle.Fill;
			pArtistContainer.Controls.Add(_artistControl);
		}

		/// <summary>
		/// Constructs a new AddAlbumForm with an existing album to edit
		/// </summary>
		/// <param name="album">album</param>
		public AddAlbumForm(Album album)
		{
			InitializeComponent();
			this.FormBorderStyle = FormBorderStyle.FixedSingle;
			cbType.DropDownStyle = ComboBoxStyle.DropDownList;
			cbType.Text = album.Type;
			dtpReleaseDate.Value = album.ReleaseDate.ToDateTime(new TimeOnly(0, 0, 0));
			this.Text = "Edit Album";
			bAddAlbum.Text = "Save Changes";
			_album = album;
			tbName.Text = album.Name;
			if (album.Artist != null)
			{
				_artistControl = new NewArtistFormControl(album.Artist, true);
			}
			else
			{
				_artistControl = new NewArtistFormControl(true);
			}
			_artistControl.Dock = DockStyle.Fill;
			pArtistContainer.Controls.Add(_artistControl);
			if (album.Image != null) pbImage.Image = IconImage.ResizeImage(album.Image.Image, pbImage.Width, pbImage.Height);

			foreach (Song song in album.Songs)
			{
				lbSongs.Items.Add(song);
			}

			AddNewSongForm();
		}

		private void AddNewSongForm()
		{
			NewSongFormControl newSongFormControl = null!;
			newSongFormControl = new NewSongFormControl((song) =>
			{
				lbSongs.Items.Add(song);
				newSongFormControl.Clear();
			}, true);
			this.pNewSongFormContainer.Controls.Add(newSongFormControl);
		}

		/// <summary>
		/// EventHandler that changes the album picture
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bChange_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filter = "All files (*.*)|*.*|JPEG (*.jpeg)|*.jpeg|PNG (*.png)|*.png|Bitmap (*.bmp)|*.bmp";
			if (dialog.ShowDialog() != DialogResult.OK) return;
			try
			{
				_album.Image = new IconImage(new Bitmap(dialog.FileName));
				pbImage.Image = IconImage.ResizeImage(new Bitmap(dialog.FileName), pbImage.Width, pbImage.Height);
				pbImage.SizeMode = PictureBoxSizeMode.StretchImage;
			}
			catch (Exception ex)
			{
				ErrorHandler.HandleException(ex, "Unable to load image: " + dialog.FileName, "Error");
				return;
			}
		}
		/// <summary>
		/// Adds the album to the database
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bAddAlbum_Click(object sender, EventArgs e)
		{
			bool albumValid = IsValid(true);
			bool artistValid = _artistControl.IsValid(true);
			if (!albumValid || !artistValid)
			{
				MessageBox.Show("Please fix all validation errors", "Add album");
				return;
			}
			_album.ReleaseDate = DateOnly.FromDateTime(dtpReleaseDate.Value);
			_album.Type = cbType.Text.ToLower();
			_album.Songs.Clear();
			foreach (var lbItem in lbSongs.Items)
			{
				Song? song = lbItem as Song;
				if (song == null) continue;
				song.Artist = _artistControl.Artist;
				_album.Songs.Add(song);
			}

			_album.Artist = _artistControl.Artist;

			AlbumDAO dao = new AlbumDAO();
			ArtistDAO artistDAO = new ArtistDAO();
			try
			{
				var connection = DatabaseConnection.GetConnection();
				connection.Open();
				DatabaseConnection.CreateTransaction();
				try
				{
					artistDAO.Upload(_album.Artist);
					dao.Upload(_album);
					if (_album.Id != null)
					{
						MessageBox.Show("Album \"" + _album.Name + "\" was successfully uploaded.", "Add Album");
						DatabaseConnection.CommitTransaction();
						connection.Close();
						this.Close();
					}
					else
					{
						DatabaseConnection.RollbackTransaction();
						connection.Close();
						throw new Exception("ID is null!");
					}
				}
				catch (Exception)
				{
					DatabaseConnection.RollbackTransaction();
					connection.Close();
					throw;
				}
			}
			catch (Exception ex)
			{
				ErrorHandler.HandleException(ex, "Error", "Unable to upload album due to an internal error");
			}
		}

		/// <summary>
		/// Opens the add song dialog to add a song to the album
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bAddSong_Click(object sender, EventArgs e)
		{
			SongPicker picker = new SongPicker();
			picker.ShowDialog();
			if (picker.Song == null) return;
			lbSongs.Items.Add(picker.Song);
		}

		/// <summary>
		/// Shifts the selected song up a level
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bUp_Click(object sender, EventArgs e)
		{
			if (lbSongs.SelectedItem == null || lbSongs.SelectedIndex <= 0 || lbSongs.Items.Count <= 1) return;
			int selectedIndex = lbSongs.SelectedIndex;
			int swapIndex = selectedIndex - 1;
			object? selectedSong = lbSongs.SelectedItem;
			object? swapSong = lbSongs.Items[swapIndex];

			lbSongs.Items[swapIndex] = selectedSong;
			lbSongs.Items[selectedIndex] = swapSong;

			lbSongs.SelectedIndex = swapIndex;
		}

		/// <summary>
		/// Lowers the selected song down a level
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bDown_Click(object sender, EventArgs e)
		{
			if (lbSongs.SelectedItem == null || lbSongs.SelectedIndex >= lbSongs.Items.Count - 1 || lbSongs.Items.Count <= 1) return;
			int selectedIndex = lbSongs.SelectedIndex;
			int swapIndex = selectedIndex + 1;
			object? selectedSong = lbSongs.SelectedItem;
			object? swapSong = lbSongs.Items[swapIndex];

			lbSongs.Items[swapIndex] = selectedSong;
			lbSongs.Items[selectedIndex] = swapSong;

			lbSongs.SelectedIndex = swapIndex;
		}

		/// <summary>
		/// Sets the album name to the textbox value
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

			_album.Name = tbName.Text;
		}

		public bool IsValid(bool flag = true)
		{
			var length = tbName.Text.Length;
			bool invalid = length < 3 || length > 100;
			if (invalid && flag) lNameValidation.Visible = true;
			return !invalid;
		}

		private void FormKeyDown(object sender, KeyEventArgs e)
		{

		}

		private void label1_Click(object sender, EventArgs e)
		{

		}
	}
}
