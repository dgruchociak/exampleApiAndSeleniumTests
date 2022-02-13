using NUnit.Framework;
using OpenQA.Selenium;

namespace exampleFramework.PageObjects
{
    public class GoogleSearchPage : BasePage
    {
        public GoogleSearchPage(IWebDriver driver) : base(driver){ }

        public IList<IWebElement> productsList => wait.Until(e => driver.FindElements(By.CssSelector("div.mnr-c.pla-unit")));

        public void clickOnFirstProduct()
        {
            string title = getWindowTitle();
            productsList.ToList().FirstOrDefault().Click();
            isNewWindowOpened(title);
        }

        public void clickOnRandomProduct()
        {
            string title = getWindowTitle();
            getRandomFromList(productsList).Click();
            isNewWindowOpened(title);
        }

        private IWebElement getRandomFromList(IList<IWebElement> list)
        {
            Random rnd = new Random();
            int ele = rnd.Next(list.Count);
            return list[ele];
        }
        
        //assert w Steps jako krok
        private void isNewWindowOpened(string windowTitle)
        {
            string newWindow = driver.CurrentWindowHandle;

            Assert.AreNotEqual(newWindow, windowTitle);
        }

        private string getWindowTitle()
        {
            return driver.Title;
        }
    }
}
