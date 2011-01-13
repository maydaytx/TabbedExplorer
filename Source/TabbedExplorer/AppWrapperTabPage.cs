using System;
using System.Windows.Forms;

namespace TabbedExplorer
{
	internal class AppWrapperTabPage : TabPage
	{
		private AppWrapper appWrapper;

		internal IntPtr Child
		{
			get { return appWrapper.Child; }
		}

		internal AppWrapperTabPage(string text, IntPtr child)
		{
			Text = text;
			Name = child.ToString();

			appWrapper = new AppWrapper(child);

			Controls.Add(appWrapper);
		}

		internal void Rename()
		{
			RenameTabForm form = new RenameTabForm();

			if (form.ShowDialog() == DialogResult.OK)
				Text = form.NewName;
		}

		internal void Close(bool closeApp)
		{
			if (appWrapper != null)
			{
				appWrapper.Close(closeApp);
				appWrapper = null;
			}
		}
	}
}