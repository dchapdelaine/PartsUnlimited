In some cases, during deployment using either Kudu/git or msdeploy (e.g. Visual Studio), you can run into errors related to files being in use.

Generally, the strategy to use to investigate is to answer some basic questions:

- what file is locked? This is often clear from the error message
- what is causing the file to be locked
- what can you do to avoid having it be locked? 

## Determining what's causing the file to be locked

The Kudu process explorer can be useful here. To access it, [go to Kudu](https://github.com/projectkudu/kudu/wiki/Kudu-console), and click on Process Explorer in the top bar.

Looking at the process list, you may already be able to guess which one is likely to lock the file. You can try clicking 'Find Handle' at the top and searching for the file name. Note that this will not include loaded DLLs.

You can also right click on a process and choose Properties.  From here, you can get a list of both loaded modules (typically DLLs), and file handles.

## Preventing files from being locked

### Stopping the site before deploying

There are two sites for a given Web App:

- The main site where your app (or functions) normally run
- The 'scm' site, where Kudu, WebJobs and msdeploy run 

Note that if you stop the app from the portal, only the main site stops (see [here](https://github.com/projectkudu/kudu/wiki/Full-stopping-a-Web-App) for details). This is good news, because assuming the files are locked by the main site and not the scm site (often the case), you can stop the site and still deploy via Kudu or msdeploy, since they run on the scm site.

### For msdeploy, try enabling 'App Offline'

msdeploy support an [App Offline mode](https://stackoverflow.com/questions/1153449/asp-net-2-0-how-to-use-app-offline-htm), which causes it to create a file called `App_Offline.htm` in the web root folder (e.g. wwwroot) during the deployment.

Why does this help? Because both ASP.NET and ASP.NET Core know how to listen for the appearance of this file, and shut themselves down. This causes all resources to be unloaded, which normally unlock all files.

The downside of course is that your site is unavailable during the deployment. This can be avoided by relying on a Staging slot instead of deploying straight to Production.

If you have enabled App Offline and you're still seeing issues with files in use, there are two main possibilities:

1. The first is that the runtime is completely failing to detect App_Offline.htm
2. The second is that the runtime detects it, but it takes a while to kick in, and msdeploy is not waiting long enough for file locks to get releases

To identify which of the two it is, do the following:

- Go to [Kudu Console](https://github.com/projectkudu/kudu/wiki/Kudu-console)
- Verify that the file at stake is indeed locked by trying to delete it. This should fail
- Now manually create App_Offline.htm from Kudu, by running `touch App_Offline.htm` from `d:\home\site\wwwroot`
- Then wait for a while, and see whether the file is eventually released (case #2), or is locked forever (case #1).

If it's case #1, report it on the forums/GitHub/SO. If it's case #2, try adding `-retryAttempts:10 -retryInterval:6000` to the msdeploy command line. This tells msdeploy to make 10 attempts, waiting 6 seconds between each.

### For msdeploy, there is another option to rename locked files

You can set this Azure App Setting on your app:

    MSDEPLOY_RENAME_LOCKED_FILES=1

This causes msdeploy to attempt to rename DLLs if they can't be copied during deployment. This often works because even when DLLs are loaded, they can typically still be renamed. It renames them with a .delete extension, which it then cleans up on the next round.

While this technique can work, using 'App Offline' is cleaner and is the preferred technique.

### Check if files have the read-only attribute

In some cases, files end up with this attribute, which prevents them getting overwritten. To check for this, go to Kudu Console and run `attrib somefile.txt`, and check if it includes the R (read-only) attribute. If it does, run `attrib -r somefile.txt` to remove it. You should also check other files in the folder in case they have the same issue (you can run `attrib -r *.*` to do them all at once).
