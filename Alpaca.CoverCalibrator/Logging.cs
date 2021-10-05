using ASCOM.Common;
using ASCOM.Common.DeviceInterfaces;
using ASCOM.Common.Interfaces;
using ASCOM.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Alpaca.CoverCalibrator
{
    internal static class Logging
    {
        //A single static instance of the logger that is shared with the rest of the project
        internal static ILogger Log
        {
            get;
            private set;
        }

        static Logging()
        {
            //Create and activate a TraceLogger, which implements ILogger
            Log = new TraceLogger(AlpacaSettings.DriverSettingsFileName, true);

            Log.SetMinimumLoggingLevel(AlpacaSettings.LoggingLevel);

            //Set platform logging 
            //In this case the platform uses the same logger as the driver.
            Logger.SetLogProvider(Log);
        }

        //Helper function to log out API requests without a payload
        internal static void LogAPICall(IPAddress remoteIpAddress, string request, uint clientID, uint clientTransactionID, uint transactionID)
        {
            Log.LogVerbose($"Transaction: {transactionID} - {remoteIpAddress} ({clientID}, {clientTransactionID}) requested {request}");
        }
    }
}
