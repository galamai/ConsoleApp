using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddAppConfiguration()
                .AddJsonFile("appsetting.json")
                .Build();

            foreach (var kv in configuration.AsEnumerable())
            {
                Console.WriteLine($"{kv.Key}\t{kv.Value}");
            }

            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(configuration);

            // 0
            //services.Configure<RealOptions>(x =>
            //{
            //    x.Name = "TestName";
            //    x.Limit = 1;
            //});

            // 1
            services.Configure<RealOptionsAppConfig>(configuration.GetSection("Section1"));
            services.Configure<RealOptionsAppConfig2>(configuration.GetSection("Section2"));
            services.Configure<RealOptionsAppSettings>(configuration.GetSection("Section3"));
            services.Configure<RealOptionsAppSettings2>(configuration.GetSection("Section4"));

            // 2
            //services.AddOptions<RealOptionsAppConfig>().Configure<IConfiguration>((x, cfg) => cfg.GetSection("Section1").Bind(x));
            //services.AddOptions<RealOptionsAppConfig2>().Configure<IConfiguration>((x, cfg) => cfg.GetSection("Section2").Bind(x));
            //services.AddOptions<RealOptionsAppSettings>().Configure<IConfiguration>((x, cfg) => cfg.GetSection("Section3").Bind(x));
            //services.AddOptions<RealOptionsAppSettings>().Configure<IConfiguration>((x, cfg) => cfg.GetSection("Section4").Bind(x));

            using (var provider = services.BuildServiceProvider())
            {
                var realOptionsAppConfig = provider.GetService<IOptionsSnapshot<RealOptionsAppConfig>>().Value;
                var realOptionsAppConfig2 = provider.GetService<IOptionsSnapshot<RealOptionsAppConfig2>>().Value;

                var realOptionsAppSettings = provider.GetService<IOptionsSnapshot<RealOptionsAppSettings>>().Value;
                var realOptionsAppSettings2 = provider.GetService<IOptionsSnapshot<RealOptionsAppSettings2>>().Value;
            }

            Console.WriteLine("Press enter to exit...");
            Console.ReadLine();
        }
    }

    class RealOptionsAppConfig
    {
        public string Name { get; set; }
        public int Limit { get; set; }
        public List<OptionItem> OptionItems { get; set; } = new List<OptionItem>();
    }

    class RealOptionsAppConfig2
    {
        public string Name { get; set; }
        public int Limit { get; set; }
        public string OptionItems { get; set; }
    }

    class RealOptionsAppSettings : RealOptionsAppConfig
    {

    }

    class RealOptionsAppSettings2 : RealOptionsAppConfig2
    {

    }

    public class OptionItem
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
