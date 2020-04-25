using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpaca.CoverCalibrator
{
    internal static class DeviceManager
    {
        private static List<ASCOM.Alpaca.Interfaces.ICoverCalibratorV1> coverCalibratorV1s = new List<ASCOM.Alpaca.Interfaces.ICoverCalibratorV1>();

        internal static CoverCalibratorSimulator.CoverCalibrator coverCalibrator;

        static DeviceManager()
        {
            coverCalibrator = new CoverCalibratorSimulator.CoverCalibrator(new ConsoleLogger(), new DictionarySettings());
            coverCalibratorV1s.Add(coverCalibrator);
        }

        internal static ASCOM.Alpaca.Interfaces.ICoverCalibratorV1 GetCoverCalibrator(int DeviceID)
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

        internal static List<ASCOM.Alpaca.Interfaces.ICoverCalibratorV1> GetCoverCalibrators()
        {
            return coverCalibratorV1s;
        }

        private class DictionarySettings : CoverCalibratorSimulator.IProfile
        {
            private Dictionary<string, string> Settings = new Dictionary<string, string>();

            public string GetValue(string DriverID, string Name)
            {
                Settings.TryGetValue(DriverID + Name, out string value);
                return value;
            }

            public string GetValue(string DriverID, string Name, string SubKey)
            {
                Settings.TryGetValue(DriverID + Name + SubKey, out string value);
                return value;
            }

            public string GetValue(string DriverID, string Name, string SubKey, string DefaultValue)
            {
                if(Settings.ContainsKey(DriverID + Name + SubKey))
                {
                    Settings.TryGetValue(DriverID + Name + SubKey, out string value);
                    return value;
                }
                else
                {
                    return DefaultValue;
                }
            }

            public void WriteValue(string DriverID, string Name, string Value)
            {
                Settings.Add(DriverID + Name, Value);
            }

            public void WriteValue(string DriverID, string Name, string Value, string SubKey)
            {
                Settings.Add(DriverID + Name + SubKey, Value);
            }
        }

        private class ConsoleLogger : CoverCalibratorSimulator.ITraceLogger
        {
            public bool Enabled { get; set; }

            public void Dispose()
            {
            }

            public void LogMessage(string Identifier, string Message)
            {
                Console.WriteLine(Identifier + ": " + Message);
            }

            public void LogMessage(string Identifier, string Message, bool HexDump)
            {
                LogMessage(Identifier, Message);
            }

            public void LogMessageCrLf(string Identifier, string Message)
            {
                LogMessage(Identifier, Message);
            }
        }
    }
}
