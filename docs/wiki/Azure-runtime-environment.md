This document describes various aspects of the environment that Azure Web Apps run under.

## Environment

Azure Web Apps sets some environment variables with information about your Web App.

### Website Environment Variables

- `WEBSITE_SITE_NAME` - The name of the site.
- `WEBSITE_SKU` - The sku of the site (Possible values: `Free`, `Shared`, `Basic`, `Standard`).
- `WEBSITE_COMPUTE_MODE` - Specifies whether website is on a **dedicated** or **shared** VM/s (Possible values: `Shared`, `Dedicated`).
- `WEBSITE_HOSTNAME` - The Azure Website's primary host name for the site (For example: `site.azurewebsites.net`). Note that custom hostnames are not accounted for here.
- `WEBSITE_INSTANCE_ID` - The id representing the VM that the site is running on (If site runs on multiple instances, each instance will have a different id).
- `WEBSITE_NODE_DEFAULT_VERSION` - The default node version this website is using.
- `WEBSOCKET_CONCURRENT_REQUEST_LIMIT` - The limit for websocket's concurrent requests.

### Environment Variables used to get perf counters

See [[Perf Counters exposed as environment variables]] for details.

### WebJobs Environment Variables

[[Web-jobs#environment-settings]]

## File system

[[Understanding the Azure App Service file system]]

## Restrictions

Azure Web Apps run in a sandboxed environment that allows multiple sites to safely co-exist on the same machine.

In most cases, sites are able to do most things that 'normal' web sites typically need to do. But in some scenario, you may run into something that doesn't work due to sandbox limitations. See [[Azure Web-App Sandbox]] for more details.
