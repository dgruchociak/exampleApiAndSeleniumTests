using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace exampleFramework.PageObjects
{
    public abstract class BasePage
    {
        protected IWebDriver driver { get; }
        protected WebDriverWait wait;

        protected BasePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, new System.TimeSpan(0, 0, 0, 15, 0));
        }

        public IConfigurationRoot GetConfiguration()
        {
            var directory = Directory.GetCurrentDirectory().Replace("\\bin\\Debug\\net6.0", "");
            var settings = new ConfigurationBuilder()
                .AddJsonFile(directory + "\\appsettings.json")
                .Build();
            return settings;
        }

        public virtual void Open()
        {
            if (string.IsNullOrEmpty(GetConfiguration()["appSettings:BASE_URL"]))
            {
                throw new ArgumentException("The main URL cannot be null or empty.");
            }

            try
            {
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl(GetConfiguration()["appSettings:BASE_URL"]);
            }
            catch (NotImplementedException)
            {
            }
            catch (WebDriverException)
            {
            }
        }

        public void AssertTitle(string title)
        {
            string pageTitle = driver.Title;
            pageTitle.Should().Be(title);
        }
    }
}
