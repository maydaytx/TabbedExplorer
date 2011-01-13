using System;
using System.Drawing;
using System.Windows.Forms;

namespace TabbedExplorer
{
	internal abstract class AppWrapperTabControl : TabControl
	{
		private static readonly object tabPagesLock = new object();

		internal event Action<IntPtr> TabRemoved;

		private TabPage draggingTab;
		private bool isLeftMouseButtonDown;

		internal bool IsWrapped(IntPtr child)
		{
			bool result;

			lock (tabPagesLock)
			{
				result = TabPages.ContainsKey(child.ToString());
			}

			return result;
		}

		internal void AddTabPage(IntPtr child, string text)
		{
			InvokeAction(() =>
			{
				lock (tabPagesLock)
				{
					AppWrapperTabPage tabPage = new AppWrapperTabPage(text, child);

					TabPages.Add(tabPage);
					SelectedTab = tabPage;
				}
			});
		}

		internal abstract void Close();

		internal void CloseTabPage(IntPtr child, bool closeApp)
		{
			InvokeAction(() => CloseTabPage((AppWrapperTabPage) TabPages[child.ToString()], closeApp));
		}

		private void CloseTabPage(AppWrapperTabPage tab, bool closeApp)
		{
			if (TabRemoved != null)
				TabRemoved(tab.Child);

			lock (tabPagesLock)
			{
				tab.Close(closeApp);

				TabPages.Remove(tab);
			}
		}

		internal void SetTabPageText(IntPtr child, string text)
		{
			InvokeAction(() =>
			{
				lock (tabPagesLock)
				{
					TabPages[child.ToString()].Text = text;
				}
			});
		}

		internal void FocusTabPage(IntPtr child)
		{
			InvokeAction(() =>
			{
				lock (tabPagesLock)
				{
					SelectedTab = TabPages[child.ToString()];
				}
			});
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			AppWrapperTabPage tab;

			lock (tabPagesLock)
			{
				tab = GetTabUnderMouse(e.Location);
			}

			if (e.Button == MouseButtons.Right)
			{
				ContextMenuStrip contextMenuStrip = new ContextMenuStrip();

				contextMenuStrip.Items.AddRange(new ToolStripItem[]
				{
					new ToolStripMenuItem("Close Tab", null, (o, eargs) => CloseTabPage(tab, true)),
					new ToolStripMenuItem("Popout Tab", null, (o, eargs) => CloseTabPage(tab, false)),
					new ToolStripMenuItem("Rename Tab", null, (o, eargs) => tab.Rename())
				});

				ContextMenuStrip = contextMenuStrip;
			}
			else if (e.Button == MouseButtons.Middle)
			{
				CloseTabPage(tab, true);
			}
			else if (e.Button == MouseButtons.Left)
			{
				draggingTab = tab;
				isLeftMouseButtonDown = true;
			}

			base.OnMouseDown(e);
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (isLeftMouseButtonDown)
			{
				lock (tabPagesLock)
				{
					TabPage tab = GetTabUnderMouse(e.Location);

					if (tab != null && draggingTab != null && tab != draggingTab)
					{
						int index = TabPages.IndexOf(tab);

						TabPages.Remove(draggingTab);
						TabPages.Insert(index, draggingTab);
						SelectedIndex = index;
					}
				}
			}

			base.OnMouseMove(e);
		}

		protected override void OnDoubleClick(EventArgs e)
		{
			lock (tabPagesLock)
			{
				((AppWrapperTabPage) SelectedTab).Rename();
			}

			base.OnDoubleClick(e);
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				isLeftMouseButtonDown = false;

			ContextMenu = null;
		}

		private AppWrapperTabPage GetTabUnderMouse(Point location)
		{
			WinAPI.TCHITTESTINFO hti = new WinAPI.TCHITTESTINFO(location.X, location.Y);

			int index = WinAPI.SendMessage(Handle, WinAPI.TCM_HITTEST, IntPtr.Zero, ref hti);

			if (index >= 0)
				return (AppWrapperTabPage) TabPages[index];
			else
				return null;
		}

		private void InvokeAction(Action action)
		{
			Invoke(action);
		}
	}
}
