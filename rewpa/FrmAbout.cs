using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace rewpa
{
	/// <summary>
	/// Form with information about the program.
	/// </summary>
	public partial class FrmAbout : Form
	{
		/// <summary>
		/// Creates new instance.
		/// </summary>
		public FrmAbout()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Closes form.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnGotcha_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// Shows message about how to get more help.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnHuh_Click(object sender, EventArgs e)
		{
			MessageBox.Show(this, "If you need help operating the program, create an issue on rewpa2's GitHub repository or contact exec.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		/// <summary>
		/// Opens the link in the control's Tag.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ImgLink_Click(object sender, EventArgs e)
		{
			if ((sender as Control)?.Tag is string link)
				Process.Start(link);
		}
	}
}
