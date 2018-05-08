There are three different ways to publish files to an Azure Web App:

1. git: the main topic of this wiki, and what the Kudu project is all about!
2. [Web Deploy](http://www.iis.net/download/webdeploy) (also known as MSDeploy)
3. FTP

When it comes to authenticating with these three mechanisms, you cannot use your Microsoft ID (aka Live ID) credentials (which the protocols can't easily support), but instead need to use deployment credentials.

To make things more interesting, there are actually two sets of deployment credentials that you can use to publish to a Website. This page discusses the two alternatives.

## User credentials (aka Deployment Credentials)

These are the credentials that you choose yourself in the Azure portal. If you're not sure what they are, you can reset them by going to the Dashboard tab for any site and clicking 'Reset deployment credentials' (under quick glance).

These credentials are directly tied to a Microsoft Account, and not to a particular web app. This needs to be emphasized, because the Azure portal UI is a bit confusing, as you need to go under a specific site on order to change them. But changing them under one site affects all of them!

Note that when an Azure subscription has multiple admins/co-admins, each person has their own set of credentials, since they each have a different Microsoft Account. The same is true for users that are given access to a Web App via RBAC (Role Based Access Control). In other words, user-level credentials are never meant to be shared among different users.

One key point about the user-level credentials is that since you specifically set them, they are meant to be memorized, and directly typed by the user when needed (e.g. when doing a git push).


## Site credentials (aka Publish Profile Credentials)

These are the credentials that are automatically generated for each site. In order to see them, you need to download the **Publish Profile**, which you can do in the **Overview** blade of a site.
The same credentials are also exposed through the _Properties_ blade under **Deployment Trigger URL**.

The publish profile is an XML file that contains both WebDeploy and FTP related things. If you glance in there, you will easily locate the credentials. They look like:

- Username: if your site is named 'MyCoolSite', the user name will be '$MyCoolSite'
- Password: some long random string

Note that the multi co-admins scenario does not affect those credentials. All the admins/co-admins will end up with the exact same site-level credentials for a given site (but each site has different ones of course).

And unlike User lever credentials, the site-level credentials are definitely not meant to be memorized (though you're welcome to try!). Instead, they're meant to be used by programs that automates deployment for you (like Visual Studio).

## When to use which set of credentials

Typically, you use the user-level credentials for git and FTP, and the site-level credentials for Web Deploy. However, **you can use both sets of credentials for all three publishing techniques**.

So why would you want to use the big random site-level credentials when doing a git push? Well, you wouldn't. However, there can be scenarios where a tool needs to do this on your behalf, and using the site-level credentials can make a lot of sense there.

Also, note that when it comes to credentials, everything that mentions git applies equally to all entry points provided by the Kudu service.


## Important note about the FTP username

When using user-level credentials, note that you need to prepend the site name to the username you use for FTP. So you'd have FTP full user names that look like this:

* With user-level credentials: **MyCoolSite\davidebbo**
* With site-level credentials: either **MyCoolSite\\$MyCoolSite**, **$MyCoolSite** or, simply, **MyCoolSite** would work.

For user with **CSP** subscription, to access FTP with user-level credential, you must provide the tenantId (guid) directive.  This is similar info user needs to provide when accessing portal (https://portal.azure.com/tenantName).

* With user-level credentials: **MyCoolSite\davidebbo@tenantId**

See more details about [[accessing files via ftp]].
