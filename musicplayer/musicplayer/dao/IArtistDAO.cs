using Microsoft.Data.SqlClient;
using musicplayer.dataobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicplayer.dao
{
	public interface IArtistDAO : IDAO<Artist>
	{
		IEnumerable<Artist> GetByName(string name, int count = 10);
	}
}
