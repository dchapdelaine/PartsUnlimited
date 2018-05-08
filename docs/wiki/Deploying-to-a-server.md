Here are some of the steps that are needed to deploy Kudu to a Windows 2012 server:

- Enable Web Server role, and within that:
  - Application Development / ASP.NET 4.5
- Make sure you have all the relevant target files in your "%ProgramFiles(x86)%\MSBuild" folder. Installing Visual Studio brings in those files. See related discussion [here](http://stackoverflow.com/questions/3980909/microsoft-webapplication-targets-was-not-found-on-the-build-server-whats-your).
- Install:
  - Git
  - Node
- On the machine with your Kudu repo, run `build.cmd` at the root. This will create an 'artifacts' folder
- Copy all the files from `artifacts\Release\KuduWeb' into C:\inetpub\wwwroot
  - Create an empty App_Data in there
- Copy the `artifacts\Release\SiteExtensions\Kudu` directory into into `C:\inetpub`, and rename it to `Kudu.Services.Web`
- Change AppPool to run as LocalSystem so it can manage IIS sites
- Give 'Users' full access to C:\inetpub\apps, which is where sites get created
  - Note that you can change that location using the `sitesPath` AppSetting in Kudu.Web's web.config.

You should end up with a folder structure on the server that looks like this:

````
C:\inetpub\
  - Kudu.Web.Services\
    - App_Data
    - web.config, ...
  - wwwroot\
    - web.config, ...
````
