using System.Collections;

namespace LiveChatMultiplexer
{
    public interface IChatMonitor
    {
        public void InitChat();
        public List<Message> Poll(List<Message> previous);
        public void Exit();
    }
}
