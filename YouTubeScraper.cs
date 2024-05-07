using System.Collections;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace LiveChatMultiplexer 
{
    public class YouTubeScraper : IChatMonitor
    {
        private String url;
        private ChromeOptions options;
        private ChromeDriver driver;
        private List<Message> newMessages = new List<Message>();
        public YouTubeScraper(String url) 
        {
            this.url = url;
            this.options = new ChromeOptions();
            this.options.AddArgument("--headless=new");
            this.driver = new ChromeDriver(this.options);
        }

        public void InitChat()
        {
            this.driver.Url = this.url;
        }

        public void Poll(List<Message> updates)
        {
        }

        public void exit()
        {
            driver.Quit();
        }

    }
}
