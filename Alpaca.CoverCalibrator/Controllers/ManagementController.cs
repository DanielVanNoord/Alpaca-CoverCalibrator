using ASCOM.Alpaca.Responses;
using ASCOM.Standard.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Alpaca.CoverCalibrator
{
    [ApiController]
    public class ManagementController : Controller
    {
        [HttpGet]
        [Route("management/apiversions")]
        public IntListResponse ApiVersions(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
        {
            var TransactionID = ServerManager.ServerTransactionID;
            Logging.LogAPICall(HttpContext.Connection.RemoteIpAddress, HttpContext.Request.Path.ToString(), ClientID, ClientTransactionID, TransactionID);
            return new IntListResponse(ClientTransactionID, TransactionID, new int[1] { 1 });
        }

        [HttpGet]
        [Route("management/v1/description")]
        public AlpacaDescriptionResponse Description(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            string version = FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion;

            var TransactionID = ServerManager.ServerTransactionID;
            Logging.LogAPICall(HttpContext.Connection.RemoteIpAddress, HttpContext.Request.Path.ToString(), ClientID, ClientTransactionID, TransactionID);

            return new AlpacaDescriptionResponse(ClientTransactionID, TransactionID, new AlpacaDeviceDescription("Alpaca server for Cover Calibrator simulator", "ASCOM Initiative", version, ServerSettings.Location));
        }

        [HttpGet]
        [Route("management/v1/configureddevices")]
        public AlpacaConfiguredDevicesResponse ConfiguredDevices(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
        {
            List<AlpacaConfiguredDevice> devices = new List<AlpacaConfiguredDevice>();
            try
            {
                    devices.Add((DeviceManager.GetCoverCalibrator(0) as ASCOMSimulators.IAlpacaDevice).Configuration);
            }
            catch(Exception ex)
            {
                Logging.LogError(ex.Message);
            }

            var TransactionID = ServerManager.ServerTransactionID;
            Logging.LogAPICall(HttpContext.Connection.RemoteIpAddress, HttpContext.Request.Path.ToString(), ClientID, ClientTransactionID, TransactionID);

            return new AlpacaConfiguredDevicesResponse(ClientTransactionID, TransactionID, devices);
        }
    }
}