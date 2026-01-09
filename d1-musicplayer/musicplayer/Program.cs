using Microsoft.Data.SqlClient;
using musicplayer.io.export;
using musicplayer.io.model;
using musicplayer.tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace musicplayer
{
	internal static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			// TEST

			//var data = new ExportDataRetriever().GetExport();

			/*var artist = new Artist()
			{
				Name = "test",
				ImageID = 2
			};
			var json = JsonSerializer.Serialize(artist);*/

			// ENDTEST

			try
			{
				DatabaseConnection.GetConnection();
			}
			catch (SqlException e)
			{
				var mapper = new SqlErrorMessageMapper();
				MessageBox.Show(mapper.GetMessage(e.Number), "Unable to initialize connection to the database");
				return;
			}
			catch (Exception e)
			{
				MessageBox.Show("An unknown database-related error occured. Please verify the database configuration and try again.", "Unable to initialize connection to the database");
				return;
			}

			PlaybackLengthHandler handler = new PlaybackLengthHandler();
			handler.Register();

            Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MusicPlayerWindow());
		}
	}
}
