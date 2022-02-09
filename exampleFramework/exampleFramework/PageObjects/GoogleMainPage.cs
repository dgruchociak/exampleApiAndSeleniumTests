using OpenQA.Selenium;

namespace exampleFramework.PageObjects
{
    public class GoogleMainPage : BasePage
    {
        public GoogleMainPage(IWebDriver driver) : base(driver){
        }

        private string pageTitle = "Google";
        public IWebElement acceptButton => wait.Until(e => driver.FindElement(By.CssSelector("#L2AGLb")));
        public IWebElement searchInput => wait.Until(e => driver.FindElement(By.CssSelector("input.gLFyf.gsfi")));

        public override void Open()
        {
            base.Open();
            acceptButton.Click();
            AssertTitle(pageTitle);
        }

        public GoogleSearchPage SearchFor(string searchText)
        {
            searchInput.SendKeys(searchText);
            searchInput.SendKeys(Keys.Enter);
            return new GoogleSearchPage(driver);
        }
    }
}
