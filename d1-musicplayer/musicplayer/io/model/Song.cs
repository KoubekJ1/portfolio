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
        public required string Name { get; set; }
        public int? DataID { get; set; }
		public int Length { get; set; }
        public double Rating { get; set; }

		public int? ArtistID { get; set; }
	}
}
