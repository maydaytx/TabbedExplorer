using System;
using System.Windows.Forms;

namespace TabbedExplorer
{
	internal class AppWrapper : UserControl
	{
		private IntPtr child;
		private IntPtr originalState;
		private WinAPI.WINDOWINFO info;
		private bool closed;

		internal IntPtr Child
		{
			get { return child; }
		}

		internal AppWrapper(IntPtr child)
		{
			Dock = DockStyle.Fill;

			info = WinAPI.WINDOWINFO.Construct();

			SetChild(child);
		}

		private void SetChild(IntPtr child)
		{
			WinAPI.GetWindowInfo(child, ref info);

			this.child = child;

			WinAPI.SetParent(this.child, Handle);

			//remove border
			originalState = WinAPI.GetWindowLongPtr(this.child, WinAPI.GWL_STYLE);
			WinAPI.SetWindowLong(this.child, WinAPI.GWL_STYLE, WinAPI.WS_VISIBLE);

			SetSizeForOverlay();
		}

		private void SetSizeForOverlay()
		{
			WinAPI.MoveWindow(child, 0, 0, Width, Height, true);
		}

		protected override void OnResize(EventArgs e)
		{
			SetSizeForOverlay();
		}

		internal void Close(bool closeApp)
		{
			if (!closed)
			{
				if (closeApp)
				{
					//post close message to the window
					WinAPI.PostMessage(child, WinAPI.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
				}
				else
				{
					//put back the state
					WinAPI.SetWindowLong(child, WinAPI.GWL_STYLE, originalState.ToInt32());

					//set back to the desktop
					WinAPI.SetParent(child, WinAPI.GetDesktopWindow());
				}

				WinAPI.SetWindowPos(child, WinAPI.HWND_TOP, info.rcWindow.Location.X, info.rcWindow.Location.Y, info.rcWindow.Size.Width, info.rcWindow.Size.Height, WinAPI.SWP_DRAWFRAME | WinAPI.SWP_SHOWWINDOW);
			}

			closed = true;
		}
	}
}
