using musicplayer.controls;
using musicplayer.dao;
using musicplayer.dataobjects;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace musicplayer
{
	/// <summary>
	/// Singleton-based class that handles all music-playing related operations
	/// </summary>
	public class AudioPlayerManager
	{
		private static AudioPlayerManager? s_instance;
		private static object s_instanceLock = new object();

		/// <summary>
		/// Returns the singleton AudioPlayerManager instance
		/// </summary>
		/// <returns></returns>
		public static AudioPlayerManager GetPlayerManager()
		{
			lock (s_instanceLock)
			{
				if (s_instance == null) s_instance = new AudioPlayerManager();
				return s_instance;
			}
		}

		/// <summary>
		/// Retrieves the duration of the song
		/// </summary>
		/// <param name="data">mp3 song data</param>
		/// <returns></returns>
		public static int? GetDuration(byte[] data)
		{
			MemoryStream memoryStream = new MemoryStream(data);
			return (int)Math.Ceiling(new Mp3FileReader(memoryStream).TotalTime.TotalSeconds);
		}

		private WaveOutEvent? _outputDevice;
		private Mp3FileReader? _fileReader;
		private MemoryStream? _stream;
		private float volume;

		private LinkedList<Song> _songQueue;
		private LinkedList<Song> _songHistory;

		private Song? _currentSong;

		public Action<int?> OnStartedPlaying { get; set; } = new Action<int?>(delegate { });
		public Action<int?> OnFinishedPlaying { get; set; } = new Action<int?>(delegate { });

		/// <summary>
		/// Constructs a new AudioPlayerManager instance
		/// </summary>
		private AudioPlayerManager()
		{
			volume = 1.0f;
			_songQueue = new LinkedList<Song>();
			_songHistory = new LinkedList<Song>();
		}

		/// <summary>
		/// Plays the given song
		/// </summary>
		/// <param name="song">song</param>
		/// <param name="replace">whether to end the current playback if there is one</param>
		/// <returns></returns>
		public bool PlaySong(Song song, bool replace = true)
		{
			if (song.Data == null && song.DataID == null) return false;
			if (song.Data == null)
			{
				song.Data = new SongDAO().GetSongData((int)song.DataID);
			}
			_songHistory.AddFirst(song);

			PlayerControl.GetPlayerControl().SongName = song.Name;
			if (song.Artist != null) PlayerControl.GetPlayerControl().ArtistName = song.Artist.Name;
			else PlayerControl.GetPlayerControl().ArtistName = "Unknown artist";
			OnStartedPlaying.Invoke(song.Id);
			_currentSong = song;
			PlayAudio(song.Data, replace);
			song.Data = null;
			return true;
		}

		/// <summary>
		/// Adds the given song to the queue
		/// </summary>
		/// <param name="song">song</param>
		public void AddToQueue(Song song)
		{
			_songQueue.AddLast(song);
		}

		/// <summary>
		/// Adds the given song to the song history
		/// </summary>
		/// <param name="song">song</param>
		public void AddToHistory(Song song)
		{
			_songHistory.AddLast(song);
		}

		/// <summary>
		/// Cleares the song queue
		/// </summary>
		public void ClearQueue()
		{
			_songQueue.Clear();
		}

		/// <summary>
		/// Plays the raw mp3 audio data
		/// </summary>
		/// <param name="data">audio data</param>
		/// <param name="replace">whether to end the current playback if there is one</param>
		public void PlayAudio(byte[] data, bool replace)
		{
			if (_outputDevice != null)
			{
				if (!replace) return;
				//_outputDevice.Stop();
				Dispose();
			}
			_outputDevice = new WaveOutEvent();

			_stream = new MemoryStream(data);
			//_paused = false;

			_fileReader = new Mp3FileReader(_stream);
			_outputDevice.Init(_fileReader);

			_outputDevice.PlaybackStopped += OnPlaybackStopped;
			_outputDevice.Volume = volume;

			_outputDevice.Play();

			PlayerControl.GetPlayerControl().PlayButtonText = "Stop";
			PlayerControl.GetPlayerControl().Enable();
		}

		/// <summary>
		/// Toggles the playback state between paused and unpaused
		/// </summary>
		/// <returns>if the song is playing</returns>
		public bool TogglePause()
		{
			if (_outputDevice == null) return false;
			if (_outputDevice.PlaybackState == PlaybackState.Stopped)
			{
				Continue();
				return true;
			}
			else
			{
				Stop();
				return false;
			}
		}

		/// <summary>
		/// Continues playing the song
		/// </summary>
		private void Continue()
		{
			if (_outputDevice == null || _fileReader == null || _stream == null) return;

			int? id = _currentSong?.Id;
			OnStartedPlaying.Invoke(id);
			_outputDevice.Play();
		}

		/// <summary>
		/// Stops playing the song
		/// </summary>
		private void Stop()
		{
			int? id = _currentSong?.Id;
			OnFinishedPlaying.Invoke(id);
			_outputDevice?.Stop();
		}

		/// <summary>
		/// Plays the next song in the queue
		/// </summary>
		public void Next()
		{
			Stop();
			PlayNextSong();
		}

		/// <summary>
		/// Replays the current song or plays the previous song based on the current progress
		/// </summary>
		public void Back()
		{
			if (_songHistory.Count < 1) return;
			Stop();
			if (Progress > 0.1)
			{
				Song song = _songHistory.First();
				_songHistory.RemoveFirst();
				PlaySong(song);
			}
			else
			{
				if (_songHistory.Count < 2) Clear();
				else
				{
					Song currentSong = _songHistory.First();
					_songHistory.RemoveFirst();
					_songQueue.AddFirst(currentSong);
					Song newSong = _songHistory.First();
					_songHistory.RemoveFirst();
					PlaySong(newSong);
				}
			}
		}

		/// <summary>
		/// Releases music data related memory
		/// </summary>
		public void Dispose()
		{
			_outputDevice?.Dispose();
			_fileReader?.Dispose();
			_stream?.Close();
		}

		/// <summary>
		/// Releases music data related memory and clears the queue, disabling the player control instance in the process
		/// </summary>
		public void Clear()
		{
			Dispose();
			_songQueue?.Clear();
			_songHistory?.Clear();
			PlayerControl.GetPlayerControl().Disable();
		}

		/// <summary>
		/// Floating point value determining the current playback progress (0-1)
		/// </summary>
		public float Progress
		{
			get
			{
				if (_outputDevice == null || _fileReader == null) return 0;
				try
				{
					return _fileReader.Position / (float)_fileReader.Length;
				}
				catch
				{
					return 0;
				}
			}

			set
			{
				if (_outputDevice == null || _fileReader == null) return;
				_fileReader.Position = (long)(_fileReader.Length * value);
			}
		}

		/// <summary>
		/// Floating point value determining the current volume (0-1)
		/// </summary>
		public float Volume
		{
			get
			{
				return volume;
			}
			set
			{
				volume = value;
				if (_outputDevice != null) _outputDevice.Volume = volume;
			}
		}

		/// <summary>
		/// Advances to the next song if there is no song playing
		/// </summary>
		private void PlayNextSong()
		{
			Dispose();
			/*Song? song;
			bool calledBreak = false;
			while (_songQueue.Count)
			{
				if (song != null && PlaySong(song, true))
				{
					calledBreak = true;
					break;
				}
			}
			if (!calledBreak)
			{
				Clear();
			}*/

			if (_songQueue.Count >= 1)
			{
				Song song = _songQueue.First();
				_songQueue.Remove(song);
				PlaySong(song);
			}
			else
			{
				Clear();
			}
		}

		/// <summary>
		/// EventHandler that handles behaviour upon playback stopping
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void OnPlaybackStopped(object? sender, EventArgs args)
		{
			if (Progress >= 1)
			{
				int? id = _currentSong?.Id;
				OnFinishedPlaying.Invoke(id);
				PlayNextSong();
			}
		}
	}
}
