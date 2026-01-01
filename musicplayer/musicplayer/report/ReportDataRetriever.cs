using Microsoft.Data.SqlClient;
using musicplayer.dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicplayer.report
{
	public class ReportDataRetriever
	{
        private ArtistDAO artistDAO = new ArtistDAO();
        private AlbumDAO albumDAO = new AlbumDAO();
        private SongDAO songDAO = new SongDAO();
        private IconImageDAO iconImageDAO = new IconImageDAO();

		public ReportContent GetReportContent()
		{
			ReportContent content = new ReportContent();
			content.ArtistCount = artistDAO.GetCount();
			content.AlbumCount = albumDAO.GetCount();
			content.SongCount = songDAO.GetCount();

            // Total Listening Time
            content.TotalListeningTime = songDAO.GetTotalListeningTime();

            // Most Popular Song
            var mostPopularSong = songDAO.GetMostPopularSong();
            if (mostPopularSong != null)
            {
                content.MostPopularSong = mostPopularSong.Value.Item1;
                content.MostPopularSongListeningTime = mostPopularSong.Value.Item2;
            }

            // Least Popular Song
            var leastPopularSong = songDAO.GetLeastPopularSong();
            if (leastPopularSong != null)
            {
                content.LeastPopularSong = leastPopularSong.Value.Item1;
                content.LeastPopularSongListeningTime = leastPopularSong.Value.Item2;
            }

            // Most Popular Artist
            var mostPopularArtist = artistDAO.GetMostPopularArtist();
            if (mostPopularArtist != null)
            {
                content.MostPopularArtist = mostPopularArtist.Value.Item1;
                content.MostPopularArtistListeningTime = mostPopularArtist.Value.Item2;
            }

            // Least Popular Artist
            var leastPopularArtist = artistDAO.GetLeastPopularArtist();
            if (leastPopularArtist != null)
            {
                content.LeastPopularArtist = leastPopularArtist.Value.Item1;
                content.LeastPopularArtistListeningTime = leastPopularArtist.Value.Item2;
            }

            // Most Popular Album
            var mostPopularAlbum = albumDAO.GetMostPopularAlbum();
            if (mostPopularAlbum != null)
            {
                content.MostPopularAlbum = $"{mostPopularAlbum.Value.Item1} - {mostPopularAlbum.Value.Item2}";
                content.MostPopularAlbumListeningTime = mostPopularAlbum.Value.Item3;
            }

            // Least Popular Album
            var leastPopularAlbum = albumDAO.GetLeastPopularAlbum();
            if (leastPopularAlbum != null)
            {
                content.LeastPopularAlbum = leastPopularAlbum.Value.Item1;
                content.LeastPopularAlbumListeningTime = leastPopularAlbum.Value.Item2;
            }

			return content;
		}
	}
}
