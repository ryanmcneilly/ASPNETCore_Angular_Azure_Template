# ASPNETCore_Angular_Azure_Template
Template for ASP.NET Core application using AngularJS for the client hosted on Azure App Service.

# Required Azure App Service Configuration Items
## Application settings
The following configuration items are expected to be in the "Application settings" section of the Azure App Service configuration:
* APPCONFIG_CACHESECONDS [Integer] - specifies how long (seconds) the app config should be held in cache before expiring and being re-fetched.
* ENVIRONMENT [String] - specifies the environment the application instance is for (example:  PROD, UAT, TEST, DEV...) and the AppConfig label filter for the application instance.

## Connection strings
The following configuration items are expected to be in the "Connection strings" section of the App Service configuration:
* APPCONFIG (CONNECTIONSTRINGS:APPCONFIG) [String] - the Azure App Configuration connection string (found in Access Keys for App Config).