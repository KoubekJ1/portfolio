using musicplayer.io.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicplayer.io
{
	public class DataCollection
	{
		public Dictionary<string, Artist> Artists { get; private set; }
		
		public IEnumerable<Album> Albums { get; private set; }

		public IEnumerable<Song> Songs { get; private set; }

		public DataCollection(IEnumerable<Album> albums, IEnumerable<Song> songs, Dictionary<string, Artist>? artists = null)
		{
			Albums = albums;
			Songs = songs;
			Artists = artists == null ? new Dictionary<string, Artist>() : artists;
		}
	}
}
