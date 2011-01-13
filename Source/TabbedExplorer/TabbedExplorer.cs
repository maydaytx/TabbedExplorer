using System;
using System.Windows.Forms;

namespace TabbedExplorer
{
	internal partial class TabbedExplorer : Form
	{
		internal TabbedExplorer()
		{
			InitializeComponent();

			Closing += (o, e) => explorerTabs.Close();
		}

		private void buttonAdd_Click(object sender, EventArgs e)
		{
			explorerTabs.OpenNewExplorerTab();
		}
	}
}
