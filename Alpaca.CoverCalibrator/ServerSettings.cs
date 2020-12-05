using ASCOM.Standard.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Alpaca.CoverCalibrator
{
    internal static class ServerSettings
    {
        internal const string ServerName = "Alpaca CoverCalibrator Simulator";
        internal const string Manufacturer = "ASCOM Initiative";
        internal static readonly int[] APIVersions = { 1 };

        internal static string Version
        {
            get
            {
                try
                {
                    System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                    return FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion;
                }
                catch
                {

                }
                return "1.0.0-Error";
            }
        }

        //Change this to be unique for your server, it is the name of the settings folder
        internal const string ServerFileName = "ASCOM-Simulator-CovCal";

        private readonly static ASCOM.Standard.Utilities.XMLProfile Profile = new ASCOM.Standard.Utilities.XMLProfile(ServerFileName, "Server");

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
                Logging.LogError(ex.Message);
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
                Discovery.DiscoveryManager.DiscoveryResponder.AllowRemoteAccess = value;
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
                Profile.WriteValue("AllowDiscovery", value.ToString());

                if (value)
                {
                    if (!Discovery.DiscoveryManager.IsRunning)
                    {
                        Discovery.DiscoveryManager.Start(ServerPort, LocalRespondOnlyToLocalHost, true);
                    }
                }
                else
                {
                    if (Discovery.DiscoveryManager.IsRunning)
                    {
                        Discovery.DiscoveryManager.Stop();
                    }
                }
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
                Discovery.DiscoveryManager.DiscoveryResponder.LocalRespondOnlyToLocalHost = value;
                Profile.WriteValue("LocalRespondOnlyToLocalHost", value.ToString());
            }
        }

        internal static bool PreventRemoteDisconnects
        {
            get
            {
                return Profile.GetValue("PreventRemoteDisconnects", true.ToString()) == true.ToString();
            }
            set
            {
                Discovery.DiscoveryManager.DiscoveryResponder.LocalRespondOnlyToLocalHost = value;
                Profile.WriteValue("PreventRemoteDisconnects", value.ToString());
            }
        }

        internal static bool PreventRemoteDisposes
        {
            get
            {
                return Profile.GetValue("PreventRemoteDisposes", true.ToString()) == true.ToString();
            }
            set
            {
                Discovery.DiscoveryManager.DiscoveryResponder.LocalRespondOnlyToLocalHost = value;
                Profile.WriteValue("PreventRemoteDisposes", value.ToString());
            }
        }

        internal static bool UseAuth
        {
            get
            {
                return Profile.GetValue("UseAuth", false.ToString()) == true.ToString();
            }
            set
            {
                Profile.WriteValue("UseAuth", value.ToString());
            }
        }

        internal static string UserName
        {
            get
            {
                return Profile.GetValue("UserName", "User");
            }
            set
            {
                Profile.WriteValue("UserName", value.ToString());
            }
        }

        internal static string Password
        {
            get
            {
                return Profile.GetValue("Password");
            }
            set
            {
                Profile.WriteValue("Password", Hash.GetStoragePassword(value));
            }
        }

        internal static LogLevel LoggingLevel
        {
            get
            {
                return (LogLevel) Enum.Parse(typeof(LogLevel), Profile.GetValue("LoggingLevel", LogLevel.Information.ToString()));
            }
            set
            {
                Logging.Log.SetMinimumLoggingLevel(value);
                Profile.WriteValue("LoggingLevel", value.ToString());
            }
        }
    }
}
