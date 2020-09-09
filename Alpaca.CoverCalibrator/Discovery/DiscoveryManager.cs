using Alpaca.CoverCalibrator.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
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

        internal static void Start(int port, bool localHostOnly)
        {
            DiscoveryServer = new Server(port, true, false)
            {
                AllowDiscovery = ServerSettings.AllowDiscovery,
                AllowRemoteAccess = !localHostOnly,
                LocalRespondOnlyToLocalHost = ServerSettings.LocalRespondOnlyToLocalHost
            };
        }
    }
}
