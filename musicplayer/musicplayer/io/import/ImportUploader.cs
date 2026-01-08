using musicplayer.dao;
using musicplayer.dataobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicplayer.io.import
{
	public class ImportUploader
	{
		public ImportUploader() { }

		public void UploadData(DataCollection data, string musicDirectory, string imageDirectory)
		{
			ArtistDAO artistDAO = new ArtistDAO();
			AlbumDAO albumDAO = new AlbumDAO();
			SongDAO songDAO = new SongDAO();

			Dictionary<int, int> artistIDs = new Dictionary<int, int>();
			Dictionary<int, int> albumIDs = new Dictionary<int, int>();
			Dictionary<int, int> songIDs = new Dictionary<int, int>();

			foreach (var pair in data.Artists)
			{
				var artist = pair.Value;

				var image = artist.ImageID != null ? LoadImage((int)artist.ImageID, imageDirectory) : null;

				var artistModel = new Artist(artist.Name)
				{
					Image = image
				};
				artistDAO.Upload(artistModel);
				if (artistModel.Id == null) throw new InvalidOperationException("Unable to add artist to the database!");
				artistIDs.Add(pair.Key, (int)artistModel.Id);
			}

			foreach (var pair in data.Albums)
			{
				var album = pair.Value;

				var image = album.ImageId != null ? LoadImage((int)album.ImageId, imageDirectory) : null;

				var albumModel = new Album(album.Name)
				{
					Image = image,
					ReleaseDate = album.ReleaseDate,
					Type = album.Type,
					Artist = album.ArtistID != null ? new Artist("") { Id = artistIDs[(int)album.ArtistID] } : null,
				};

				albumDAO.Upload(albumModel);
				if (albumModel.Id == null) throw new InvalidOperationException("Unable to add artist to the database!");
				albumIDs.Add(pair.Key, (int)albumModel.Id);
			}

			foreach (var pair in data.Songs)
			{
				var song = pair.Value;

				var songData = song.DataID != null ? LoadSongData((int)song.DataID, musicDirectory) : null;

				var songModel = new Song(song.Name)
				{
					Length = song.Length,
					Rating = song.Rating,
					Data = songData,
					
					// ! Improve this
					Artist = song.ArtistID != null ? new Artist("") { Id = artistIDs[(int)song.ArtistID] } : null,
				};

				songDAO.Upload(songModel);
				if (songModel.Id == null) throw new InvalidOperationException("Unable to add song to the database!");
				songIDs.Add(pair.Key, (int)songModel.Id);
			}

			foreach (var connection in data.AlbumSongs)
			{
				albumDAO.CreateSongConnectionRow(albumIDs[connection.AlbumId], songIDs[connection.SongId], connection.Order);
			}
		}

		private byte[] LoadSongData(int id, string dirPath)
		{
			var data = File.ReadAllBytes($"{dirPath}/{id}");
			return data;
		}

		private IconImage LoadImage(int id, string dirPath)
		{
			IconImage? image;
			using (var fs = File.OpenRead($"{dirPath}/{id}"))
			{
				image = new IconImage(new Bitmap(fs));
			}
			return image;
		}
	}
}
