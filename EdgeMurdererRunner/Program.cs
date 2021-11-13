using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Management;

namespace EdgeMurdererRunner
{
	internal class Program
	{
		//private static List<string> blacklistedPrograms = new List<string>(new[] { "msedge", "microsoft edge", "notepad", "microsoftedgeupdate" });
		private static List<string> blacklistedPrograms = new List<string>();

		public static void Main(string[] args)
		{
			LoadSettings();

			WqlEventQuery query =
				new WqlEventQuery("__InstanceCreationEvent",
					new TimeSpan(0, 0, 2),
					"TargetInstance isa \"Win32_Process\"");

			ManagementEventWatcher watcher =
				new ManagementEventWatcher();
			watcher.Query = query;

			while (true)
			{
				ManagementBaseObject e = watcher.WaitForNextEvent();

				Console.Write(DateTime.Now + " | ");
				string pName = (string)((ManagementBaseObject)e["TargetInstance"])["Name"];
				if (blacklistedPrograms.Contains(pName.Replace(".exe", "")))
				{
					Console.WriteLine(pName + " needs to die");
					foreach (Process p in Process.GetProcessesByName(pName.Replace(".exe", "")))
					{
						try
						{
							p.Kill();
							Console.WriteLine("\tKilled " + p.ProcessName + " (ID: " + p.Id + ")");
						}
						catch (Exception)
						{
							Console.WriteLine("\tError killing " + p.ProcessName + " (ID: " + p.Id + ")");
						}
					}
				}
				else
				{
					Console.WriteLine(pName + " can live");
				}
			}
		}

		private static void LoadSettings()
		{
			if (!File.Exists("blacklisted_programs.ini"))
			{
				blacklistedPrograms.Add("msedge");
			}else 
				foreach (string line in File.ReadLines("blacklisted_programs.ini"))
					if (line != "")
						blacklistedPrograms.Add(line);
			Console.WriteLine("Blacklisted programs: ");
			foreach (string line in blacklistedPrograms)
				Console.WriteLine(line + ".exe");
			Console.WriteLine("======================");
		}
	}
}