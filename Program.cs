using InjectionTool.Commands;
using CommandLine;

Parser.Default.ParseArguments<InjectionCommands, ProcessCommands>(args)
	.MapResult(
		(InjectionCommands _) => _.Inject(),
		(ProcessCommands _) => _.ShowProcesses(),
		errs => 1
	);
;
