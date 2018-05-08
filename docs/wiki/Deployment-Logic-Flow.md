## Preparation

Updating the repository with the new changes, at this point the folder: `site\repository` will have the pre-compiled website with all the new changes.

## Deployment

### 1. Figure out the type of project we're dealing with and the project/solution file to deploy.

Options:

- .NET Web Application (has a .csproj or .vbproj file).
- .NET Web Site (has a solution and no proj files).
- Other, treated as the basic deployment path.

> Custom deployment is not part of this doc yet exists since it's the part we are about to change.

### 2.1 .NET Web Application

- Prepare temporary path.
- Build project to temporary path.
- Smart copy from temp path to web root (also: use previous manifest for the smart copy and create a new manifest after the copy completes).
- Delete temporary path.

### 2.2 .NET Web Site

- Build web site to current repository path.
- Smart copy from repository path to web root.

### 2.3 Plain web site with no build step (e.g. Node, PHP)

- Smart copy from repository path to web root.
- Download npm packages on web root (using npm install).
- Add IIS node configuration (if required == no web.config and this looks like a node.js web site).
- Select node version (if required == looks like a node.js web site).

## Post Deployment

Update the deployment status (the deployment is finished and active).
