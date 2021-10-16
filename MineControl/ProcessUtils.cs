using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MineControl
{
    public static class ProcessUtils
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);

        /// <summary>
        /// Refreshes process info and returns running status
        /// </summary>
        /// <param name="process"></param>
        /// <returns> True if _process is running as a sub-process of current process </returns>
        public static bool IsProcessRunningFromObject(Process process)
        {
            try
            {
                process.Refresh();

                // may raise an exception if process is not running from the process object
                return !Process.GetProcessById(process.Id).HasExited;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Returns true if 1+ instances of a process with _processName exist
        /// </summary>
        /// <param name="_processName"></param>
        /// <returns> True if at least 1 instance of _processName is running on the system </returns>
        public static bool IsProcessRunningByName(string _processName)
        {
            Process[] procname = Process.GetProcessesByName(_processName);            
            return procname.Length > 0;            
        }

        /// <summary>
        /// Kills all instances of processes with _processName
        /// </summary>
        /// <param name="_processName"></param>
        /// <returns> True if all instances were killed successfully, false if no instances were found </returns>
        public static bool KillProcessInstancesByName(string _processName)
        {
            // kill any external instances (not ours)
            Process[] procs = Process.GetProcessesByName(_processName);
            foreach (Process proc in procs)
            {
                proc.Kill();
            }
            return procs.Length > 0;
        }
    }
}
