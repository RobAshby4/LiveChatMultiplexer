using System.Collections;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace LiveChatMultiplexer
{
    public class TwitchScraper : IChatMonitor
    {
        private String url;
        private ChromeOptions options;
        private ChromeDriver driver;
        private ArrayList newMessages = new ArrayList();
        public TwitchScraper(String url)
        {
            this.url = url;
            this.options = new ChromeOptions();
            //  this.options.AddArgument("--headless=new");
            this.driver = new ChromeDriver(this.options);
        }

        public void InitChat()
        {
            this.driver.Url = this.url;
        }

        public void Poll(ArrayList updates)
        {
            var elems = driver.FindElements(By.ClassName("chat-line__message"));
            foreach (IWebElement elem in elems)
            {
                String text = "";
                try
                {
                    IWebElement textElem = elem.FindElement(By.ClassName("text-fragment"));
                    text = textElem.Text;
                }
                catch (Exception)
                {
                    continue;
                }
                updates.Add(text);
            }
        }

        public void exit()
        {
            driver.Quit();
        }
    }

}