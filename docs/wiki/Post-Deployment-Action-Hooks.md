A **post deployment action hook** is a script/executable that runs after the deployment has completed successfully as part of the default deployment script.

To add a **post deployment action hook** you simply need to place a script file (.bat, .cmd or .ps1) under ```site\deployments\tools\PostDeploymentActions```.

The location of these scripts is set as a default setting which can be overwritten by updating the setting: `SCM_POST_DEPLOYMENT_ACTIONS_PATH`. That can be set to

- And absolute path
- A relative path from D:\home\site\repository

You can add more than one script files and they will all run one by one. If a script returns an error exit code (<>0), the deployment fails and the next script is not called.

Post deployment scripts run with the same environment variables as the main deployment script. The Script filename (minus extension) becomes title in deployment log.

Pre-installed site extensions (%ProgramFiles(x86)%\SiteExtensions) can also have post deployment scripts if below conditions are satisfied:

 - There is a `PostDeploymentActions` folder under the the root of the package
 - There is an environment variable {extension id}_EXTENSION_VERSION with value "latest" or {version}