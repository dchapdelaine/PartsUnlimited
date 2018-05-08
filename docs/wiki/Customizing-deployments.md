When deploying a git repository via Kudu, the rules for picking a specific project are as follows:

1. If there's a .deployment file at the root of the repository go to step 4, otherwise go to the next step.
2. Scan for solution files, if there's multiple solutions, fail, if there's 1 solution file go to next step, if none, go to step 6.
3. Now that we have a solution, pick the first website or WAP in that solution and deploy it, if there's none, then fail.
4. Find the target project file, if there's multiple projects, fail, if there's 1 project file, deploy it, otherwise go to next step.
5. look for a website project in the specified folder (by finding a solution that has a website with the same path), if more than one, fail, otherwise deploy the website project.
6. Find the target WAP, if there's none then Xcopy deploy all the repository files (excluding .git and .hg) to the web root.

# .deployment file
Deployment configuration files let you override the default heuristics of deployment by allowing you to specify a project or folder to be deployed. It has to be at the root of the repository and it's in .ini format. Here are some examples:

## Deploying with custom script

You can specify the custom deployment script to build and deploy your application.

Here is an example:

    [config]
    command = deploy.cmd

For PowerShell, it takes a little bit of crazy syntax, but the following works:

    [config]
    command = powershell -NoProfile -NoLogo -ExecutionPolicy Unrestricted -Command "& "$pwd\deploy.ps1" 2>&1 | echo"

| Syntax                          | Please explain                                           |
|:--------------------------------|:---------------------------------------------------------|
| `&`                             | Call operator. Runs a command, script, or script block. |
| `-ExecutionPolicy Unrestricted` | On Azure App Service the system-wide setting is `RemoteSigned`, you need this parameter in to execute scripts |
| `2>&1`                          | Redirect standard error (2) to standard output (1)      |
| `echo`                          | Alias to `Write-Output`. Same effect as `Out-Default`. Necessary to work around [this console host window title behavior](https://github.com/projectkudu/KuduScript/pull/33#issuecomment-143817501)

Bash scripts are supported as well, on both Windows and Linux:

    [config]
    command = bash deploy.sh

Note that on Linux, bash scripts *must* have Unix-style (LF) line endings! If you are developing on Windows, make sure to configure your editor properly, as well as [git](https://help.github.com/articles/dealing-with-line-endings/).

## Deploying a specific ASP.NET or ASP.NET Core project file

You can specify the path to the project file, relative to the root of your repo. **Note that this is not a path to the solution file (.sln), but to the project file (.csproj/.vbproj)**. The reason for this is that Kudu only builds the minimal dependency tree for this project, and avoids building unrelated projects in the solution that are not needed by the web project.

Here is an example:

    [config]
    project = WebProject/WebProject.csproj

## Deploying a specific folder for a Node, PHP, or ASP.NET site

If the folder you want to deploy is not the root of  the repository, you can specify which folder to deploy. e.g.

    [config]
    project = MyWebRoot

In case you have a solution/project file in your repository but you want it to be ignored and have your full repo deployed as a web site, you can use the following (e.g. Orchard use this):

    [config]
    project = .

## Using App Settings instead of a .deployment file

One downside of using a .deployment file is that it is committed to the repo, and sometimes you want to make that selection outside of the repo.

Let's say that you have one repo that contains two different ASP.NET projects (possibly in the same solution), which you want to deploy to different sites. You couldn't use a .deployment file here, as it can only point to one project.

Instead, you can use App Settings to set the same values that are supported in the .deployment file. The steps are:

- Go to the Configure tab for you site in the Azure portal
- Add an App Setting called `PROJECT`, and set its value to something like `WebProject/WebProject.csproj`
- Then in your other web site you can set `PROJECT` to point to a different .csproj file.