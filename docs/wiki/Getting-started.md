### Prerequisites to run Kudu and functional tests
We have recently created a few custom scripts to automate the setup of Kudu development dependencies
see the readme for [KuduDevSetup.cmd](https://github.com/projectkudu/kudu/tree/master/Setup) for details. The new approach has been minimally tested so the older manual instruction below are still worth reading to understand what the new script does. The script requires that [WebPI 4.5](http://www.microsoft.com/web/downloads/platform.aspx) be installed. Once you have WebPI install simply execute the script from the directory.

* To fully run functional tests, you will still need to manually execute first two steps from [Additional prerequisites to run the functional tests](https://github.com/projectkudu/kudu/wiki/Getting-started#additional-prerequisites-to-run-the-functional-tests)

### Manual Setup Instructions
* Visual Studio 2017. Note that you need to run it as admin. Install/Update to the latest version of NuGet Package Manager (Tools -> Extensions and Updates).
* IIS >= 7, configured to run ASP.NET 4.0. This can be installed using [WebPI](http://www.microsoft.com/web/downloads/platform.aspx).
* Install Git (http://git-scm.com/downloads) to the default location (we expect it in ```C:\Program Files (x86)\Git\bin```).
* Install [Node](http://nodejs.org/). Node is used as part of the build process for sites.

Note: If you manually install IIS, here is what needs to be enabled:

![Windows Features](http://i.imgur.com/ZdFpz.png)

### Additional prerequisites to run the functional tests

* If you don't see the tests in Test Explorer pane, it may be because you have Xunit runner install as VSIX.  For Xunit 2.0, the runner is installed as part of the project (not VSIX).  To fix, open VS -> TOOLS -> Extensions and Updates ..., and uninstall Xunit runner VSIX. 
* If still don't see the tests, try remove this folder `%TEMP%\VisualStudioTestExplorerExtensions`.  See [this](http://xunit.github.io/docs/running-tests-in-vs.html).

You are done here if you had run [KuduDevSetup.cmd](https://github.com/projectkudu/kudu/tree/master/Setup), otherwise continue.

* Install [MVC 3](http://www.asp.net/mvc/mvc3). After installing, ensure KB2993937 is installed by running Windows Update or downloading it directly from [here](https://support.microsoft.com/en-us/kb/2993937).
* Install [ASP.NET WebPages 1.0](http://www.microsoft.com/en-us/download/details.aspx?id=15979).
* Install F#. This can be done during a Visual Studio install or from within Visual Studio, or you can install it directly from [here](https://www.microsoft.com/en-us/download/details.aspx?id=48179).
* Open your %WINDIR%\System32\inetsrv\config\applicationHost.config and look for `<applicationPoolDefaults>`. Under `<processModel>`, make sure you don't have `setProfileEnvironment="false"`. If you do, set it to true (or remove the attribute if you are running the latest Windows 8.1).
* Unzip [this file](https://www.dropbox.com/s/69fmhaai8pbk3ww/Kudu_Progx86_Msbuild_Microsoft_VisualStudio.zip?_download_id=7197179630581643280074688252250765505493753073268277526454639605&dl=1) and copy the contents into your `\Program Files (x86)\MSBuild\Microsoft\VisualStudio`. This brings in various files from older VS versions that you may not have installed.
* Install the [URL rewrite module for IIS](http://www.iis.net/download/URLRewrite).
* Install [IISNode](https://github.com/Azure/iisnode/releases/download/v0.2.11/iisnode-full-v0.2.11-x64.msi)
* Pretend to have the 32 bit version of Node to fool the version detection logic:
 * Copy your `\Program Files\nodejs` folder to \Program Files (x86)\nodejs.
 * Under `\Program Files (x86)\nodejs`, you need to have a folder named `0.8.2`, and containing [node.exe version 0.8.2](http://nodejs.org/dist/v0.8.2/node.exe). And then same deal for Node 0.10.5 (you can put the 0.8.2 exe in the 0.10.5 folder; it's the folder that matters). Yes, this is kind of painful, and we need to make it easier!
* Install [Mercurial](http://mercurial.selenic.com/downloads) to the default location (we expect it in ```C:\Program Files (x86)\Mercurial```)
* Install HttpPlatformHandler ([x64 msi](http://go.microsoft.com/fwlink/?LinkId=690721)) for ASP.NET 5 support. *If you're running Windows 32bit you'll need [the x86](http://go.microsoft.com/fwlink/?LinkId=690722) version.*

### Deploying your first application
1. First clone the repository.

        git clone git://github.com/projectkudu/kudu.git

2. Run build.cmd. During development, builds can be run from within Visual Studio, but this ensures you have a Release build and that the KuduSync.NET package dependency from the solution-level packages.config file is in place.

3. Open Kudu.sln as **Administrator** and build it.

4. Set **Kudu.Web** as the startup project and run!

After running Kudu.Web, you should be able to use the dashboard to manage your application. Below is a walk through on how to create and deploy your first application using git.

### Step 1
Create an application.

![Step 1](http://i.imgur.com/pScf7.png)

### Step 2
Give it a name.

![Step 2](http://i.imgur.com/xebWn.png)

### Step 3
Copy the git url and push.

![Step 3](http://i.imgur.com/vsWqb.png)

### Step 4
You're live!

![Step 4](http://i.imgur.com/zN5lf.png)