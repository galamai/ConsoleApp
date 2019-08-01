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

                if (IsJsonValue(value))
                {
                    var keyValues = JsonConfigurationValueParser.Parse(value);

                    foreach (var kv in keyValues)
                    {
                        Data.Add($"{key}:{kv.Key}", kv.Value);
                    }
                }
                else
                {
                    Data.Add(key, value);
                }
            }
        }

        private bool IsJsonValue(string value)
        {
            return (value.StartsWith("{") && value.EndsWith("}")) ||
                (value.StartsWith("[") && value.EndsWith("]"));
        }
    }
}
