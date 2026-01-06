using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using musicplayer.controls;
using musicplayer.dao;
using musicplayer.forms;
using musicplayer.import;
using musicplayer.report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace musicplayer
{
	/// <summary>
	/// MusicPlayerWindow serves as the main window where all the music playing takes place. All playing related subcontrols are directly below this form.
	/// </summary>
	public partial class MusicPlayerWindow : Form
	{
		/// <summary>
		/// Initializes a new MusicPlayerWindow instance
		/// </summary>
		public MusicPlayerWindow()
		{
			InitializeComponent();
			//this.FormBorderStyle = FormBorderStyle.FixedSingle;
			this.MinimumSize = this.Size;
			PlayerControl control = PlayerControl.GetPlayerControl();
			control.Dock = DockStyle.Top;
			this.Controls.Add(control);
			this.FormClosing += DisposeAudioManager;
		}

		/// <summary>
		/// EventHandler that disposes of the current audio buffers.
		/// This should be done before exiting the program
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DisposeAudioManager(object? sender, FormClosingEventArgs e)
		{
			AudioPlayerManager.GetPlayerManager().Dispose();
		}

		/// <summary>
		/// EventHandler that opens the new artist window
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void artistToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new AddArtistForm().ShowDialog();
		}

		/// <summary>
		/// EventHandler that opens the about window
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new AboutForm().ShowDialog();
		}

		/// <summary>
		/// EventHandler that sets the content panel's content to the artists menu
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonArtists_Click(object sender, EventArgs e)
		{
			try
			{
				panelContent.Controls.Clear();
				ArtistsControl artistsControl = new ArtistsControl(panelContent);
				artistsControl.Dock = DockStyle.Fill;
				panelContent.Controls.Add(artistsControl);
			}
			catch (SqlException ex) when (ex.Number == 208)
			{
				MessageBox.Show("Unable to show page due to database schema being incorrect.", "Invalid database schema");
			}
			catch (Exception ex)
			{
				ErrorHandler.HandleException(ex);
			}
		}

		/// <summary>
		/// EventHandler that opens the new song window
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void songToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new AddSongForm().ShowDialog();
		}

		/// <summary>
		/// EventHandler that opens the new album window
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void albumToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new AddAlbumForm().ShowDialog();
		}

		private void MusicPlayerWindow_Load(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// EventHandler that sets the content panel's content to the albums menu
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bAlbums_Click(object sender, EventArgs e)
		{
			panelContent.Controls.Clear();
			try
			{
				AlbumsListControl displayControl = new AlbumsListControl(new AlbumDAO().GetAll(), panelContent);
				displayControl.Dock = DockStyle.Fill;
				panelContent.Controls.Add(displayControl);
			}
			catch (SqlException ex) when (ex.Number == 208)
			{
				MessageBox.Show("Unable to show page due to database schema being incorrect.", "Invalid database schema");
			}
			catch (Exception ex)
			{
				ErrorHandler.HandleException(ex, "Unable to load albums.", "Error");
			}
		}

		/// <summary>
		/// EventHandler that sets the content panel's content to the songs menu
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bSongs_Click(object sender, EventArgs e)
		{
			panelContent.Controls.Clear();
			try
			{
				var songs = new SongDAO().GetAll();
				var songControl = new SongOverviewControl(songs, panelContent);
				songControl.Dock = DockStyle.Fill;
				panelContent.Controls.Add(songControl);
			}
			catch (SqlException ex) when (ex.Number == 208)
			{
				MessageBox.Show("Unable to show page due to database schema being incorrect.", "Invalid database schema");
			}
			catch (Exception ex)
			{
				ErrorHandler.HandleException(ex, "Error", "Could not load songs.");
			}
		}

		private void createReportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (SaveFileDialog saveFileDialog = new SaveFileDialog())
			{
				// Force the user to stick to the format you support. 
				// Don't let them save as .txt if you are writing HTML.
				saveFileDialog.Filter = "HTML Files (*.html)|*.html|All Files (*.*)|*.*";
				saveFileDialog.Title = "Select report file destination";
				saveFileDialog.DefaultExt = "html";
				saveFileDialog.AddExtension = true;
				saveFileDialog.FileName = $"Report_{DateTime.Now:yyyyMMdd}"; // Suggest a decent default.

				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					string filePath = saveFileDialog.FileName;

					try
					{
						var content = new ReportDataRetriever().GetReportContent();
						var generator = new ReportGenerator();
						var reportText = generator.CreateReport(content);
						File.WriteAllText(filePath, reportText);
						MessageBox.Show("Report generated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					catch (Exception ex)
					{
						// Handle the inevitable IO errors (permissions, open files, etc.)
						ErrorHandler.HandleException(ex, "Failed to save report", "Create report");
					}
				}
			}
		}

		private void importJSONToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				using var dialog = new OpenFileDialog
				{
					Title = "Select a JSON file",
					Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
					FilterIndex = 1,
					Multiselect = false
				};

				if (dialog.ShowDialog() != DialogResult.OK) return;

				var json = File.ReadAllText(dialog.FileName);
				ImportData? data = null;
				try
				{
					data = new ImportDataLoader().LoadFromJson(json);
					if (data == null) throw new ArgumentException();
				}
				catch (ArgumentException ex)
				{
					ErrorHandler.HandleException(ex, "Invalid file", "Selected file is not valid!");
					return;
				}
				try
				{
					new ImportUploader().UploadData(data);
				}
				catch (Exception ex)
				{
					ErrorHandler.HandleException(ex, "Import JSON", "An error occured when uploading the data to the database");
				}

			}
			catch (Exception ex)
			{
				ErrorHandler.HandleException(ex, "Import JSON", "An error occured when uploading the data to the database");
			}
		}
	}
}
