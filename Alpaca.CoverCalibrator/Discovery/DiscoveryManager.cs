﻿using Alpaca.CoverCalibrator.Pages;
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

        internal static bool IsRunning => !DiscoveryServer?.Disposed ?? false;

        internal static void Start()
        {
            if (ServerSettings.AllowDiscovery)
            {
                Console.WriteLine("Starting discovery server from defaults");

                DiscoveryServer = new Server(ServerSettings.ServerPort, true, false)
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

                DiscoveryServer = new Server(port, true, ipv6)
                {
                    AllowRemoteAccess = !localHostOnly,
                    LocalRespondOnlyToLocalHost = ServerSettings.LocalRespondOnlyToLocalHost
                };
            }
        }

        internal static void Stop()
        {
            DiscoveryServer.Dispose();
        }
    }
}
