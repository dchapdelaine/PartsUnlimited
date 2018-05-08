In many cases, for the Azure Web App team to properly investigate an issue with a site or a Function App (alternate instructions [below](https://github.com/projectkudu/kudu/wiki/Reporting-your-site-name-without-posting-it-publicly#what-about-function-apps)), we need to know your site name. If you are fine sharing your site name publicly (e.g. `mysite.azurewebsites.net`), then you can just do that.

However, if you prefer not to share it publicly on the forum or in a Github issue (which we fully understand), there is an alternate way that will allow the team to know your site name without revealing it to others:
- Create a new dummy site in the same App Service Plan (or at least the same region) as the site you need help with. e.g. call this site `dummy2468`, or whatever name you find is available.
- You don't need to deploy anything in that dummy site. Just leave it empty.
- Mention the name of that dummy site in the public forum. This will allow us to locate your Web Hosting Plan, and other relevant details about your subscription.
- **IMPORTANT: If there are multiple sites in your Web Hosting Plan, you need to give us some small additional hint about which one is at stake (e.g. the site that starts with 'a').**
- Try to keep the site around until you no longer need assistance.


## What if you can't even create a site

If things are so broken for you that you are not even able to create a new site, you can still convey to us information about your subscription by creating a dummy storage account in your subscription and sharing its name.

## What about Function Apps

See [here](https://github.com/Azure/azure-webjobs-sdk-script/wiki/Sharing-Your-Function-App-name-privately) for alternate easier instructions that are specific to Function Apps.