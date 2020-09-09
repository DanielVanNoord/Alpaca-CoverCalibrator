using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting.Server.Features;
using System.Net;

namespace Alpaca.CoverCalibrator
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        internal static IHostApplicationLifetime Lifetime
        {
            get;
            private set;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            //services.AddSingleton<MenuRefreshService>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            int port = ServerSettings.ServerPort;

            try
            {
                var serverAddressesFeature = app.ServerFeatures.Get<IServerAddressesFeature>();

                if (serverAddressesFeature.Addresses.Count > 0)
                {
                    var serverAddress = serverAddressesFeature.Addresses.First();
                    bool localHostOnly = false;
                    bool ipv6 = false;


                    if(Uri.TryCreate(serverAddress, UriKind.RelativeOrAbsolute, out Uri serverUri))
                    {
                        try
                        {
                            port = serverUri.Port;
                            if (serverUri.Host.ToLowerInvariant().Contains("localhost") || IPAddress.IsLoopback(IPAddress.Parse(serverUri.Host)))
                            {
                                localHostOnly = true;

                                if(IPAddress.TryParse(serverUri.Host, out IPAddress address))
                                {
                                    if(address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                                    {
                                        if (address.IsIPv6LinkLocal)
                                        {
                                            localHostOnly = false;
                                        }
                                        ipv6 = true;
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Logging.LogMessage(ex);
                        }
                    }
                    else //Invalid Uri, simply parse out port
                    {
                        if (serverAddress.Contains(":"))
                        {
                            if(int.TryParse(serverAddress.Split(':').Last(), out int result))
                            {
                                port = result;
                            }
                        }

                        ipv6 = serverAddress.Contains("*") || serverAddress.Contains("+");
                    }

                    Console.WriteLine($"Starting Discovery on port: {port}");
                    Discovery.DiscoveryManager.Start(port, localHostOnly, ipv6);

                }
                else
                {
                    Console.WriteLine("Starting discovery server from defaults");
                    Discovery.DiscoveryManager.Start();
                }

            }
            catch (Exception ex)
            {
                Logging.LogMessage(ex);
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

            lifetime.ApplicationStarted.Register(() =>
            {
                Logging.LogMessage("Server Starting");

                try
                {
                    if (ServerSettings.AutoStartBrowser) //AutoStart Browser
                    {
                        ProcessStartInfo psi = new ProcessStartInfo
                        {
                            FileName = string.Format("http://localhost:{0}", port),
                            UseShellExecute = true
                        };
                        Process.Start(psi);
                    }
                }
                catch (Exception ex)
                {
                    Logging.LogMessage(ex);
                }
            });

            lifetime.ApplicationStopping.Register(() =>
            {
                Logging.LogMessage("Server Stopping");
            });

            lifetime.ApplicationStopped.Register(() =>
            {
                Logging.LogMessage("Server Stopped");
            });

            Lifetime = lifetime;
        }
    }
}
