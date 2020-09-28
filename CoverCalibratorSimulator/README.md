NetStandard 2.0 library for device control 
* Device control code can be stored in separate libraries from the actual drivers. By exposing the ASCOM Standard interface for a device type both the Alpaca and ASCOM driver projects can work with custom code with minimal changes. 
* By targeting Netstandard or dual targeting the library can be called by both the .Net Framework ASCOM COM driver and the .Net Core Alpaca driver.
* Platform specific code (like the ASCOM profile registry) can be accessed through dependency injection
* OS specific behavior can be managed using System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform()
* Device specific settings and functions can be exposed and called directly from setup pages in Alpaca and the SetupDialog in ASCOM
* Unit and integration tests can be run directly against this project(s)