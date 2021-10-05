using ASCOM.Common.Alpaca;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

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
        public async Task<ActionResult<StringResponse>> Action([DefaultValue(0)] int DeviceNumber, [Required][FromForm] string Action, [FromForm] string Parameters = "", [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            return await ProcessRequest(() => DriverManager.GetCoverCalibrator(DeviceNumber).Action(Action, Parameters), DriverManager.ServerTransactionID, ClientID, ClientTransactionID, $"Action: {Action}, Parameters {Parameters}");
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/CommandBlind")]
        public async Task<ActionResult<Response>> CommandBlind([DefaultValue(0)] int DeviceNumber, [Required][FromForm] string Command, [FromForm] bool Raw = false, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            return await ProcessRequest(() => DriverManager.GetCoverCalibrator(DeviceNumber).CommandBlind(Command, Raw), DriverManager.ServerTransactionID, ClientID, ClientTransactionID, $"Command {Command}, Raw {Raw}");
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/CommandBool")]
        public async Task<ActionResult<BoolResponse>> CommandBool([DefaultValue(0)] int DeviceNumber, [Required][FromForm] string Command, [FromForm] bool Raw = false, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            return await ProcessRequest(() => DriverManager.GetCoverCalibrator(DeviceNumber).CommandBool(Command, Raw), DriverManager.ServerTransactionID, ClientID, ClientTransactionID, $"Command={Command}, Raw={Raw}");
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/CommandString")]
        public async Task<ActionResult<StringResponse>> CommandString([DefaultValue(0)] int DeviceNumber, [Required][FromForm] string Command, [FromForm] bool Raw = false, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            return await ProcessRequest(() => DriverManager.GetCoverCalibrator(DeviceNumber).CommandString(Command, Raw), DriverManager.ServerTransactionID, ClientID, ClientTransactionID, $"Command={Command}, Raw={Raw}");
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/Connected")]
        public async Task<ActionResult<BoolResponse>> Connected([DefaultValue(0)]int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            return await ProcessRequest(() => DriverManager.GetCoverCalibrator(DeviceNumber).Connected, DriverManager.ServerTransactionID, ClientID, ClientTransactionID);
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/Connected")]
        public async Task<ActionResult<Response>> Connected([DefaultValue(0)] int DeviceNumber, [Required][FromForm] bool Connected, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            if (Connected || !AlpacaSettings.PreventRemoteDisconnects)
            {
                return await ProcessRequest(() => { DriverManager.GetCoverCalibrator(DeviceNumber).Connected = Connected; }, DriverManager.ServerTransactionID, ClientID, ClientTransactionID, $"Connected={Connected}");
            }
            return await ProcessRequest(() => { }, DriverManager.ServerTransactionID, ClientID, ClientTransactionID);
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/Description")]
        public async Task<ActionResult<StringResponse>> Description([DefaultValue(0)]int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            return await ProcessRequest(() => DriverManager.GetCoverCalibrator(DeviceNumber).Description, DriverManager.ServerTransactionID, ClientID, ClientTransactionID);
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/DriverInfo")]
        public async Task<ActionResult<StringResponse>> DriverInfo([DefaultValue(0)]int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            return await ProcessRequest(() => DriverManager.GetCoverCalibrator(DeviceNumber).DriverInfo, DriverManager.ServerTransactionID, ClientID, ClientTransactionID);
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/DriverVersion")]
        public async Task<ActionResult<StringResponse>> DriverVersion([DefaultValue(0)]int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            return await ProcessRequest(() => DriverManager.GetCoverCalibrator(DeviceNumber).DriverVersion, DriverManager.ServerTransactionID, ClientID, ClientTransactionID);
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/InterfaceVersion")]
        public async Task<ActionResult<IntResponse>> InterfaceVersion([DefaultValue(0)]int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            return await ProcessRequest(() => DriverManager.GetCoverCalibrator(DeviceNumber).InterfaceVersion, DriverManager.ServerTransactionID, ClientID, ClientTransactionID);
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/Name")]
        public async Task<ActionResult<StringResponse>> Name([DefaultValue(0)]int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            return await ProcessRequest(() => DriverManager.GetCoverCalibrator(DeviceNumber).Name, DriverManager.ServerTransactionID, ClientID, ClientTransactionID);
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/SupportedActions")]
        public async Task<ActionResult<StringListResponse>> SupportedActions([DefaultValue(0)]int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            return await ProcessRequest(() => DriverManager.GetCoverCalibrator(DeviceNumber).SupportedActions, DriverManager.ServerTransactionID, ClientID, ClientTransactionID);
        }

        #region IDisposable Members

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/Dispose")]
        public async Task<ActionResult<Response>> Dispose([DefaultValue(0)]int DeviceNumber, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            if (!AlpacaSettings.PreventRemoteDisposes)
            {
                return await ProcessRequest(() => { DriverManager.GetCoverCalibrator(DeviceNumber).Dispose(); }, DriverManager.ServerTransactionID, ClientID, ClientTransactionID);
            }
            return await ProcessRequest(() => { }, DriverManager.ServerTransactionID, ClientID, ClientTransactionID);
        }

        #endregion IDisposable Members

        #endregion Common Methods

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/CoverState")]
        public async Task<ActionResult<IntResponse>> CoverState([DefaultValue(0)]int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            return await ProcessRequest(() => (int)DriverManager.GetCoverCalibrator(DeviceNumber).CoverState, DriverManager.ServerTransactionID, ClientID, ClientTransactionID);
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/CalibratorState")]
        public async Task<ActionResult<IntResponse>> CalibratorState([DefaultValue(0)]int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            return await ProcessRequest(() => (int)DriverManager.GetCoverCalibrator(DeviceNumber).CalibratorState, DriverManager.ServerTransactionID, ClientID, ClientTransactionID);
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/Brightness")]
        public async Task<ActionResult<IntResponse>> Brightness([DefaultValue(0)]int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            return await ProcessRequest(() => DriverManager.GetCoverCalibrator(DeviceNumber).Brightness, DriverManager.ServerTransactionID, ClientID, ClientTransactionID);
        }

        [HttpGet]
        [Route(APIRoot + "{DeviceNumber}/MaxBrightness")]
        public async Task<ActionResult<IntResponse>> MaxBrightness([DefaultValue(0)]int DeviceNumber, uint ClientID = 0, uint ClientTransactionID = 0)
        {
            return await ProcessRequest(() => DriverManager.GetCoverCalibrator(DeviceNumber).MaxBrightness, DriverManager.ServerTransactionID, ClientID, ClientTransactionID);
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/OpenCover")]
        public async Task<ActionResult<Response>> OpenCover([DefaultValue(0)]int DeviceNumber, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            return await ProcessRequest(() => DriverManager.GetCoverCalibrator(DeviceNumber).OpenCover(), DriverManager.ServerTransactionID, ClientID, ClientTransactionID);
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/CloseCover")]
        public async Task<ActionResult<Response>> CloseCover([DefaultValue(0)]int DeviceNumber, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            return await ProcessRequest(() => DriverManager.GetCoverCalibrator(DeviceNumber).CloseCover(), DriverManager.ServerTransactionID, ClientID, ClientTransactionID);
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/HaltCover")]
        public async Task<ActionResult<Response>> HaltCover([DefaultValue(0)]int DeviceNumber, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            return await ProcessRequest(() => DriverManager.GetCoverCalibrator(DeviceNumber).HaltCover(), DriverManager.ServerTransactionID, ClientID, ClientTransactionID);
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/CalibratorOn")]
        public async Task<ActionResult<Response>> CalibratorOn([DefaultValue(0)]int DeviceNumber, [DefaultValue(0)][Required][FromForm] int Brightness, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            return await ProcessRequest(() => DriverManager.GetCoverCalibrator(DeviceNumber).CalibratorOn(Brightness), DriverManager.ServerTransactionID, ClientID, ClientTransactionID, $"Brightness={Brightness}");
        }

        [HttpPut]
        [Route(APIRoot + "{DeviceNumber}/CalibratorOff")]
        public async Task<ActionResult<Response>> CalibratorOff([DefaultValue(0)]int DeviceNumber, [FromForm] uint ClientID = 0, [FromForm] uint ClientTransactionID = 0)
        {
            return await ProcessRequest(() => DriverManager.GetCoverCalibrator(DeviceNumber).CalibratorOff(), DriverManager.ServerTransactionID, ClientID, ClientTransactionID);
        }
    }
}