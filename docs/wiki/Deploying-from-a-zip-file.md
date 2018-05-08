To deploy the content of a zip file to your site, `POST` the zip to `/api/zipdeploy`.

Using curl:

```
curl -X POST -u <user> --data-binary @<zipfile> https://{sitename}.scm.azurewebsites.net/api/zipdeploy
```

`zipdeploy` is intended for fast and easy deployments from development environments, as well as deployment of ready-to-run sites built by continuous integration services such as [Visual Studio Team Services](https://www.visualstudio.com/team-services/). Unlike other Kudu deployment mechanisms, **Kudu assumes by default that deployments from zip files are ready to run and do not require additional build steps during deployment**, such as `npm install` or `dotnet restore`/`dotnet publish`. This can be overridden by setting the [`SCM_DO_BUILD_DURING_DEPLOYMENT` deployment setting](https://github.com/projectkudu/kudu/wiki/Configurable-settings#enabledisable-build-actions) to `true`, or by specifying [custom script generator arguments](https://github.com/projectkudu/kudu/wiki/Configurable-settings#taking-over-the-script-generator-command-line) or a [custom deployment script](https://github.com/projectkudu/kudu/wiki/Configurable-settings#take-over-the-whole-deployment-script).

#### Asynchronous zip deployment
Add `?isAsync=true` to the URL to deploy asynchronously. You will receive a response as soon as the zip file is uploaded with a `Location` header pointing to the pollable deployment status URL.

#### Comparison with zip API
Kudu's [zip API](https://github.com/projectkudu/kudu/wiki/REST-API#zip) is an effective way to move multiple files to your site, but `zipdeploy` is preferable for deployments for a number of reasons:
* **Deletion of files no longer in the build**: When a new build is deployed with `zipdeploy`, files and directories that were created by the previous deployment but are no longer present in the build will be deleted. Any other files and directories found in the site that aren't being overwritten by the deployment, such as those placed there via FTP or created by your app during runtime, will be preserved.
* **Efficient file copy**: Files will only be copied if their timestamps don't match what is already deployed. Generating a zip using a build process that caches outputs can result in faster deployments.
* **Function trigger sync**: If you are running Functions on a Consumption plan, modifying their triggers requires a synchronization process that doesn't occur when using the zip API or other file-based deployment methods like FTP. Zipdeploy will perform this synchronization for you.
* **Logging, status tracking and history**: `zipdeploy` generates live status, deployment logs and recorded history in Kudu's [deployment API](https://github.com/projectkudu/kudu/wiki/REST-API#deployment). However, zip deployments cannot be redeployed.
* **Async support**: By adding `?isAsync=true` to the URL, the deployment will run asynchronously. The `Location` header of the response will contain the deployment URL that can be polled for deployment status. [Webhooks](https://github.com/projectkudu/kudu/wiki/Web-hooks) can be used for fully asynchronous notification of completion.
* **Customizable deployment**: [Deployment-related settings](https://github.com/projectkudu/kudu/wiki/Configurable-settings#repository-and-deployment-related-settings) will be respected, including app settings and values in a [.deployment file](https://github.com/projectkudu/kudu/wiki/Customizing-deployments#deployment-file).
* **Build support**: By default, `zipdeploy` assumes that the zip contains a ready-to-run site, but Kudu's built-in build process can be re-enabled via the [`SCM_DO_BUILD_DURING_DEPLOYMENT` deployment setting](https://github.com/projectkudu/kudu/wiki/Configurable-settings#enabledisable-build-actions-preview).
* **Safe deployment**: `zipdeploy` engages Kudu's deployment locks, preventing multiple simultaneous deployments from clobbering your site.
* **Auto Swap and container restart support**: A zip deployment will trigger an [Auto Swap](https://docs.microsoft.com/en-us/azure/app-service/web-sites-staged-publishing#Auto-Swap) if your site is configured for it. On App Service on Linux, zip deployment will trigger a restart of the app container.