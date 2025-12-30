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
    /// Data object representing an album
    /// </summary>
    public class Album : IDataObject
    {
        private int? id;
        private string _name;
        private IconImage? _image;
        private Artist? _artist;

        private List<Song> _songs;

        /// <summary>
        /// Constructs a new album with the given name
        /// </summary>
        /// <param name="name">album name</param>
        public Album(string name)
        {
            _name = name;
            _songs = new List<Song>();
        }

        /// <summary>
        /// Constructs a new album with the given name and image
        /// </summary>
        /// <param name="name">album name</param>
        /// <param name="_image">album image</param>
        public Album(string name, IconImage _image)
        {
            _name = name;
            this._image = _image;
            _songs = new List<Song>();
        }

        public int? Id { get => id; set => id = value; }
        public IconImage? Image { get => _image; set => _image = value; }
		public string Name { get => _name; set => _name = value; }
		public List<Song> Songs { get => _songs; set => _songs = value; }
		public Artist? Artist { get => _artist; set => _artist = value; }

		public int? GetID()
        {
            return id;
        }
    }
}
