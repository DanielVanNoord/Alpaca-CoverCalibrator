﻿using ASCOM.Common;
using ASCOM.Common.Interfaces;
using ASCOM.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Alpaca.CoverCalibrator
{
    internal static class AlpacaSettings
    {
        internal const string DriverName = "Alpaca CoverCalibrator Simulator";
        internal const string Manufacturer = "ASCOM Initiative";

        //Change this to be unique for your server, it is the name of the settings folder
        internal const string DriverSettingsFileName = "ASCOM-Simulator-CovCal";


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

        private readonly static XMLProfile Profile = new XMLProfile(DriverSettingsFileName, "Server");

        internal static void Reset()
        {
            try
            {
                Profile.Clear();
            }
            catch (Exception ex)
            {
                Logging.Log.LogError(ex.Message);
            }
        }

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

        internal static bool AutoStartBrowser
        {
            get
            {
  
                if (bool.TryParse(Profile.GetValue("AutoStartBrowser", true.ToString()), out bool result))
                {
                    return result;
                }
                return true;
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
                if (ushort.TryParse(Profile.GetValue("ServerPort", 5000.ToString()), out ushort result))
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
                if (bool.TryParse(Profile.GetValue("AllowRemoteAccess", true.ToString()), out bool result))
                {
                    return result;
                }
                return true;
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
                if (bool.TryParse(Profile.GetValue("AllowDiscovery", true.ToString()), out bool result))
                {
                    return result;
                }
                return true;
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
                if (ushort.TryParse(Profile.GetValue("DiscoveryPort", 32227.ToString()), out ushort result))
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
                if (bool.TryParse(Profile.GetValue("LocalRespondOnlyToLocalHost", true.ToString()), out bool result))
                {
                    return result;
                }
                return true;
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
                if (bool.TryParse(Profile.GetValue("PreventRemoteDisconnects", true.ToString()), out bool result))
                {
                    return result;
                }
                return true;
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
                if (bool.TryParse(Profile.GetValue("PreventRemoteDisposes", true.ToString()), out bool result))
                {
                    return result;
                }
                return true;
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
                if (bool.TryParse(Profile.GetValue("UseAuth", false.ToString()), out bool result))
                {
                    return result;
                }
                return false;
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
                if(Enum.TryParse(Profile.GetValue("LoggingLevel", LogLevel.Information.ToString()), out LogLevel result))
                {
                    return result;
                }
                return LogLevel.Information;
            }
            set
            {
                Logging.Log.SetMinimumLoggingLevel(value);
                Profile.WriteValue("LoggingLevel", value.ToString());
            }
        }

        internal static bool RunSwagger
        {
            get
            {
                if (bool.TryParse(Profile.GetValue("RunSwagger", true.ToString()), out bool result))
                {
                    return result;
                }
                return true;
            }
            set
            {
                Profile.WriteValue("RunSwagger", value.ToString());
            }
        }
    }
}
