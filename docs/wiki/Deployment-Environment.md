In addition to the environment variables Kudu provide for every command (e.g. in Debug Console and `/api/command`), Kudu provides several additional environment variables to the deployment script.
- `DEPLOYMENT_SOURCE` is the repository directory (e.g. `site\repository`).
- `DEPLOYMENT_TARGET` is the runtime location of the website (e.g. `site\wwwroot`).
- `POST_DEPLOYMENT_ACTION` and `POST_DEPLOYMENT_ACTIONS_DIR` are the command and directory to run post deployment.
- `SCM_COMMIT_ID` provides the deployment ID. In the case of Git deployment it is the commit ID.
- `SCM_COMMIT_MESSAGE` has the commit message.
- `DEPLOYMENT_TEMP` is the directory for storing temporary files during deployment.
- `KUDU_SELECT_NODE_VERSION_CMD` command to run `selectNodeVersion.js`, which handles the deployment time node/npm version, and generates `web.config` and `iisnode.yml` if needed.
- `WEBJOBS_DEPLOY_CMD` is the command to deploy web jobs.