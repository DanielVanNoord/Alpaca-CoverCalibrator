Alpaca REST server with Blazor UI

Folders
* Controllers - Asp.Net MVC controllers exposing the Alpaca interfaces.
  *  Device controllers access the device through the device manager and expose the Alpaca API. They should work with any device that exposes the correct API with minimal (ideally no) changes.
  * The Management Controller exposes the Alpaca Management API. Some customization may be required to expose a servers custom information. Although it may be better to put that information in the ServerManager class and access those constants.
  * The SetupController may contain redirects from the standard API for setup to pages that actually have the UI. Because Blazor supports multiple routes for Blazor pages this should be not be needed very often.
* Discovery - Wrappers and an implementation of the Alpaca Discovery Protocol. Some of this will likely be removed when the discovery library is available. 
* Pages - Blazor pages containing device setup, server setup and device control. Currently the Alpaca setup api routes here. These allow for custom UI across all platforms.
* Shared - Blazor components that are shared between other Blazor pages. Currently this consists of the Navigation Menu and main layout. Links to all pages can be added to NavMenu so they can be accessed from the menu.
* wwwroot - the root folder for www access. Mostly contains the favicon and css assets for the site. Other static resources can be added here as well.