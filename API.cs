using System.Runtime.InteropServices;

namespace InjectionTool;

internal class API {
	[DllImport("kernel32.dll")]
	internal static extern IntPtr OpenProcess(int dwAccess, bool bInheritHandle, int dwProcessId);

	[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
	internal static extern IntPtr GetModuleHandle(string lpModuleName);

	[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
	internal static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

	[DllImport("kernel32.dll", SetLastError = true)]
	internal static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddr, uint dwSize, uint fwAllocType, uint flProtect);

	[DllImport("kernel32.dll", SetLastError = true)]
	internal static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddr, byte[] lpBuffer, uint nSize, out UIntPtr lpnBytesWritten);

	[DllImport("kernel32.dll")]
	internal static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpAttributes, uint dwStackSize, IntPtr lpStartAddr, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadID);

	[DllImport("kernel32.dll", SetLastError = true)]
	internal static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPWStr)] string lpFileName);

	// privileges
	internal const int PROCESS_CREATE_THREAD = 0x0002;
	internal const int PROCESS_QUERY_INFORMATION = 0x0400;
	internal const int PROCESS_VM_OPERATION = 0x0008;
	internal const int PROCESS_VM_WRITE = 0x0020;
	internal const int PROCESS_VM_READ = 0x0010;

	// used for memory allocation
	internal const uint MEM_COMMIT = 0x00001000;
	internal const uint MEM_RESERVE = 0x00002000;
	internal const uint PAGE_READWRITE = 4;
}
