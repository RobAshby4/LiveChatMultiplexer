using System.Collections;
using System.Threading;

namespace LiveChatMultiplexer
{

    enum Platform
    {
        YouTube,
        Twitch
    }

    internal class ChatThread
    {

        String chatURL;
        Platform chatPlatform;
        IChatMonitor chatMonitor;
        private static Mutex writeMut = new Mutex();
        private static List<Message> callbackList = new List<Message>();

        public ChatThread(Platform p, String url)
        {
            this.chatPlatform = p;
            this.chatURL = url;
            if (p == Platform.YouTube)
            {
                chatMonitor = new YouTubeScraper(url);
            }
            else
            {
                chatMonitor = new TwitchScraper(url);
            }
        }

        public static void setCallbackList(List<Message> callbackList)
        {
            writeMut.WaitOne();
            ChatThread.callbackList = callbackList;
            writeMut.ReleaseMutex();
        }

        public void runMonitor()
        {
            List<Message> updates = new List<Message>();
            chatMonitor.InitChat();
            Thread.Sleep(5000);
            updates = chatMonitor.Poll(updates);
            writeToCallback(updates);
            Thread.Sleep(1500);
            updates = chatMonitor.Poll(updates);
            writeToCallback(updates);
            Thread.Sleep(1500);
            updates = chatMonitor.Poll(updates);
            writeToCallback(updates);
            Thread.Sleep(1500);
            updates = chatMonitor.Poll(updates);
            writeToCallback(updates);
        }

        private void writeToCallback(List<Message> updates)
        {
            writeMut.WaitOne();
            Console.WriteLine("Writing to main");
            foreach (var c in updates)
            {
                ChatThread.callbackList.Add(c);
            }
            writeMut.ReleaseMutex();
        }
    }
}
