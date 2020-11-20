using Alpaca.CoverCalibrator.Pages;
using ASCOM.Standard.Discovery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Alpaca.CoverCalibrator.Discovery
{
    internal static class DiscoveryManager
    {
        internal static Responder DiscoveryResponder
        {
            get;
            private set;
        }

        internal static bool IsRunning => !DiscoveryResponder?.Disposed ?? false;

        internal static void Start()
        {
            if (ServerSettings.AllowDiscovery)
            {
                Console.WriteLine("Starting discovery server from defaults");

                DiscoveryResponder = new Responder(ServerSettings.ServerPort, true, false)
                {
                    AllowRemoteAccess = ServerSettings.AllowRemoteAccess,
                    LocalRespondOnlyToLocalHost = ServerSettings.LocalRespondOnlyToLocalHost
                };
            }
        }

        internal static void Start(int port, bool localHostOnly, bool ipv6)
        {
            if (ServerSettings.AllowDiscovery)
            {
                Console.WriteLine($"Starting Discovery on port: {port}");

                if (!Dns.GetHostAddresses(Dns.GetHostName()).Any(o => o.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6))
                {
                    ipv6 = false;
                }

                DiscoveryResponder = new Responder(port, true, ipv6)
                {
                    AllowRemoteAccess = !localHostOnly,
                    LocalRespondOnlyToLocalHost = ServerSettings.LocalRespondOnlyToLocalHost
                };
            }
        }

        internal static void Stop()
        {
            DiscoveryResponder.Dispose();
        }
    }
}
