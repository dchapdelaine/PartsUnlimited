## Overview

There are a couple of settings that can be used to optimize the deployment.  Below describes what those are and how one or more can be used to fit your scenario.

## Settings

* SCM_REPOSITORY_PATH

This represents the repository path.  The value can either be absolute path (such as `d:\home\site\myfolder`) or a relative path of site directory; for instance, `myfolder` setting means `d:\home\site\myfolder`.  The default is `repository`; meaning the repository will be created at `d:\home\site\repository` folder.
  
* SCM_NO_REPOSITORY

This indicates whether repository should be created with source control (Git/Hg).  By default, the source control is always enabled.  With an overhead of additional source control files and functionalities, the benefit is one can rollback and deploy from a certain changeset id in the history.  If set to `1`, no source control is enabled and deployment history will only be read-only (not rollback-able).  However, there is no overhead of source control (faster deployment).  Setting this to '1' effectively ensures no git deployment can occur. All git deployments after SCM_NO_REPOSITORY is set to '1' will result in a 400 error. Only dropbox can be used with this setting since it has no source control.

* PROJECT

This indicates which sub folder in the repository folder to be deployed.  This is often used where there are multiple projects in the repository, this knob will dictate which project/folder to be deployed.  The default is `.`; meaning deploying from the root of repository. 

* SCM_TARGET_PATH

This indicates which folder to deploy to.  The value can either be absolute path (such as `d:\home\site\myfolder`) or a relative path of wwwroot directory; for instance, `myfolder` setting means `d:\home\site\wwwroot\myfolder`.  The default is `.`; meaning deploying to `d:\home\site\wwwroot`.

## Scenarios
Given the above 4 settings, one can use in combination to achieve interesting scenarios.

### Inplace deployment

By setting `SCM_REPOSITORY_PATH` to `wwwroot`, the repository (artifact source) and wwwroot (destination) folders are the same!  The benefit is there is no need to copy from repository (or temporary folder in case of msbuild) to wwwroot.  This speeds up the deployment especially for sites with many files and only a few changed for each deployments.  This is very suitable for site (aka. Basic Web Site) that requires no msbuild.

By the way, this mode is turned on automatically when the `git clone\fetch` is occurred for site initially deployed by msdeploy (gallery) as well as ftp deploy. 

### Inplace deployment with readonly history

By setting `SCM_NO_REPOSITORY` to `1`, the default `SCM_REPOSITORY_PATH` will auto adjust to `wwwroot`.  This allows inplace deployment without any source control overhead.   This is suitable for deploying from Dropbox where you simply want to file-to-file sync up as is with wwwroot and no source control.

### Deploy from a subfolder of a repository

By setting `PROJECT`, you can define `from` which subfolder in repository to deploy.  See [[Customizing deployments]].

### Deploy to a subfolder of a wwwroot

By setting `SCM_TARGET_PATH`, you can define `to` which subfolder in `wwwroot` to deploy.

### Deploy from a subfolder of a repository to a subfolder of a wwwroot

Simply use a combination of `PROJECT` and `SCM_TARGET_PATH` [#664](https://github.com/projectkudu/kudu/issues/664)

## Notes

See these [steps](http://social.msdn.microsoft.com/Forums/windowsazure/en-US/46cd0039-7427-4c77-b796-cabeb5eb5316/git-deployment-and-get-my-files-back) from a forum user who successfully used this.