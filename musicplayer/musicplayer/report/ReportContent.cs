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
		public int MostPopularArtistListeningTime { get; set; }

		public string LeastPopularArtist = "<unknown>";
		public int LeastPopularArtistListeningTime { get; set; }

		public string MostPopularAlbum = "<unknown>";
		public int MostPopularAlbumListeningTime { get; set; }

		public string LeastPopularAlbum = "<unknown>";
		public int LeastPopularAlbumListeningTime { get; set; }

		public string MostPopularSong = "<unknown>";
		public int MostPopularSongListeningTime { get; set; }

		public string LeastPopularSong = "<unknown>";
		public int LeastPopularSongListeningTime { get; set; }
	}
}
