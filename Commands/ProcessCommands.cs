using CommandLine;

using System.Diagnostics;

namespace InjectionTool.Commands;

[Verb("processes", isDefault: true, HelpText = "Shows a list of running processes")]
public class ProcessCommands {

	[Option('v', "verbose")]
	public bool Verbose { get; set; } = false;

	[Option('n', "name")]
	public string Name { get; set; } = string.Empty;

	[Option('i', "id")]
	public int? ID { get; set; }

	internal int ShowProcesses() {
		// Really guys?
		// wdym? LGTM
		Process[] proc = 
			string.IsNullOrWhiteSpace(Name) && ID is null ? Process.GetProcesses() :
				ID is not null && string.IsNullOrEmpty(Name) ? new[] { Process.GetProcessById(Convert.ToInt32(ID)) } :
					!string.IsNullOrWhiteSpace(Name) && ID is null ? Process.GetProcessesByName(Name) :
						Process.GetProcessesByName(Name).Where(_ => _.Id == Convert.ToInt32(ID)).ToArray();

		foreach (Process p in proc) {
			Console.WriteLine($"[{p.Id}]: {p.ProcessName}");
		}

		return 0;
	}
}
