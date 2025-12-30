using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace musicplayer.controls
{
	/// <summary>
	/// Singleton-based UserControl used for displaying and managing the current playback state
	/// </summary>
	public partial class PlayerControl : UserControl
	{
		private static PlayerControl? s_instance;
		private static object s_instanceLock = new object();

		private const string NO_SONG_TEXT = "(no song playing)";
		private const string NO_ARTIST_TEXT = "(no artist playing)";

		/// <summary>
		/// Returns the singleton player control instance
		/// </summary>
		/// <returns>player control</returns>
		public static PlayerControl GetPlayerControl()
		{
			lock (s_instanceLock)
			{
				if (s_instance == null) s_instance = new PlayerControl();
				return s_instance;
			}
		}

		private System.Windows.Forms.Timer timer;

		/// <summary>
		/// Constructs a new PlayerControl instance
		/// </summary>
		private PlayerControl()
		{
			InitializeComponent();
			timer = new System.Windows.Forms.Timer();
			timer.Interval = 1000;
			timer.Tick += ProgressBarTimerTick;
			Disable();
		}

		/// <summary>
		/// The displayed song name
		/// </summary>
		public string SongName
		{
			get => lSongName.Text;
			set => lSongName.Text = value;
		}

		/// <summary>
		/// The displayed artist name
		/// </summary>
		public string ArtistName
		{
			get => lArtist.Text;
			set => lArtist.Text = value;
		}

		/// <summary>
		/// The text on the play button
		/// </summary>
		public string PlayButtonText
		{
			get => bPlayPause.Text;
			set => bPlayPause.Text = value;
		}

		/// <summary>
		/// Enables player control element manipulation
		/// </summary>
		public void Enable()
		{
			bBack.Enabled = true;
			bPlayPause.Enabled = true;
			bNext.Enabled = true;

			trbProgress.Enabled = true;

			lSongName.Enabled = true;
			lArtist.Enabled = true;

			StartTimer();
		}

		/// <summary>
		/// Disables player control element manipulation
		/// </summary>
		public void Disable()
		{
			bBack.Enabled = false;
			bPlayPause.Enabled = false;
			bNext.Enabled = false;

			trbProgress.Enabled = false;
			trbProgress.Value = 0;

			lSongName.Enabled = false;
			lSongName.Text = NO_SONG_TEXT;
			lArtist.Enabled = false;
			lArtist.Text = NO_ARTIST_TEXT;

			bPlayPause.Text = "Play";

			StopTimer();
		}

		/// <summary>
		/// Starts the timer used for updating the progress bar
		/// </summary>
		private void StartTimer()
		{
			timer.Start();
		}

		/// <summary>
		/// Stops the timer used for updating the progress bar
		/// </summary>
		private void StopTimer()
		{
			timer.Stop();
		}

		/// <summary>
		/// Sets the position of the progress bar to a value between 0 and 1
		/// </summary>
		/// <param name="progress">0-1</param>
		private void SetProgress(float progress)
		{
			if (progress > 1 || progress < 0) return;
			trbProgress.Value = (int)(progress * trbProgress.Maximum);
		}

		/// <summary>
		/// EventHandler that updates the progress bar progress
		/// </summary>
		/// <param name="source"></param>
		/// <param name="args"></param>
		private void ProgressBarTimerTick(object? source, EventArgs args)
		{
			SetProgress(AudioPlayerManager.GetPlayerManager().Progress);
		}

		/// <summary>
		/// EventHandler that handles progress bar scrolling input and sets playback position to the set position
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void trbProgress_Scroll(object sender, EventArgs e)
		{
			StopTimer();
			AudioPlayerManager.GetPlayerManager().Progress = trbProgress.Value / 100.0f;
			StartTimer();
		}

		/// <summary>
		/// EventHandler used by the volume slider for setting the volume
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void trbVolume_Scroll(object sender, EventArgs e)
		{
			AudioPlayerManager.GetPlayerManager().Volume = trbVolume.Value / 100.0f;
		}

		/// <summary>
		/// EventHandler that plays the previous song
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bBack_Click(object sender, EventArgs e)
		{
			AudioPlayerManager.GetPlayerManager().Back();
		}

		/// <summary>
		/// EventHandler that skips the current song
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bNext_Click(object sender, EventArgs e)
		{
			AudioPlayerManager.GetPlayerManager().Next();
		}

		/// <summary>
		/// EventHandler that plays or pauses the current song
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bPlayPause_Click(object sender, EventArgs e)
		{
			if (AudioPlayerManager.GetPlayerManager().TogglePause())
			{
				PlayButtonText = "Stop";
			}
			else
			{
				PlayButtonText = "Play";
			}
		}
	}
}
