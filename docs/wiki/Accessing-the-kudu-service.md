Every Azure web site has an associated 'scm' service site, which runs both Kudu and other [Site Extensions](https://github.com/projectkudu/kudu/wiki/Azure-Site-Extensions).

If your web site has URL `http://mysite.azurewebsites.net/`, then the root URL of the Kudu service is `https://mysite.scm.azurewebsites.net/`. Note the added `scm` token.

For mobile services, the site URL looks like `https://myservice.azure-mobile.net/`, and the Kudu root URL is 
`https://myservice.scm.azure-mobile.net/default.aspx`.

Notes:

- The scm service host name is not affected by the presence of custom host names in your site. It always has the pattern above.
- The scm site uses single sign on, so if you're already logged on to the Azure portal, you will be able to go directly to it without typing additional credentials.
- If you prefer not to use Single Sign On and instead use basic auth, you can go to `http://mysite.scm.azurewebsites.net/basicauth` (`https://myservice.scm.azure-mobile.net/basicauth` for mobile services). The scm service will prompt you for credentials. Here, you can use either the user publishing credentials (same as you use for git), or the site level credentials. See [[Deployment-credentials]] for details.


## Once you're in the Kudu service

The root of the Kudu service has links to various things that can be done from there, and are discussed elsewhere in this wiki. e.g., you can:

- Get the Kudu diagnostic dump
- Launch the [[diagnostic console|Kudu-console]]
- See info about the runtime environment
- Navigate the REST API
