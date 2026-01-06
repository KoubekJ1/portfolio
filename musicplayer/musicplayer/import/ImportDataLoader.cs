using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace musicplayer.import
{
	public class ImportDataLoader
	{
		public ImportDataLoader()
		{
		}

		public ImportData LoadFromJson(string json)
		{
			ImportData? data = JsonSerializer.Deserialize<ImportData>(json);
			if (data == null) throw new ArgumentException("Unable to convert JSON string to ImportData!");
			return data;
		}
	}
}
