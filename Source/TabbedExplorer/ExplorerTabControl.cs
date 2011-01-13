using System;
using System.Collections.Generic;
using System.Linq;

namespace TabbedExplorer
{
	internal class ExplorerTabControl : AppWrapperTabControl
	{
		private readonly ExplorerManager explorerManager = new ExplorerManager();
		private readonly IDictionary<IntPtr, Explorer> explorers = new Dictionary<IntPtr, Explorer>();

		internal ExplorerTabControl()
		{
			TabRemoved += RemoveExplorer;
		}

		internal void OpenNewExplorerTab()
		{
			foreach (var explorer in explorers)
			{
				if (explorer.Value.Url == Config.DefaultExplorerURL)
				{
					FocusTabPage(explorer.Key);

					return;
				}
			}

			explorerManager.Open(Config.DefaultExplorerURL, AddExplorer);
		}

		internal override void Close()
		{
			foreach (IntPtr explorer in explorers.Keys.ToArray())
				CloseTabPage(explorer, true);
		}

		private void AddExplorer(Explorer explorer)
		{
			if (explorer == null)
				return;

			explorer.UrlChanged += explorer_UrlChanged;
			explorer.Closed += explorer_Closed;

			explorers.Add(explorer.MainWindowHandle, explorer);
			AddTabPage(explorer.MainWindowHandle, explorer.Url);
		}

		private void RemoveExplorer(IntPtr child)
		{
			explorers[child].UrlChanged -= explorer_UrlChanged;
			explorers[child].Closed -= explorer_Closed;
			explorers.Remove(child);
		}

		private void explorer_UrlChanged(Explorer explorer)
		{
			SetTabPageText(explorer.MainWindowHandle, explorer.Url);
		}

		private void explorer_Closed(Explorer explorer)
		{
			CloseTabPage(explorer.MainWindowHandle, false);
		}
	}
}
