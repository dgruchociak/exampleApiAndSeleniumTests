using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace exampleFramework.Drivers
{
    public static class DriverFactory
    {
        public static IWebDriver ReturnDriver(DriverType driverType)
        {
            IWebDriver driver;
            switch (driverType)
            {
                case DriverType.Chrome:
                    driver = new ChromeDriver();
                    break;
                case DriverType.Firefox:
                    driver = new FirefoxDriver();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(driverType), driverType, null);
            }
            return driver;
        }
    }
}