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
using System;
using System.Collections.Generic;
using System.Text;
using ASCOM;
using ASCOM.Compatibility.Interfaces;

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
        // object used for locking to prevent multiple drivers accessing common code at the same time
        private static readonly object lockObject = new object();

        // Shared serial port. This will allow multiple drivers to use one single serial port.
        private static int s_z = 0;     // counter for the number of connections to the serial port

        //
        // Public access to shared resources
        //

        // Simulator components 
        internal static ITraceLoggerFull Logger = new ASCOM.Compatibility.Utilities.TraceLoggerCompat("", "CoverCalibratorSimulatorLS"); // ASCOM Trace Logger component
        internal static IProfileFull Profile = new ASCOM.Compatibility.Utilities.ProfileCompat(); //Access to device settings

        #region Multi Driver handling
        // this section illustrates how multiple drivers could be handled,
        // it's for drivers where multiple connections to the hardware can be made and ensures that the
        // hardware is only disconnected from when all the connected devices have disconnected.

        // It is NOT a complete solution!  This is to give ideas of what can - or should be done.
        //
        // An alternative would be to move the hardware control here, handle connecting and disconnecting,
        // and provide the device with a suitable connection to the hardware.
        //
        /// <summary>
        /// dictionary carrying device connections.
        /// The Key is the connection number that identifies the device, it could be the COM port name,
        /// USB ID or IP Address, the Value is the DeviceHardware class
        /// </summary>
        private static Dictionary<string, DeviceHardware> connectedDevices = new Dictionary<string, DeviceHardware>();

        /// <summary>
        /// This is called in the driver Connect(true) property,
        /// it add the device id to the list of devices if it's not there and increments the device count.
        /// </summary>
        /// <param name="deviceId"></param>
        public static void Connect(string deviceId)
        {
            lock (lockObject)
            {
                if (!connectedDevices.ContainsKey(deviceId))
                    connectedDevices.Add(deviceId, new DeviceHardware());
                connectedDevices[deviceId].count++;       // increment the value
            }
        }

        public static void Disconnect(string deviceId)
        {
            lock (lockObject)
            {
                if (connectedDevices.ContainsKey(deviceId))
                {
                    connectedDevices[deviceId].count--;
                    if (connectedDevices[deviceId].count <= 0)
                        connectedDevices.Remove(deviceId);
                }
            }
        }

        public static bool IsConnected(string deviceId)
        {
            if (connectedDevices.ContainsKey(deviceId))
                return (connectedDevices[deviceId].count > 0);
            else
                return false;
        }

        #endregion

    }

    /// <summary>
    /// Skeleton of a hardware class, all this does is hold a count of the connections,
    /// in reality extra code will be needed to handle the hardware in some way
    /// </summary>
    public class DeviceHardware
    {
        internal int count { set; get; }

        internal DeviceHardware()
        {
            count = 0;
        }
    }
}
