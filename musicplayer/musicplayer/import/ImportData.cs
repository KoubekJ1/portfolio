using musicplayer.dataobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicplayer.import
{
	public class ImportData
	{
		public Dictionary<string, Artist> Artists { get; private set; }
		
		public IEnumerable<Album> Albums { get; private set; }

		public IEnumerable<Song> Songs { get; private set; }

		public ImportData(IEnumerable<Album> albums, IEnumerable<Song> songs, Dictionary<string, Artist>? artists = null)
		{
			Albums = albums;
			Songs = songs;
			Artists = artists == null ? new Dictionary<string, Artist>() : artists;
		}
	}
}
