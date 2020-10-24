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
using System;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Alpaca.CoverCalibrator
{
    public class Startup
    {
        public const string Auths = CookieAuthenticationDefaults.AuthenticationScheme + "," + "BasicAuthentication";

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

        internal static string[] Addresses
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
            services.AddMvc();

            // configure basic authentication
            // CookieAuthenticationDefaults.AuthenticationScheme
            // "BasicAuthentication"
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null).AddCookie();

            //Make sure to set Auths to the correct values for your schemes

            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
            services.AddBlazoredToast();

            // setup for cookie auth in blazor pages
            services.Configure<CookiePolicyOptions>(options =>

            {
                options.CheckConsentNeeded = context => true;

                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


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
                            Logging.LogError(ex.Message);
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
                Logging.LogError(ex.Message);
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

            lifetime.ApplicationStarted.Register(() =>
            {
                Logging.LogInformation("Server Starting");

                try
                {
                    if (ServerSettings.AutoStartBrowser) //AutoStart Browser
                    {
                        ServerManager.StartBrowser(port);
                    }
                }
                catch (Exception ex)
                {
                    Logging.LogError(ex.Message);
                }
            });

            lifetime.ApplicationStopping.Register(() =>
            {
                Logging.LogInformation("Server Stopping");
            });

            lifetime.ApplicationStopped.Register(() =>
            {
                Logging.LogInformation("Server Stopped");
            });

            Lifetime = lifetime;
        }
    }
}