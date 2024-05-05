using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Threading;

namespace LiveChatMultiplexer {

    internal class Program {

        public static void Main(String[] args)
        {
            Console.WriteLine("Starting Multiplexer");
            
            var options = new ChromeOptions();
            options.AddArgument("--headless=new");
            var driver = new ChromeDriver(options);

            driver.Url = "https://www.google.com";
            driver.FindElement(By.Name("q")).SendKeys("webdriver" + Keys.Return);
            Console.WriteLine(driver.Title);

            driver.Quit();
        }

    }

}

