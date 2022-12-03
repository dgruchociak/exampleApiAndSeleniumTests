using Microsoft.Extensions.Configuration;

namespace exampleFramework.Support
{
    public class ConfigurationHelper
    {
        public ConfigurationHelper(){}

        public IConfigurationRoot GetConfiguration()
        {
            var directory = Directory.GetCurrentDirectory().Replace("\\bin\\Debug\\net6.0", "");
            var settings = new ConfigurationBuilder()
                .AddJsonFile(directory + "\\appsettings.json")
                .Build();
            return settings;
        }
    }
}