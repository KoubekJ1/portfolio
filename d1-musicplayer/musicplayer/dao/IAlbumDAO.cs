using musicplayer.dataobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicplayer.dao
{
	public interface IAlbumDAO : IDAO<Album>
	{
		List<Album> GetArtistAlbums(int artistID);
	}
}
