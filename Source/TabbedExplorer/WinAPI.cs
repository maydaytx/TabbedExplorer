using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace TabbedExplorer
{
	internal static class WinAPI
	{
		internal const uint WM_CLOSE = 0x0010;
		internal const int GWL_STYLE = -16;
		internal const int WS_VISIBLE = 0x10000000;

		internal static readonly IntPtr HWND_TOP = new IntPtr(0);
		internal const uint SWP_DRAWFRAME = 32;
		internal const uint SWP_SHOWWINDOW = 64;

		[DllImport("user32.dll", SetLastError = true)]
		internal static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

		[DllImport("user32.dll", SetLastError = true)]
		internal static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

		[DllImport("user32.dll", SetLastError = true)]
		internal static extern bool MoveWindow(IntPtr handle, int x, int y, int width, int height, bool redraw);

		[DllImport("user32.dll")]
		internal static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

		[DllImport("user32.dll", EntryPoint = "PostMessageA", SetLastError = true)]
		internal static extern bool PostMessage(IntPtr hwnd, uint Msg, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll", SetLastError = true)]
		internal static extern bool GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);

		[DllImport("user32.dll", SetLastError = false)]
		internal static extern IntPtr GetDesktopWindow();

		internal static IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex)
		{
			if (IntPtr.Size == 8) return GetWindowLongPtr64(hWnd, nIndex);
			else return new IntPtr(GetWindowLong32(hWnd, nIndex));
		}

		[DllImport("user32.dll", EntryPoint = "GetWindowLong")]
		internal static extern int GetWindowLong32(IntPtr hWnd, int nIndex);

		[DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
		internal static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, int nIndex);

		[StructLayout(LayoutKind.Sequential)]
		internal struct WINDOWINFO
		{
			internal uint cbSize;
			internal RECT rcWindow;
			internal RECT rcClient;
			internal uint dwStyle;
			internal uint dwExStyle;
			internal uint dwWindowStatus;
			internal uint cxWindowBorders;
			internal uint cyWindowBorders;
			internal ushort atomWindowType;
			internal ushort wCreatorVersion;

			public static WINDOWINFO Construct()
			{
				WINDOWINFO info = new WINDOWINFO();

				info.cbSize = (uint) Marshal.SizeOf(info);

				return info;
			}
		}

		[Serializable, StructLayout(LayoutKind.Sequential)]
		internal struct RECT
		{
			internal int Left;
			internal int Top;
			internal int Right;
			internal int Bottom;

			internal RECT(int left_, int top_, int right_, int bottom_)
			{
				Left = left_;
				Top = top_;
				Right = right_;
				Bottom = bottom_;
			}

			internal Size Size { get { return new Size(Right - Left, Bottom - Top); } }

			internal Point Location { get { return new Point(Left, Top); } }
		}

		[Flags]
		internal enum TCHITTESTFLAGS
		{
			TCHT_NOWHERE = 1,
			TCHT_ONITEMICON = 2,
			TCHT_ONITEMLABEL = 4,
			TCHT_ONITEM = TCHT_ONITEMICON | TCHT_ONITEMLABEL
		}

		internal const int TCM_HITTEST = 0x130D;

		[StructLayout(LayoutKind.Sequential)]
		internal struct TCHITTESTINFO
		{
			internal Point pt;
			internal TCHITTESTFLAGS flags;

			internal TCHITTESTINFO(int x, int y)
			{
				pt = new Point(x, y);
				flags = TCHITTESTFLAGS.TCHT_ONITEM;
			}
		}

		[DllImport("user32.dll")]
		internal static extern int SendMessage(IntPtr hwnd, int msg, IntPtr wParam, ref TCHITTESTINFO lParam);
	}
}
