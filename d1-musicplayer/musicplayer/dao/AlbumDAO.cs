using Microsoft.Data.SqlClient;
using musicplayer.dataobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicplayer.dao
{
    /// <summary>
    /// DAO implementation that retrieves Album instances using the DatabaseConnection singleton
    /// </summary>
    public class AlbumDAO : IAlbumDAO
    {

        /// <summary>
        /// Retrieves all albums from the database
        /// </summary>
        /// <returns>all albums</returns>
        public IEnumerable<Album> GetAll()
        {
            LinkedList<Album> albums = new LinkedList<Album>();
            LinkedList<int?> imgIDs = new LinkedList<int?>();
            LinkedList<int?> artistIDs = new LinkedList<int?>();

			SqlConnection connection = DatabaseConnection.GetConnection();
            bool wasOpen = connection.State == System.Data.ConnectionState.Open;
            if (!wasOpen) connection.Open();

            SqlCommand command = new SqlCommand("SELECT alb_id, alb_name, alb_img_id, alb_ar_id, alb_release_date, alb_type FROM albums", connection);
            command.Transaction = DatabaseConnection.GetTransaction();

            SqlDataReader reader = command.ExecuteReader();

            Album album;
            while (reader.Read())
            {
                album = new Album(reader.GetString(1));
                album.Id = reader.GetInt32(0);
				if (!reader.IsDBNull(2))
				{
					int? imgID = reader.GetInt32(2);
					imgIDs.AddLast(imgID);
				}
                if (!reader.IsDBNull(3)) artistIDs.AddLast(reader.GetInt32(3));
				album.ReleaseDate = reader.GetFieldValue<DateOnly>(4);
				album.Type = reader.GetString(5);
				albums.AddLast(album);
            }
            reader.Close();
			if (!wasOpen) connection.Close();

			var iconImageDAO = new IconImageDAO();
            var albumEnumerator = albums.GetEnumerator();
            foreach (int? imgID in imgIDs)
            {
                if (!albumEnumerator.MoveNext()) break;
                if (imgID == null) continue;
                albumEnumerator.Current.Image = iconImageDAO.GetByID((int)imgID);
            }

			var arstistDAO = new ArtistDAO();
			albumEnumerator = albums.GetEnumerator();
            foreach (int? artistID in artistIDs)
            {
                if (!albumEnumerator.MoveNext()) break;
                if (artistID == null) continue;
                albumEnumerator.Current.Artist = arstistDAO.GetByID((int)artistID);
            }


			return albums;
        }

		public IEnumerable<Album> GetRange(int beginKey = 0, int count = 10)
		{
			LinkedList<Album> albums = new LinkedList<Album>();
			LinkedList<int?> imgIDs = new LinkedList<int?>();
			LinkedList<int?> artistIDs = new LinkedList<int?>();

			SqlConnection connection = DatabaseConnection.GetConnection();
			bool wasOpen = connection.State == System.Data.ConnectionState.Open;
			if (!wasOpen) connection.Open();

			SqlCommand command = new SqlCommand("SELECT TOP (@count) alb_id, alb_name, alb_img_id, alb_ar_id, alb_release_date, alb_type FROM albums WHERE alb_id >= @beginKey ORDER BY alb_id ASC", connection);
			command.Transaction = DatabaseConnection.GetTransaction();
			command.Parameters.AddWithValue("beginKey", beginKey);
			command.Parameters.AddWithValue("count", count);

			SqlDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				var album = new Album(reader.GetString(1));
				album.Id = reader.GetInt32(0);
				if (!reader.IsDBNull(2))
				{
					int? imgID = reader.GetInt32(2);
					imgIDs.AddLast(imgID);
				}
				if (!reader.IsDBNull(3)) artistIDs.AddLast(reader.GetInt32(3));
				album.ReleaseDate = reader.GetFieldValue<DateOnly>(4);
				album.Type = reader.GetString(5);
				albums.AddLast(album);
			}
			reader.Close();

			if (!wasOpen) connection.Close();

			var iconImageDAO = new IconImageDAO();
			var albumEnumerator = albums.GetEnumerator();
			foreach (int? imgID in imgIDs)
			{
				if (!albumEnumerator.MoveNext()) break;
				if (imgID == null) continue;
				albumEnumerator.Current.Image = iconImageDAO.GetByID((int)imgID);
			}

			var artistDAO = new ArtistDAO();
			albumEnumerator = albums.GetEnumerator();
			foreach (int? artistID in artistIDs)
			{
				if (!albumEnumerator.MoveNext()) break;
				if (artistID == null) continue;
				albumEnumerator.Current.Artist = artistDAO.GetByID((int)artistID);
			}

			return albums;
		}

        /// <summary>
        /// Retrieves an album based on its ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>Album</returns>
        public Album? GetByID(int id)
        {
            SqlConnection connection = DatabaseConnection.GetConnection();
            bool wasOpen = connection.State == System.Data.ConnectionState.Open;
            if (!wasOpen) connection.Open();

            SqlCommand command = new SqlCommand("SELECT alb_id, alb_name, alb_img_id, alb_release_date, alb_type FROM albums WHERE alb_id = @id", connection);
            command.Transaction = DatabaseConnection.GetTransaction();
            command.Parameters.AddWithValue("id", id);

            SqlDataReader reader = command.ExecuteReader();
            if (!reader.Read()) 
            {
                reader.Close();
                if (!wasOpen) connection.Close();
                return null;
            }
            Album album = new Album(reader.GetString(1));
            album.Id = reader.GetInt32(0);
            int? imgID = reader.GetInt32(2);
            album.ReleaseDate = reader.GetFieldValue<DateOnly>(3);
            album.Type = reader.GetString(4);

            reader.Close();
            if (!wasOpen) connection.Close();

            if (imgID != null)
            {
                album.Image = new IconImageDAO().GetByID((int)imgID);
            }

            return album;
        }

        /// <summary>
        /// Retrieves the given artist's albums
        /// </summary>
        /// <param name="artistID">artist ID</param>
        /// <returns>artist's albums</returns>
        public List<Album> GetArtistAlbums(int artistID)
        {
            List<Album> albums = new List<Album>();
            LinkedList<int?> imageIDs = new LinkedList<int?>();

            ArtistDAO artistDAO = new ArtistDAO();
			Artist? artist = artistDAO.GetByID(artistID);

			SqlConnection connection = DatabaseConnection.GetConnection();

            bool wasOpen = connection.State == System.Data.ConnectionState.Open;
            if (!wasOpen) connection.Open();

            SqlCommand command = new SqlCommand("SELECT alb_id, alb_img_id, alb_name, alb_release_date, alb_type FROM albums WHERE alb_ar_id = @artist_id", connection);
            command.Transaction = DatabaseConnection.GetTransaction();
            command.Parameters.AddWithValue("artist_id", artistID);

            SqlDataReader reader = command.ExecuteReader();

            Album album;
            while (reader.Read())
            {
                album = new Album(reader.GetString(2));
                album.Id = reader.GetInt32(0);
                imageIDs.AddLast(reader[1] == DBNull.Value ? null : reader.GetInt32(1));
                album.Artist = artist;
				album.ReleaseDate = reader.GetFieldValue<DateOnly>(3);
				album.Type = reader.GetString(4);
				albums.Add(album);
            }
            reader.Close();

			if (!wasOpen) connection.Close();

			int i = 0;
            foreach (int? id in imageIDs)
            {
                if (id == null)
				{
					i++;
					continue;
				}

				albums[i].Image = new IconImageDAO().GetByID((int)id);
                i++;
            }

            return albums;
        }

        /// <summary>
        /// Removes the given album from the database
        /// </summary>
        /// <param name="id">album ID</param>
		public void Remove(int id)
		{
            SqlConnection connection = DatabaseConnection.GetConnection();
			bool wasOpen = connection.State == System.Data.ConnectionState.Open;
            if (!wasOpen) connection.Open();

            SqlCommand command = new SqlCommand("DELETE FROM albums WHERE alb_id = @id", connection);
            command.Transaction = DatabaseConnection.GetTransaction();
			command.Parameters.AddWithValue("@id", id);
			command.ExecuteNonQuery();

            if (!wasOpen) connection.Close();
		}

        /// <summary>
        /// Uploads the given album to the database
        /// </summary>
        /// <param name="data">album</param>
        /// <returns>new album ID</returns>
        public int? Upload(Album data)
        {
            if (data.Id != null)
            {
                Update(data);
                return data.Id;
            }
            if (data.Artist != null && data.Artist?.Id == null)
            {
                data.Artist!.Id = new ArtistDAO().Upload(data.Artist);
            }
			if (data.Image != null && data.Image.Id == null)
			{
				data.Image.Id = new IconImageDAO().Upload(data.Image);
			}

			SqlConnection connection = DatabaseConnection.GetConnection();
			bool wasOpen = connection.State == System.Data.ConnectionState.Open;
            if (!wasOpen) connection.Open();

			SqlCommand command = new SqlCommand("INSERT INTO albums (alb_img_id, alb_ar_id, alb_name, alb_release_date, alb_type) OUTPUT INSERTED.alb_id VALUES (@img_id, @ar_id, @name, @release_date, @type)", connection);
			command.Transaction = DatabaseConnection.GetTransaction();
			command.Parameters.AddWithValue("img_id", data.Image != null && data.Image.Id != null ? data.Image.Id : DBNull.Value);
			command.Parameters.AddWithValue("ar_id", data.Artist != null && data.Artist.Id != null ? data.Artist.Id : DBNull.Value);
			command.Parameters.AddWithValue("name", data.Name);
			command.Parameters.AddWithValue("release_date", data.ReleaseDate);
			command.Parameters.AddWithValue("type", data.Type);

			data.Id = (int?)command.ExecuteScalar();

			if (!wasOpen) connection.Close();

            for (int i = 0; i < data.Songs.Count; i++)
            {
                new SongDAO().Upload(data.Songs[i]);
                CreateSongConnectionRow((int)data.Id, (int)data.Songs[i].Id, i);
            }

			return data.Id;
		}
        
        /// <summary>
        /// Updates the album instance in the database
        /// </summary>
        /// <param name="data">album</param>
		public void Update(Album data)
		{
            if (data.Id == null) return;
            DeleteSongConnectionRows((int)data.Id);
			if (data.Image != null && data.Image.Id == null)
			{
				data.Image.Id = new IconImageDAO().Upload(data.Image);
			}

			SqlConnection connection = DatabaseConnection.GetConnection();
			bool wasOpen = connection.State == System.Data.ConnectionState.Open;
            if (!wasOpen) connection.Open();

			SqlCommand command = new SqlCommand("UPDATE albums SET alb_img_id = @img_id, alb_ar_id = @ar_id, alb_name = @name, alb_release_date = @release_date, alb_type = @type WHERE alb_id = @id", connection);
			command.Transaction = DatabaseConnection.GetTransaction();
			command.Parameters.AddWithValue("id", data.Id);
			command.Parameters.AddWithValue("img_id", data.Image != null && data.Image.Id != null ? data.Image.Id : DBNull.Value);
			command.Parameters.AddWithValue("ar_id", data.Artist != null && data.Artist.Id != null ? data.Artist.Id : DBNull.Value);
			command.Parameters.AddWithValue("name", data.Name);
			command.Parameters.AddWithValue("release_date", data.ReleaseDate);
			command.Parameters.AddWithValue("type", data.Type);

			command.ExecuteNonQuery();

			if (!wasOpen) connection.Close();

			for (int i = 0; i < data.Songs.Count; i++)
			{
				new SongDAO().Upload(data.Songs[i]);
				CreateSongConnectionRow((int)data.Id, (int)data.Songs[i].Id, i);
			}
		}

        /// <summary>
        /// Creates a connection in the linking table between the given album and the given song
        /// </summary>
        /// <param name="albumID">album ID</param>
        /// <param name="songID">song ID</param>
        /// <param name="order">song order</param>
		public void CreateSongConnectionRow(int albumID, int songID, int order)
        {
            SqlConnection connection = DatabaseConnection.GetConnection();
            bool wasOpen = connection.State == System.Data.ConnectionState.Open;
            if (!wasOpen) connection.Open();

            SqlCommand command = new SqlCommand("INSERT INTO album_songs (as_alb_id, as_so_id, as_order) VALUES (@albumID, @songID, @order)", connection);
            command.Transaction = DatabaseConnection.GetTransaction();
            command.Parameters.AddWithValue("albumID", albumID);
            command.Parameters.AddWithValue("songID", songID);
            command.Parameters.AddWithValue("order", order);
			command.ExecuteNonQuery();

			if (!wasOpen) connection.Close();
        }

        /// <summary>
        /// Deletes all album-song connections with the given albumID
        /// </summary>
        /// <param name="albumID"></param>
        public void DeleteSongConnectionRows(int albumID)
        {
            SqlConnection connection = DatabaseConnection.GetConnection();
            bool wasOpen = connection.State == System.Data.ConnectionState.Open;
            if (!wasOpen) connection.Open();

            SqlCommand command = new SqlCommand("DELETE FROM album_songs WHERE as_alb_id = @id", connection);
            command.Transaction = DatabaseConnection.GetTransaction();
            command.Parameters.AddWithValue("id", albumID);

            command.ExecuteNonQuery();
            if (!wasOpen) connection.Close();
        }

        public int GetCount()
        {
            SqlConnection connection = DatabaseConnection.GetConnection();
            bool wasOpen = connection.State == System.Data.ConnectionState.Open;
            if (!wasOpen) connection.Open();

            SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM albums", connection);
            command.Transaction = DatabaseConnection.GetTransaction();
            int count = (int)command.ExecuteScalar();

            if (!wasOpen) connection.Close();

            return count;
        }

        public (string, long)? GetMostPopularAlbum()
        {
            SqlConnection connection = DatabaseConnection.GetConnection();
            bool wasOpen = connection.State == System.Data.ConnectionState.Open;
            if (!wasOpen) connection.Open();

            SqlCommand command = new SqlCommand("SELECT alb_name, alb_listening_time FROM most_popular_album", connection);
            command.Transaction = DatabaseConnection.GetTransaction();
            SqlDataReader reader = command.ExecuteReader();

            (string, long)? result = null;
            if (reader.Read())
            {
                result = (reader.GetString(0), reader.GetInt32(1));
            }
            reader.Close();

            if (!wasOpen) connection.Close();

            return result;
        }

        public (string, long)? GetLeastPopularAlbum()
        {
            SqlConnection connection = DatabaseConnection.GetConnection();
            bool wasOpen = connection.State == System.Data.ConnectionState.Open;
            if (!wasOpen) connection.Open();

            SqlCommand command = new SqlCommand("SELECT alb_name, alb_listening_time FROM least_popular_album", connection);
            command.Transaction = DatabaseConnection.GetTransaction();
            SqlDataReader reader = command.ExecuteReader();

            (string, long)? result = null;
            if (reader.Read())
            {
                result = (reader.GetString(0), reader.GetInt32(1));
            }
            reader.Close();

            if (!wasOpen) connection.Close();

            return result;
        }

		public IEnumerable<Album> GetFeaturedAlbums()
		{
			LinkedList<Album> albums = new LinkedList<Album>();
			LinkedList<int?> imgIDs = new LinkedList<int?>();
			LinkedList<int?> artistIDs = new LinkedList<int?>();

			SqlConnection connection = DatabaseConnection.GetConnection();
			bool wasOpen = connection.State == System.Data.ConnectionState.Open;
			if (!wasOpen) connection.Open();

			SqlCommand command = new SqlCommand("SELECT alb_id, alb_name, alb_img_id, alb_ar_id, alb_release_date, alb_type FROM albums WHERE alb_featured = 1", connection);
			command.Transaction = DatabaseConnection.GetTransaction();

			SqlDataReader reader = command.ExecuteReader();

			Album album;
			while (reader.Read())
			{
				album = new Album(reader.GetString(1));
				album.Id = reader.GetInt32(0);
				if (!reader.IsDBNull(2))
				{
					int? imgID = reader.GetInt32(2);
					imgIDs.AddLast(imgID);
				}
				if (!reader.IsDBNull(3)) artistIDs.AddLast(reader.GetInt32(3));
				album.ReleaseDate = reader.GetFieldValue<DateOnly>(4);
				album.Type = reader.GetString(5);
				albums.AddLast(album);
			}
			reader.Close();
			if (!wasOpen) connection.Close();

			var iconImageDAO = new IconImageDAO();
			var albumEnumerator = albums.GetEnumerator();
			foreach (int? imgID in imgIDs)
			{
				if (!albumEnumerator.MoveNext()) break;
				if (imgID == null) continue;
				albumEnumerator.Current.Image = iconImageDAO.GetByID((int)imgID);
			}

			var arstistDAO = new ArtistDAO();
			albumEnumerator = albums.GetEnumerator();
			foreach (int? artistID in artistIDs)
			{
				if (!albumEnumerator.MoveNext()) break;
				if (artistID == null) continue;
				albumEnumerator.Current.Artist = arstistDAO.GetByID((int)artistID);
			}


			return albums;
		}
	}
}
