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
            coverCalibrator = new CoverCalibratorSimulator.CoverCalibrator(new ASCOM.Standard.Utilities.TraceLogger(string.Empty, "AlpacaCoverCalibratorSimulator"), 
                new ASCOM.Standard.Utilities.XMLProfile("ASCOM-Simulator", "CoverCalibrator", 0));
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

        private class ConsoleLogger : ASCOM.Compatibility.Interfaces.ITraceLogger
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
