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

        internal static CoverCalibratorSimulator.CoverCalibratorSimulator coverCalibrator;

        static DeviceManager()
        {
            coverCalibrator = new CoverCalibratorSimulator.CoverCalibratorSimulator(0, Logging.Log, 
                new ASCOM.Standard.Utilities.XMLProfile("ASCOM-Simulator", "CoverCalibrator", 0));
            coverCalibratorV1s.Add(coverCalibrator);
        }

        internal static ASCOM.Standard.Interfaces.ICoverCalibratorV1 GetCoverCalibrator(int DeviceID)
        {
            if(DeviceID == 0)
            {
                return coverCalibrator;
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
