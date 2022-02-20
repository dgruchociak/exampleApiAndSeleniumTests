using NUnit.Framework;
using OpenQA.Selenium;

namespace exampleFramework.PageObjects
{
    public class GoogleSearchPage : BasePage
    {
        public GoogleSearchPage(IWebDriver driver) : base(driver){ }

        private IList<IWebElement> productsList => wait.Until(e => driver.FindElements(By.CssSelector("div.mnr-c.pla-unit")));

        public void clickOnFirstProduct()
        {
            productsList.ToList().FirstOrDefault().Click();
        }

        public void clickOnRandomProduct()
        {
            getRandomFromList(productsList).Click();
        }

        private IWebElement getRandomFromList(IList<IWebElement> list)
        {
            Random rnd = new Random();
            int ele = rnd.Next(list.Count);
            return list[ele];
        }

        public string getNewWindowTitle()
        {
            driver.SwitchTo().Window(driver.WindowHandles.Last());

            return driver.Title;
        }
    }
}
