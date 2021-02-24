using Alpaca.CoverCalibrator.Pages;
using ASCOM.Standard.Discovery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Alpaca.CoverCalibrator.Discovery
{
    /// <summary>
    /// A static class that contains a Discovery Responder and some helper functions
    /// </summary>
    internal static class DiscoveryManager
    {
        /// <summary>
        /// An instance of the ASCOM Alpaca Discovery Responder
        /// </summary>
        internal static Responder DiscoveryResponder
        {
            get;
            private set;
        }

        /// <summary>
        /// A check if the Responder is running
        /// </summary>
        internal static bool IsRunning => !DiscoveryResponder?.Disposed ?? false;


        /// <summary>
        /// The IP Address of the host computers network adapter(s)
        /// </summary>
        internal static List<IPAddress> AdapterAddress
        {
            get
            { 
                List<IPAddress> Addresses = new List<IPAddress>();
                NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface adapter in adapters)
                {
                    //Do not try and use non-operational adapters
                    if (adapter.OperationalStatus != OperationalStatus.Up)
                        continue;

                    IPInterfaceProperties adapterProperties = adapter.GetIPProperties();
                    if (adapterProperties == null)
                        continue;


                    UnicastIPAddressInformationCollection uniCast = adapterProperties.UnicastAddresses;
                    if (uniCast.Count > 0)
                    {
                        foreach (UnicastIPAddressInformation uni in uniCast)
                        {
                            if (uni.Address.AddressFamily != AddressFamily.InterNetwork && uni.Address.AddressFamily != AddressFamily.InterNetworkV6)
                                continue;

                            Addresses.Add(uni.Address);
                        }
                    }
                }

                return Addresses;
            }
        }

        /// <summary>
        /// Start default discovery without IPv6 on all defaults
        /// </summary>
        internal static void Start()
        {
            if (AlpacaSettings.AllowDiscovery)
            {
                Console.WriteLine("Starting discovery responder from defaults");

                DiscoveryResponder = new Responder(AlpacaSettings.ServerPort, true, false)
                {
                    AllowRemoteAccess = AlpacaSettings.AllowRemoteAccess,
                    LocalRespondOnlyToLocalHost = AlpacaSettings.LocalRespondOnlyToLocalHost
                };
            }
        }

        /// <summary>
        /// Start discovery with custom ports
        /// </summary>
        /// <param name="port">Port to use</param>
        /// <param name="localHostOnly">Respond only on local host</param>
        /// <param name="ipv6">Support IPv6 discovery (if available on adapters)</param>
        internal static void Start(int port, bool localHostOnly, bool ipv6)
        {
            if (AlpacaSettings.AllowDiscovery)
            {
                Console.WriteLine($"Starting Discovery on port: {port}");

                if (!Dns.GetHostAddresses(Dns.GetHostName()).Any(o => o.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6))
                {
                    ipv6 = false;
                }

                DiscoveryResponder = new Responder(port, true, ipv6)
                {
                    AllowRemoteAccess = !localHostOnly,
                    LocalRespondOnlyToLocalHost = AlpacaSettings.LocalRespondOnlyToLocalHost
                };
            }
        }

        /// <summary>
        /// Stop and Dispose the current Responder
        /// </summary>
        internal static void Stop()
        {
            DiscoveryResponder.Dispose();
        }
    }
}
