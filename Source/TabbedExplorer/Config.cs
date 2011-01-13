using System;
using System.Configuration;

namespace TabbedExplorer
{
	internal static class Config
	{
		internal static string DefaultExplorerURL
		{
			get { return ConfigurationManager.AppSettings["DefaultExplorerURL"] ?? Environment.GetEnvironmentVariable("USERPROFILE"); }
		}
	}
}
