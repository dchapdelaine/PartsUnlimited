All Web Apps have both a main site and an SCM site (where Kudu runs, including WebJobs). When you click Stop in the portal, only the main site is stopped, while the SCM site keeps running. Stopping both is possible, but the steps are more complex.

To stop both the main site and SCM site:

- Go to Resource Explorer (https://resources.azure.com/)
- Find your Web App (aka 'site'), either from the tree or using search box
- While on your site node, you'll see some JSON in the right panel. Click `Edit` at the top (you may have to switch to Read/Write mode first)
- In the JSON, you need to change two things: change `"state"` from `"Running"` to `"Stopped"`, and set `"scmSiteAlsoStopped"` to `true`
- Press the green `PUT` button at the top

Note that when you hit Restart, both the main and SCM sites get restarted.

### Why would you need to do this?

It can be useful in rare cases where the SCM site is in a bad state, and you don't even want it to start. e.g. it could be keeping something locked as soon as it starts, and you want to prevent that so you can connect to the site using FTP without anything running (e.g. to delete some files).

Be aware that stopping the scm site disables all Kudu features, all site extensions, as well as msdeploy (e.g. VS deployments).

### How do you restart it after that?

The simplest way is to just hit Run in the portal, which will cause both sites to run.