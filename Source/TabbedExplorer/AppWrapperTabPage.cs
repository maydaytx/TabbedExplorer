using System;
using System.Windows.Forms;

namespace TabbedExplorer
{
	internal class AppWrapperTabPage : TabPage
	{
		private readonly IntPtr child;
		private AppWrapper appWrapper;

		internal IntPtr Child
		{
			get { return child; }
		}

		internal AppWrapperTabPage(string text, IntPtr child)
		{
			this.child = child;
			appWrapper = new AppWrapper(child);

			Text = text;
			Name = child.ToString();

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