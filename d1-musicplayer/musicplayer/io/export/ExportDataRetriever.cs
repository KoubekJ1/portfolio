using musicplayer.dao;
using musicplayer.io.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicplayer.io.export
{
	public class ExportDataRetriever
	{
		public ExportDataRetriever() { }

		public DataCollection GetExport()
		{
			var data = new DataCollection();

			var albumDao = new AlbumDAO();
			var artistDao = new ArtistDAO();

			GetSongs(data.Songs);
			GetAlbums(data.Albums);
			GetArtists(data.Artists);
			GetAlbumSongs(data.Albums.Keys, data.AlbumSongs);

			return data;
		}

		private void GetSongs(Dictionary<int, Song> songs)
		{
			if (songs.Count > 0)
			{
				throw new InvalidOperationException("Argument must be an empty Linked List!");
			}

			var songDao = new SongDAO();

			int totalSongCount = songDao.GetCount();
			int lastIndex = 0;

			while (songs.Count < totalSongCount)
			{
				int? id;
				var loadedSongs = songDao.GetRange(lastIndex+1, 100);
				foreach (var song in loadedSongs)
				{
					id = song.Id;
					if (id == null) throw new InvalidDataException("A song with no primary key was loaded!");
					var newSong = new Song()
					{
						Name = song.Name,
						DataID = song.DataID,
						Length = song.Length,
						Rating = song.Rating,
						ArtistID = song.Artist?.Id,
					};
					songs.Add((int)id, newSong);
					lastIndex = (int)id;
				}
				
			}
		}

		private void GetAlbums(Dictionary<int, Album> albums)
		{
			if (albums.Count > 0)
			{
				throw new InvalidOperationException("Argument must be empty!");
			}

			var albumDao = new AlbumDAO();
			int totalCount = albumDao.GetCount();
			int lastIndex = 0;

			while (albums.Count < totalCount)
			{
				var loadedAlbums = albumDao.GetRange(lastIndex + 1, 100);
				foreach (var album in loadedAlbums)
				{
					int? id = album.Id;
					if (id == null) throw new InvalidDataException("An album with no primary key was loaded!");

					var newAlbum = new Album()
					{
						Name = album.Name,
						ImageId = album.Image?.Id,
						ArtistID = album.Artist?.Id,
						ReleaseDate = album.ReleaseDate,
						Type = album.Type
					};

					albums.Add((int)id, newAlbum);
					lastIndex = (int)id;
				}
			}
		}

		private void GetArtists(Dictionary<int, Artist> artists)
		{
			if (artists.Count > 0)
			{
				throw new InvalidOperationException("Argument must be empty!");
			}

			var artistDao = new ArtistDAO();
			int totalCount = artistDao.GetCount();
			int lastIndex = 0;

			while (artists.Count < totalCount)
			{
				var loadedArtists = artistDao.GetRange(lastIndex + 1, 100);
				foreach (var artist in loadedArtists)
				{
					int? id = artist.Id;
					if (id == null) throw new InvalidDataException("An artist with no primary key was loaded!");

					var newArtist = new Artist()
					{
						Name = artist.Name,
						ImageID = artist.Image?.Id,
					};

					artists.Add((int)id, newArtist);
					lastIndex = (int)id;
				}
			}
		}

		private void GetAlbumSongs(IEnumerable<int> albumIds, LinkedList<AlbumSong> albumSongs)
		{
			if (albumSongs.Count > 0)
			{
				throw new InvalidOperationException("albumSongs argument must be empty!");
			}

			var songDao = new SongDAO();

			foreach (var albumID in albumIds)
			{
				var songs = songDao.GetSongsFromAlbum(albumID);
				for (int i = 0; i < songs.Count; i++)
				{
					var song = songs[i];
					var songId = song.Id;
					if (songId == null) throw new InvalidDataException("An artist with no primary key was loaded!");
					albumSongs.AddLast(new LinkedListNode<AlbumSong>(new AlbumSong()
					{
						AlbumId = albumID,
						SongId = (int)songId,
						Order = i
					}));
				}
			}
		}
	}
}
