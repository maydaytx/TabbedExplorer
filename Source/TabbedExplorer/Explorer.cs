using System;
using SHDocVw;

namespace TabbedExplorer
{
	internal class Explorer
	{
		private readonly int hwnd;
		private readonly InternetExplorer explorer;
		private string url;

		internal string Url
		{
			get { return url; }
		}

		internal int HWND
		{
			get { return hwnd; }
		}

		internal IntPtr MainWindowHandle
		{
			get { return new IntPtr(HWND); }
		}

		internal event Action<Explorer> UrlChanged;
		internal event Action<Explorer> Closed;

		internal Explorer(InternetExplorer explorer)
		{
			this.explorer = explorer;
			hwnd = explorer.HWND;

			SetUrl();

			explorer.OnQuit += Explorer_OnQuit;
			explorer.NavigateComplete2 += Explorer_NavigateComplete2;
		}

		private void Explorer_NavigateComplete2(object pDisp, ref object URL)
		{
			SetUrl();

			if (UrlChanged != null)
				UrlChanged(this);
		}

		private void Explorer_OnQuit()
		{
			if (Closed != null)
				Closed(this);
		}

		private void SetUrl()
		{
			Uri uri;

			url = Uri.TryCreate(explorer.LocationURL, UriKind.Absolute, out uri)
				? uri.LocalPath
				: explorer.LocationName;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
				return false;
			else if (ReferenceEquals(this, obj))
				return true;
			else if (obj.GetType() != typeof (Explorer))
				return false;
			else
				return hwnd == ((Explorer) obj).hwnd;
		}

		public override int GetHashCode()
		{
			return hwnd;
		}
	}
}
