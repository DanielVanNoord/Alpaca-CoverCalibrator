using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpaca.CoverCalibrator
{
    internal static class DeviceManager
    {
        private static List<ASCOM.Standard.Interfaces.ICoverCalibratorV1> coverCalibratorV1s = new List<ASCOM.Standard.Interfaces.ICoverCalibratorV1>();


        static DeviceManager()
        {
            //Only one instance
            coverCalibratorV1s.Add(new ASCOMSimulators.CoverCalibratorSimulator(0, Logging.Log,
                new ASCOM.Standard.Utilities.XMLProfile("ASCOM-Simulator-CovCal", "CoverCalibrator", 0)));
        }

        internal static void Reset()
        {
            foreach (var covcal in coverCalibratorV1s)
            {
                (covcal as ASCOMSimulators.CoverCalibratorSimulator)?.ResetSettings();
            }
        }

        internal static ASCOM.Standard.Interfaces.ICoverCalibratorV1 GetCoverCalibrator(int DeviceID)
        {
            if(DeviceID == 0)
            {
                return coverCalibratorV1s.First();
            }
            else
            {
                throw new Exception(string.Format("Instance {0} does not exist in this server.", DeviceID));
            }
        }

        internal static List<ASCOM.Standard.Interfaces.ICoverCalibratorV1> GetCoverCalibrators()
        {
            return coverCalibratorV1s;
        }
    }
}
