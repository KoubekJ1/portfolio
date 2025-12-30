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
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT alb_id, alb_name, alb_img_id, alb_ar_id FROM albums", connection);

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
                albums.AddLast(album);
            }

            connection.Close();

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

        /// <summary>
        /// Retrieves an album based on its ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>Album</returns>
        public Album? GetByID(int id)
        {
            SqlConnection connection = DatabaseConnection.GetConnection();
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT alb_id, alb_name, alb_img_id FROM albums WHERE alb_id = @id", connection);
            command.Parameters.AddWithValue("id", id);

            SqlDataReader reader = command.ExecuteReader();
            if (!reader.Read()) return null;
            Album album = new Album(reader.GetString(1));
            album.Id = reader.GetInt32(0);
            int? imgID = reader.GetInt32(3);

            connection.Close();

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

            

            connection.Open();

            SqlCommand command = new SqlCommand("SELECT alb_id, alb_img_id, alb_name FROM albums WHERE alb_ar_id = @artist_id", connection);
            command.Parameters.AddWithValue("artist_id", artistID);

            SqlDataReader reader = command.ExecuteReader();

            Album album;
            while (reader.Read())
            {
                album = new Album(reader.GetString(2));
                album.Id = reader.GetInt32(0);
                imageIDs.AddLast(reader[1] == DBNull.Value ? null : reader.GetInt32(1));
                album.Artist = artist;
                albums.Add(album);
            }

			connection.Close();

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
			connection.Open();

            SqlCommand command = new SqlCommand("DELETE FROM albums WHERE alb_id = @id", connection);
			command.Parameters.AddWithValue("@id", id);
			command.ExecuteNonQuery();

            connection.Close();
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
			if (data.Image != null && data.Image.Id == null)
			{
				data.Image.Id = new IconImageDAO().Upload(data.Image);
			}

			SqlConnection connection = DatabaseConnection.GetConnection();
			connection.Open();

			SqlCommand command = new SqlCommand("INSERT INTO albums (alb_img_id, alb_ar_id, alb_name) OUTPUT INSERTED.alb_id VALUES (@img_id, @ar_id, @name)", connection);
			command.Parameters.AddWithValue("img_id", data.Image != null && data.Image.Id != null ? data.Image.Id : DBNull.Value);
			command.Parameters.AddWithValue("ar_id", data.Artist != null && data.Artist.Id != null ? data.Artist.Id : DBNull.Value);
			command.Parameters.AddWithValue("name", data.Name);

			data.Id = (int?)command.ExecuteScalar();

			connection.Close();

            for (int i = 0; i < data.Songs.Count; i++)
            {
                if (data.Songs[i].Id == null)
                {
                    new SongDAO().Upload(data.Songs[i]);
                }
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
			connection.Open();

			SqlCommand command = new SqlCommand("UPDATE albums SET alb_img_id = @img_id, alb_ar_id = @ar_id, alb_name = @name WHERE alb_id = @id", connection);
			command.Parameters.AddWithValue("id", data.Id);
			command.Parameters.AddWithValue("img_id", data.Image != null && data.Image.Id != null ? data.Image.Id : DBNull.Value);
			command.Parameters.AddWithValue("ar_id", data.Artist != null && data.Artist.Id != null ? data.Artist.Id : DBNull.Value);
			command.Parameters.AddWithValue("name", data.Name);

            command.ExecuteNonQuery();

			connection.Close();

			for (int i = 0; i < data.Songs.Count; i++)
			{
				if (data.Songs[i].Id == null)
				{
					new SongDAO().Upload(data.Songs[i]);
				}
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
            connection.Open();

            SqlCommand command = new SqlCommand("INSERT INTO album_songs (as_alb_id, as_so_id, as_order) VALUES (@albumID, @songID, @order)", connection);
            command.Parameters.AddWithValue("albumID", albumID);
            command.Parameters.AddWithValue("songID", songID);
            command.Parameters.AddWithValue("order", order);
			command.ExecuteNonQuery();

			connection.Close();
        }

        /// <summary>
        /// Deletes all album-song connections with the given albumID
        /// </summary>
        /// <param name="albumID"></param>
        public void DeleteSongConnectionRows(int albumID)
        {
            SqlConnection connection = DatabaseConnection.GetConnection();
            connection.Open();

            SqlCommand command = new SqlCommand("DELETE FROM album_songs WHERE as_alb_id = @id", connection);
            command.Parameters.AddWithValue("id", albumID);

            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
