using FluentScheduler;

namespace CozyLauncher.Plugin.BreakReminder
{
    public class TaskRegistry : Registry
    {
        public TaskRegistry()
        {
            Schedule<BreakTask>().ToRunEvery(1).Minutes();
        }
    }
}
