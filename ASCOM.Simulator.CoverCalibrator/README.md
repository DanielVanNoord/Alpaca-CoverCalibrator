ASCOM out of process (Local Server) driver

* Differences from default Local Server Template
    * The Local Server only loads from itself for security
    * The Local Server does not show an empty form, instead it uses bare Application.Run(); It will show up in the Background Processes and cannot be found by Alt Tabbing around.
* The Drivers folder contains implementations of the ASCOM interface. This is a thin wrapper around the device library. Access is managed by SharedResources. The class(es) inherit from ReferenceCountedObjects which handles reference counting.
* The SetupDialog folder contains the device specific setup forms. These can be customized and call functions outside of the ASCOM interface for device configuration.
* The Resources folder contains non-code resource (like images and icons) to be included in the project.
* SharedResources contains static resources that only exist once in the server. Because each client access creates an instance of the device driver within the server these drivers use SharedResources to share access to a single instance of the underlying device connection.