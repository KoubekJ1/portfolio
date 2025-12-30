using musicplayer.dataobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicplayer.dao
{
	public interface ISongDAO : IDAO<Album>
	{
		int? UploadSongData(byte[] data);
	}
}
