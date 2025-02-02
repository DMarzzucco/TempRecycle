
using System.Diagnostics;
using System.Security.Principal;

namespace TempRecycle.Auth
{
    public class AdminConfig
    {
        public static bool isRunningAsAdmin()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);

            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
        public static void RestarAsAdminstrator()
        {
            var processInfo = new ProcessStartInfo
            {
                FileName = System.Reflection.Assembly.GetExecutingAssembly().Location,
                Verb = "runas",
                UseShellExecute = true
            };
            Process.Start(processInfo);
            Environment.Exit(0);
        }
    }
}
