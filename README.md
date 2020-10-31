Alpaca-CoverCalibrator

* Alpaca.CoverCalibrator is an ASP.Net Core project. It exposes the Alpaca REST API and contains a Blazor web ui for device control and configuration. It uses the cross platform ASCOM Standard libraries to provide logging and configuration.
* ASCOM.Simulator.CoverCalibrator is an ASCOM Local Server driver. It exposes the COM API for Windows ASCOM clients. It uses the standard ASCOM Platform logging and configuration libraries provided by a compatibility wrapper.
* CoverCalibratorSimulator is a .Net Standard project that contains the device specific code and exposes the ASCOM interface. Both the Alpaca and the ASCOM project use this to control the simulator. The logging and configuration libraries are injected into the project, allowing it to use different libraries depending on the target platform.

ToDo
* Add library and project licenses.
* Add readmes and comments
* Continue move developer set constants to a central location
* Cleanup code / namespaces
* Change Alpaca Port from test port of 5000
* Add packaging / deployment
* Clarify proper handling of ASCOM dispose in Alpaca
* Refactor Auth into library
  * Add cookie revoke
  * Add reset password to command line arguments
* Consider refactoring Alpaca API controllers into library
* Consider switching to mutex locks to detect running program to work better with the new .Net Core Single File deployment
* Add set logging level to ASCOM and Alpaca UI