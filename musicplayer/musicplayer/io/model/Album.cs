using musicplayer.dao;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicplayer.io.model
{
	public class Album
	{
		public int Id { get; set; }

		public required string Name { get; set; }

		public int? ImageId { get; set; }
		public int ArtistID { get; set; }

		public DateOnly ReleaseDate { get; set; }

		public required string Type { get; set; }
	}
}
