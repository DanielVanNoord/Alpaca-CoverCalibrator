Alpaca REST server with Blazor UI

Folders
* Controllers - Asp.Net MVC controllers exposing the Alpaca interfaces.
  *  Device controllers access the device through the device manager and expose the Alpaca API. They should work with any device that exposes the correct API with minimal (ideally no) changes.
  * The Management Controller exposes the Alpaca Management API. Some customization may be required to expose a servers custom information. Although it may be better to put that information in the ServerManager class and access those constants.
  * The SetupController may contain redirects from the standard API for setup to pages that actually have the UI. Because Blazor supports multiple routes for Blazor pages this should be not be needed very often.
* Pages - Blazor pages containing device setup, server setup and device control. Currently the Alpaca setup api routes here. These allow for custom UI across all platforms.