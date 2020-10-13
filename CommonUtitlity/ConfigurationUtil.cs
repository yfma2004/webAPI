using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.Options;

namespace CommonUtitlity
{
    public class ConfigurationUtil
    {
        private static IConfiguration configuration = null;

        static ConfigurationUtil()
        {
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true)
                .Build();
        }

        public static T GetSection<T>(string key) where T : class, new()
        {
            var obj = new ServiceCollection()
                .AddOptions()
                .Configure<T>(configuration.GetSection(key))
                .BuildServiceProvider()
                .GetService<IOptions<T>>()
                .Value;
            return obj;
        }

        public static string GetSection(string key)
        {
            return configuration.GetValue<string>(key);
        }

    }


}
