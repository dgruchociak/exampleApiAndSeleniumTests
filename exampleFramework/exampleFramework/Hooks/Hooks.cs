using OpenQA.Selenium;
using exampleFramework.Drivers;
using exampleFramework.Support;

namespace exampleFramework.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        private IWebDriver driver;
        private ScenarioContext _scenarioContext;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario("UI")]
        public void BeforeScenarioUI()
        {
            driver = DriverFactory.ReturnDriver(DriverType.Chrome);
            _scenarioContext["driver"] = driver;
        }

        [BeforeScenario("API")]
        public void BeforeScenarioAPI()
        {
            var api = new APIHelper();
            api.DeleteAllBoards();
        }

        [AfterScenario("UI")]
        public void AfterScenario()
        {
            driver.Close();
            driver.Dispose();
        }
    }
}