using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;

namespace ConsoleApp
{
    public class AppConfigurationProvider : ConfigurationProvider
    {
        public override void Load()
        {
            foreach (ConnectionStringSettings connectionString in ConfigurationManager.ConnectionStrings)
            {
                Data.Add($"ConnectionStrings:{connectionString.Name}", connectionString.ConnectionString);
            }

            var appSettings = ConfigurationManager.AppSettings;

            foreach (var settingsKey in appSettings.AllKeys)
            {
                var key = settingsKey.Replace(".", ":");
                var value = appSettings[settingsKey];

                Data.Add(key, value);
            }
        }
    }
}
