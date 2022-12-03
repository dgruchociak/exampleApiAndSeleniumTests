using exampleFramework.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;

namespace exampleFramework.StepDefinitions
{
    [Binding]
    public class UITestsStepDefinitions
    {
        private IWebDriver driver;
        private ScenarioContext _scenarioContext;

        private GoogleMainPage googleMainPage => new GoogleMainPage(driver);
        private GoogleSearchPage googleSearchPage => new GoogleSearchPage(driver);


        public UITestsStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            driver = (IWebDriver)scenarioContext["driver"];
        }

        [Given(@"User opens the google")]
        public void GivenUserOpensTheGoogle()
        {
            googleMainPage.Open();
        }

        [When(@"User searches for ""(.*)"" phrase")]
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
                    string name = googleSearchPage.getTitle();
                    googleSearchPage.clickOnFirstProduct();
                    Assert.IsTrue(googleSearchPage.getNewWindowTitle() != name);
                    break;
                case "random":
                    name = googleSearchPage.getTitle();
                    googleSearchPage.clickOnRandomProduct();
                    Assert.IsTrue(googleSearchPage.getNewWindowTitle() != name);
                    break;
                default:
                    name = googleSearchPage.getTitle();
                    googleSearchPage.clickOnFirstProduct();
                    Assert.IsTrue(googleSearchPage.getNewWindowTitle() != name);
                    break;
            }
        }
    }
}