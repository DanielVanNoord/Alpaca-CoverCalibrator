using ASCOM.Alpaca.Interfaces;
using ASCOM.Alpaca.Responses;
using ASCOM.Standard.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Alpaca.CoverCalibrator
{
    [ApiController]
    public class CoverCalibrator : Controller
    {
        private const string APIRoot = "api/v1/covercalibrator/";

        #region Common Methods

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/Action")]
        public StringResponse Action(int DeviceNumber, [FromForm] string Action, [FromForm] string Parameters, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                return new StringResponse(ClientTransactionID, ServerManager.ServerTransactionID, DeviceManager.GetCoverCalibrator(DeviceNumber).Action(Action, Parameters));
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<StringResponse>(ex, ClientTransactionID, ServerManager.ServerTransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/CommandBlind")]
        public Response CommandBlind(int DeviceNumber, [FromForm] string Command, [FromForm] bool Raw = false, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetCoverCalibrator(DeviceNumber).CommandBlind(Command, Raw);
                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = ServerManager.ServerTransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, ServerManager.ServerTransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/CommandBool")]
        public BoolResponse CommandBool(int DeviceNumber, [FromForm] string Command, [FromForm] bool Raw = false, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                return new BoolResponse(ClientTransactionID, ServerManager.ServerTransactionID, DeviceManager.GetCoverCalibrator(DeviceNumber).CommandBool(Command, Raw));
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<BoolResponse>(ex, ClientTransactionID, ServerManager.ServerTransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/CommandString")]
        public StringResponse CommandString(int DeviceNumber, [FromForm] string Command, [FromForm] bool Raw = false, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                return new StringResponse(ClientTransactionID, ServerManager.ServerTransactionID, DeviceManager.GetCoverCalibrator(DeviceNumber).CommandString(Command, Raw));
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<StringResponse>(ex, ClientTransactionID, ServerManager.ServerTransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/Connected")]
        public BoolResponse Connected(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
        {
            try
            {
                return new BoolResponse(ClientTransactionID, ServerManager.ServerTransactionID, DeviceManager.GetCoverCalibrator(DeviceNumber).Connected);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<BoolResponse>(ex, ClientTransactionID, ServerManager.ServerTransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/Connected")]
        public Response Connected(int DeviceNumber, [FromForm] bool Connected, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                
                DeviceManager.GetCoverCalibrator(DeviceNumber).Connected = Connected;

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = ServerManager.ServerTransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, ServerManager.ServerTransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/Description")]
        public StringResponse Description(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
        {
            try
            {
                return new StringResponse(ClientTransactionID, ServerManager.ServerTransactionID, DeviceManager.GetCoverCalibrator(DeviceNumber).Description);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<StringResponse>(ex, ClientTransactionID, ServerManager.ServerTransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/DriverInfo")]
        public StringResponse DriverInfo(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
        {
            try
            {
                return new StringResponse(ClientTransactionID, ServerManager.ServerTransactionID, DeviceManager.GetCoverCalibrator(DeviceNumber).DriverInfo);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<StringResponse>(ex, ClientTransactionID, ServerManager.ServerTransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/DriverVersion")]
        public StringResponse DriverVersion(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
        {
            try
            {
                return new StringResponse(ClientTransactionID, ServerManager.ServerTransactionID, DeviceManager.GetCoverCalibrator(DeviceNumber).DriverVersion);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<StringResponse>(ex, ClientTransactionID, ServerManager.ServerTransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/InterfaceVersion")]
        public IntResponse InterfaceVersion(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
        {
            try
            {
                return new IntResponse(ClientTransactionID, ServerManager.ServerTransactionID, DeviceManager.GetCoverCalibrator(DeviceNumber).InterfaceVersion);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<IntResponse>(ex, ClientTransactionID, ServerManager.ServerTransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/Name")]
        public StringResponse Name(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
        {
            try
            {
                return new StringResponse(ClientTransactionID, ServerManager.ServerTransactionID, DeviceManager.GetCoverCalibrator(DeviceNumber).Name);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<StringResponse>(ex, ClientTransactionID, ServerManager.ServerTransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/SupportedActions")]
        public StringListResponse SupportedActions(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
        {
            try
            {
                return new StringListResponse(ClientTransactionID, ServerManager.ServerTransactionID, new List<string>(DeviceManager.GetCoverCalibrator(DeviceNumber).SupportedActions.Cast<string>().ToList()));
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<StringListResponse>(ex, ClientTransactionID, ServerManager.ServerTransactionID);
            }
        }

        #region IDisposable Members

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/Dispose")]
        public Response Dispose(int DeviceNumber, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                //ToDo Do not allow remote Dispose
                DeviceManager.GetCoverCalibrator(DeviceNumber).Dispose();

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = ServerManager.ServerTransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, ServerManager.ServerTransactionID);
            }
        }

        #endregion IDisposable Members

        #endregion Common Methods

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/CoverState")]
        public CoverStatusResponse CoverState(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
        {
            try
            {
                return new CoverStatusResponse(ClientTransactionID, ServerManager.ServerTransactionID, DeviceManager.GetCoverCalibrator(DeviceNumber).CoverState);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<CoverStatusResponse>(ex, ClientTransactionID, ServerManager.ServerTransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/CalibratorState")]
        public CalibratorStatusResponse CalibratorState(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
        {
            try
            {
                return new CalibratorStatusResponse(ClientTransactionID, ServerManager.ServerTransactionID, DeviceManager.GetCoverCalibrator(DeviceNumber).CalibratorState);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<CalibratorStatusResponse>(ex, ClientTransactionID, ServerManager.ServerTransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/Brightness")]
        public IntResponse Brightness(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
        {
            try
            {
                return new IntResponse(ClientTransactionID, ServerManager.ServerTransactionID, DeviceManager.GetCoverCalibrator(DeviceNumber).Brightness);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<IntResponse>(ex, ClientTransactionID, ServerManager.ServerTransactionID);
            }
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/MaxBrightness")]
        public IntResponse MaxBrightness(int DeviceNumber, int ClientID = -1, uint ClientTransactionID = 0)
        {
            try
            {
                return new IntResponse(ClientTransactionID, ServerManager.ServerTransactionID, DeviceManager.GetCoverCalibrator(DeviceNumber).MaxBrightness);
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<IntResponse>(ex, ClientTransactionID, ServerManager.ServerTransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/OpenCover")]
        public Response OpenCover(int DeviceNumber, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetCoverCalibrator(DeviceNumber).OpenCover();

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = ServerManager.ServerTransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, ServerManager.ServerTransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/CloseCover")]
        public Response CloseCover(int DeviceNumber, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetCoverCalibrator(DeviceNumber).CloseCover();

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = ServerManager.ServerTransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, ServerManager.ServerTransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/HaltCover")]
        public Response HaltCover(int DeviceNumber, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetCoverCalibrator(DeviceNumber).HaltCover();

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = ServerManager.ServerTransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, ServerManager.ServerTransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/CalibratorOn")]
        public Response CalibratorOn(int DeviceNumber, [FromForm] int Brightness, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetCoverCalibrator(DeviceNumber).CalibratorOn(Brightness);
                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = ServerManager.ServerTransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, ServerManager.ServerTransactionID);
            }
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/CalibratorOff")]
        public Response CalibratorOff(int DeviceNumber, [FromForm] int ClientID = -1, [FromForm] uint ClientTransactionID = 0)
        {
            try
            {
                DeviceManager.GetCoverCalibrator(DeviceNumber).CalibratorOff();

                return new Response() { ClientTransactionID = ClientTransactionID, ServerTransactionID = ServerManager.ServerTransactionID };
            }
            catch (Exception ex)
            {
                return ResponseHelpers.ExceptionResponseBuilder<Response>(ex, ClientTransactionID, ServerManager.ServerTransactionID);
            }
        }
    }
}
