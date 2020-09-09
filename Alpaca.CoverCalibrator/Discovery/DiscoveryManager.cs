using Alpaca.CoverCalibrator.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Alpaca.CoverCalibrator.Discovery
{
    internal static class DiscoveryManager
    {
        internal static Server DiscoveryServer
        {
            get;
            private set;
        }

        internal static void Start()
        {
            DiscoveryServer = new Server(ServerSettings.ServerPort, true, false)
            {
                AllowDiscovery = ServerSettings.AllowDiscovery,
                AllowRemoteAccess = ServerSettings.AllowRemoteAccess,
                LocalRespondOnlyToLocalHost = ServerSettings.LocalRespondOnlyToLocalHost
            };
        }

        internal static void Start(int port, bool localHostOnly, bool ipv6)
        {
            if(!Dns.GetHostAddresses(Dns.GetHostName()).Any(o=>o.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6))
            {
                ipv6 = false;
            }

            DiscoveryServer = new Server(port, true, ipv6)
            {
                AllowDiscovery = ServerSettings.AllowDiscovery,
                AllowRemoteAccess = !localHostOnly,
                LocalRespondOnlyToLocalHost = ServerSettings.LocalRespondOnlyToLocalHost
            };
        }
    }
}
