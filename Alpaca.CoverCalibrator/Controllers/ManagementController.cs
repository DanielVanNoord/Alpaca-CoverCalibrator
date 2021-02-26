using ASCOM.Alpaca.Responses;
using ASCOM.Standard.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Alpaca.CoverCalibrator
{
    [ServiceFilter(typeof(AuthorizationFilter))]
    [ApiController]
    public class ManagementController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        [Route("management/apiversions")]
        public IntListResponse ApiVersions(uint ClientID = 0, uint ClientTransactionID = 0)
        {
            var TransactionID = DriverManager.ServerTransactionID;
            Logging.LogAPICall(HttpContext.Connection.RemoteIpAddress, HttpContext.Request.Path.ToString(), ClientID, ClientTransactionID, TransactionID);
            return new IntListResponse(ClientTransactionID, TransactionID, AlpacaSettings.APIVersions);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("management/v1/description")]
        public AlpacaDescriptionResponse Description(uint ClientID = 0, uint ClientTransactionID = 0)
        {


            var TransactionID = DriverManager.ServerTransactionID;
            Logging.LogAPICall(HttpContext.Connection.RemoteIpAddress, HttpContext.Request.Path.ToString(), ClientID, ClientTransactionID, TransactionID);

            return new AlpacaDescriptionResponse(ClientTransactionID, TransactionID, new AlpacaDeviceDescription(AlpacaSettings.DriverName, AlpacaSettings.Manufacturer, AlpacaSettings.Version, AlpacaSettings.Location));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("management/v1/configureddevices")]
        public AlpacaConfiguredDevicesResponse ConfiguredDevices(uint ClientID = 0, uint ClientTransactionID = 0)
        {
            List<AlpacaConfiguredDevice> devices = new List<AlpacaConfiguredDevice>();
            try
            {
                    devices.Add((DriverManager.GetCoverCalibrator(0) as IAlpacaDevice).Configuration);
            }
            catch(Exception ex)
            {
                Logging.Log.LogError(ex.Message);
            }

            var TransactionID = DriverManager.ServerTransactionID;
            Logging.LogAPICall(HttpContext.Connection.RemoteIpAddress, HttpContext.Request.Path.ToString(), ClientID, ClientTransactionID, TransactionID);

            return new AlpacaConfiguredDevicesResponse(ClientTransactionID, TransactionID, devices);
        }
    }
}