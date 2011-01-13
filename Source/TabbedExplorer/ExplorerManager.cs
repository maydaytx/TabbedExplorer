using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using SHDocVw;

namespace TabbedExplorer
{
	internal class ExplorerManager
	{
		private readonly ShellWindows shellWindows = new ShellWindows();
		private readonly object explorerLock = new object();
		private readonly List<Explorer> explorers = new List<Explorer>();

		private readonly IDictionary<string, Action<Explorer>> awaitingExplorers = new Dictionary<string, Action<Explorer>>();

		internal ExplorerManager()
		{
			shellWindows.WindowRegistered += ShellWindows_WindowRegistered;

			CheckExplorers();
		}

		private bool IsUrlOpen(string url)
		{
			bool isOpen = false;

			lock (explorerLock)
			{
				foreach (Explorer explorer in explorers)
					if (explorer.Url == url)
						isOpen = true;
			}

			return isOpen;
		}

		private void CheckExplorers()
		{
			lock (explorerLock)
			{
				foreach (InternetExplorer internetExplorer in shellWindows)
				{
					if (IsExplorer(internetExplorer) && !Contains(internetExplorer))
					{
						Explorer explorer = new Explorer(internetExplorer);

						explorer.Closed += explorer_Closed;

						explorers.Add(explorer);

						if (awaitingExplorers.ContainsKey(explorer.Url))
						{
							awaitingExplorers[explorer.Url](explorer);
							awaitingExplorers.Remove(explorer.Url);
						}
					}
				}
			}
		}

		private void explorer_Closed(Explorer sender)
		{
			lock (explorerLock)
			{
				explorers.Remove(sender);
			}
		}

		private void ShellWindows_WindowRegistered(int lCookie)
		{
			CheckExplorers();
		}

		private bool Contains(IWebBrowser2 internetExplorer)
		{
			return explorers.Any(explorer => internetExplorer.HWND == explorer.HWND);
		}

		private static bool IsExplorer(IWebBrowser2 explorer)
		{
			return Path.GetFileNameWithoutExtension(explorer.FullName).ToLower().Equals("explorer");
		}

		internal void Open(string url, Action<Explorer> callback)
		{
			lock (explorerLock)
			{
				if (!IsUrlOpen(url))
					awaitingExplorers.Add(url, callback);

				Process.Start(url);
			}
		}
	}
}
