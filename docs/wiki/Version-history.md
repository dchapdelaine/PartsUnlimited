## S62 (04/2017)
* Compiled function apps deployment support
* Support specifying a name to ZipController when downloading a directory
* New WEBJOBS_DISABLE_SCHEDULE flag
* Remove support for old xproj Core projects
* Linux changes

## S61 (03/2017)
* Logic app sync support
* Function changes

## S60 (02/2017)
* Improved dotnet Core support
* Linux fixes 

## S59 (10/2016)
* Improve WebJob logging and watcher error handling
* Dotnet Core Preview2/Preview3 support
* Autoswap improvements
* Linux fixes

## S58 (08/2016)
* Optimize startup assembly loading
* Use nuget.exe 3.5 RTM
* Editor improvements
* Functions related fixes

## S57 (07/2016)
* Use nuget.exe 3.5.0-rc1
* Add new SCM_MAX_RANDOM_START_DELAY flag

## S56 (06/2016)
* Changes to support ASP.NET Core GA
* Remove support for pre-installed site extensions
* More autoswap fixes 

## S55 (05/2016)
*  Use NuGet 3.5 beta 2
*  Autoswap fixes

## S54 (04/2016)
* Support ASP.NET Core RC2
* Add initial Mono support (not yet complete)
* Initial work to move up to newer git version
* Update to latest semver to fix Node/npm version handling
* Use newest nuget.exe beta to get ready for .NET Core RC2
* More function changes

## S53 (03/2016)
* Yet more Azure Functions support :)

## S52 (02/2016)
* More Azure Functions support

## S51 (01/2016)
* Add initial Azure Functions support
* Add support for multiple post deployment scripts
* Add caching in /api/deployments API to improve perf when called repeatedly by portal
* Make scheduled WebJobs available in Basic mode (used to be Standard+ only)
* Make /deploy API support async (issue #1802)  

## S50 (12/2015)
* Add new `SCM_COMMIT_MESSAGE` env variable available in deployment scripts
* Fix some issues relating to auto-swap with continuous deployment
* Fix site extension update issue
* Move to a more recent version of the ACE editor

## S49 (11/2015)
* Make Kudu work better with ASP.NET 5 RC1
* Add smarts to avoid touching web.config during deployment when it's not needed
* Support ASP.NET 5 beta8

## S48 (10/2015)
* Add WebHooks support to WebJobs
* Support ASP.NET 5 beta7
* Display update percentage during file upload to Kudu Console
* Added Ace editor help to Kudu Console

## S47 (8/2015)
* New profiling support
* Add support for DNX based WebJobs
* Add support for continuous WebJobs to handle http requests
* Add support for deploying from VS Online
* Add support for deploying from OneDrive
* Change Kudu Console editor to use Ace
* Change logger to be text based instead of XML, for big perf improvement
* Adds the TriggeredJobFinished event to the WebHooks form
* Support ASP.NET 5 beta6
* Support Bitbucket Webhook 2.0 payload (git and public repo only)
* Fix vfs/zip controller issue with paths that contain % characters
* Add new Kudu icon!

## S46 (6/2015)
* Add support for scheduled WebJobs without using external scheduler ([blog post](http://blog.amitapple.com/post/2015/06/scheduling-azure-webjobs/))
* Support ASP.NET 5 beta4
* Add basic support for the Go language
* Turn on libgit2sharp by default, and update it to latest

## S45 (4/2015)
* Support for WebJobs as an API app
* Site extension fixes and optimizations
* Make Kudu behave better when the disk is full
* Remove gcdump support

## S44 (3/2015)
* Support ASP.NET 5 beta3
* Fix GitLab payload support
* Optimize site extension install/update to be idempotent and support async
* Other site extension improvements for API app scenarios
* Increase file browser max count from 100 to 200
* Improved tracing

## S43 (2/2015)
* Add initial support for LibGit2Sharp (not on by default)
* Add ARM support for WebJobs api
* Add support for NuGet v3 site extension feeds
* Kudu trace improvements

## S42-QFE2 (12/30/2014)
* Added support for deploying WebJobs when installing site extensions.

## S42-QFE1 (12/09/2014)
* Support a new inplace mode for WebJobs
* Add support for ASP.NET 5 beta
* Change Kudu tracing to create one file per request

## S42 (11/14/2014)
* Add **in place** mode for WebJobs
* Change tracing to use one file per entry instead of a single trace.xml
* Add better Python support
* Add username to process info
* Don't ask why it jumped from S30 to S42. The reasons are bizarre and uninteresting :)

## S30 (10/27/2014)
* Support for auto-swap in continuous deployment scenarios
* Support passing feed url when installing site extensions
* Handle copying over a DLL in use by trying to rename it first (relevant to NewRelic)

## S29-QFE1 (9/29/2014)
* Disable deployment with app setting SCM_DISABLE_DEPLOY_ON_PUSH
* Gravatar support

## S29 (9/18/2014)
* New `SITE_BITNESS` environment variable

## S28-QFE6 (9/10/2014)
* Add initial support for ProjectK apps (aka ASP.NET vnext)
* New feature to download custom deployment scripts
* Upgrade Kudu to SignalR 2.x
* Marking processes with `scm` and `webjob` labels in Process Explorer
* Return LastModified header from `/api/vfs` API

## S28-QFE5 (8/22/2014)
* Temporary remedy for `npm install` failing when %APPDATA%\npm is not present
* Fixed issue of reading xml files in Debug Console

## S28-QFE4 (8/20/2014)
* Add support for viewing process command line in process API and UI
* Improve WebJobs error handling
* Fixed deallock issue when OnLockAcquired throws exception

## S28-QFE3 (8/15/2014)
* Adding Environment Variables view to Process API and UI
* Improving webjobs logging error handling
* change (add) api for webjobs from `jobs` to `* change (add) api for processes from `api/diagnostics/process` to `api/process`.
* Add httpErrors PassThrough to default Node config

## S28-QFE2 (7/20/2014)
* Enable commit-ish for generic deploy handler
* Support copy-paste several lines to Debug console
* Some visual changes for FileExplorer

## S28-QFE1 (7/2/2014)
* Cleanup index.lock for ScmVfs operations.
* Use Newtonsoft instead of DataContract for Json output annotations.
* Fixes for DebugConsole UI and autocomplete.

## S28 (6/19/2014)
* Fix small issue with site extensions API

## S27-QFE4 (6/17/2014)
* Add support for `install.cmd`/`uninstall.cmd` scripts in gallery Site Extensions
* Add support for F# Web Applications (sample [here](https://github.com/KuduApps/fsharp-owin-sample))

## S27-QFE3 (6/2/2014)
* More Site Extension tweaks

## S27-QFE2 (5/29/2014)
* Misc Site Extension and other fixes

## S27-QFE (5/8/2014)
* Improve tab auto-complete behavior in Kudu console

## S27 (5/6/2014)
* Typescript 1.0 support
* Added graceful shutdown support for WebJobs
* Improve deployment logic when disconnecting from a repo and reconnecting to a different one
* New logic to use the custom start script from package.json

## S26-QFE2 (3/31/2014)
* New Process Explorer UI
* Webhooks support for triggered WebJobs (on run complete)

## S26-QFE (3/22/2014)
* Support dropping Zip files into Dev Console
* New WebJobs dashboard for both SDK and non-SDK Jobs
* Improve Site Extension Gallery UI
* Add F# 3.1 support

## S26 (3/10/2014)
* Initial Preview of Site Extension Gallery

## S25-QFE4 (2/28/2014)
* Support new `SCM_DISABLE_SUBMODULES=1` flag to turn off git submodule update
* Add direct PowerShell support in the Debug Console
* Add auto refresh support to the Debug Console tree view when files change

## S25-QFE3 (2/11/2014)
* Add support for PowerShell WebJobs
* Add `/logs/recent` endpoint
* Add add/remove APIs for WebJobs
* Update to MVC5.1 and other newer packages
* Switch from aspx to Razor pages

## S25-QFE2 (1/30/2014)
* Add support for TypeScript
* Add Node v0.10.24 (and matching npm 1.3.21)
* Fix Kudu Console drag and drop in newest Chrome

## S25-QFE (1/16/2014)
* New [[Web Jobs]] feature
* Web hook payload now includes the site name ([issue](https://github.com/projectkudu/kudu/issues/906))
* New Kudu Console mode is now default. Can move back to old one optionally
* Correct npm version is put on the PATH ([issue](https://github.com/projectkudu/kudu/issues/950))
* Lots of [fixes](https://github.com/projectkudu/kudu/issues?direction=desc&labels=&milestone=16&page=1&sort=created&state=closed)

## S25 (12/17/2013)
* Support for deploying Console 'worker' apps
* Made SqlCmd.exe available to deployment scripts
* New API to return the list of available node/npm versions (https://github.com/projectkudu/kudu/issues/784)
* Process API now returns the list of open file handles for each process
* Updated msysgit to version 1.8.4
* Kudu now uses `d:\home` in Azure to refer to the site's files (instead of much longer folder)
* New optional alpha version of the Kudu Console that works more interactively

## S24.5 (11/21/2013)
* Added .NET 4.5.1 SDK to help build 4.5.1 projects more cleanly. Note that the **runtime** is still 4.5

## S24 (11/1/2013)
* New [Site Extension](https://github.com/projectkudu/kudu/wiki/Azure-Site-Extensions) feature
* Added diagsession and gcdump support to the [[Process API|Process Threads list and minidump gcdump diagsession]]
* Dump files now include matching mscordacwks.dll and SOS.dll for convenience
* Added threads to the [[Process API|Process Threads list and minidump gcdump diagsession]]
* New [SCM_SCRIPT_GENERATOR_ARGS](https://github.com/projectkudu/kudu/wiki/Configurable-settings#taking-over-the-script-generator-command-line) to get better control over the script generator
* Fix [selection issue](https://github.com/chrisdone/jquery-console/issues/32) in the Console.
* Moved .ssh folder to the root instead of inside the Site folder.

## S23 (8/27/2013)
* [[Web Hooks]]
* [[Post Deployment Action Hooks]]
* Enable automatic NuGet package restore, based on NuGet 2.7 semantic

## S22
* Add support for [in-place and repo-less deployments](https://github.com/projectkudu/kudu/wiki/Deploying-inplace-and-without-repository)
* Add support for [getting process information and mini-dumps](https://github.com/projectkudu/kudu/wiki/Process-list-and-minidump)
* Add new kudexec page giving a console windows from within the browser

## S21.5

### Features
* Add vstest support (including test frameworks: mstest, nunit and xunit)
* Add support for F# sites
* Add support for RIA sites
* Add ability to override some deployment settings (e.g. `SCM_BUILD_ARGS`) using .deployment file

## S21

### Features
* Add optional support for shallow clones (to save disk space) via SCM_USE_SHALLOW_CLONE setting
* Add etag support to /deployments API to avoid getting data unless necessary
* Improve progress reporting during deployment
* Allow the root of the Kudu service to be used as an alternate git endpoint
* Trim old deployments after reaching some limit
* Several improvement and fixes to the diagnostic trace file

### Perf improvements
* Cache deployment script to avoid having to generate it each time
* Use unbuffered input stream during git push operations
* Removed some unnecessary operations when fetching from external git repository
* Removed OData and DotNetZip dependencies, reducing memory usage

### Key fixes (many more small fixes not listed)
* Fixed issue that was causing some failures git pushing from Macs
* Fixed several issues that were causing timeouts when pushing large repositories
* Improved detection for processes that are waiting for user input
* Improved recovery after a crash (previously git locks and unwanted git remotes would remain)
* Added retry loop when fetching from external repos to deal with network reliability issues
* Improved how Kudu detects if the git repository already exists

## S20
* Add support for deploying from Dropbox
* Add support for deploying Mercurial sites on Bitbucket and Codeplex
* Switch to use script generator as default deployment logic
* Add support for vfs REST API to access the file system
* Add support for zip download/upload API
* Improvements to Log streaming support
* Automatically update git submodules
* Support simple payload to force fetch and deploy
* Add support for Codebase, GitlabHq and Kiln in Kudu (no portal UI though)
* Switch Kudu to use .NET 4.5

## Earlier releases

Sorry, this page was not maintained earlier. Ideally, we'd go back in fill this in, but probably won't :)