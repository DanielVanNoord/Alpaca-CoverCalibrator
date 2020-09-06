using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpaca.CoverCalibrator
{
    internal static class ServerSettings
    {
        private static ASCOM.Standard.Utilities.XMLProfile Profile = new ASCOM.Standard.Utilities.XMLProfile("ASCOM-Simulator-CovCal", "Server");

        internal static string Location
        {
            get;
            set;
        } = "Unknown";

        internal static bool AutoStartBrowser
        {
            get;
            set;
        } = true;

        internal static UInt16 ServerPort
        {
            get;
            set;
        } = 5000;

        internal static bool AllowRemoteAccess
        {
            get
            {
                return Profile.GetValue("AllowRemoteAccess", true.ToString()) == true.ToString();
            }
            set
            {
                Discovery.DiscoveryManager.DiscoveryServer.AllowRemoteAccess = value;
                Profile.WriteValue("AllowRemoteAccess", value.ToString());
            }
        }

        internal static bool AllowDiscovery
        {
            get
            {
                return Profile.GetValue("AllowDiscovery", true.ToString()) == true.ToString();
            }
            set
            {
                Discovery.DiscoveryManager.DiscoveryServer.AllowDiscovery = value;
                Profile.WriteValue("AllowDiscovery", value.ToString());
            }
        }

        internal static UInt16 DiscoveryPort
        {
            get;
            set;
        } = 32227;

        internal static bool LocalRespondOnlyToLocalHost
        {
            get
            {
                return Profile.GetValue("LocalRespondOnlyToLocalHost", true.ToString()) == true.ToString();
            }
            set
            {
                Discovery.DiscoveryManager.DiscoveryServer.LocalRespondOnlyToLocalHost = value;
                Profile.WriteValue("LocalRespondOnlyToLocalHost", value.ToString());
            }
        }
    }
}
