using FluentScheduler;

namespace CozyLauncher.Plugin.InfoCollect
{
    public class TaskRegistry : Registry
    {
        public TaskRegistry()
        {
            Schedule<ReportActiveTask>().ToRunNow().AndEvery(60).Minutes();
        }
    }
}
