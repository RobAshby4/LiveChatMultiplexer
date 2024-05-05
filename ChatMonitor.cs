using System.Threading;

namespace LiveChatMultiplexer 
{

    enum Platform 
    {
        Youtube,
        Twitch
    }

    internal class ChatMonitor 
    {
        
        String chatURL;
        Platform chatPlatform;

        public ChatMonitor(Platform p, String url)
        {
            this.chatPlatform = p; 
            this.chatURL = url; 
        }
        // https://learn.microsoft.com/en-us/dotnet/standard/threading/creating-threads-and-passing-data-at-start-time
        // I need to implement callbacks for the chat monitor
    }
}
