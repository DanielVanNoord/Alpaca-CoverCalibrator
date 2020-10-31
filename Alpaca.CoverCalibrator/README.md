# Alpaca REST server with Blazor UI
Notes and open questions

## Folders
* Authorization - Authorization filters for controllers and a user server for authenticating users.
* Controllers - Asp.Net MVC controllers exposing the Alpaca interfaces.
  *  Device controllers access the device through the device manager and expose the Alpaca API. They should work with any device that exposes the correct Interface with minimal (ideally no) changes.
  * The Management Controller exposes the Alpaca Management API. Some customization may be required to expose a servers custom information. Development note, it may be better to put that information in the ServerManager class and access those constants.
  * The SetupController is only needed under special circumstances. It may contain redirects from the standard APIs for setup to pages that actually have the UI. Blazor supports multiple routes for Blazor pages so you can directly route to pages by adding the path directly to the page (see the pages section). If you have multiple different models of the same device type (IE multiple focuser models) with different setup pages you can check the type of the requested device and use redirects to the correct setup page. Alternatively you can load different components on the actual setup page based on the device's features.
* Discovery - Wrappers and an implementation of the Alpaca Discovery Protocol. Some of this will likely be removed when the discovery library is available. 
* Pages - Blazor pages containing device setup, server setup and device control. Currently the Alpaca setup api routes here. These allow for custom UI across all platforms.
* Shared - Blazor components that are shared between other Blazor pages. Currently this consists of the Navigation Menu and main layout. Links to all pages can be added to NavMenu so they can be accessed from the menu.
* wwwroot - the root folder for www access. Mostly contains the favicon and css assets for the site. Other static resources can be added here as well.
* Almost all api calls are logged at the verbose level. This is useful for tests / debugging but will generate a very large amount of logs

## Files

* DeviceManager.cs is a static class that manages access to device instances. This may be refactored into a service that can be accessed through dependency injection or function access, allowing the device controllers to be provided by a library
* Logging.cs is a static class for logging management. This could be removed, using the logging instance included with ASCOM Standard instead. This architecture should be decided by the group for the next release.
* Program.cs starts all components and parses command line arguments
* ServerManager contains TransactionID and Browser start commands
* ServerSettings contains all settings that are used by the server. These are stored in the XMLProfile or another IProfile
* Startup manages all ASP.Net core services and components