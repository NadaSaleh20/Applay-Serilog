using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsingSerilog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //we need to read the value we making using the serilog library
            //First We inilaze var to inilaize Configration(key,value) add value from Appsetting File
            //,Then Build it => this var have the value of serilog
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            //Then read the value from config and create logger
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(config).CreateLogger();

            try
            {
                //just statment to ensure that is enter the try
                Log.Information("Application Start");
                //then build the project and start ruuning it
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception  ex)
            {
                Log.Fatal(ex , "The Application Is Failed To Start !");
            }
            finally
            {
                Log.CloseAndFlush();
            }        

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
