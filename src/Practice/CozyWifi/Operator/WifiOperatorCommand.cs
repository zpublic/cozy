using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;

namespace CozyWifi.Operator
{
    public class WifiOperatorCommand : IWifiOperator
    {
        public void StartWifi()
        {
            string command = @"netsh wlan start hostednetwork";
            ExecuteCmd(command);
        }

        public void StopWifi()
        {
            string command = @"netsh wlan stop hostednetwork";
            ExecuteCmd(command);
        }

        public void SetWifiProperty(string name, string password)
        {
            string command = String.Format("netsh wlan set hostednetwork ssid={0} key={1} mode=allow", name, password);
            ExecuteCmd(command);
        }

        private void ExecuteCmd(string command)
        {
            ProcessStartInfo cmdStartInfo = new ProcessStartInfo();

            cmdStartInfo.FileName = System.Environment.SystemDirectory + @"\cmd.exe";
            cmdStartInfo.RedirectStandardOutput = true;
            cmdStartInfo.RedirectStandardError = true;
            cmdStartInfo.RedirectStandardInput = true;
            cmdStartInfo.UseShellExecute = false;
            cmdStartInfo.CreateNoWindow = true;

            Process cmdProcess = new Process();
            cmdProcess.StartInfo = cmdStartInfo;
            cmdProcess.EnableRaisingEvents = true;
            cmdProcess.Start();
            cmdProcess.BeginOutputReadLine();
            cmdProcess.BeginErrorReadLine();

            cmdProcess.StandardInput.WriteLine(command);
            cmdProcess.StandardInput.WriteLine("exit");

            cmdProcess.WaitForExit();
        }
    }
}
