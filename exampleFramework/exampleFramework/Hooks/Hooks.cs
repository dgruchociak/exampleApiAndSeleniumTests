using OpenQA.Selenium;
using exampleFramework.Drivers;
using exampleFramework.Support;

namespace exampleFramework.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        private IWebDriver driver;
        private ScenarioContext scenarioContext;
        private APIHelper apiHelper;

        public Hooks(ScenarioContext scenarioContext)
        {
            this.apiHelper = new APIHelper();
            scenarioContext = scenarioContext;
        }

        [BeforeScenario("UI")]
        public void BeforeScenarioUI()
        {
            driver = DriverFactory.ReturnDriver(DriverType.Chrome);
            scenarioContext["driver"] = driver;
        }

        [BeforeScenario("API")]
        public void BeforeScenarioAPI()
        {
            apiHelper.DeleteAllBoards();
        }

        [AfterScenario("UI")]
        public void AfterScenario()
        {
            driver.Close();
            driver.Dispose();
        }
    }
}