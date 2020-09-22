//
// ================
// Shared Resources
// ================
//
// This class is a container for all shared resources that may be needed
// by the drivers served by the Local Server.
//
// NOTES:
//
//	* ALL DECLARATIONS MUST BE STATIC HERE!! INSTANCES OF THIS CLASS MUST NEVER BE CREATED!
//
// Written by:	Bob Denny	29-May-2007
// Modified by Chris Rowland and Peter Simpson to hamdle multiple hardware devices March 2011
//
using ASCOM.Compatibility.Utilities;
using ASCOM.Standard.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ASCOM.Simulator
{
    /// <summary>
    /// The resources shared by all drivers and devices, in this example it's a serial port with a shared SendMessage method
    /// an idea for locking the message and handling connecting is given.
    /// In reality extensive changes will probably be needed.
    /// Multiple drivers means that several applications connect to the same hardware device, aka a hub.
    /// Multiple devices means that there are more than one instance of the hardware, such as two focusers.
    /// In this case there needs to be multiple instances of the hardware connector, each with it's own connection count.
    /// </summary>
    public static class SharedResources
    {
        //
        // Public access to shared resources
        //

        // ASCOM Trace Logger component
        internal static ITraceLogger Logger = new TraceLoggerCompat("", "CoverCalibratorSimulatorLS");

        internal static DeviceHardware DeviceInstance = new DeviceHardware(0, CoverCalibrator.ProgID);

        #region Multi Driver handling

        /// <summary>
        /// This is called in the driver Connect(true) property,
        /// it add the device id to the list of devices if it's not there and increments the device count.
        /// </summary>
        /// <param name="deviceId"></param>

        #endregion Multi Driver handling
    }

    /// <summary>
    /// Skeleton of a hardware class, all this does is hold a count of the connections,
    /// in reality extra code will be needed to handle the hardware in some way
    /// </summary>
    public class DeviceHardware : ASCOMSimulators.CoverCalibratorSimulator
    {
        private object lockObj = new object();
        private readonly string ProgID = string.Empty;

        internal DeviceHardware(int deviceNumber, string progID) : base(deviceNumber, SharedResources.Logger, new ProfileCompat(progID, "CoverCalibrator"))
        {
            ProgID = progID;
        }

        //Mark dispose requests or move to server shutdown?
        public new void Dispose()
        {
            if (!AnyClientConnected)
            {
                base.Dispose();
            }
        }

        public string GetUniqueDriverId()
        {
            int i = 0;
            string driverId = ProgID;
            while (driverConnections.ContainsKey(driverId))
            {
                driverId = ProgID + i.ToString();
                i++;
            }
            driverConnections.Add(driverId, false);
            return driverId;
        }

        private readonly Dictionary<string, bool> driverConnections = new Dictionary<string, bool>();

        public void ConnectClient(string uniqueID)
        {
            lock (lockObj)
            {
                if (!Connected)
                    Connected = true;
                if (!driverConnections.ContainsKey(uniqueID))
                    driverConnections.Add(uniqueID, true);
                else
                    driverConnections[uniqueID] = true;
            }
        }

        public void DisconnectClient(string uniqueID)
        {
            lock (lockObj)
            {
                if (driverConnections.ContainsKey(uniqueID))
                    driverConnections[uniqueID] = false;
                if (!AnyClientConnected)
                {
                    Connected = false;
                }
            }
        }

        public bool IsClientConnected(string uniqueID)
        {
            if (!driverConnections.ContainsKey(uniqueID)) return false;
            else return driverConnections[uniqueID];
        }

        private bool AnyClientConnected => driverConnections.Any(c => c.Value);
    }
}