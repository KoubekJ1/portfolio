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

namespace musicplayer.forms
{
	/// <summary>
	/// Form used for picking an album
	/// </summary>
	public partial class AlbumPicker : Form
	{
		private IEnumerable<Album> _originalAlbumsQuery;
		private Album? _album;

		public Album? Album { get => _album; }

		/// <summary>
		/// Constructs a new AlbumPicker instance
		/// </summary>
		public AlbumPicker()
		{
			InitializeComponent();
			this.FormBorderStyle = FormBorderStyle.FixedSingle;

			AlbumDAO dao = new AlbumDAO();
			_originalAlbumsQuery = dao.GetAll();
			foreach (var album in _originalAlbumsQuery)
			{
				lbAlbums.Items.Add(album);
			}
		}

		/// <summary>
		/// Filters the albums based on the given name
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tbSearch_TextChanged(object sender, EventArgs e)
		{
			// Ordinary contains doesnt work due to case sensitivity
			IEnumerable<Album> albumQuery = _originalAlbumsQuery.Where(art => art.Name.IndexOf(tbSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0);
			lbAlbums.Items.Clear();
			foreach (var songInQuery in albumQuery)
			{
				lbAlbums.Items.Add(songInQuery);
			}
		}

		/// <summary>
		/// Selects the album
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bSelect_Click(object sender, EventArgs e)
		{
			_album = lbAlbums.SelectedItem as Album;
			this.Close();
		}
	}
}
