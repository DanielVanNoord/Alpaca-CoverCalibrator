using ASCOM.Standard.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpaca.CoverCalibrator
{
    internal static class Logging
    {
        static Logging()
        {
            Log = new ASCOM.Standard.Utilities.TraceLogger(string.Empty, "AlpacaCoverCalibratorSimulator") { Enabled = true };
        }
        internal static ITraceLogger Log
        {
            get;
            private set;
        }

        internal static void LogMessage(string Message)
        {
            var enabled = Log.Enabled;

            if (!enabled)
            {
                Log.Enabled = true;
            }
            Log.LogMessage("CoverCalibrator", Message);

            if (!enabled)
            {
                Log.Enabled = false;
            }
        }

        internal static void LogMessage(Exception ex)
        {
            var enabled = Log.Enabled;

            if (!enabled)
            {
                Log.Enabled = true;
            }
            Log.LogIssue("CoverCalibrator", ex.Message);
            if (!enabled)
            {
                Log.Enabled = false;
            }
        }
    }
}
