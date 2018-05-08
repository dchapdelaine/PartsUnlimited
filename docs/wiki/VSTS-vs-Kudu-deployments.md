One area that is a bit confusing is that Azure Website deployment using Visual Studio Team Services (VSTS) works quite different from deployment using Kudu.

When you set up deployment from source control in the Azure portal, you get a number of options:
- Visual Studio Team Services
- OneDrive
- Local Git repository
- GitHub
- Bitbucket
- Dropbox
- External repository

Under the cover, all of these options are using the Kudu deployment engine. For option "Visual Studio Team Services", Kudu only support git repository, see [Can I host my repo on VSTS and have Kudu build it](https://github.com/projectkudu/kudu/wiki/VSTS-vs-Kudu-deployments#can-i-host-my-repo-on-vsts-and-have-kudu-build-it) for details.

This article explains some of the differences you may encounter, and how to report and investigate them.

## How does Kudu deployment work

When using one of the options that go through Kudu, your sources live within the Azure Websites infrastructure, and get built and deployed from within Azure Websites.

If you connect to your site via [[Kudu Console]], you will see your sources under `site\repository`, as well as various deployment related artifacts.

## How does Visual Studio Team Services deployment work

When setting up continuous deployment to an Azure Website from Visual Studio Team Services, things work in the following way:
- You repository lives in the VSTS infrastructure.
- When you push a change via git, the VSTS build infrastructure builds your sources, and produces some deployment artifacts.
- VSTS then uses WebDeploy (the same technology used by Visual Studio) to deploy the artifacts to your Azure Websites.

Here, if you connect to your site using Kudu console, you will see the deployed files under your `site\wwwroot` folder, but won't see a repository folder, or any of the other things you get with Kudu. The Kudu engine is not involved at all in this scenario.

## Why does this matter?

It matters because the build process and build environment are not identical in both cases. e.g. they may not have exactly the same version of the SDKs, as they update on different schedules.

Also, everything you read in this Kudu wiki applies to Kudu based deployments and not to VSTS deployments. e.g. the various Kudu app settings like `SCM_BUILD_ARGS` (from [this page](https://github.com/projectkudu/kudu/wiki/Configurable-settings)) will have no effect at all when using VSTS.

Instead, with VSTS, you have to use the mechanisms provided by VSTS (like the Build Definition file) to tweak your deployment process.

Also, it matters in term of reporting issues to the correct set of people:
- For Kudu, you can use the [Azure App Service forum](https://social.msdn.microsoft.com/Forums/azure/en-US/home?forum=windowsazurewebsitespreview), or open issue in this Kudu GitHub project.
- For VSTS, you should instead use the [Visual Studio Online Forum](https://social.msdn.microsoft.com/Forums/vstudio/en-US/home?forum=TFService), where you will find the right set of experts.

## Can I host my repo on VSTS and have Kudu build it

As a step forward to offer consistent experience on Azure App Service, Kudu supports Git repository from VSTS. When you choose option "Visual Studio Team Services" from Azure portal to setup continuous deployment, Kudu will register a webhook to your VSTS repository and maintain a clone of your repository next to your site under ````site\repository````, all your build and deployment will be handled by Kudu deployment engine.

- How about Team Foundation Version Control repository?

At this time, this is not supported, but you can setup build/deployment via [visualstudio.com](https://www.visualstudio.com/). ["Build and deploy your ASP.NET 4 code to an Azure web app"](https://msdn.microsoft.com/en-us/Library/vs/alm/Build/azure/aspnet4) has a great video tutorial to guide your through.

- For VSTS Git repo, my site had continuous deployment setup using VSTS infrastructure before, how can I migrate to use Kudu deployment engine?

 - [Disable continuous deployment](https://azure.microsoft.com/en-us/documentation/articles/web-sites-publish-source-control/) from your site.
 - Make sure build definition is deleted, you can check your build definition from ````https://{VSTS Account Name}.visualstudio.com/DefaultCollection/{Project Name}/_build````. If there is leftover build definition, right click on it and choose "Delete".
 - From Azure portal, select option "Visual Studio Team Services" and follow the UI guidance.
