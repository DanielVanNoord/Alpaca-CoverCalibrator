using ASCOM.Alpaca.Responses;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Alpaca.CoverCalibrator
{
    [ServiceFilter(typeof(AuthorizationFilter))]
    [ApiController]
    public class CoverCalibrator : AlpacaController
    {
        private const string APIRoot = "api/v1/covercalibrator/";

        #region Common Methods

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/Action")]
        public ActionResult<StringResponse> Action([Required][FromForm] string Action, [Required][FromForm] string Parameters, int DeviceNumber = 0, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            return ProcessRequest(() => DriverManager.GetCoverCalibrator(DeviceNumber).Action(Action, Parameters), DriverManager.ServerTransactionID, ClientID, ClientTransactionID, $"Action: {Action}, Parameters {Parameters}");
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/CommandBlind")]
        public ActionResult<Response> CommandBlind([Required][FromForm] string Command, int DeviceNumber = 0, [FromForm] bool Raw = false, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            return ProcessRequest(() => DriverManager.GetCoverCalibrator(DeviceNumber).CommandBlind(Command, Raw), DriverManager.ServerTransactionID, ClientID, ClientTransactionID, $"Command {Command}, Raw {Raw}");
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/CommandBool")]
        public ActionResult<BoolResponse> CommandBool([Required][FromForm] string Command, int DeviceNumber = 0, [FromForm] bool Raw = false, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            return ProcessRequest(() => DriverManager.GetCoverCalibrator(DeviceNumber).CommandBool(Command, Raw), DriverManager.ServerTransactionID, ClientID, ClientTransactionID, $"Command={Command}, Raw={Raw}");
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/CommandString")]
        public ActionResult<StringResponse> CommandString([Required][FromForm] string Command, int DeviceNumber = 0, [FromForm] bool Raw = false, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            return ProcessRequest(() => DriverManager.GetCoverCalibrator(DeviceNumber).CommandString(Command, Raw), DriverManager.ServerTransactionID, ClientID, ClientTransactionID, $"Command={Command}, Raw={Raw}");
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/Connected")]
        public ActionResult<BoolResponse> Connected(int DeviceNumber = 0, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            return ProcessRequest(() => DriverManager.GetCoverCalibrator(DeviceNumber).Connected, DriverManager.ServerTransactionID, ClientID, ClientTransactionID);
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/Connected")]
        public ActionResult<Response> Connected([Required][FromForm] bool Connected, int DeviceNumber = 0, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            if (Connected || !AlpacaSettings.PreventRemoteDisconnects)
            {
                return ProcessRequest(() => { DriverManager.GetCoverCalibrator(DeviceNumber).Connected = Connected; }, DriverManager.ServerTransactionID, ClientID, ClientTransactionID, $"Connected={Connected}");
            }
            return ProcessRequest(() => { }, DriverManager.ServerTransactionID, ClientID, ClientTransactionID);
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/Description")]
        public ActionResult<StringResponse> Description(int DeviceNumber = 0, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            return ProcessRequest(() => DriverManager.GetCoverCalibrator(DeviceNumber).Description, DriverManager.ServerTransactionID, ClientID, ClientTransactionID);
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/DriverInfo")]
        public ActionResult<StringResponse> DriverInfo(int DeviceNumber = 0, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            return ProcessRequest(() => DriverManager.GetCoverCalibrator(DeviceNumber).DriverInfo, DriverManager.ServerTransactionID, ClientID, ClientTransactionID);
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/DriverVersion")]
        public ActionResult<StringResponse> DriverVersion(int DeviceNumber = 0, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            return ProcessRequest(() => DriverManager.GetCoverCalibrator(DeviceNumber).DriverVersion, DriverManager.ServerTransactionID, ClientID, ClientTransactionID);
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/InterfaceVersion")]
        public ActionResult<IntResponse> InterfaceVersion(int DeviceNumber = 0, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            return ProcessRequest(() => DriverManager.GetCoverCalibrator(DeviceNumber).InterfaceVersion, DriverManager.ServerTransactionID, ClientID, ClientTransactionID);
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/Name")]
        public ActionResult<StringResponse> Name(int DeviceNumber = 0, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            return ProcessRequest(() => DriverManager.GetCoverCalibrator(DeviceNumber).Name, DriverManager.ServerTransactionID, ClientID, ClientTransactionID);
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/SupportedActions")]
        public ActionResult<StringListResponse> SupportedActions(int DeviceNumber = 0, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            return ProcessRequest(() => DriverManager.GetCoverCalibrator(DeviceNumber).SupportedActions, DriverManager.ServerTransactionID, ClientID, ClientTransactionID);
        }

        #region IDisposable Members

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/Dispose")]
        public ActionResult<Response> Dispose(int DeviceNumber = 0, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            if (!AlpacaSettings.PreventRemoteDisposes)
            {
                return ProcessRequest(() => { DriverManager.GetCoverCalibrator(DeviceNumber).Dispose(); }, DriverManager.ServerTransactionID, ClientID, ClientTransactionID);
            }
            return ProcessRequest(() => { }, DriverManager.ServerTransactionID, ClientID, ClientTransactionID);
        }

        #endregion IDisposable Members

        #endregion Common Methods

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/CoverState")]
        public ActionResult<IntResponse> CoverState(int DeviceNumber = 0, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            return ProcessRequest(() => (int)DriverManager.GetCoverCalibrator(DeviceNumber).CoverState, DriverManager.ServerTransactionID, ClientID, ClientTransactionID);
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/CalibratorState")]
        public ActionResult<IntResponse> CalibratorState(int DeviceNumber = 0, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            return ProcessRequest(() => (int)DriverManager.GetCoverCalibrator(DeviceNumber).CalibratorState, DriverManager.ServerTransactionID, ClientID, ClientTransactionID);
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/Brightness")]
        public ActionResult<IntResponse> Brightness(int DeviceNumber = 0, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            return ProcessRequest(() => DriverManager.GetCoverCalibrator(DeviceNumber).Brightness, DriverManager.ServerTransactionID, ClientID, ClientTransactionID);
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/MaxBrightness")]
        public ActionResult<IntResponse> MaxBrightness(int DeviceNumber = 0, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            return ProcessRequest(() => DriverManager.GetCoverCalibrator(DeviceNumber).MaxBrightness, DriverManager.ServerTransactionID, ClientID, ClientTransactionID);
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/OpenCover")]
        public ActionResult<Response> OpenCover(int DeviceNumber = 0, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            return ProcessRequest(() => DriverManager.GetCoverCalibrator(DeviceNumber).OpenCover(), DriverManager.ServerTransactionID, ClientID, ClientTransactionID);
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/CloseCover")]
        public ActionResult<Response> CloseCover(int DeviceNumber = 0, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            return ProcessRequest(() => DriverManager.GetCoverCalibrator(DeviceNumber).CloseCover(), DriverManager.ServerTransactionID, ClientID, ClientTransactionID);
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/HaltCover")]
        public ActionResult<Response> HaltCover(int DeviceNumber = 0, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            return ProcessRequest(() => DriverManager.GetCoverCalibrator(DeviceNumber).HaltCover(), DriverManager.ServerTransactionID, ClientID, ClientTransactionID);
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/CalibratorOn")]
        public ActionResult<Response> CalibratorOn(int DeviceNumber = 0, [Required][FromForm] int Brightness = 0, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            return ProcessRequest(() => DriverManager.GetCoverCalibrator(DeviceNumber).CalibratorOn(Brightness), DriverManager.ServerTransactionID, ClientID, ClientTransactionID, $"Brightness={Brightness}");
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/CalibratorOff")]
        public ActionResult<Response> CalibratorOff(int DeviceNumber = 0, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            return ProcessRequest(() => DriverManager.GetCoverCalibrator(DeviceNumber).CalibratorOff(), DriverManager.ServerTransactionID, ClientID, ClientTransactionID);
        }
    }
}