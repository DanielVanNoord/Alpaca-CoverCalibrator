using ASCOM.Compatibility.Interfaces;
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
    public class CoverCalibrator : CoverCalibratorSimulator.CoverCalibrator, ASCOM.DeviceInterface.ICoverCalibratorV1
    {

        internal static string ProgID = "ASCOM.SimulatorLS.CoverCalibrator";
        public CoverCalibrator() : base(SharedResources.Logger, SharedResources.Profile)
        {
            // We increment the global count of objects.
            Server.CountObject();
        }

        ~CoverCalibrator()
        {
            // We decrement the global count of objects.
            Server.UncountObject();
            // We then immediately test to see if we the conditions
            // are right to attempt to terminate this server application.
            Server.ExitIf();
        }

        ArrayList ICoverCalibratorV1.SupportedActions => new ArrayList(base.SupportedActions.ToList());

        CoverStatus ICoverCalibratorV1.CoverState => (CoverStatus) base.CoverState;

        CalibratorStatus ICoverCalibratorV1.CalibratorState => (CalibratorStatus) base.CalibratorState;

        public void SetupDialog()
        {
            // consider only showing the setup dialogue if not connected
            // or call a different dialogue if connected
            if (Connected) MessageBox.Show("Already connected, just press OK");

            using (SetupDialogForm F = new SetupDialogForm(this, SharedResources.Logger))
            {
                var result = F.ShowDialog();
                if (result == DialogResult.OK)
                {
                    base.WriteProfile(); // Persist device configuration values to the ASCOM Profile store
                }
            }
        }
    }
}