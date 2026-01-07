using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace musicplayer.io.import
{
	public class ImportDataLoader
	{
		public ImportDataLoader()
		{
		}

		public DataCollection LoadFromJson(string json)
		{
			DataCollection? data = JsonSerializer.Deserialize<DataCollection>(json);
			if (data == null) throw new ArgumentException("Unable to convert JSON string to ImportData!");
			return data;
		}
	}
}
