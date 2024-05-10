using System.Collections;

namespace LiveChatMultiplexer 
{
    class TwitchChatReader : IChatMonitor
    {
        public TwitchChatReader()
        {
            // Initialize the Twitch Chat Reader
        }

        public void InitChat() 
        {
            throw new NotImplementedException();
        }

        public List<Message> Poll(List<Message> previous)
        {
            throw new NotImplementedException();
        }
    
        public void Exit()
        {
            throw new NotImplementedException();
        }
    }
}
