using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpaca.CoverCalibrator
{
    internal static class DeviceManager
    {
        private readonly static Dictionary<int,ASCOM.Standard.Interfaces.ICoverCalibratorV1> coverCalibratorV1s = new Dictionary<int,ASCOM.Standard.Interfaces.ICoverCalibratorV1>();


        static DeviceManager()
        {
            //Only one instance
            coverCalibratorV1s.Add(0,new ASCOMSimulators.CoverCalibratorSimulator(0, new ASCOM.Standard.Utilities.TraceLogger(),
                new ASCOM.Standard.Utilities.XMLProfile("ASCOM-Simulator-CovCal", "CoverCalibrator", 0)));
        }

        internal static void Reset()
        {
            foreach (var covcal in coverCalibratorV1s)
            {
                (covcal.Value as ASCOMSimulators.CoverCalibratorSimulator)?.ResetSettings();
            }
        }

        internal static ASCOM.Standard.Interfaces.ICoverCalibratorV1 GetCoverCalibrator(int DeviceID)
        {
            if(coverCalibratorV1s.ContainsKey(DeviceID))
            {
                return coverCalibratorV1s[DeviceID];
            }
            else
            {
                throw new Exception(string.Format("Instance {0} does not exist in this server.", DeviceID));
            }
        }

        internal static List<ASCOM.Standard.Interfaces.ICoverCalibratorV1> GetCoverCalibrators()
        {
            return coverCalibratorV1s.Values.ToList();
        }
    }
}
