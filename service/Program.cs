using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace aspnetcoreapp
{
    public class Program
    {
        public static IConfiguration Configuration {get;set;}
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();

            new WebHostBuilder()
                .UseKestrel()
                .UseConfiguration(Configuration)
                .UseStartup<Startup>()
                .Build()
                .Run();
        }
    }
}
