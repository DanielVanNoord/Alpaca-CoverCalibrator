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
            get
            {
                return Profile.GetValue("Location", "Unknown");
            }
            set
            {            
                Profile.WriteValue("Location", value.ToString());
            }
        }

        internal static void Reset()
        {
            try
            {
                Profile.Clear();
            }
            catch(Exception ex)
            {
                Logging.LogMessage(ex);
            }
        }

        internal static bool AutoStartBrowser
        {
            get
            {
                return Profile.GetValue("AutoStartBrowser", true.ToString()) == true.ToString();
            }
            set
            {
                Profile.WriteValue("AutoStartBrowser", value.ToString());
            }
        }

        internal static ushort ServerPort
        {
            get
            {
                var value = Profile.GetValue("ServerPort", 5000.ToString());
                if (ushort.TryParse(value, out ushort result))
                {
                    return result;
                }
                return 5000;
            }
            set
            {
                Profile.WriteValue("ServerPort", value.ToString());
            }
        }

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

        internal static ushort DiscoveryPort
        {
            get
            {
                var value = Profile.GetValue("DiscoveryPort", 32227.ToString());
                if (ushort.TryParse(value, out ushort result))
                {
                    return result;
                }
                return 32227;
            }
            set
            {
                Profile.WriteValue("DiscoveryPort", value.ToString());
            }
        }

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
