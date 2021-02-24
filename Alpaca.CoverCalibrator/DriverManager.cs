﻿using System;
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

        private readonly static Dictionary<int,ASCOM.Standard.Interfaces.ICoverCalibratorV1> coverCalibratorV1s = new Dictionary<int,ASCOM.Standard.Interfaces.ICoverCalibratorV1>();


        static DriverManager()
        {
            //Only one instance
            coverCalibratorV1s.Add(0,new ASCOMSimulators.CoverCalibratorSimulator(0, Logging.Log,
                new ASCOM.Standard.Utilities.XMLProfile(AlpacaSettings.ServerFileName, "CoverCalibrator", 0)));
        }

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
                    Logging.LogError(ex.Message);
                }
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
                throw new DeviceNotFoundException(string.Format("Instance {0} does not exist in this server.", DeviceID));
            }
        }

        internal static List<ASCOM.Standard.Interfaces.ICoverCalibratorV1> GetCoverCalibrators()
        {
            return coverCalibratorV1s.Values.ToList();
        }
    }
}