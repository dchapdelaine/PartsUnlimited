If you're having problems deploying your ASP.NET Core repo to Kudu/Azure, the first step is to check whether it works correctly locally.

Please use the following steps. If anything doesn't work, then you have an issue that is not related to Kudu, and you should work through it before attempting to deploy to Azure.

#### Start clean

Make a clean clone of your repository in a new folder. This is important to make sure you don't rely on uncommitted files that Kudu won't have.

#### Restore NuGet packages

Go to the folder that has your solution file, and run

    nuget restore

#### Build your solution

Build it in-place using the following command. Change `MySolution.sln` to the name of your solution file:

    msbuild MySolution.sln /t:Build /p:Configuration=Release 

#### Publish the files to a test folder

Replace `src\MyAspNetCoreProject` by the path to the folder that has your web app's project.json. You can also adjust the test output folder.

    dotnet publish src\MyAspNetCoreProject --output c:\output --configuration Release --no-build

#### Run your web app from the output folder

Go to the output folder you used in the previous steps, and run (adjust with the name of your web project's assembly):

    dotnet MyAspNetCoreProject.dll

It should give you a URL you can go to, e.g.

    Now listening on: http://localhost:5000

Go to that URL and make sure your app works.