using OpenQA.Selenium;

namespace exampleFramework.PageObjects
{
    public class GoogleMainPage : BasePage
    {
        public GoogleMainPage(IWebDriver driver) : base(driver){
        }

        private IWebElement acceptButton => wait.Until(e => driver.FindElement(By.CssSelector("#L2AGLb")));
        private IWebElement searchInput => wait.Until(e => driver.FindElement(By.CssSelector("input.gLFyf.gsfi")));

        public override void Open()
        {
            base.Open();
            acceptButton.Click();
        }

        public GoogleSearchPage SearchFor(string searchText)
        {
            searchInput.SendKeys(searchText);
            searchInput.SendKeys(Keys.Enter);
            return new GoogleSearchPage(driver);
        }
    }
}
