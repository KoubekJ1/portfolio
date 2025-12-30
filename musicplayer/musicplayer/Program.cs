using System;
using System.Collections.Generic;
using System.Linq;
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

			try
			{
				DatabaseConnection.GetConnection();
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message, "Unable to initialize connection to the database");
				return;
			}


            Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MusicPlayerWindow());
		}
	}
}
