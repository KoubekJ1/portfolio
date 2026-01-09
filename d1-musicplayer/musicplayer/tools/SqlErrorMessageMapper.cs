using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicplayer.tools
{
	public class SqlErrorMessageMapper
	{
		private static readonly Dictionary<int, string> _messages;

		private const string _default = "An unknown database-related error occured. Please verify the database configuration and try again.";

		static SqlErrorMessageMapper()
		{
			_messages = new Dictionary<int, string>();

			_messages.Add(SqlErrorNumberConstants.INVALID_CATALOG_NAME, "The initial catalog specified in the configuration file does not exist. Please verify the database configuration and try again.");
			_messages.Add(SqlErrorNumberConstants.CONNECTION_ERROR, "Unable to connect to the specified data source. Please check your network connection and verify the database configuration.");
			_messages.Add(SqlErrorNumberConstants.INVALID_OBJECT_NAME, "A database model inconsistency has occured. Please initialize the database with the provided SQL script and try again.");
		}

		public string GetMessage(int code)
		{
			return _messages.GetValueOrDefault(code, _default);
		}
	}
}
