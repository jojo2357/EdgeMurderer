using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Text.RegularExpressions;

namespace EdgeMurdererRunner
{
	internal class Program
	{
		//private static List<string> blacklistedPrograms = new List<string>(new[] { "msedge", "microsoft edge", "notepad", "microsoftedgeupdate" });
		private static List<string> blacklistedPrograms = new List<string>();

		private static long lastFileOpenTime = 0;
		private static string lastFileOpenArgs = "";

		private const long fileOpenDelay = 5000;

		public static void Main(string[] args)
		{
			LoadSettings();

			WqlEventQuery query =
				new WqlEventQuery("__InstanceCreationEvent",
					new TimeSpan(0, 0, 0, 0, 250),
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
							if (pName == "msedge.exe")
							{
								var argz = Regex.Replace(GetCommandLine(p), "^\\\".*msedge\\.exe\\\" ", "");
								Uri uriResult;
								if (Uri.TryCreate(argz, UriKind.Absolute, out uriResult)
								    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
								{
									if (CurrentTime() - lastFileOpenTime > fileOpenDelay || lastFileOpenArgs != argz)
									{
										Process.Start("chrome", argz);
										Console.WriteLine("\tRespawning in chrome: " + argz);
										lastFileOpenTime = CurrentTime();
										lastFileOpenArgs = argz;
									}
									else
									{
										Console.WriteLine("\tDeclining to respawn in chrome: " + argz + " (" + CurrentTime() + ", " + lastFileOpenTime + ") (" + lastFileOpenArgs + ")");
									}
								}
								else
								{
									argz = Regex.Replace(argz, "^--single-argument ", "");
									if (File.Exists(argz))
									{
										if (CurrentTime() - lastFileOpenTime > fileOpenDelay || lastFileOpenArgs != argz)
										{
											Process.Start("chrome", argz);
											Console.WriteLine("\tRespawning in chrome: " + argz);
											lastFileOpenTime = CurrentTime();
											lastFileOpenArgs = argz;
										}
										else
										{
											Console.WriteLine("\tDeclining to respawn in chrome: " + argz + " (" + CurrentTime() + ", " + lastFileOpenTime + ") (" + lastFileOpenArgs + ")");
										}
									}
								}
							}

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
			}
			else
				foreach (string line in File.ReadLines("blacklisted_programs.ini"))
					if (line != "")
						blacklistedPrograms.Add(line);

			Console.WriteLine("Blacklisted programs: ");
			foreach (string line in blacklistedPrograms)
				Console.WriteLine(line + ".exe");
			Console.WriteLine("======================");
		}

		private static string GetCommandLine(Process process)
		{
			using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT CommandLine FROM Win32_Process WHERE ProcessId = " + process.Id))
			using (ManagementObjectCollection objects = searcher.Get())
			{
				return objects.Cast<ManagementBaseObject>().SingleOrDefault()?["CommandLine"]?.ToString();
			}
		}

		private static long CurrentTime()
		{
			return DateTimeOffset.Now.ToUnixTimeMilliseconds();
		}
	}
}