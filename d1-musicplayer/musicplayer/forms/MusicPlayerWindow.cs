using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using musicplayer.controls;
using musicplayer.dao;
using musicplayer.forms;
using musicplayer.io;
using musicplayer.io.export;
using musicplayer.io.import;
using musicplayer.report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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

			var featuredAlbumsControl = new FeaturedAlbumsControl(panelContent);
			featuredAlbumsControl.Dock = DockStyle.Fill;
			panelContent.Controls.Add(featuredAlbumsControl);
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
				saveFileDialog.Filter = "HTML Files (*.html)|*.html|All Files (*.*)|*.*";
				saveFileDialog.Title = "Select report file destination";
				saveFileDialog.DefaultExt = "html";
				saveFileDialog.AddExtension = true;
				saveFileDialog.FileName = $"Report_{DateTime.Now:yyyyMMdd}";

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
				DataCollection? data = null;
				try
				{
					data = new ImportDataLoader().LoadFromJson(json);
					if (data == null) throw new ArgumentException();
				}
				catch (ArgumentException ex)
				{
					ErrorHandler.HandleException(ex, "Invalid file", "Selected file is not valid, please verify it is a valid JSON file with correct unmodified data inside.");
					return;
				}
				try
				{
					var directory = Path.GetDirectoryName(dialog.FileName);
					if (directory == null) directory = "";
					new ImportUploader().UploadData(data, directory + "/music", directory + "/images");
					MessageBox.Show("Successfully imported JSON data!", "Import JSON");
				}
				catch (Exception ex)
				{
					ErrorHandler.HandleException(ex, "Import JSON", "An error occured when uploading the data to the database, please verify the JSON file correctness.");
				}

			}
			catch (Exception ex)
			{
				ErrorHandler.HandleException(ex, "Import JSON", "An unknown error occured when importing data.");
			}
		}

		private void exportJSONToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				using (SaveFileDialog saveFileDialog = new SaveFileDialog())
				{
					saveFileDialog.Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*";
					saveFileDialog.Title = "Select export destination";
					saveFileDialog.DefaultExt = "json";
					saveFileDialog.AddExtension = true;
					saveFileDialog.FileName = $"Export_{DateTime.Now:yyyyMMdd}";

					if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

					string filePath = saveFileDialog.FileName;
					DataCollection? data = null;

					try
					{
						data = new ExportDataRetriever().GetExport();
					}
					catch (Exception ex)
					{
						ErrorHandler.HandleException(ex, "Export JSON", "Unable to retrieve data from the database.");
					}
					if (data == null) throw new Exception();

					var directory = Path.GetDirectoryName(filePath);
					if (directory != null) Directory.CreateDirectory(directory);
					else directory = "";
					using (FileStream fs = File.Create(filePath))
					{
						JsonSerializer.Serialize(fs, data, new JsonSerializerOptions()
						{
							WriteIndented = true
						});
					}

					var musicDirectory = $"{directory}/music";
					Directory.CreateDirectory(musicDirectory);
					var imageDirectory = $"{directory}/images";
					Directory.CreateDirectory(imageDirectory);

					var songDao = new SongDAO();
					foreach (var song in data.Songs)
					{
						var dataId = song.Value.DataID;
						if (dataId == null) continue;
						var songData = songDao.GetSongData((int)dataId);
						if (songData == null) continue;
						File.WriteAllBytes($"{musicDirectory}/{dataId}", songData);
					}

					var imageDao = new IconImageDAO();
					foreach (var album in data.Albums)
					{
						var dataId = album.Value.ImageId;
						if (dataId == null) continue;
						var image = imageDao.GetByID((int)dataId);
						if (image == null) continue;
						image.Image.Save($"{imageDirectory}/{dataId}");
					}

					foreach (var artist in data.Artists)
					{
						var dataId = artist.Value.ImageID;
						if (dataId == null) continue;
						var image = imageDao.GetByID((int)dataId);
						if (image == null) continue;
						image.Image.Save($"{imageDirectory}/{dataId}");
					}
				}

				MessageBox.Show("Successfully exported JSON!", "Export JSON");
			}
			catch (Exception ex)
			{
				ErrorHandler.HandleException(ex, "Export JSON", "An error occured when uploading the data to the database.");
			}
		}

		private void bFeatured_Click(object sender, EventArgs e)
		{
			panelContent.Controls.Clear();
			var featuredAlbumsControl = new FeaturedAlbumsControl(panelContent);
			featuredAlbumsControl.Dock = DockStyle.Fill;
			panelContent.Controls.Add(featuredAlbumsControl);
		}
	}
}
