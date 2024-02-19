using System.Diagnostics;

namespace PowerPlan.Services;

public class PowerManagementService
{
    public async Task MonitorAndAjustPowerPlansAsync()
    {
        while (true)
        {
            var currentPlan = GetCurrentPlan();
            Debug.WriteLine($"Current Power Plan: {currentPlan}");
            await Task.Delay(10000);
        }
    }
    private void SetPowerPlan(string planGuid)
    {
        ProcessStartInfo startInfo = new ProcessStartInfo("powercfg", $"/setactive {planGuid}")
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
            }
        }
    }

    private string GetCurrentPlan()
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
