using musicplayer.dao;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicplayer.dataobjects
{
    /// <summary>
    /// Data class representing an artist in the database
    /// </summary>
    public class Artist : IDataObject
    {
        private int? _id;
        private string _name;
        private int? _imageID;
        private IconImage? _image;
        
        private List<Album> _albums;

        /// <summary>
        /// Constructs a new artist with the given name
        /// </summary>
        /// <param name="name">artist name</param>
        public Artist(string name)
        {
			_name = name;
			_albums = new List<Album>();
        }

        /// <summary>
        /// Constructs a new artist with the given name and image
        /// </summary>
        /// <param name="name">artist name</param>
        /// <param name="image">artist image</param>
        public Artist(string name, IconImage image)
        {
			_name = name;
			_image = image;
			_albums = new List<Album>();
        }

        public int? Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public IconImage? Image { get => _image; set => _image = value; }
		public int? ImageID { get => _imageID; set => _imageID = value; }
		public List<Album> Albums { get => _albums; }

		public int? GetID()
        {
            return _id;
        }

        /// <summary>
        /// Loads the artist's albums from the database
        /// </summary>
        public void LoadAlbums()
        {
			if (_id == null) return;
            _albums = new AlbumDAO().GetArtistAlbums((int)_id);
        }

		public override string? ToString()
		{
            return _name;
		}
	}
}
