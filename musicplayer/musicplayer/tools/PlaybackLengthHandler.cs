using musicplayer.dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicplayer.tools
{
	public class PlaybackLengthHandler
	{
		public PlaybackLengthHandler() { }

		private long? _playbackStartTime = null;

		public void Register()
		{
			AudioPlayerManager.GetPlayerManager().OnStartedPlaying += OnStartedPlayingListener;
			AudioPlayerManager.GetPlayerManager().OnFinishedPlaying += OnFinishedPlayingListener;
		}

		private void OnStartedPlayingListener(int? id)
		{
			_playbackStartTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
		}

		private void OnFinishedPlayingListener(int? id)
		{
			if (id == null || _playbackStartTime == null) return;

			long time = (long)(DateTimeOffset.UtcNow.ToUnixTimeSeconds() - _playbackStartTime);
			_playbackStartTime = null;

			try
			{
				ISongDAO songDAO = new SongDAO();
				songDAO.AddListeningTime(time, (int)id);
			}
			catch (Exception)
			{
			}
		}
	}
}
