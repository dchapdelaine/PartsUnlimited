Normally, the way to deploy changes to Kudu is to push to the repository provided by Kudu. As an alternative, some users prefer to push to GitHub, and have that automatically trigger a deployment.

The following guide is GitHub specific, for a more general approach (e.g. repository outside GitHub) see [[Manually triggering a deployment]]

GitHub has a Service Hooks feature which makes implementing this relatively straightforward.

## High level workflow

### Initial setup

- Kudu provides a URL that can be hit to trigger deployments
- The user sets that URL as a github WebHook


### Deployment cycle

- The user pushed a change to GitHub
- GitHub sends a POST request to the URL set as the WebHook, passing it a JSON payload as described in the [GitHub docs](https://help.github.com/articles/post-receive-hooks)
- Kudu receives that request and gets the GitHub repo URL from the payload
- Kudu pulls from the GitHub repo URL (or clones if it doesn't have a repo yet)
- Kudu makes a deployment, using the same logic as if the user had pushed directly to the Kudu repo


## Authenticating with Kudu

Obviously, only a trusted source should be able to send a request to the Kudu URL and cause it to trigger a build. Since the Kudu service site is protected via basic auth (in the Azure Web Site scenario), that request needs to be authenticated.

Having to include the user's credentials in the GitHub hook would not be ideal. But luckily, Azure Web Sites also support [Site-Level credentials](https://github.com/projectkudu/kudu/wiki/Deployment-credentials). They are a  better fit here because that distinct for each site, and are randomly generated.

So the Kudu deployment trigger URL might look like:

`https://$MyCoolSite:SOME_BIG_RANDOM_TOKEN@mycoolsite.scm.azurewebsites.net/deploy`


## Authenticating with GitHub

If the GitHub repo is public, then anyone can pull from it without authentication. Initially, we might only support public repos in this workflow.

For private repos, there are several possible approaches:

### 1. Using GitHub credentials

If the user provides their GitHub credentials to Kudu, could would be able to pull from the private repo using basic auth over https.

This is probably not a great solution.

### 2. Using SSH keys

For private repository, Kudu supports SSH.  Kudu can generate the SSH key-pair.  The private key will be kept within Kudu.  You can get the public key via `/sshkey?ensurePublicKey=1` endpoint.  Note: `ensurePublicKey` directs Kudu to generate key-pair if not exists.

In addition to setting hook, you need to set the public key as `Deploy Key` for the repository in order to allow Kudu to fetch from it.

### 3. Using a Kudu-owned GitHub account

If Kudu owned its own GitHub account (e.g. kudu_user), the user could add it as a readonly collaborator.

Unfortunately, this solution doesn't work well with Kudu because the Kudu service itself runs as user code. This means that the Kudu service cannot easily keep a secret from the user, which it would need to do to protect the kudu_user GitHub account credentials.