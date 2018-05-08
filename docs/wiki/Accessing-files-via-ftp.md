When you have a Windows Azure Web Site, it is possible to access its files via FTP. This includes both the files that are deployed, and the repository and deployment related files.

To connect via FTP, you need the following information:

- **The FTP host name**: which will be something like `waws-prod-blu-001.ftp.azurewebsites.windows.net` (the `blu` token might instead be `am2` or `db3` based on location). You'll find it under the Dashboard tab for your site in the Azure portal.
- **Your username**: you'll also find it on the dashboard, under Deployment User. It's the same as your git username, but prefixed with the site name. **Don't forget the site name part, or it won't work!** e.g. __MyCoolSite\davidebbo__
- **Your password**: use the same password as when you `git push`

You can use whatever FTP client you like, but you can't go wrong if you go with [FileZilla](http://filezilla-project.org/). Note that the Windows Explorer FTP support has a tendency to do over-aggressive caching, which can yield unexpected results.

Once you've FTP'd, check out [[File structure on Azure]] to navigate through the files.