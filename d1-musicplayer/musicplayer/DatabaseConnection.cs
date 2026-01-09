using Microsoft.Data.SqlClient;
using System.Configuration;

namespace musicplayer
{
	/// <summary>
	/// The class DatabaseConnection contains a static instance of SqlConnection used for accessing the database
	/// No actual instances of this class should be constructed
	/// </summary>
	public static class DatabaseConnection
	{
		private static SqlConnection connection;
		private static SqlTransaction? transaction;

		/// <summary>
		/// Returns the SqlConnection instance used by the program
		/// </summary>
		/// <returns>SqlConnection</returns>
		public static SqlConnection GetConnection()
		{
			if (connection == null) initializeConnection();
			//if (connection.State == System.Data.ConnectionState.Open) connection.Close();
			return connection;
		}

		public static SqlTransaction? GetTransaction()
		{
			return transaction;
		}

		public static void CreateTransaction()
		{
			if (connection == null) initializeConnection();
			if (connection.State != System.Data.ConnectionState.Open) throw new InvalidOperationException("Unable to create transaction when connection is closed!");
			transaction = connection.BeginTransaction();
		}

		public static void CommitTransaction()
		{
			transaction?.Commit();
			transaction?.Dispose();
			transaction = null;
		}

		public static void RollbackTransaction()
		{
			transaction?.Rollback();
			transaction?.Dispose();
			transaction = null;
		}

		/// <summary>
		/// Constructs and sets up the SqlConnection instance
		/// Automatically called by the getter if it hasn't been done yet
		/// </summary>
		private static void initializeConnection()
		{
			SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
			builder.DataSource = ConfigurationManager.AppSettings["DataSource"];
			builder.InitialCatalog = ConfigurationManager.AppSettings["InitialCatalog"];

			bool isIntegratedSecurity = ConfigurationManager.AppSettings["IntegratedSecurity"] == "true";
			if (isIntegratedSecurity)
			{
				builder.IntegratedSecurity = true;
			}
			else
			{
				builder.UserID = ConfigurationManager.AppSettings["UserID"];
				builder.Password = ConfigurationManager.AppSettings["Password"];
			}

			builder.TrustServerCertificate = true;
			builder.ConnectTimeout = 5;

			connection = new SqlConnection(builder.ConnectionString);
			connection.Open();
			connection.Close();
		}
	}
}
