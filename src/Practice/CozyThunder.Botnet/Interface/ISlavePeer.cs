namespace CozyThunder.Botnet.Interface
{
    public interface ISlavePeer
    {
        bool Start(string ip, int port, ISlavePeerListener listener);
        bool Stop();
    }
}
