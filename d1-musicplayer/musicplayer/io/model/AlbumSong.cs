using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicplayer.io.model
{
	public class AlbumSong
	{
		public int SongId { get; set; }
		public int AlbumId { get; set; }
		public int Order { get; set; }
	}
}
