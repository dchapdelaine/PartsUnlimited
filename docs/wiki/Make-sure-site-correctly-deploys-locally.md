When you push an ASP.NET Web Application to an Azure Web Site, Kudu builds it by using msbuild to create deployment artifacts.

In some cases, it's possible to have a Web Application that runs fine from VS, but that becomes broken when you use msbuild to deploy it, whether you run it in Azure or elsewhere.

Here are a few example scenarios

### Content files in repo, but missing from csproj

Example scenario:
- you need an extra .js file and add it to your repo
- you use it and test it in Visual Studio, and everything works great
- you deploy it and find that things are busted. You investigate and realize that the .js file is missing. **Where did you go wrong?**

**Answer**: you didn't add the file to your VS Project, so msbuild doesn't know about it! But when you run directly in VS you can't see that because the file is there. Or maybe the file is in the project, but the Build Action is not `Content`, so it doesn't get copied.

### Problem with transform file

Another example is when there is a problem with your Web.release.config transform file, which doesn't affect things when you run straight from VS, but blows up when you publish.


### Project dependency issues

In some cases, multiple projects in a solution don't correctly depend on each other. If you build the entire solution, than things might still work. But if you just build the Web project, only the strict dependency tree is built, which may fail is things are not quite right.


## Locally testing your deployment

The simplest way to detect this type of issues early on is to test your deployment locally. You can do this either using VS, or from the command line.

In either case, start by completely cleaning your repository to make sure it has no files other than what's committed (you can do this by running `git clean -dxf`). Or you can use a fresh clone, which will guarantee it's clean.

### Local deployment using VS

- In VS, right click on your Project and choose 'Publish...'
- In the drop down, choose 'New Profile' and call it something like 'local publish'
- In the Publish Method drop down, choose 'File System', and pick a target location, e.g. `c:\mysite`
- Click the Publish button

### Local deployment using msbuild

Go to the folder that has your `.sln` file, and run the following command (adjust the path to the `.csproj` for your scenario). This is what Kudu runs by default when you deploy (it can be overridden if you have custom deployment script).

For classic .NET:

    msbuild MyMvcApp\MyMvcApp.csproj /nologo /verbosity:m /t:Build /t:pipelinePreDeployCopyAllFilesToOneFolder /p:_PackageTempDir="c:\mysite";AutoParameterizationWebConfigConnectionStrings=false;Configuration=Release;UseSharedCompilation=false /p:SolutionDir="."

And for .NET Core:

    msbuild ASPNetCore\ASPNetCore.csproj /p:PublishUrl="c:\mysite" /p:DeployOnBuild=true /p:Configuration=Release /p:WebPublishMethod=FileSystem /p:DeployTarget=WebPublish /p:AutoParameterizationWebConfigConnectionStrings=false /p:SolutionDir="."

### Testing the resulting files

With either approach, your `c:\mysite` folder now has a set of files that is very similar to what you would have on Azure.

The next step is to try running it locally. One simple way to do this is to open that folder as a Web Site in VS (Alt-Shift-O). Or you could configure an IIS application to point to it.

If it doesn't run correctly, then it won't run in Azure either, and you'll need to tackle that problem first.
