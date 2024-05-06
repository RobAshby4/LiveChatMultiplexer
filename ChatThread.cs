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
        private static ArrayList callbackList = new ArrayList();

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

        public static void setCallbackList(ArrayList callbackList)
        {
            writeMut.WaitOne();
            ChatThread.callbackList = callbackList;
            writeMut.ReleaseMutex();
        }

        public void runMonitor()
        {
            ArrayList updates = new ArrayList();
            chatMonitor.InitChat();
            Thread.Sleep(1000);
            chatMonitor.Poll(updates);
            writeToCallback(updates);
            updates = new ArrayList();
            Thread.Sleep(1000);
            chatMonitor.Poll(updates);
            writeToCallback(updates);
            updates = new ArrayList();
            Thread.Sleep(1000);
            chatMonitor.Poll(updates);
            writeToCallback(updates);
        }

        private void writeToCallback(ArrayList updates)
        {
            writeMut.WaitOne();
            foreach (var c in updates)
            {
                ChatThread.callbackList.Add(c);
            }
            writeMut.ReleaseMutex();
        }
    }
}
