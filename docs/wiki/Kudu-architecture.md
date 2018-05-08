### Kudu's single tenant architecture

Kudu has a somewhat unusual architecture, in the sense that it is a single-tenant rather than multi-tenant service. What this means is that each Azure Web Site which uses git deployment has its own instance of the Kudu service, completely distinct from the Kudu service sites used for other Azure sites.

Furthermore, the Kudu site itself runs as an Azure web site, albeit a somewhat special site. The best way to look at the Kudu service is that it is a 'buddy site' to the real Azure site. The main difference is that the Kudu site is being given access to the files of the real site, allowing it to publish to it.


### Security model

The Kudu site runs in the same sandbox as the real site. This has some important implications.

First, the Kudu site cannot do anything that the site itself wouldn't be able to do itself. This is very important, because as part of building/deploying an app, arbitrary user code can run (e.g. msbuild custom code, etc...). But being in the same sandbox as the site, the only thing it can harm is the site itself.

Furthermore, the Kudu site shares the same quotas as the site. That is, the CPU/RAM/Disk used by the Kudu service is counted toward the site's quota. This makes sense, as it prevents a bad build process from using more resources than it should. In normal situations, the build process should not hurt the site quotas in any measurable way, but if it starts doing bad things, it will get blocked in the exact same way as if the site itself tried to do those things.

So to summarize, the Kudu services completely relies on the security model of the Azure Web Site runtime, which keeps it both simple and secure.


### Authentication model

One difference between the Kudu service site and the real site is that the Kudu site is authenticated by http basic auth, which by default the real site is not authenticated.

Note that the basic auth happens *before* requests reach the Kudu service, which is why you won't find any code related to this in the Kudu sources. See the [[deployment credentials]] page for more details on this.

As for the real site, it can of course implement any authentication mechanism that it wants, but it is something that it needed to handle through its own logic.


### Process model

The Kudu service site runs in the same sandbox as the site.  By default, it runs in the different *process* as the site. 

The only thing that really 'connects' the Kudu site to the real site is the file system, and that works regardless of what process or machine they each run on.