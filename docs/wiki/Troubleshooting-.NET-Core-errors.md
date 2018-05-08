Here are several steps that can help diagnose issues with .NET Core apps:

## Turn on debug tracing

Using Kudu Console, go to `D:\home\site\wwwroot` and edit your web.config file. In there, turn on `stdoutLogEnabled` and make sure `stdoutLogFile` is set as below:

    <aspNetCore processPath="dotnet" arguments=".\app.dll" stdoutLogEnabled="true" stdoutLogFile="\\?\%home%\LogFiles\stdout" />

Then after running your app, you should see log files under `D:\home\LogFiles` with names that look like `stdout_4200_2017112717490.log`. Check if they contain any clues about the issue.

Please note that seeing the following error in there is expected, and is not a source of concern:

    Unable to bind to http://localhost:4302 on the IPv6 loopback interface: 'Error -4090 EADDRNOTAVAIL address not available'.

## Turn on Detailed error messages

In the portal, under Diagnostics logs, turn on "Detailed error messages" and run your app. If there are errors, you may see some html files appearing under `D:\home\LogFiles\DetailedErrors`. Click the download link in Kudu, which should let you view the HTML, which may contain important clues about the error.