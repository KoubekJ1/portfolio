using musicplayer.controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicplayer.dataobjects
{
    /// <summary>
    /// Song instances represent songs in a data model
    /// </summary>
    public class Song : IDataObject
    {
        private int? id;
        private string _name;
        private byte[]? _data;
        private int _length;

        private Album? _album;
        private int? _dataID;

        /// <summary>
        /// Constructs a new song with the given name
        /// </summary>
        /// <param name="name">song name</param>
        public Song(string name)
        {
            _name = name;
        }

        public int? GetID()
        {
            return id;
        }

        /// <summary>
        /// Plays the song using the AudioPlayerManager singleton instance
        /// </summary>
        /// <param name="replace">whether to replace an existing audiosource</param>
        /// <returns></returns>
        public bool PlaySong(bool replace)
        {
            //AudioPlayerManager.GetPlayerManager().Stop();
			//AudioPlayerManager.GetPlayerManager().PlayAudio(_data, replace);
			if (!AudioPlayerManager.GetPlayerManager().PlaySong(this, replace)) return false;

            return true;
        }

		public override string? ToString()
		{
            return _name;
		}

		public byte[]? Data { get => _data; set => _data = value; }
        public string Name { get => _name; set => _name = value; }
        public int? Id { get => id; set => id = value; }
        public int Length { get => _length; set => _length = value; }
		public int? DataID { get => _dataID; set => _dataID = value; }
		public Album? Album { get => _album; set => _album = value; }
	}
}
