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
		public Dictionary<int, Artist> Artists { get; set; } = new Dictionary<int, Artist>();
		
		public Dictionary<int, Album> Albums { get; set; } = new Dictionary<int, Album>();

		public Dictionary<int, Song> Songs { get; set; } = new Dictionary<int, Song>();

		public LinkedList<AlbumSong> AlbumSongs { get; set; } = new LinkedList<AlbumSong>();

		public DataCollection()
		{
		}
	}
}