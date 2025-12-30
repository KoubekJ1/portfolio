using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicplayer
{
	/// <summary>
	/// Static class used for handling exceptions.
	/// If there is a debugger attached to the program instance, the user will be asked if they wish to throw the exception.
	/// </summary>
	public static class ErrorHandler
	{
		/// <summary>
		/// Handles the exception and prints its details to the user
		/// </summary>
		/// <param name="ex">exception</param>
		public static void HandleException(Exception ex)
		{
			HandleException(ex, ex.ToString(), ex.Message);
		}

		/// <summary>
		/// Handles the exception and prints its details as well as the custom title to the user
		/// </summary>
		/// <param name="ex">exception</param>
		/// <param name="title">window title</param>
		public static void HandleException(Exception ex, string title)
		{
			HandleException(ex, title, ex.Message);
		}

		/// <summary>
		/// Handles the exception and prints the given message to the user
		/// </summary>
		/// <param name="ex">exception</param>
		/// <param name="title">window title</param>
		/// <param name="text">message displayed</param>
		public static void HandleException(Exception ex, string title, string text)
		{
			if (System.Diagnostics.Debugger.IsAttached)
			{
				//throw ex;
				MessageBox.Show(text, "Error: " + title);
				if (MessageBox.Show(ex.Message + "\n" + ex.StackTrace + "\n\nDo you wish to throw this exception?", ex.ToString(), MessageBoxButtons.YesNo) == DialogResult.Yes) throw ex;
			}
			else
			{
				MessageBox.Show(text, "Error: " + title);
			}
		}
	}
}
