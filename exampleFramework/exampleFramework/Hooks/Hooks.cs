using OpenQA.Selenium;
using exampleFramework.Drivers;

namespace exampleFramework.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        private IWebDriver driver;

        [BeforeScenario]
        public void BeforeScenario()
        {
            driver = DriverFactory.ReturnDriver(DriverType.Chrome);
            ScenarioContext.Current["driver"] = driver;
        }

        [AfterScenario]
        public void AfterScenario()
        {
            driver.Close();
            driver.Dispose();
        }
    }
}