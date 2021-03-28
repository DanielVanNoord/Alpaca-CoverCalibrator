using ASCOM.Standard.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpaca.CoverCalibrator
{
    internal static class DriverManager
    {
        private static uint transactionID = 0;

        internal static uint ServerTransactionID
        {
            get
            {
                return transactionID++;
            }
        }

        // This stores the actual instance of the device drivers. It is keyed to the Device Number
        private readonly static Dictionary<int,ASCOM.Standard.Interfaces.ICoverCalibratorV1> coverCalibratorV1s = new Dictionary<int,ASCOM.Standard.Interfaces.ICoverCalibratorV1>();


        static DriverManager()
        {
            //Only one instance
            coverCalibratorV1s.Add(0,new ASCOMSimulators.CoverCalibratorSimulator(0, Logging.Log,
                new ASCOM.Standard.Utilities.XMLProfile(AlpacaSettings.DriverSettingsFileName, "CoverCalibrator", 0)));
        }

        /// <summary>
        /// Reset all devices settings profiles.
        /// </summary>
        internal static void Reset()
        {
            foreach (var covcal in coverCalibratorV1s)
            {
                try
                {
                    (covcal.Value as ASCOMSimulators.CoverCalibratorSimulator)?.ResetSettings();
                }
                catch(Exception ex)
                {
                    Logging.Log.LogError(ex.Message);
                }
            }
        }

        //This allows access to specific devices for the API controllers and the device Blazor UI Pages  
        internal static ASCOM.Standard.Interfaces.ICoverCalibratorV1 GetCoverCalibrator(int DeviceID)
        {
            if(coverCalibratorV1s.ContainsKey(DeviceID))
            {
                return coverCalibratorV1s[DeviceID];
            }
            else
            {
                throw new DeviceNotFoundException(string.Format("Instance {0} does not exist in this server.", DeviceID));
            }
        }
    }
}
