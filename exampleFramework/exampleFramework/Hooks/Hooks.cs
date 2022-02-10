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
        private APIHelper apiHelper;

        public Hooks(ScenarioContext scenarioContext)
        {
            this.apiHelper = new APIHelper();
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