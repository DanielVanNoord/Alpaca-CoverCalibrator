# Alpaca REST server with Blazor UI
This uses ASP.Net Core (.Net 5.0)

Notes and open questions

* Currently this does not work with discovery when debugging with IIS as it does not match the discovery port. Select a build target of Alpaca.CoverCalibrator for testing. This should be fixed in a future update. For now IIS express has been removed from launchSettings.json.
* There is a Swagger / OpenAPI UI on /swagger. This can help test the APIs

## Folders
* Authorization - Authorization filters for controllers and a user service for authenticating users.
  * AuthorizationFilter - A IAuthorizationFilter implementation for REST API Controllers. It allows access if
    1. Authorization is off
    2. The endpoint allows anonymous access
    3. The caller has a valid cookie or auth token
    4. The request has a valid http basic auth header with the correct credentials
  * Hash - store and access user credentials in a hashed form. Uses RFC 2898 hashing.
  * UserService - A service to authenticate a user
* Controllers - Asp.Net MVC controllers exposing the Alpaca interfaces.
  *  Device controllers access the device through the device manager and expose the Alpaca API. They should work with any device that exposes the correct Interface with minimal (ideally no) changes.
  * The Management Controller exposes the Alpaca Management API. Some customization may be required to expose a servers custom information. Development note, it may be better to put that information in the ServerManager class and access those constants.
  * The CatchAll controller responds to REST requests on the /api endpoint that the driver does not implement. This responds with the specification required HTTP 400.
  * The SetupController is only needed under special circumstances. It may contain redirects from the standard APIs for setup to pages that actually have the UI. Blazor supports multiple routes for Blazor pages so you can directly route to pages by adding the path directly to the page (see the pages section). If you have multiple different models of the same device type (IE multiple focuser models) with different setup pages you can check the type of the requested device and use redirects to the correct setup page. Alternatively you can load different components on the actual setup page based on the device's features.
* Discovery - Wrappers and helpers around the ASCOM Standard Discovery Library. These manage the service and create / destroy the service if it is running or stopped.
* Pages - Blazor pages containing device setup, server setup and device control. Currently the Alpaca setup api routes here. These allow for custom UI across all platforms.
  * _Host the standard Blazor app host
  * CoverCalibratorControl is a Blazor page for device control from the browser
  * CoverCalibratorSetup is a Blazor page for device setup. The standard Alpaca device setup routes here. Device specific settings go here.
  * Error is the standard Blazor error page
  * Login and Logout are cshmtl pages for login management. They are this because Login requires access to HTTPContext which Blazor has limited access to
  * The driver level setup. The shared Alpaca Setup routes here. Settings like discovery, ports and access go here.
* Shared - Blazor components that are shared between other Blazor pages. Currently this consists of the Navigation Menu, the login / out UI, and main layout. Links to all pages can be added to NavMenu so they can be accessed from the menu.
* wwwroot - the root folder for www access. Mostly contains the favicon and css assets for the site. Other static resources can be added here as well. The site.css file contains custom css for the Blazor pages.
* Almost all api calls are logged at the verbose level. This is useful for tests / debugging but will generate a very large amount of logs

## Files

* AlpacaController.cs contains code to process requests and return them as a http 200 with json response or http 400 error response.
* DriverManager.cs is a static class that manages access to device instances. This may be refactored into a service that can be accessed through dependency injection or function access, allowing the device controllers to be provided by a library. It also contains TransactionID access.
* Logging.cs is a static class for logging management. This could be removed, using the logging instance included with ASCOM Standard instead. This architecture should be decided by the group for the next release.
* Program.cs starts all components and parses command line arguments
* AlpacaSettings contains all settings that are used by the Alpaca REST API driver (not the device level settings). These are stored in the XMLProfile or another IProfile
* Startup manages all ASP.Net core services and components