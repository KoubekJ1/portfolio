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
    /// DAO implementation that retrieves Artist objects from an SQL Server database handled by the DatabaseConnection singleton instance
    /// </summary>
    public class ArtistDAO : IArtistDAO
    {
        /// <summary>
        /// Retrieves all artists from the database
        /// </summary>
        /// <returns>all artists</returns>
        public IEnumerable<Artist> GetAll()
        {
            LinkedList<Artist> artists = new LinkedList<Artist>();
            LinkedList<int?> imageIDs = new LinkedList<int?>();

			SqlConnection connection = DatabaseConnection.GetConnection();
            bool wasOpen = connection.State == System.Data.ConnectionState.Open;
            if (!wasOpen) connection.Open();

            SqlCommand command = new SqlCommand("SELECT ar_id, ar_name, ar_img_id FROM artists", connection);
            command.Transaction = DatabaseConnection.GetTransaction();
            SqlDataReader reader = command.ExecuteReader();

            Artist artist;
            while (reader.Read())
            {
                artist = new Artist(reader.GetString(1));
                artist.Id = reader.GetInt32(0);
				imageIDs.AddLast(reader.IsDBNull(2) ? null : reader.GetInt32(2));
				artists.AddLast(artist);
            }
            reader.Close();

            var imageEnumerator = imageIDs.GetEnumerator();
			foreach (Artist artist1 in artists)
            {
                if (imageEnumerator.MoveNext() && imageEnumerator.Current != null)
				artist1.Image = new IconImageDAO().GetByID((int)imageEnumerator.Current);
			}

            if (!wasOpen) connection.Close();

            return artists;
        }

        /// <summary>
        /// Retrieves an artist based on his ID
        /// </summary>
        /// <param name="id">artist ID</param>
        /// <returns>artist</returns>
        public Artist? GetByID(int id)
        {
            SqlConnection connection = DatabaseConnection.GetConnection();
            bool wasOpen = connection.State == System.Data.ConnectionState.Open;
            if (!wasOpen) connection.Open();

            //SqlCommand command = new SqlCommand("SELECT ar_id, ar_name, ar_img_id FROM artists", connection);
            SqlCommand command = new SqlCommand("SELECT ar_id, ar_name, ar_img_id FROM artists WHERE ar_id = @id", connection);
            command.Transaction = DatabaseConnection.GetTransaction();
            command.Parameters.AddWithValue("id", id);

            SqlDataReader reader = command.ExecuteReader();

            if (!reader.Read()) 
            {
                reader.Close();
                if (!wasOpen) connection.Close();
                return null;
            }

            Artist artist = new Artist(reader.GetString(1));
            artist.Id = reader.GetInt32(0);
            int? imgID = null;
			if (!reader.IsDBNull(2)) imgID = reader.GetInt32(2);

            reader.Close();
            if (!wasOpen) connection.Close();

            if (imgID != null)
            {
                artist.Image = new IconImageDAO().GetByID((int)imgID);
            }

            return artist;
        }

        /// <summary>
        /// Removes an artist with the given ID from the database
        /// </summary>
        /// <param name="id">artist ID</param>
        public void Remove(int id)
        {
            SqlConnection connection = DatabaseConnection.GetConnection();
            bool wasOpen = connection.State == System.Data.ConnectionState.Open;
            if (!wasOpen) connection.Open();

            SqlCommand command = new SqlCommand("DELETE FROM artists WHERE ar_id = @id", connection);
            command.Transaction = DatabaseConnection.GetTransaction();
            command.Parameters.AddWithValue("id", id);
            command.ExecuteNonQuery();

            if (!wasOpen) connection.Close();
        }

        /// <summary>
        /// Uploads the given artist to the database
        /// </summary>
        /// <param name="artist">artist</param>
        /// <returns>new artist ID</returns>
        public int? Upload(Artist artist)
        {
            if (artist.Id != null)
            {
                Update(artist);
                return artist.Id;
            }

            if (artist.Image != null)
            {
                artist.Image.Id = new IconImageDAO().Upload(artist.Image);
            }

            SqlConnection connection = DatabaseConnection.GetConnection();
            bool wasOpen = connection.State == System.Data.ConnectionState.Open;
            if (!wasOpen) connection.Open();

            SqlCommand command = new SqlCommand("INSERT INTO artists (ar_name, ar_img_id) OUTPUT INSERTED.ar_id VALUES (@name, @img_id)", connection);
            command.Transaction = DatabaseConnection.GetTransaction();
            command.Parameters.AddWithValue("name", artist.Name);
            command.Parameters.AddWithValue("img_id", artist.Image != null ? artist.Image.Id : DBNull.Value);

            int id = (int)command.ExecuteScalar();

            if (!wasOpen) connection.Close();

            artist.Id = id;

            return id;
        }

        /// <summary>
        /// Updates the given artist instance in the database
        /// </summary>
        /// <param name="artist">artist</param>
        public void Update(Artist artist)
        {
            if (artist.Id == null) return;

            if (artist.Image?.Id == null)
            {
                artist.Image.Id = new IconImageDAO().Upload(artist.Image);
            }

            SqlConnection connection = DatabaseConnection.GetConnection();
            bool wasOpen = connection.State == System.Data.ConnectionState.Open;
            if (!wasOpen) connection.Open();

            SqlCommand command = new SqlCommand("UPDATE artists SET ar_name = @name, ar_img_id = @img_id WHERE ar_id = @id", connection);
            command.Transaction = DatabaseConnection.GetTransaction();
            command.Parameters.AddWithValue("id", artist.Id);
            command.Parameters.AddWithValue("name", artist.Name);
            command.Parameters.AddWithValue("img_id", artist.Image?.Id != null ? artist.Image.Id : DBNull.Value);

            command.ExecuteNonQuery();

			if (!wasOpen) connection.Close();
        }

		public IEnumerable<Artist> GetByName(string name, int count = 10)
		{
			LinkedList<Artist> artists = new LinkedList<Artist>();

			SqlConnection connection = DatabaseConnection.GetConnection();
			bool wasOpen = connection.State == System.Data.ConnectionState.Open;
            if (!wasOpen) connection.Open();

			SqlCommand command = new SqlCommand("SELECT TOP (@count) ar_id, ar_name, ar_img_id FROM artists WHERE ar_name LIKE @name + '%' ORDER BY ar_listening_time DESC", connection);
			command.Transaction = DatabaseConnection.GetTransaction();
            command.Parameters.AddWithValue("name", name);
            command.Parameters.AddWithValue("count", count);
			SqlDataReader reader = command.ExecuteReader();

			Artist artist;
			while (reader.Read())
			{
				artist = new Artist(reader.GetString(1));
				artist.Id = reader.GetInt32(0);
				if (!reader.IsDBNull(2)) artist.ImageID = reader.GetInt32(2);
				artists.AddLast(artist);
			}
            reader.Close();

			if (!wasOpen) connection.Close();

			return artists;
		}

		public IEnumerable<Artist> GetRange(int beginKey = 0, int count = 10)
		{
			LinkedList<Artist> artists = new LinkedList<Artist>();
			LinkedList<int?> imageIDs = new LinkedList<int?>();

			SqlConnection connection = DatabaseConnection.GetConnection();
			bool wasOpen = connection.State == System.Data.ConnectionState.Open;
			if (!wasOpen) connection.Open();

			SqlCommand command = new SqlCommand("SELECT TOP (@count) ar_id, ar_name, ar_img_id FROM artists WHERE ar_id >= @beginKey ORDER BY ar_id ASC", connection);
			command.Transaction = DatabaseConnection.GetTransaction();
			command.Parameters.AddWithValue("beginKey", beginKey);
			command.Parameters.AddWithValue("count", count);
			SqlDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				var artist = new Artist(reader.GetString(1));
				artist.Id = reader.GetInt32(0);
				imageIDs.AddLast(reader.IsDBNull(2) ? null : reader.GetInt32(2));
				artists.AddLast(artist);
			}
			reader.Close();

			var imageEnumerator = imageIDs.GetEnumerator();
			foreach (Artist a in artists)
			{
				if (imageEnumerator.MoveNext() && imageEnumerator.Current != null)
				{
					a.Image = new IconImageDAO().GetByID((int)imageEnumerator.Current);
				}
			}

			if (!wasOpen) connection.Close();

			return artists;
		}

        public int GetCount()
        {
            SqlConnection connection = DatabaseConnection.GetConnection();
            bool wasOpen = connection.State == System.Data.ConnectionState.Open;
            if (!wasOpen) connection.Open();

            SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM artists", connection);
            command.Transaction = DatabaseConnection.GetTransaction();
            int count = (int)command.ExecuteScalar();

            if (!wasOpen) connection.Close();

            return count;
        }

        public (string, long)? GetMostPopularArtist()
        {
            SqlConnection connection = DatabaseConnection.GetConnection();
            bool wasOpen = connection.State == System.Data.ConnectionState.Open;
            if (!wasOpen) connection.Open();

            SqlCommand command = new SqlCommand("SELECT ar_name, ar_listening_time FROM most_popular_artist", connection);
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

        public (string, long)? GetLeastPopularArtist()
        {
            SqlConnection connection = DatabaseConnection.GetConnection();
            bool wasOpen = connection.State == System.Data.ConnectionState.Open;
            if (!wasOpen) connection.Open();

            SqlCommand command = new SqlCommand("SELECT ar_name, ar_listening_time FROM least_popular_artist", connection);
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
	}
}
