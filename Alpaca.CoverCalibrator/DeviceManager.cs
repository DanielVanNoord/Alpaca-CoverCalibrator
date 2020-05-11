using System;
using System.Collections;
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
            coverCalibrator = new CoverCalibratorSimulator.CoverCalibrator(new ASCOM.Standard.Utilities.TraceLogger(string.Empty, "AlpacaCoverCalibratorSimulator"), new DictionarySettings());
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

        private class DictionarySettings : ASCOM.Compatibility.Interfaces.IProfileFull
        {
            private Dictionary<string, string> Settings = new Dictionary<string, string>();

            public string DeviceType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            public ArrayList RegisteredDeviceTypes => throw new NotImplementedException();

            public void CreateSubKey(string DriverID, string SubKey)
            {
                throw new NotImplementedException();
            }

            public void DeleteSubKey(string DriverID, string SubKey)
            {
                throw new NotImplementedException();
            }

            public void DeleteValue(string DriverID, string Name, string SubKey)
            {
                throw new NotImplementedException();
            }

            public void DeleteValue(string DriverID, string Name)
            {
                throw new NotImplementedException();
            }

            public string GetProfileXML(string deviceId)
            {
                throw new NotImplementedException();
            }

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

            public bool IsRegistered(string DriverID)
            {
                throw new NotImplementedException();
            }

            public void MigrateProfile(string CurrentPlatformVersion)
            {
                throw new NotImplementedException();
            }

            public void Register(string DriverID, string DescriptiveName)
            {
                throw new NotImplementedException();
            }

            public ArrayList RegisteredDevices(string DeviceType)
            {
                throw new NotImplementedException();
            }

            public void SetProfileXML(string deviceId, string xml)
            {
                throw new NotImplementedException();
            }

            public ArrayList SubKeys(string DriverID, string SubKey)
            {
                throw new NotImplementedException();
            }

            public ArrayList SubKeys(string DriverID)
            {
                throw new NotImplementedException();
            }

            public void Unregister(string DriverID)
            {
                throw new NotImplementedException();
            }

            public ArrayList Values(string DriverID, string SubKey)
            {
                throw new NotImplementedException();
            }

            public ArrayList Values(string DriverID)
            {
                throw new NotImplementedException();
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

        private class ConsoleLogger : ASCOM.Compatibility.Interfaces.ITraceLoggerFull
        {
            public bool Enabled { get; set; }

            public string LogFileName => throw new NotImplementedException();

            public string LogFilePath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public int IdentifierWidth { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            public void BlankLine()
            {
                throw new NotImplementedException();
            }

            public void Dispose()
            {
            }

            public void LogContinue(string Message)
            {
                throw new NotImplementedException();
            }

            public void LogContinue(string Message, bool HexDump)
            {
                throw new NotImplementedException();
            }

            public void LogFinish(string Message)
            {
                throw new NotImplementedException();
            }

            public void LogFinish(string Message, bool HexDump)
            {
                throw new NotImplementedException();
            }

            public void LogIssue(string Identifier, string Message)
            {
                throw new NotImplementedException();
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

            public void LogStart(string Identifier, string Message)
            {
                throw new NotImplementedException();
            }

            public void SetLogFile(string LogFileName, string LogFileType)
            {
                throw new NotImplementedException();
            }
        }
    }
}
