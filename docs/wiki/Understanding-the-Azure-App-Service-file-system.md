See also [this page](https://docs.microsoft.com/en-us/azure/app-service-web/web-sites-available-operating-system-functionality#file-access) with related information.

There are three main types of files that an Azure Web App can deal with

### Persisted files

This is what you can view as your web site's files. They follow a structure described [[here|File structure on azure]]. They are rooted in `d:\home`, which can also be found using the `%HOME%` environment variable. For App Service on Linux and Web app for Containers, persistent storage is rooted in `/home`.

These files are persistent, meaning that you can rely on them staying there until you do something to change them. Also, they are shared between all instances of your site (when you scale it up to multiple instances). Internally, the way this works is that they are stored in Azure Storage instead of living on the local file system.

Free and Shared sites get 1GB of space, Basic sites get 10GB, and Standard sites get 50GB. See more details on the [Web App Pricing](http://www.windowsazure.com/en-us/pricing/details/web-sites/) page.


### Temporary files

A number of common Windows locations are using temporary storage on the local machine. For instance

- `%APPDATA%` points to something like `D:\local\AppData`.
- `%TMP%` goes to `D:\local\Temp`.

Unlike Persisted files, these files are not shared among site instances. Also, you cannot rely on them staying there. For instance, if you restart a web app, you'll find that all of these folders get reset to their original state.

For Free, Shared and Consumption (Functions) sites, there is a 500MB limit for all these locations together (i.e. not per-folder). For Standard and Basic sites, the limit is very high (50 GB or more).

Another important note is that the Main site and the scm site do not share temp files. So if you write some files there from your site, you will not see them from Kudu Console (and vice versa). You can make them use the same temp space if you disable separation (via [WEBSITE_DISABLE_SCM_SEPARATION](https://github.com/projectkudu/kudu/wiki/Configurable-settings#use-the-same-process-for-the-user-site-and-the-scm-site)). But note that this is a legacy flag, and its use is not recommended/supported.


### Machine level read-only files

The Web App is able to access many standard Windows locations like %ProgramFiles% and %windir%. These files can never be modified by the Web App.

