using System.Collections;

namespace LiveChatMultiplexer
{
    public interface IChatMonitor 
    {
        public void InitChat();
        public void Poll(ArrayList updates);
    }
}