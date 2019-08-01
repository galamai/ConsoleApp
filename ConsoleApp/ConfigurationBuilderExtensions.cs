using Microsoft.Extensions.Configuration;
using System;

namespace ConsoleApp
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddAppConfiguration(this IConfigurationBuilder builder)
        {
            return builder.Add(new AppConfigurationSource());
        }
    }
}
