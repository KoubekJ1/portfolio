using Microsoft.Data.SqlClient;
using musicplayer.dataobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace musicplayer.dao
{

    /// <summary>
    /// DAO implementation used for working with Songs
    /// </summary>
    public class SongDAO : ISongDAO
    {

        /// <summary>
        /// Retrieves all songs from the database
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Song> GetAll()
        {
            LinkedList<Song> songs = new LinkedList<Song>();

            SqlConnection connection = DatabaseConnection.GetConnection();
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT so_id, so_sd_id, so_name, so_length, so_rating, so_ar_id FROM songs", connection);
            
            SqlDataReader reader = command.ExecuteReader();

            LinkedList<int?> artistIDs = new LinkedList<int?>();

            Song song;
            while (reader.Read())
            {
                song = new Song(reader.GetString(2));
                song.Id = reader.GetInt32(0);
                song.Length = reader.GetInt32(3);
				song.DataID = reader[1] != DBNull.Value ? reader.GetInt32(1) : null;
				song.Rating = Math.Round(reader.GetDouble(4), 1);
				artistIDs.AddLast(reader[5] != DBNull.Value ? reader.GetInt32(5) : null);
				songs.AddLast(song);
			}

            connection.Close();

			LinkedListNode<int?> currentArtistIdNode = artistIDs.First;

            var artistDao = new ArtistDAO();
			foreach (Song s in songs)
			{
				// Check if there is a valid Artist ID for this song
				if (currentArtistIdNode != null && currentArtistIdNode.Value.HasValue)
				{
					// CALL THE DAO: Assuming a method like GetArtist or GetById exists
					s.Artist = artistDao.GetByID(currentArtistIdNode.Value.Value);
				}

				// Move to the next artist ID in the parallel list
				if (currentArtistIdNode != null)
				{
					currentArtistIdNode = currentArtistIdNode.Next;
				}
			}

			return songs;
        }

        /// <summary>
        /// Retrieves a song based on the given ID
        /// </summary>
        /// <param name="id">song ID</param>
        /// <returns>Song</returns>
        public Song? GetByID(int id)
        {
            SqlConnection connection = DatabaseConnection.GetConnection();
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT so_id, so_sd_id, so_name, so_length, so_rating, so_ar_id FROM songs WHERE so_id = @id", connection);
            command.Parameters.AddWithValue("id", id);

            SqlDataReader reader = command.ExecuteReader();
            if (!reader.Read()) return null;
            Song song = new Song(reader.GetString(2));
            song.Id = reader.GetInt32(0);
			song.Length = reader.GetInt32(3);
			song.DataID = reader[1] != DBNull.Value ? reader.GetInt32(1) : null;
            song.Rating = Math.Round(reader.GetDouble(4), 1);
			int? artistID = reader[5] != DBNull.Value ? reader.GetInt32(5) : null;

			connection.Close();

            if (artistID != null) song.Artist = new ArtistDAO().GetByID((int)artistID);

            //song.Data = GetSongData(dataID);

            return song;
        }

        /// <summary>
        /// Returns all songs in the given album
        /// </summary>
        /// <param name="albumID">album</param>
        /// <returns>songs</returns>
        public List<Song> GetSongsFromAlbum(int albumID)
        {
            LinkedList<int> songIDs = new LinkedList<int>();

            SqlConnection connection = DatabaseConnection.GetConnection();
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT so_id FROM songs INNER JOIN album_songs ON as_so_id = so_id WHERE as_alb_id = @id ORDER BY as_order ASC", connection);
            command.Parameters.AddWithValue("id", albumID);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                songIDs.AddLast(reader.GetInt32(0));
			}

			connection.Close();

            List<Song> songs = new List<Song>(songIDs.Count);

            foreach (int i in songIDs)
            {
                Song? song = GetByID(i);
                if (song == null) continue;
                songs.Add(song);
            }

            return songs;
        }

        /// <summary>
        /// Removes the given instance from the database
        /// </summary>
        /// <param name="id">id</param>
        public void Remove(int id)
        {
            SqlConnection connection = DatabaseConnection.GetConnection();
            connection.Open();


			SqlCommand command1 = new SqlCommand("DELETE FROM song_data WHERE sd_id IN (SELECT so_sd_id FROM songs WHERE so_id = @id)", connection);
			command1.Parameters.AddWithValue("id", id);
			command1.ExecuteNonQuery();

            // Should not be necessary due to reference integrity (ON DELETE CASCADE)
			/*SqlCommand command = new SqlCommand("DELETE FROM songs WHERE so_id = @id", connection);
            command.Parameters.AddWithValue("id", id);

			command.ExecuteNonQuery();*/


            connection.Close();
        }

        /// <summary>
        /// Uploads the song into the database
        /// </summary>
        /// <param name="song">song</param>
        /// <returns>id</returns>
        public int? Upload(Song song)
        {
            if (song.Id != null)
            {
                Update(song);
                return song.Id;
            }
            if (song.Data == null) return null;
            int? dataID = UploadSongData(song.Data);
            song.DataID = dataID;
            if (dataID == null) return null;

            SqlConnection connection = DatabaseConnection.GetConnection();
            connection.Open();

            SqlCommand command = new SqlCommand("INSERT INTO songs (so_sd_id, so_name, so_length, so_rating, so_ar_id) OUTPUT INSERTED.so_id VALUES (@data_id, @name, @length, @rating, @artistID)", connection);
            command.Parameters.AddWithValue("data_id", dataID);
            //command.Parameters.AddWithValue("alb_id", song.AlbumID != null ? song.AlbumID : DBNull.Value);
            command.Parameters.AddWithValue("name", song.Name);
            command.Parameters.AddWithValue("length", song.Length);
            command.Parameters.AddWithValue("rating", song.Rating);
			command.Parameters.AddWithValue("artistID", song.Artist?.Id != null ? song.Artist.Id : DBNull.Value);

			int? id = (int?)command.ExecuteScalar();
            song.Id = id;

            connection.Close();

            return id;
        }

        /// <summary>
        /// Updates the song instance in the database
        /// </summary>
        /// <param name="song">song</param>
        public void Update(Song song)
        {
            if (song.Id == null) return;

			if (song.DataID == null && song.Data == null)
			{
				throw new Exception("Song data is null!");
			}

			if (song.DataID == null)
			{
				song.DataID = UploadSongData(song.Data);
			}

			SqlConnection connection = DatabaseConnection.GetConnection();
            connection.Open();            

            SqlCommand command = new SqlCommand("UPDATE songs SET so_sd_id = @sd_id, so_name = @name, so_length = @length, so_rating = @rating, so_ar_id = @artistID WHERE so_id = @id", connection);
            command.Parameters.AddWithValue("id", song.Id);
            command.Parameters.AddWithValue("sd_id", song.DataID);
			command.Parameters.AddWithValue("name", song.Name);
            command.Parameters.AddWithValue("length", song.Length);
			command.Parameters.AddWithValue("rating", song.Rating);
			command.Parameters.AddWithValue("artistID", song.Artist?.Id != null ? song.Artist.Id : DBNull.Value);

			command.ExecuteNonQuery();

			connection.Close();
        }

        /// <summary>
        /// Retrieves the song data for a given song
        /// </summary>
        /// <param name="id">song id</param>
        /// <returns>mp3 song data</returns>
        public byte[]? GetSongData(int id)
        {
            SqlConnection connection = DatabaseConnection.GetConnection();
            connection.Open();
            SqlCommand cmd = new SqlCommand("SELECT sd_data FROM song_data WHERE sd_id = @id", connection);
            cmd.Parameters.AddWithValue("id", id);

            SqlDataReader reader = cmd.ExecuteReader();

            // First discover byte amount to prevent allocating 2GB worth of empty space
            byte[]? data = null;
            if (!reader.Read()) return data;
            long bufferSize = reader.GetBytes(0, 0, data, 0, 2147483647);
            data = new byte[bufferSize];
            reader.GetBytes(0, 0, data, 0, (int)bufferSize);

            connection.Close();
            return data;
        }

        /// <summary>
        /// Uploads the song data into the database
        /// </summary>
        /// <param name="data">song data</param>
        /// <returns>data id</returns>
        public int? UploadSongData(byte[] data)
        {
            SqlConnection connection = DatabaseConnection.GetConnection();
            connection.Open();

            SqlCommand command = new SqlCommand("INSERT INTO song_data (sd_data) OUTPUT INSERTED.sd_id VALUES (@data)", connection);
            command.Parameters.AddWithValue("data", data);
            int? id = (int?)command.ExecuteScalar();

            connection.Close();
            return id;
        }

		public void AddListeningTime(long time, int songID)
		{
			SqlConnection connection = DatabaseConnection.GetConnection();
			connection.Open();

			SqlCommand command = new SqlCommand("UPDATE songs SET so_listening_time = so_listening_time + @time WHERE so_id = @id", connection);
			command.Parameters.AddWithValue("time", time);
			command.Parameters.AddWithValue("id", songID);
			command.ExecuteNonQuery();

			connection.Close();
		}
	}
}
