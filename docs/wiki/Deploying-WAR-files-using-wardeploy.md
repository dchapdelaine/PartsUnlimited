This page talks about the _wardeploy_ deployment mechanism for deploying WAR files to [Azure Web Apps](https://docs.microsoft.com/en-us/azure/app-service/) running Tomcat.

# Why _wardeploy_
There are multiple ways to deploy a WAR (Web Application Arhcive) file to an Azure Web App running Tomcat, but the most common one is to use FTP to [upload the WAR file](https://docs.microsoft.com/en-us/azure/app-service/web-sites-java-add-app) in the `/home/site/wwwroot/webapps` directory. When Tomcat detects the presence of a new WAR file, it unpacks it and loads the web application from the directory the WAR file was unpacked to.

This works great when you have an Azure Web App that isn't [scaled out](https://docs.microsoft.com/en-us/azure/monitoring-and-diagnostics/insights-how-to-scale). However, when you scale out the web app, multiple Tomcat instances race to unpack the WAR file leading to race conditions that may land one or more workers / instances in a bad state. This issue manifests in different ways, most common being a `404` status code when an incoming request is routed to any of the bad workers.

_wardeploy_ is a deployment mechanism for addressing this problem.

# How to use _wardeploy_

_wardeploy_ is available as a REST API - `/api/wardeploy` - and can be used to deploy to an Azure Web App running Tomcat on either Windows or Linux. The following sections explain how to use it from bash and Powershell.

## bash

Run the following script after editing the placeholder values for web app name, credentials, etc. Credentials can be either your [user credentials or the site credentials](https://docs.microsoft.com/en-us/azure/app-service/app-service-deployment-credentials)

```bash

# *** BEGIN *** Customizations
sitename='siteabc1' # if you site is siteabc1.azurewebsites.net
username='yourusername'
password='yourpassword'
warFilePath='./test1.war'
# *** END *** Customizations

curl -X POST -u $username:$password https://$sitename.scm.azurewebsites.net/api/wardeploy --data-binary @$warFilePath
```

## Powershell

Run the following script after editing the placeholder values for web app name, credentials, etc. Credentials can be either your [user credentials or the site credentials](https://docs.microsoft.com/en-us/azure/app-service/app-service-deployment-credentials)

```powershell
# *** BEGIN *** Customizations
$sitename='siteabc1' # if you site is siteabc1.azurewebsites.net
$username='yourusername'
$password='yourpassword'
$warFilePath='D:\test1.war'
# *** END *** Customizations

$base64AuthInfo = [Convert]::ToBase64String([Text.Encoding]::ASCII.GetBytes(("{0}:{1}" -f $username, $password)))
$apiUrl = "https://$sitename.scm.azurewebsites.net/api/wardeploy"
Invoke-RestMethod -Uri $apiUrl -Headers @{Authorization=("Basic {0}" -f $base64AuthInfo)} -Method POST -InFile $warFilePath -ContentType "multipart/form-data"
```
# How does _wardeploy_ work

_wardeploy_ builds on top of [zip push deploy](https://github.com/projectkudu/kudu/wiki/Deploying-from-a-zip-file). In simplest terms, it takes over the unpacking of the WAR file from Tomcat by unpacking it by itself. The WAR file is unpacked to a temporary location and the unpacked contents are then copied over to the appropriate directory in `/home/site/wwwroot/webapps`. While doing this, only the files that have changed since the previous deployment are copied. This works great with scaled out web apps as Tomcat no more performs the unpacking, thereby eliminating the race conditions.

# Known issues
The _wardeploy_ feature is currently in preview and has some known issues:
- The WAR file needs to have a web.xml for the new deployment to be picked up without having to restart the app.
- Deploying to web apps other than the ROOT webapp is not supported yet.

We are actively working on these limitations and they will be addressed very soon.
