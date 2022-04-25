using CommandLine;

using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace InjectionTool.Commands;

[Verb("inject", HelpText = "Inject library into running process")]
public class InjectionCommands {
	[Option('v', "verbose")]
	public bool Verbose { get; set; }

	[Option('l', "lib", HelpText = "Path to library", Required = true)]
	public string Library { get; set; } = string.Empty;

	[Option('p', "pid", HelpText = "Process ID to attach to", Required = true)]
	public int ProcessID { get; set; }

	[Option('n', "name", HelpText = "The name of created process")]
	public string Name { get; set; } = string.Empty;


	public int Inject() {
		string procName = string.IsNullOrWhiteSpace(Name) ? Path.GetFileNameWithoutExtension(Library) : Name;
		Process proc = Process.GetProcessById(ProcessID);
		string path = Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "DLL", "test.dll"));

		IntPtr hProcess = API.OpenProcess(API.PROCESS_CREATE_THREAD | API.PROCESS_QUERY_INFORMATION | API.PROCESS_VM_OPERATION | API.PROCESS_VM_WRITE | API.PROCESS_VM_READ, false, proc.Id);
		IntPtr allocMemAddr = API.VirtualAllocEx(hProcess, IntPtr.Zero, (uint)((procName.Length + 1) * Marshal.SizeOf(typeof(char))), API.MEM_COMMIT | API.MEM_RESERVE, API.PAGE_READWRITE);
		API.WriteProcessMemory(hProcess, allocMemAddr, Encoding.Default.GetBytes(path), (uint)((procName.Length + 1) * Marshal.SizeOf(typeof(char))), out UIntPtr nBytesWritten);
		API.CreateRemoteThread(hProcess, IntPtr.Zero, 0, API.LoadLibrary(path), allocMemAddr, 0, IntPtr.Zero);
		return 0;
	}
}
