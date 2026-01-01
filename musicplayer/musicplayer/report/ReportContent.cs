using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicplayer.report
{
	public class ReportContent
	{
		public int ArtistCount { get; set; }
		public int AlbumCount { get; set; }
		public int SongCount { get; set; }

		public long TotalListeningTime { get; set; }

		public string MostPopularArtist = "<unknown>";
		public long MostPopularArtistListeningTime { get; set; }

		public string LeastPopularArtist = "<unknown>";
		public long LeastPopularArtistListeningTime { get; set; }

		public string MostPopularAlbum = "<unknown>";
		public long MostPopularAlbumListeningTime { get; set; }

		public string LeastPopularAlbum = "<unknown>";
		public long LeastPopularAlbumListeningTime { get; set; }

		public string MostPopularSong = "<unknown>";
		public long MostPopularSongListeningTime { get; set; }

		public string LeastPopularSong = "<unknown>";
		public long LeastPopularSongListeningTime { get; set; }
	}
}
