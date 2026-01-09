using musicplayer.controls.forms;
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
	/// Form used for creating or updating an artist
	/// </summary>
	public partial class AddArtistForm : Form
	{
		public Artist? Artist { get; private set; }

		/// <summary>
		/// Constructs a new AddArtistForm
		/// </summary>
		public AddArtistForm()
		{
			InitializeComponent();
			this.FormBorderStyle = FormBorderStyle.FixedSingle;

			NewArtistFormControl control = new NewArtistFormControl();
			control.OnCreate += (artist) =>
			{
				Artist = artist;
				MessageBox.Show("Artist " + artist.Name + " added successfully", artist.Name);
				this.Close();
			};
			control.Dock = DockStyle.Fill;
			this.Controls.Add(control);
		}

		/// <summary>
		/// Constructs a new AddArtistForm with the given artist to edit
		/// </summary>
		/// <param name="artist">Artist</param>
		public AddArtistForm(Artist artist)
		{
			InitializeComponent();
			this.FormBorderStyle = FormBorderStyle.FixedSingle;
			Artist = artist;

			NewArtistFormControl control = new NewArtistFormControl(artist);
			control.OnCreate += (updatedArtist) =>
			{
				Artist = updatedArtist;
				MessageBox.Show("Artist " + updatedArtist.Name + " updated successfully", updatedArtist.Name);
				this.Close();
			};
			control.Dock = DockStyle.Fill;
			this.Controls.Add(control);
		}

	}
}
