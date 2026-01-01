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
    /// DAO implementation that operates on IconImage instances in an SQL database defined by the DatabaseConnection singleton
    /// </summary>
    public class IconImageDAO : IDAO<IconImage>
    {

        /// <summary>
        /// Retrieves all IconImages in the database
        /// </summary>
        /// <returns>All IconImages</returns>
        public IEnumerable<IconImage> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves an IconImage based on its ID
        /// </summary>
        /// <param name="id">IconImage ID</param>
        /// <returns>IconImage</returns>
        public IconImage? GetByID(int id)
        {
            SqlConnection connection = DatabaseConnection.GetConnection();
            bool wasOpen = connection.State == System.Data.ConnectionState.Open;
            if (!wasOpen) connection.Open();

            SqlCommand command = new SqlCommand("SELECT img_data FROM image_data WHERE img_id = @id", connection);
            command.Parameters.AddWithValue("id", id);

            SqlDataReader reader = command.ExecuteReader();
            if (!reader.Read()) 
            {
                reader.Close();
                if (!wasOpen) connection.Close();
                return null;
            }

            byte[] buffer = null;
            long byteAmount = reader.GetBytes(0, 0, buffer, 0, 8000);
            buffer = new byte[byteAmount];
            reader.GetBytes(0, 0, buffer, 0, (int)byteAmount);
            MemoryStream stream = new MemoryStream(buffer);
            Bitmap bitmap = new Bitmap(stream);

            IconImage image = new IconImage(bitmap, id);

            reader.Close();
            if (!wasOpen) connection.Close();

            return image;
        }

        /// <summary>
        /// Removes an IconImage from the database
        /// </summary>
        /// <param name="id">IconImage ID</param>
        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Uploads the IconImage into the database
        /// </summary>
        /// <param name="data">IconImage</param>
        /// <returns>IconImage ID</returns>
        public int? Upload(IconImage data)
        {
            SqlConnection connection = DatabaseConnection.GetConnection();
            bool wasOpen = connection.State == System.Data.ConnectionState.Open;
            if (!wasOpen) connection.Open();

            SqlCommand command = new SqlCommand("INSERT INTO image_data (img_data) OUTPUT INSERTED.img_id VALUES (@data)", connection);
            MemoryStream stream = new MemoryStream();
            data.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            command.Parameters.AddWithValue("data", stream.ToArray());

            int? id = (int?)command.ExecuteScalar();
            data.Id = id;

            if (!wasOpen) connection.Close();
            stream.Close();

			return id;
        }
    }
}
