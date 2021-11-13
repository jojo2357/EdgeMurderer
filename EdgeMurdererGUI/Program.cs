using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EdgeMurdererGUI
{
	static class Program
	{
		public static List<string> openedPrograms { get; private set; } = new List<string>();
		public static List<string> blacklistedPrograms { get; private set; } = new List<string>();

		private static EdgeMurdererMenu menu;

		public static bool changeMade = false;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			LoadSettings();
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			menu = new EdgeMurdererMenu();
			menu.initData();
			Thread t = new Thread(StartProgramListener);
			t.Start();

			Application.Run(menu);
			//menu.Disposed += (sender, args) => { Environment.Exit(0); };
		}

		private static void StartProgramListener()
		{
			Task programListener = ListenForPrograms();
			programListener.GetAwaiter().GetResult();
		}

		private static async Task ListenForPrograms()
		{
			WqlEventQuery query =
				new WqlEventQuery("__InstanceCreationEvent",
					new TimeSpan(0, 0, 2),
					"TargetInstance isa \"Win32_Process\"");

			ManagementEventWatcher watcher =
				new ManagementEventWatcher();
			watcher.Query = query;

			while (!menu.IsDisposed)
			{
				ManagementBaseObject e = watcher.WaitForNextEvent();

				string pName = (string)((ManagementBaseObject)e["TargetInstance"])["Name"];
				pName = pName.Replace(".exe", "");
				if (!openedPrograms.Contains(pName) && !blacklistedPrograms.Contains(pName))
				{
					openedPrograms.Add(pName);
					openedPrograms = openedPrograms.OrderBy(q => q).ToList();
					changeMade = true;
					menu.initData();
					menu.Invoke(menu._callInvoke);
				}
			}
		}

		public static void LoadSettings()
		{
			if (!File.Exists("blacklisted_programs.ini"))
			{
				blacklistedPrograms.Add("msedge");
			} else 
				foreach (string line in File.ReadAllLines("blacklisted_programs.ini"))
				{
					if (line != "")
						blacklistedPrograms.Add(line);
				}
		}

		public static void SaveSettings()
		{
			File.WriteAllLines("blacklisted_programs.ini", blacklistedPrograms);
		}
	}
}