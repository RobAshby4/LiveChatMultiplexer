using System.Collections;
using System.Threading;

namespace LiveChatMultiplexer
{

    enum Platform
    {
        YouTube,
        Twitch
    }

    internal class ChatMonitor
    {

        String chatURL;
        Platform chatPlatform;
        private static Mutex writeMut = new Mutex();
        private static ArrayList callbackList = new ArrayList();

        public ChatMonitor(Platform p, String url)
        {
            this.chatPlatform = p;
            this.chatURL = url;
        }

        public static void setCallbackList(ArrayList callbackList)
        {
            writeMut.WaitOne();
            ChatMonitor.callbackList = callbackList;
            writeMut.ReleaseMutex();
        }

        public void runMonitor()
        {
            ArrayList updateList = new ArrayList();
            Random rnd = new Random();
            Thread.Sleep(rnd.Next(1, 1000));
            updateList.Add(this.chatURL);
            writeToCallback(updateList);
        }

        private void writeToCallback(ArrayList updates)
        {
            writeMut.WaitOne();
            foreach (var c in updates) {
                ChatMonitor.callbackList.Add(c);
            }
            writeMut.ReleaseMutex();
        }

        // https://learn.microsoft.com/en-us/dotnet/standard/threading/creating-threads-and-passing-data-at-start-time
        // I need to implement callbacks for the chat monitor
    }
}
