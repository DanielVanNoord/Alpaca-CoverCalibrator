using ASCOM.Standard.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Alpaca.CoverCalibrator
{
    internal static class Logging
    {
        internal static ASCOM.Standard.Utilities.TraceLogger Log
        {
            get;
            private set;
        }

        static Logging()
        {
            Log = new ASCOM.Standard.Utilities.TraceLogger(null, "AlpacaCoverCalibratorSimulator") { Enabled = true };


            Log.SetMinimumLoggingLevel(LogLevel.Debug);

            //Set platform logging 
            ASCOM.Standard.Utilities.Logger.SetLogProvider(Log);
        }

        internal static void LogInformation(string message)
        {
            ASCOM.Standard.Utilities.Logger.LogInformation(message);
        }

        internal static void LogAPICall(IPAddress remoteIpAddress, string request, int clientID, uint clientTransactionID, uint transactionID)
        {
            Log.LogDebug($"Transaction: {transactionID} - {remoteIpAddress} with a client id of {clientID} and client transaction of {clientTransactionID} requested {request}");
        }

        internal static void LogAPICall(IPAddress remoteIpAddress, string request, int clientID, uint clientTransactionID, uint transactionID, string payload)
        {
            throw new NotImplementedException();
        }

        internal static void LogError(string message)
        {
            ASCOM.Standard.Utilities.Logger.LogError(message);
        }
    }
}
