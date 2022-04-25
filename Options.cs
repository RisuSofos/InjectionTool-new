using CommandLine;

namespace InjectionTool;

public class Options {
	[Option('v', "verbose", HelpText = "extra output")]
	public bool Verbose { get; set; } = false;
}
