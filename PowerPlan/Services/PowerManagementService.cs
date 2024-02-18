using System.Diagnostics;

namespace PowerPlan.Services;

public class PowerManagementService
{
    public string GetCurrentPlan()
    {
        ProcessStartInfo startInfo = new ProcessStartInfo("powercfg", "/getactivescheme")
        {
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };

        using (Process process = Process.Start(startInfo))
        {
            if (process != null)
            {
                process.WaitForExit();
                string output = process.StandardOutput.ReadToEnd();
                return output;
            }
            return string.Empty;
        }
    }
}
