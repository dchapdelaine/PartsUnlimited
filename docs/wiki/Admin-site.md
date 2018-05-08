*Note: this is an experimental feature that is subject to change or could be removed altogether*

Each App Service Plan (aka Server Farm) can have an **Admin Site**, which can be used to share behavior or files among all the other Web Apps in the App Service Plan.

Note that this feature is only supported in Standard or higher mode. It is not available to Free/Shared/Basic Web Apps.

## Scenarios

This is useful in two scenarios:
- Creating [[Azure Site Extensions]] that will be available in all the Web Apps in the Plan
- Creating a folder on the local drive that will be accessible (read-only) to all the Web Apps in the Plan 

## Creating an admin site

- Create a new Web App that you will use as your admin site, inside the relevant App Service Plan 
- Go to https://resources.azure.com and locate the App Service Plan (under 'serverfarms')
- Click Edit, and set the `adminSiteName` property to your admin site's name. e.g. `"adminSiteName": "myadminsite"`
- Click Put to save the change

## Deploying Bits into the Admin Site

- Go to your admin site's [[Kudu Console]]
- Create a folder with this structure: `D:\home\SiteExtensions\MySiteExtension\1.0.0`
- Under the version folder, drop a zip file (as is, not expanded!)
- Restart the admin site

The expanded content of the zip file will get copied to a folder named `D:\Program Files (x86)\AdminExtensions\1040747975\MySiteExtension\1.0.0`. Note that the `1040747975` will vary.

If your zip file contains a site extension, it is now active in all other Web Apps in the plan (pending restart), and you are done.

## Accessing the deployed admin bits from other Web Apps

- After deploying the admin bits, pick another Web App in the same plan, and restart it.
- Go into that Web App's Kudu Console, and click on the Environment tab at the top
- You should see a new environment variable called `WEBSITE_ADMIN_SITEID`, with value `1040747975` (or whatever your segment is above).
- You can now access the bits from code by looking up their location in `@"D:\Program Files (x86)\AdminExtensions\{0}\MySiteExtension\1.0.0"`, where you replace `{0}` with `Environment.GetEnvironmentVariable("WEBSITE_ADMIN_SITEID")`.