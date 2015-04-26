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

        public bool WifiStateQuery()
        {
            string command = @"netsh wlan show hostednetwork";
            string request = ExecuteCmd(command).Replace(" ","");
            string result = request.Substring(request.LastIndexOf("状态:") + 3, 3);
            return result == "已启动";
        }

        private string ExecuteCmd(string command)
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

            cmdProcess.StandardInput.WriteLine(command);
            cmdProcess.StandardInput.WriteLine("exit");

            string result = cmdProcess.StandardOutput.ReadToEnd();
            cmdProcess.WaitForExit();
            return result;
        }
    }
}
