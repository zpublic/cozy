namespace CozyLauncher.Plugin.BreakReminder
{
    public class TaskConfig
    {
        private TaskConfig() { }
        public static readonly TaskConfig Instance = new TaskConfig();

        public volatile int WorkMinutes = 0;
        public volatile int IntervalMinutes = 60;
        public volatile int BreakMinutes = 5;
    }
}
