using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using ASCOM.Utilities;
using ASCOM.Simulator;
using ASCOM.DeviceInterface;
using System.Diagnostics;
using ASCOM.Standard.Interfaces;

namespace ASCOM.Simulator
{
    [ComVisible(false)]					// Form not registered for COM!
    public partial class SetupDialogForm : Form
    {
        ITraceLogger TL;

        readonly CoverCalibratorSimulator.CoverCalibratorSimulator CovCal;

        public SetupDialogForm(CoverCalibratorSimulator.CoverCalibratorSimulator cal, ITraceLogger traceLogger)
        {
            InitializeComponent();
            TL = traceLogger;
            CovCal = cal;

            // Initialise current values of user settings from the ASCOM Profile
            InitUI();

        }

        private void CmdOK_Click(object sender, EventArgs e) // OK button event handler
        {
            // Update the state variables with results from the dialogue
            TL.Enabled = chkTrace.Checked;
            CovCal.MaxBrightnessValue = Decimal.ToInt32(NumMaxBrightness.Value);
            CovCal.CalibratorStablisationTimeValue = Decimal.ToDouble(NumCalibratorStablisationTime.Value);
            CovCal.CoverOpeningTimeValue = Decimal.ToDouble(NumCoverOpeningTime.Value);
            Enum.TryParse<ASCOM.Standard.Interfaces.CalibratorStatus>(CmbCalibratorInitialisationState.SelectedItem.ToString(), out CovCal.CalibratorStateInitialisationValue);
            Enum.TryParse<ASCOM.Standard.Interfaces.CoverStatus>(CmbCoverInitialisationState.SelectedItem.ToString(), out CovCal.CoverStateInitialisationValue);
        }

        private void CmdCancel_Click(object sender, EventArgs e) // Cancel button event handler
        {
            Close();
        }

        private void BrowseToAscom(object sender, EventArgs e) // Click on ASCOM logo event handler
        {
            try
            {
                Process.Start("https://ascom-standards.org/");
            }
            catch (Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        private void InitUI()
        {
            chkTrace.Checked = TL.Enabled;
            NumMaxBrightness.Value = (decimal)CovCal.MaxBrightnessValue;
            NumCalibratorStablisationTime.Value = (decimal)CovCal.CalibratorStablisationTimeValue;
            NumCoverOpeningTime.Value = (decimal)CovCal.CoverOpeningTimeValue;
            CmbCalibratorInitialisationState.SelectedItem = CovCal.CalibratorStateInitialisationValue.ToString();
            CmbCoverInitialisationState.SelectedItem = CovCal.CoverStateInitialisationValue.ToString();

            LblSynchBehaviourTime.Text = $"* Methods will be synchronous from 0.0 and {CoverCalibrator.SYNCHRONOUS_BEHAVIOUR_LIMIT.ToString("0.0")} seconds and asynchronous above this.";
        }

        private void SetupDialogForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }
    }
}