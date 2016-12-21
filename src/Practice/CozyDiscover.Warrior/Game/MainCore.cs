using System.Timers;

namespace CozyDiscover.Warrior.Game
{
    public enum UpdateType
    {
        playerInfo,
    }
    public delegate void OnUpdate(UpdateType type);

    public class MainCore
    {
        public static readonly MainCore Instance = new MainCore();

        event OnUpdate _update;
        bool _stop = false;

        void OnTimer(object source, ElapsedEventArgs e)
        {
            if (_stop == false)
            {
                PlayerInstance.Instance.Level++;
                _update(UpdateType.playerInfo);
            }
        }

        public void Start(OnUpdate handle)
        {
            _update = handle;
            Timer timerClock = new Timer();
            timerClock.Elapsed += new ElapsedEventHandler(OnTimer);
            timerClock.Interval = 1000;
            timerClock.Enabled = true;

        }

        public void Load()
        {

        }

        public void Save()
        {

        }
    }
}
