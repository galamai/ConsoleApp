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

            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(configuration);

            // 0
            //services.Configure<RealOptions>(x =>
            //{
            //    x.Name = "TestName";
            //    x.Limit = 1;
            //});

            // 1
            services.Configure<RealOptions>(configuration.GetSection("Section1"));
            services.Configure<RealOptions2>(configuration.GetSection("Section2"));

            // 2
            //services.AddOptions<RealOptions>().Configure<IConfiguration>((x, cfg) => cfg.GetSection("Section1").Bind(x));
            //services.AddOptions<RealOptions2>().Configure<IConfiguration>((x, cfg) => cfg.GetSection("Section2").Bind(x));

            using (var provider = services.BuildServiceProvider())
            {
                var realOptions = provider.GetService<IOptionsSnapshot<RealOptions>>().Value;
                var realOptions2 = provider.GetService<IOptionsSnapshot<RealOptions2>>().Value;
            }
        }
    }

    class RealOptions
    {
        public string Name { get; set; }
        public int Limit { get; set; }
        public List<OptionItem> OptionItems { get; set; } = new List<OptionItem>();
    }

    class RealOptions2 : RealOptions
    {

    }

    public class OptionItem
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
