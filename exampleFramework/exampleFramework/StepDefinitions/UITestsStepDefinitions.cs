using exampleFramework.PageObjects;
using OpenQA.Selenium;

namespace exampleFramework.StepDefinitions
{
    [Binding]
    public class UITestsStepDefinitions
    {
        private IWebDriver driver;
        private GoogleMainPage googleMainPage => new GoogleMainPage(driver);
        private GoogleSearchPage googleSearchPage => new GoogleSearchPage(driver);


        public UITestsStepDefinitions()
        {
            driver = (IWebDriver)ScenarioContext.Current["driver"];
        }

        [Given(@"User opens the google")]
        public void GivenUserOpensTheGoogle()
        {
            googleMainPage.Open();
        }

        [When(@"User searches for '([^']*)' phrase")]
        public void WhenUserSearchesForPhrase(string text)
        {
            googleMainPage.SearchFor(text);
        }

        [Then(@"User opens the ""(.*)"" item")]
        public void ThenUserOpensTheItem(string itemName)
        {
            switch (itemName)
            {
                case "first":
                    googleSearchPage.clickOnFirstProduct();
                    break;
                case "random":
                    googleSearchPage.clickOnRandomProduct();
                    break;
                default:
                    googleSearchPage.clickOnFirstProduct();
                    break;
            }
            
        }

    }
}
