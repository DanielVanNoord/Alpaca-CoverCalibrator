using ASCOM.Standard.Interfaces;
using Blazored.Toast;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Alpaca.CoverCalibrator
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// IHostApplicationLifetime is used to shutdown the driver from the Blazor UI
        /// </summary>
        internal static IHostApplicationLifetime Lifetime
        {
            get;
            private set;
        }

        /// <summary>
        /// The addresses found at startup
        /// </summary>
        internal static string[] Addresses
        {
            get;
            private set;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add support for Razor
            services.AddRazorPages();

            // Add support for server side Blazor (server side rendering)
            services.AddServerSideBlazor();

            // Add MVC support for the Alpaca REST APIs
            services.AddMvc();

            // If Swagger is set to run start it and bind to names
            if (AlpacaSettings.RunSwagger)
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = $"{AlpacaSettings.ServerName}", Version = "v1" });
                });
            }

            // configure basic authentication
            // Expire if no access for more then one hour
            // CookieAuthenticationDefaults.AuthenticationScheme
            // "BasicAuthentication"
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
                options =>
                {
                    options.SlidingExpiration = true;
                    options.ExpireTimeSpan = TimeSpan.FromHours(1);
                }
                );

            // configure DI for application services

            // Add an instance of IUserServer for authentication
            services.AddScoped<IUserService, UserService>();

            // Add the AuthorizationFilter service for the REST APIs
            services.AddScoped<AuthorizationFilter>();

            // Add support for BlazoredToast to show toast messages.
            services.AddBlazoredToast();
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

            // Pipe Swagger responses into swagger system.
            if (AlpacaSettings.RunSwagger)
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json",
                                 $"{AlpacaSettings.ServerName} v1"));
            }

            int port = AlpacaSettings.ServerPort;

            //Find bound addresses to link discovery to the endpoints
            try
            {
                var serverAddressesFeature = app.ServerFeatures.Get<IServerAddressesFeature>();

                Addresses = serverAddressesFeature.Addresses.ToArray();

                if (serverAddressesFeature.Addresses.Count > 0)
                {
                    var serverAddress = serverAddressesFeature.Addresses.First();
                    bool localHostOnly = false;
                    bool ipv6 = false;

                    if (Uri.TryCreate(serverAddress, UriKind.RelativeOrAbsolute, out Uri serverUri))
                    {
                        try
                        {
                            port = serverUri.Port;
                            if (serverUri.Host.ToLowerInvariant().Contains("localhost") || IPAddress.IsLoopback(IPAddress.Parse(serverUri.Host)))
                            {
                                localHostOnly = true;

                                if (IPAddress.TryParse(serverUri.Host, out IPAddress address))
                                {
                                    if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
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
                            Logging.Log.LogError(ex.Message);
                        }
                    }
                    else //Invalid Uri, simply parse out port
                    {
                        if (serverAddress.Contains(":"))
                        {
                            if (int.TryParse(serverAddress.Split(':').Last(), out int result))
                            {
                                port = result;
                            }
                        }

                        ipv6 = serverAddress.Contains("*") || serverAddress.Contains("+");
                    }

                    Discovery.DiscoveryManager.Start(port, localHostOnly, ipv6);
                }
                else
                {
                    Discovery.DiscoveryManager.Start();
                }
            }
            catch (Exception ex)
            {
                Logging.Log.LogError(ex.Message);
            }

            // Using Static files for wwwroot, which contains css and javascript
            app.UseStaticFiles();

            // Use Routing to find endpoints
            app.UseRouting();

            // Activate Authentication and Cookies. This uses basic http auth for APIs and Cookie auth for APIs and blazor
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy();

            // Activate Endpoints for controllers and Blazor. The platform finds the pages.
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

            // Register lifetime. Start the browser and log when started
            lifetime.ApplicationStarted.Register(() =>
            {
                Logging.Log.LogInformation($"{AlpacaSettings.ServerName} Starting");

                try
                {
                    if (AlpacaSettings.AutoStartBrowser) //AutoStart Browser
                    {
                        StartBrowser(port);
                    }
                }
                catch (Exception ex)
                {
                    Logging.Log.LogError(ex.Message);
                }
            });

            // Register stopping to log out
            lifetime.ApplicationStopping.Register(() =>
            {
                Logging.Log.LogInformation($"{AlpacaSettings.ServerName} Stopping");
            });

            // Register stopped to log out
            lifetime.ApplicationStopped.Register(() =>
            {
                Logging.Log.LogInformation($"{AlpacaSettings.ServerName} Stopped");
            });

            // Cache the lifetime so Blazor can close the driver if requested
            Lifetime = lifetime;
        }

        // Start the local browser to the localhost and current port to access the UI
        internal static void StartBrowser(int port)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = string.Format("http://localhost:{0}", port),
                UseShellExecute = true
            };
            Process.Start(psi);
        }
    }
}