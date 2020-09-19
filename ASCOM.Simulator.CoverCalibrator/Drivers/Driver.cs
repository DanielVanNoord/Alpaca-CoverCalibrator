using ASCOM.DeviceInterface;
using System.Collections;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ASCOM.Simulator
{
    [Guid("27FC046E-167E-467B-9B82-702941571A78")]  // set by the template
    [ProgId("ASCOM.SimulatorLS.CoverCalibrator")]
    [ServedClassName("CoverCalibrator LS Simulator")]
    [ClassInterface(ClassInterfaceType.None)]
    public class CoverCalibrator : ReferenceCountedObjectBase, ICoverCalibratorV1
    {

        internal static string ProgID = "ASCOM.SimulatorLS.CoverCalibrator";

        // A unique id for this instance of the driver
        internal readonly string DriverID = SharedResources.DeviceInstance.GetUniqueDriverId();


        public CoverCalibrator() : base()
        {
        }

        public bool Connected
        {
            get => SharedResources.DeviceInstance.IsClientConnected(DriverID); 
            set
            {
                if (value)
                {
                    SharedResources.DeviceInstance.ConnectClient(DriverID);
                }
                else
                {
                    SharedResources.DeviceInstance.DisconnectClient(DriverID);
                }
            }
        }

        public string Description => SharedResources.DeviceInstance.Description;

        public string DriverInfo => SharedResources.DeviceInstance.DriverInfo;

        public string DriverVersion => SharedResources.DeviceInstance.DriverVersion;

        public short InterfaceVersion => SharedResources.DeviceInstance.InterfaceVersion;

        public string Name => SharedResources.DeviceInstance.Name;

        public int Brightness => SharedResources.DeviceInstance.Brightness;

        public int MaxBrightness => SharedResources.DeviceInstance.MaxBrightness;

        ArrayList ICoverCalibratorV1.SupportedActions => new ArrayList(SharedResources.DeviceInstance.SupportedActions.ToList());

        CoverStatus ICoverCalibratorV1.CoverState => (CoverStatus)SharedResources.DeviceInstance.CoverState;

        CalibratorStatus ICoverCalibratorV1.CalibratorState => (CalibratorStatus)SharedResources.DeviceInstance.CalibratorState;

        public string Action(string ActionName, string ActionParameters)
        {
            return SharedResources.DeviceInstance.Action(ActionName, ActionParameters);
        }

        public void CalibratorOff()
        {
            SharedResources.DeviceInstance.CalibratorOff();
        }

        public void CalibratorOn(int Brightness)
        {
            SharedResources.DeviceInstance.CalibratorOn(Brightness);
        }

        public void CloseCover()
        {
            SharedResources.DeviceInstance.CloseCover();
        }

        public void CommandBlind(string Command, bool Raw = false)
        {
            SharedResources.DeviceInstance.CommandBlind(Command, Raw);
        }

        public bool CommandBool(string Command, bool Raw = false)
        {
            return SharedResources.DeviceInstance.CommandBool(Command, Raw);
        }

        public string CommandString(string Command, bool Raw = false)
        {
            return SharedResources.DeviceInstance.CommandString(Command, Raw);
        }

        public void Dispose()
        {
            SharedResources.DeviceInstance.DisconnectClient(DriverID);
            SharedResources.DeviceInstance.Dispose();
        }

        public void HaltCover()
        {
            SharedResources.DeviceInstance.HaltCover();
        }

        public void OpenCover()
        {
            SharedResources.DeviceInstance.OpenCover();
        }

        public void SetupDialog()
        {
            // consider only showing the setup dialogue if not connected
            // or call a different dialogue if connected
            if (Connected) MessageBox.Show("Already connected, just press OK");

            using (SetupDialogForm F = new SetupDialogForm(SharedResources.Logger))
            {
                var result = F.ShowDialog();
                if (result == DialogResult.OK)
                {
                    SharedResources.DeviceInstance.WriteProfile(); // Persist device configuration values to the ASCOM Profile store
                }
            }
        }
    }
}