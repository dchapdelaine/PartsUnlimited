If you deploy an ARM template that uses msdeploy, errors can often be cryptic. To get more info after a failure, try the following:

- Go to the site's [[Kudu Console]].
- Navigate to the `D:\home\LogFiles\SiteExtensions\MSDeploy` folder.
- In there, you should see an `appManagerStatus.xml` and an `appManagerLog.xml`. The first has the status (probably failed), and the second contains a log which should give more info about the error.
- If the error is not clear to you, you can include it when asking for help on the forum.