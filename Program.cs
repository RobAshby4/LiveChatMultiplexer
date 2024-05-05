using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
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
            for (int i = 0; i < 100; i++)
            {
                monitors.Add(new ChatMonitor(Platform.Twitch, "Thread " + i.ToString()));
            }
            ChatMonitor.setCallbackList(outList);

            // Give Each of them Threads and start them
            foreach (ChatMonitor m in monitors)
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

            // Console.WriteLine("Starting Multiplexer");

            // var options = new ChromeOptions();
            // options.AddArgument("--headless=new");
            // var driver = new ChromeDriver(options);

            // driver.Url = "https://www.google.com";
            // driver.FindElement(By.Name("q")).SendKeys("webdriver" + Keys.Return);
            // Console.WriteLine(driver.Title);

            // driver.Quit();
        }

    }

}

