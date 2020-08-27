using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpaca.CoverCalibrator
{
    internal static class ServerSettings
    {
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
            get;
            set;
        } = true;

        internal static bool AllowDiscovery
        {
            get;
            set;
        } = true;

        internal static UInt16 DiscoveryPort
        {
            get;
            set;
        } = 32227;
    }
}
