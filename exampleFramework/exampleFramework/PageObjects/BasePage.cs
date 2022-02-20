using exampleFramework.Support;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace exampleFramework.PageObjects
{
    public abstract class BasePage
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;
        protected ConfigurationHelper configuration = new ConfigurationHelper();

        protected BasePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, new System.TimeSpan(0, 0, 0, 15, 0));
        }

        public virtual void Open()
        {
            if (string.IsNullOrEmpty(configuration.GetConfiguration()["appSettings:BASE_URL"]))
            {
                throw new ArgumentException("The main URL cannot be null or empty.");
            }

            try
            {
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl(configuration.GetConfiguration()["appSettings:BASE_URL"]);
            }
            catch (NotImplementedException)
            {
            }
            catch (WebDriverException)
            {
            }
        }

        public string getTitle()
        {
            return driver.Title;
        }
    }
}
