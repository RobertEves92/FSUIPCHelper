# FSUIPCHelper
Helper library for interfacing with FSUIPC

## Intro
Originally this helper was created as the core for a Virtual ACARS system for use by Virtual Ryanair as part of their relaunch, this code was then developed in to a new system designed to be an open ACARS for anyone to use with Flight Simulator to track and monitor flights, and generate a report based on the collected data, with the option to submit a PIREP to an airline using the phpVMS system.

Since its creation, Virtual Ryanair have gone on to develop far superior proprietary software for running their airline and the project was no longer required. However it still offers a huge amount of functionality that make working with FSUIPC much easier, with pre-programmed methods for obtaining data from FlightSim without having to worry about the more technical details of using FSUIPC directly.

## Quickstart
1. Reference FSUIPCHelper in your project
2. Open a connection to FlightSim using `Global.Fsuipc.OpenConnection()`
3. Request an update of the data using `Global.Fsuipc.ProcessData()`
   > This is important, you MUST do this every time you want up to date data. If you do not the helper will return whatever data was stored by FSUIPC the last time it was updated. I recommend you put this on some sort of timer or loop.

From this point you can call various methods manually to obtain the data held by FSUIPC and process it however you want.

## Automated Logs
### Flight Log
There are various methods that can be called that will update an internal and user friendly log, all of which can be called manually at any time, or alternativly using `FSData.UpdateFSData()` will update them all and add the relevant changes to the log.
The log can be called by using `FlightLog.GetFlightLog()` and cleared by using `FlightLog.ClearLog()`. Lines can be manually added to the log by using the add log method, eg `FlightLog.AddLog("This is a log")`

### Internal Log
In addition to the flight log, there is a log that is outputted automatically upon exceptions etc and is outputted to the FSUIPCHelper directory in the users APPDATA