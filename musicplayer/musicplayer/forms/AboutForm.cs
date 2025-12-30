using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace musicplayer.forms
{
	/// <summary>
	/// AboutForm serves as a window informing the user about contact information for the creator of the program (me)
	/// </summary>
	public partial class AboutForm : Form
	{
		/// <summary>
		/// Creates a new AboutForm instance
		/// </summary>
		public AboutForm()
		{
			InitializeComponent();
			this.FormBorderStyle = FormBorderStyle.FixedSingle;
		}

		private void AboutForm_Load(object sender, EventArgs e)
		{

		}
	}
}
