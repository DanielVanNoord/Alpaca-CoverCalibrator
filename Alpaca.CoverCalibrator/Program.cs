using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ASCOM.Common;
using ASCOM.Common.DeviceInterfaces;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Alpaca.CoverCalibrator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Reset all stored settings if requested
            if(args?.Any(str => str.Contains("--reset")) ?? false)
            {
                Console.WriteLine("Reseting stored settings");
                Logging.Log.LogInformation("Reseting stored settings");
                Console.WriteLine("Reseting Server settings");
                AlpacaSettings.Reset();
                Console.WriteLine("Reseting Device settings");
                DriverManager.Reset();
                Console.WriteLine("Settings reset, shutting down");
                Logging.Log.LogInformation("Settings reset, shutting down");
                return;
            }

            //Command line to turn of Auth for password reset
            if (args?.Any(str => str.Contains("--reset-auth")) ?? false)
            {
                Console.WriteLine("Turning off Authentication to allow password reset.");
                AlpacaSettings.UseAuth = false;
                Console.WriteLine("You can change the password and then re-enable Authentication.");
            }

            //Already running, start the browser
            //This was working fine for .Net Core 3.1. Initial tests for .Net 5 show a change in how single file deployments work on Linux
            //This should probably be changed to a Mutex or another similar lock
            if (System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1)
            {
                Startup.StartBrowser(AlpacaSettings.ServerPort);
                return;
            }

            //Add the --urls argument for IHostBuilder, a user can set them from the arguments or they will be created here from settings
            if (!args?.Any(str => str.Contains("--urls")) ?? true)
            {
                if (args == null)
                {
                    args = new string[0];
                }

                Console.WriteLine("No startup url args detected, binding to saved server settings.");

                var temparray = new string[args.Length + 1];

                args.CopyTo(temparray, 0);

                string startupURLArg = "--urls=http://";

                if (AlpacaSettings.AllowRemoteAccess)
                {
                    startupURLArg += "*";
                }
                else
                {
                    startupURLArg += "localhost";
                }

                startupURLArg += ":" + AlpacaSettings.ServerPort;

                Console.WriteLine("Startup URL args: " + startupURLArg);
                Logging.Log.LogInformation("Startup URL args: " + startupURLArg);

                temparray[args.Length] = startupURLArg;

                args = temparray;
            }

            try
            {
                //Build the settings using startup. Block until closing
                CreateHostBuilder(args).Build().Run();
            }
            catch (OperationCanceledException)
            {
                //Server was shutdown, already logged out.
            }
            catch (Exception ex)
            {
                Logging.Log.LogError(ex.Message);
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
