using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microphone.AspNet;
using Microphone.Consul;
using Serilog;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Net;
using System;
using System.Linq;
using System.Net.Sockets;

namespace aspnetcoreapp
{
    public class Startup
    {
        public Startup(IHostingEnvironment environment)
        { 
            Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .WriteTo.ColoredConsole()
                        .CreateLogger();
        }
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog(Log.Logger);
            var url = Program.Configuration["server.urls"].Split(';')[0];
            
            app.UseMvc()
                .UseMicrophone(Program.Configuration["SVC_NAME"], "1.0");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<IConfiguration>(_ => Program.Configuration);
            services.AddMicrophone<ConsulProvider>();

            services.Configure<ConsulOptions>(o => {
                o.Host = Program.Configuration["Env_ConsulHost"];
                o.Port = int.Parse(Program.Configuration["Env_ConsulPort"]);
            });
        }
    }

    class MyHealthChecker : Microphone.IHealthCheck
    {
        public Task CheckHealth()
        {
            return Task.FromResult(true);
        }
    }
}