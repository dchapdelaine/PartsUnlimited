When it comes to deploying sites from a git repo, Kudu support two main scenarios: .NET Web Application Project (aka. WAP), and everything else.

## Non-WAP site (e.g. Node, PHP, ASP.NET Web Site) ##

This is the simpler case, as in essence deploying just means copying files from the repository to the web root.

- A 'git push' is received and processed, triggering a post-receive hook
- Kudu checks out the relevant commit such that we have the right working files
- Files are copied from the repo to wwwroot
  - What's copied may be a subfolder if there is a [[.deployment file|Customizing deployments]])
  - The copying is done in a smart way such that it only copies files that have changed.
  - It's also smart enough to delete files that were removed from the repo, while not deleting files that were created at runtime by the site.
- For Node sites, Kudu runs 'npm install' in the wwwroot folder


## ASP.NET Web Application Projects (WAP) ##

This case is a bit more complex because there is a build process that needs to happen before files can be deployed.

- A 'git push' is received and processed, triggering a post-receive hook
- Kudu checks out the relevant commit such that we have the right working files
- Kudu locates the relevant csproj file that needs to be built. See [[Customizing deployments]] for details on this.
- msbuild is run, and produces artifact in a TMP folder
- Artifacts are copied to wwwroot, using the same smart logic as in the non-WAP case

## Taking over the deployment process

If you want to completely override how the deployment is performed, see [[Deployment-hooks]].