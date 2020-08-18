using ASCOM.Alpaca.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Alpaca.CoverCalibrator
{
    [ApiController]
    public class ManagementController : Controller
    {
        private static uint transactionID = 0;
      
        private static uint TransactionID
        {
            get
            {
                return transactionID++;
            }
        }

        [HttpGet]
        [Route("management/apiversions")]
        public IntListResponse ApiVersions(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
        {
            return new IntListResponse(ClientTransactionID, TransactionID, new int[1] { 1 });
        }

        [HttpGet]
        [Route("management/v1/description")]
        public AlpacaDescriptionResponse Description(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
        {
            List<AlpacaConfiguredDevice> configuredDevices = new List<AlpacaConfiguredDevice>() { };

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.ProductVersion;
            return new AlpacaDescriptionResponse(ClientTransactionID, TransactionID, new AlpacaDeviceDescription("Alpaca server for Cover Calibrator simulator", "ASCOM Initiative", version, "Unknown"));
        }

        [HttpGet]
        [Route("management/v1/configureddevices")]
        public AlpacaConfiguredDevicesResponse ConfiguredDevices(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
        {
            List<AlpacaConfiguredDevice> devices = new List<AlpacaConfiguredDevice>();
            try
            {
                    devices.Add(new AlpacaConfiguredDevice(DeviceManager.GetCoverCalibrator(0).Name, "CoverCalibrator", 0, "Temp-Fix-This"));
            }
            catch(Exception ex)
            {

            }
            return new AlpacaConfiguredDevicesResponse(ClientTransactionID, TransactionID, devices);
        }
    }
}