If your Node site is failing with some http 500 errors, try enabling logging. To do this, enable Application Logging to file system from the Azure portal (details [here](https://azure.microsoft.com/en-us/documentation/articles/web-sites-enable-diagnostic-log/)).

Once that's done:
- make a few failing request to get some logging going.
- using [[Kudu Console]], go to `D:\home\LogFiles\Application`. You should see some .txt files, as well as an `index.html`.
- Click the down arrow next to `index.html`, and it should open a new tab with a page showing a list of errors.
- Click the `log` link on any entry to get details
- Another place to look for possible errors is under `D:\home\site\wwwroot\iisnode`
