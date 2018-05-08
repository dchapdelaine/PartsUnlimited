This page gives instructions on investigating Kudu issues that occur in Azure Web Sites. This applies to git, Mercurial and Dropbox deployment related issues.

# Making sure things work outside of Kudu and Azure Web Sites

Sometimes, the issue exists outside of Azure Web Sites. Here are a couple things worth looking at to make sure things are healthy before looking at an issue as Azure specific:

* [[Make sure all your files are committed]]: this applies to git scenarios
* [[Make sure site correctly deploys locally]]: this applies to standard ASP.NET Web Applications (i.e. 4.x)
* [[Test your ASP.NET Core repo locally]]: this applies to ASP.NET Core web Applications

# Misc investigation topics

- Understanding the difference between deployment and runtime issues: [[deployment vs runtime issues]].
- [[Reporting your site name without posting it publicly]]
- [[Using a git repo to report an issue]]
- [[Isolating WebJobs and Deployment script issues]]
- [[VSTS vs Kudu deployments]]
- [[Setting up a VSTS account so it can deploy to a Web App]]
- [[Dealing with locked files during deployment]]
- [[Reporting WebJobs issues]]
- [[Troubleshooting PHP errors]]
- [[Troubleshooting Node errors]]
- [[Troubleshooting .NET Core errors]]

# Kudu and Azure Web Site specific investigation

## Make sure the site itself works

When you run into an issue with a Kudu feature, the first thing to try is to hit the site itself (e.g. http://yoursite.azurewebsites.net/).  If it returns 403 page, then go to portal and check whether site is stopped or certain quota may have exceeded.  If it seems completely unreachable, try access other sites in the same subscription.   If still not, then there is a chance that you're dealing with some kind of WAWS outage, which would also disable the Kudu features. When that happens, please check the [Azure Service Dashboard](http://www.windowsazure.com/en-us/support/service-dashboard/) to see if some outage is mentioned.

## Try hitting the root of the Kudu service

If the site is up but a Kudu feature is broken in some way, try hitting the [[root of the Kudu service|Accessing-the-kudu-service]] directly from the browser. This is useful whether you fail to Enable Git, get a failure on the Deployment tab, or get a failure when you git push.

If you get an error, it could mean that there is something wrong with Kudu. Please record what the error is and report it to the [forum](http://social.msdn.microsoft.com/Forums/en-US/azuregit/threads)

## Check if disk space is available

On Kudu site under `Environment` tab, you can see the d:\home and d:\local usages.   Ensure disk space is available (green).

## Check if file system is in normal mode

When there is an on-going storage issue, the file system may be temporarily switched to read-only mode.  You can find out by, on Kudu site under `Environment` tab, look for WEBSITE_VOLUME_TYPE, the normal value is `PrimaryStorageVolume`.  Otherwise, the file system is read-only.   During such, any write attempts (such as deployment) will fail. 

## Live traces

See [[Diagnostic Log Stream]].

## Getting the diagnostic dump

If you are able to hit the root of the Kudu service (per previous section), but you get an error during a git command (e.g. push/pull/clone), the next step is to download the Kudu service log.

To do this:

* Go to the [[root of the Kudu service|Accessing-the-kudu-service]]
* On that page, you'll see a 'Diagnostics Dump' menu item
* Just click it to download a zip file which you can provide to help investigation

## Error types

* **403** - This can mean many things; either you don't have access to this site, the site is stopped or the quota is exceeded.
* **404** - See [[Known issues]]
* **406** - Your Azure web site may be in stopped state.
* **409** - There's a deployment in progress.
* **501** - Git tried to use the dumb protocol and we don't support this ([[Anatomy of a git request]]), this is because the initial request failed.
* **502, 503** - Something went wrong before the request reached the Kudu service.

To get more details for any of the above status codes, capture the git client trace for more details (see below).

### Capturing the git http verbose output

1. First set GIT_CURL_VERBOSE to 1. The syntax varies depending on the shell:
  * git bash: `export GIT_CURL_VERBOSE=1`
  * cmd.exe: `set GIT_CURL_VERBOSE=1`
  * Powershell: `$env:GIT_CURL_VERBOSE=1`
2. Run your git command. (e.g. git push url master). **NOTE: To capture output you need to redirect the error stream not output!**
3. [[Analyzing a git client trace]].

### Turning on git tracing

    set GIT_TRACE=1

### Set Kudu logging to verbose level

Try setting AppSetting SCM_TRACE_LEVEL to 4 for kudu-related verbose logging.

### Deployment process got terminated due to idle timeout

By default, in Azure WebApp env, the SCM idletimeout is 20 minutes.  Idle means no more incoming request to SCM site.   If your deployment process (fetch, build, deploy) takes longer than that, it will be abruptly terminated.  You can do one of the followings.

* Set your WebApp's appSettings `WEBSITE_SCM_IDLE_TIMEOUT_IN_MINUTES = <value>`.  For instance, set to 30 if your deployment will last at most 30 mins.

* Enable your WebApp's AlwaysOn feature.  Our infrastructure will send requests periodically to your SCM site to keep it alive.   Literally, same behavior as `WEBSITE_SCM_IDLE_TIMEOUT_IN_MINUTES = <large value>`.

