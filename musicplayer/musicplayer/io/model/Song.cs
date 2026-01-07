using musicplayer.controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicplayer.io.model
{
    public class Song
    {
        public required string Name;
        public int? DataID;
        public int Length;
        public double Rating;

        public int? AlbumID;
		public int? ArtistID;
	}
}
