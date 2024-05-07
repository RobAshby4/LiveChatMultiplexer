using System.Threading;
using System.Collections;

namespace LiveChatMultiplexer
{

    internal class Program
    {

        public static void Main(String[] args)
        {
            List<Message> outList = new List<Message>();
            List<ChatThread> monitors = new List<ChatThread>();
            List<Thread> threads = new List<Thread>();

            // generate all of the ChatMonitors
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Contains("twitch.tv"))
                {
                    monitors.Add(new ChatThread(Platform.Twitch, args[i]));
                }
                else if (args[i].Contains("Youtube.com"))
                {
                    monitors.Add(new ChatThread(Platform.YouTube, args[i]));
                }
                else
                {
                    Console.WriteLine("platform not supported: " + args[i]);
                }
            }
            ChatThread.setCallbackList(outList);

            // Give Each of them Threads and start them
            foreach (ChatThread m in monitors)
            {
                Thread newThread = new Thread(new ThreadStart(m.runMonitor));
                threads.Add(newThread);
                newThread.Start();
            }

            // Join all threads
            foreach (Thread t in threads)
            {
                t.Join();
            }

            // print the callback list
            foreach (Message output in outList)
            {
                Console.WriteLine(output.contents);
            }

            Console.WriteLine(outList.Count);
        }

    }

}
