using Microsoft.Extensions.Configuration;

namespace ConsoleApp
{
    public class AppConfigurationSource : IConfigurationSource
    {
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new AppConfigurationProvider();
        }
    }
}
