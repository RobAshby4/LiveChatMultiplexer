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
        private List<Message> newMessages = new List<Message>();
        public TwitchScraper(String url)
        {
            this.url = url;
            this.options = new ChromeOptions();
            // this.options.AddArgument("--headless=new");
            this.driver = new ChromeDriver(this.options);
        }

        public void InitChat()
        {
            this.driver.Url = this.url;
        }

        public List<Message> Poll(List<Message> previous)
        {
            List<Message> updates = new List<Message>();
            try 
            {
                var elems = driver.FindElements(By.ClassName("chat-line__message"));
                foreach (IWebElement elem in elems)
                {
                    try
                    {
                        IWebElement textElem = elem.FindElement(By.ClassName("text-fragment"));
                        String text = textElem.Text;
                        updates.Add(new Message("username", text));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                driver.ExecuteScript("document.querySelectorAll('.chat-line__message').forEach(function(element) {element.remove();})");
            }
            catch (Exception)
            {
                Console.WriteLine("broke ya");
                return updates;
            }
            return updates;
        }
        // figure out better way to make this work. nesting try/catch is icky

        public void Exit()
        {
            driver.Quit();
        }
    }

}
