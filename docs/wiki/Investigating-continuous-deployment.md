This is a guide to help investigate issues with continuous deployment from GitHub or Bitbucket.

## Issues related to setting up continuous deployment from the Azure Portal

### Things to check if you are working with GitHub organizations

This [post](https://azure.microsoft.com/en-us/blog/using-app-service-web-apps-continuous-deployment-with-github-organizations/) describes what you need to do to make sure that Azure can see repos in the relevant organizations.

### Q: Can't see the repositories in the drop down!

You need to be an admin on the repositories.  The reason  is Azure WebApp will need to add the WebHook on your behalf.  To check if you are an admin of the repo, ...
* For github, see if you can browse to https://github.com/{account}/{repo}/settings/hooks.
* For bitbucket, see if you can browse to https://bitbucket.org/{account}/{repo}/admin/hooks.

### Q: Still can't see github org repositories in the drop down!

Github has recently required an explicit authorization for OAuth application to access the org repositories.  To grant the authorization, ...
* Browse to https://github.com/settings/applications
* Click `Azure Management Portal - Primary` or `Azure Management Portal - Secondary`; for old Portal, click `Windows Azure`.
* Click `Grant access` to the specific organization

### Q: WebApp did not receive notification from Bitbucket 

Bitbucket fires the notification to service hooks in sequence.   The time limit overall is 30 secs.  Given each hooks, Azure WebApp may take up to 10 seconds to process.  If one has multiple hooks registered, the first few hooks may receive the notification properly.   The remaining (once exceed 30s) may not.   Unfortunately, there is no easy workaround besides performing the sync operation (either using portal or powershell cmdlet) manually.

### Q: Get exception with AddSSHKey: key is already in use

This happens when trying to link deployment with GitHub `private` repository and somehow our cleanup (remove GitHub's Deploy Key) was unsuccessfully and left the key in certain repo.   Trying to setup again may lead to `key is in use` issue.   First, browse to the [Kudu Console](https://github.com/projectkudu/kudu/wiki/Kudu-console) of the site.  You can do either of the workarounds below.   

   - Simply remove the .ssh directory and new key will be generated.
```
D:\home> rmdir .ssh /s /q
```

   - Remove the deploy key from github manually.   The below command will help identify which repo the key was currently used.   Browse to that repo on GitHub and remove the deploy key.

```
D:\home> cd .ssh
D:\home\.ssh> ssh -i id_rsa git@github.com
```

### Q: Get exception "Cannot find SourceControlToken with name GitHub".

To setup continuous deployment, An oauth token is required to call GitHub/Bitbucket apis.  This token is given to Azure during OAuth consent flow on portal.  If you run into this issue, try logon to portal (portal.azure.com) and perform setup continuous deployment once (to any site).  The oauth token will be stored for the logon Azure user.   Each Azure user will need to go thru OAuth consent flow once.

### Q: Can I use Service Principal to setup continuous deployment

Yes.  To setup continuous deployment, An oauth token is required to call GitHub/Bitbucket apis.  Since there is no OAuth consent flow for Service Principal, one will need to associate oauth token to Service Principal manually.

- Logon to GitHub and generate personal token.
- [Use ARMClient and logon with Service Principal](https://github.com/projectKudu/ARMClient/wiki/Login-and-Acquire-Tokens).
- [Assign GitHub token for Service Principal](https://github.com/projectkudu/ARMClient/wiki/Update-SourceControlTokens).

**Note:** VSTS deployment setup with Service Principal is not supported.

## Other topics

### Look at service log  

Follow the "Get git service log" step in [[Investigating issues]].   Once you have a zipped logfile, look for Git\trace\trace.xml.  This should contain all the incoming/outgoing request logs.  See if we have received any POST /deploy request from GitHub or Bitbucket (notification of repo push) or any issues.   You may share us that trace.xml and we can help investigate.    

### Check service hook settings

If you can't find any related requests in trace.xml, we may have trouble receiving anything from GitHub or Bitbucket in the first place.  Look at your service hook settings in GitHub or Bitbucket.  For instance, in Bitbucket, it is under Services section of your repo settings. See if the azure service url (https://...scm.../deploy) is registered.

### Try POST simple payload

If there is, try POST simple payload to that URL.   The payload need to just contain repo url info such as {"url":"https://.../myrepo"}; in case of SSH, {"url":"git@...:user/myrepo"}.   You may also pass is_hg:true if your repo is mercurial.  Check if there is a deployment to your site.

### Verify Service hook

Create a http request bin at http://requestb.in/ and add it as one of Services of your repo setting.  Push to your repo and see if there exist any POST requests.  If not, then contact GitHub or Bitbucket.

Here is the Bitbucket status page: http://status.bitbucket.org/