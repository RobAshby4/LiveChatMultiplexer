using System.Threading;
using System.Collections;

namespace LiveChatMultiplexer
{

    internal class Program
    {

        public static void Main(String[] args)
        {
            ArrayList outList = new ArrayList();
            ArrayList monitors = new ArrayList();
            ArrayList threads = new ArrayList();

            // generate all of the ChatMonitors
            for (int i = 0; i < 1; i++)
            {
                monitors.Add(new ChatThread(Platform.Twitch, args[0]));
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
            foreach (String output in outList)
            {
                Console.WriteLine(output);
            }

            Console.WriteLine(outList.Count);
        }

    }

}

