using musicplayer.dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicplayer.import
{
	public class ImportUploader
	{
		public ImportUploader() { }

		public void UploadData(ImportData data)
		{
			ArtistDAO artistDAO = new ArtistDAO();
			AlbumDAO albumDAO = new AlbumDAO();
			SongDAO songDAO = new SongDAO();

			foreach (var artist in data.Artists.Values)
			{
				artistDAO.Upload(artist);
			}

			foreach (var album in data.Albums)
			{
				albumDAO.Upload(album);
			}

			foreach (var song in data.Songs)
			{
				songDAO.Upload(song);
			}
		}
	}
}
