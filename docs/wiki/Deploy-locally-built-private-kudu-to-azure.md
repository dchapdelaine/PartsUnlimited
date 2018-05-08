You can simply deploy your own version of Kudu to the Windows Azure WebSites by following steps below.

1. Clone the kudu repo.  git clone https://github.com/projectkudu/kudu

2. At the root of the repo, run build.cmd. It should build and create artifacts\Release\zip\DeployPrivateKudu.cmd file. 

3. Run DeployPrivateKudu.cmd with DEPLOYMENT TRIGGER URL as argument.  This will upload Kudu bits to your site.

  For example, `DeployPrivateKudu.cmd "https://$site:password@site.scm.azurewebsites.net/deploy"`.

  Note: To get DEPLOYMENT TRIGGER URL, go to Azure Portal and navigate to your site CONFIGURE tab.  If you don't see the URL, you can always form up scm URL (add scm in the middle) and embed publishing username/password instead.

That's it.