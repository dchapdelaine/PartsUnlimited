By default, Kudu drives the logic to deploy sites. See [[Deployment]] for details on how that works.

But in some scenarios, it can be interesting to override this mechanism and use custom logic to perform the deployment.

To support this, Kudu looks for a command in the .deployment file at the root of the repo. If it finds that command, it calls into it and lets it perform the entire deployment instead of using its own logic.

One thing that Kudu does do before executing this file is checkout the correct files in the repo. So the .deployment command should generally not be doing any git operations. Instead, it's working with an existing source tree that needs to be deployed.

### Sample .deployment file

```
[config]
command = node build.js
```

### Environment variables

To make the batch file more useful, there are a couple environment variables that it can access:

- DEPLOYMENT_SOURCE: this points to where the source files are, which is normally the root of the repo
- DEPLOYMENT_TARGET: the target of the deployment. Typically, this is the wwwroot folder
- DEPLOYMENT_TEMP: a temporary folder that can be used to store artifacts for the current build. This folder is deleted after the cmd is run.
- MSBUILD_PATH: Path to msbuild executable.


### Working folder

Also, note that the current folder is always the root of the repo (same as %DEPLOYMENT_SOURCE%) when the batch file is executed.


## Examples

To take a trivial example, let's say this file contains simply:

    @echo "Hello"

When you do a 'git push', you'll actually see Hello getting displayed, and nothing else will happen. i.e. none of your files will actually get copied!

So let's say you wanted to copy all the source files to the web root, you could do something like this:

    @echo off
    echo Deploying files...
    xcopy %DEPLOYMENT_SOURCE% %DEPLOYMENT_TARGET% /Y

To process a WAP, things get more complicated, as you need to run msbuild with some pretty advanced options (which is what Kudu normally does by default). e.g. you could do:

    @echo off
    %MSBUILD_PATH% MyWebApp\MyWebApp.csproj /t:pipelinePreDeployCopyAllFilesToOneFolder /p:_PackageTempDir="%DEPLOYMENT_TARGET%";AutoParameterizationWebConfigConnectionStrings=false;Configuration=Debug;SolutionDir="%DEPLOYMENT_SOURCE%"
    
Note that this directly deploys from the repo to wwwroot without going through any TMP folder. It also wipes out the content of wwwroot before deploying, so it doesn't do the smart copying that Kudu normally does. That's why Kudu normally deploys to TMP first, and then does smart copying to wwwroot. And you could potentially do something similar here with fancier scripts.


### Deployment Script Generator

To simplify the custom deployment script authoring we have a script generation tool that will create a script that is customized specifically to your site and will do the same logic as kudu does to deploy your site.

After you generate the script you can customize/edit it and add your own steps.

The generator tool is part of [azure-cli](http://www.windowsazure.com/en-us/manage/linux/other-resources/command-line-tools/) tool, so to use it you need to install azure-cli (you'll need to have node.js in order to use azure-cli):

    npm install azure-cli -g

Then the command to generate a deployment script is:

    azure site deploymentscript [options]

In the [options] part you'll need to specify your site type:
* --aspWAP: ASP.NET web application (requires the location or your .csproj file).
* --aspWebSite: ASP.NET web site.
* --node: node.js.
* --php, --python: self explaining.
* --basic: any other basic website (will only copy the changed files from the repository to the wwwroot directory).

More options:
* -r, --repositoryRoot [dir path]: the root path for the repository (default: current directory).
* -s, --solutionFile [file path]: the solution file path (sln).
* -p, --sitePath [directory path]: the path to the site being deployed (default: same as repositoryRoot).
* -t, --scriptType [batch|bash]: the output script can be of batch type which is the default or bash which is more for non-windows users.

For example, to generate a deployment script for an ASP.NET web application:

    azure site deploymentscript --aspWAP MySite\MySite.csproj -s MySite.sln

The resulting script can also be run and tested locally, it'll output the site into: `your repo root/artifacts/`

For more information on deployment hooks and a step by step sample of the deployment script generator go to [this blog post](http://blog.amitapple.com/post/38417491924/azurewebsitecustomdeploymentpart1).


### Easiest way to get started with a custom deployment script

Instead of using the command line to generate a starter deployment script, there is an alternative approach that is often easier:

- Deploy your repo *without* any deployment scripts.
- Go to the site's [[Kudu Console]].
- From the Tools menu, choose 'Download deployment script'. You'll get a zip with a `.deployment` and `deploy.cmd` files.
- Commit both files at the root of your repo
- Tweak them as needed

