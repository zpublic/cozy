using FluentScheduler;
using System.Windows.Forms;

namespace CozyLauncher.Plugin.BreakReminder
{
    public class BreakTask : ITask
    {
        public void Execute()
        {
            TaskConfig.Instance.WorkMinutes++;
            if (TaskConfig.Instance.WorkMinutes >= TaskConfig.Instance.IntervalMinutes)
            {
                TaskConfig.Instance.WorkMinutes = 0;
                //MessageBox.Show("休息" + TaskConfig.Instance.BreakMinutes + "分钟");
            }
        }
    }
}
