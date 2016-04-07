namespace CozyThunder.Botnet.Interface
{
    public interface ISlavePeerListener
    {
        void OnConnect(string host);
        void OnDisConnect();
        void OnMessage(byte[] msg);
    }
}
