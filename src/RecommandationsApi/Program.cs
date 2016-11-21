using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace RecommandationsApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .AddEnvironmentVariables(prefix: "ASPNETCORE_")
                .AddEnvironmentVariables(prefix: "SHOP_")
                .Build();

            string port = config["RECOMMANDATIONS_API_PORT"];

            if(string.IsNullOrEmpty(port))
            {
                throw new InvalidOperationException("SHOP_RECOMMANDATIONS_API_PORT configuration was not found.");
            }

            string url = $"http://0.0.0.0:{port}";
            
            var host = new WebHostBuilder()
                .UseConfiguration(config)
                .UseKestrel()
                .UseUrls(url)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
