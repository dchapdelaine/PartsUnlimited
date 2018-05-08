## Overview

To diagnose issues, one may need to get the list of running processes, their cpu and memory usages, or even take a minidump.  Kudu service exposes processes and threads endpoint (`/api/processes`) enabling users to achieve exactly that. If you start at the [[root of the Kudu service|Accessing-the-kudu-service]], you'll see a `Processes and mini-dumps` link that lets you easily navigate the REST API (a browser extension like [JSONView](https://chrome.google.com/webstore/detail/jsonview/chklaanhfefbnpoihckbnefhakgolnmc?hl=en) is recommended).

## Using /api/processes

`<kudu-service-url>/api/processes` endpoint returns the list of running processes in json format.   Each process info contains process name, id and a link `/api/processes/{id}` to its detail information (such as memory, cpu usages, etc.).  Note: the id `0` is reserved for current `w3wp.exe` process allowing a shortcut to this via `/api/processes/0`.  One can take the minidump of any given process by simply connect to `/api/processes/{id}/dump`.      

## Using /api/processes/{id}/threads

`<kudu-service-url>/api/processes/{id}/threads` endpoint returns the list of threads for a given process.   Each thread info contains id, state and a link `/api/processes/{id}/threads/{tid}` to its detail information (such as memory, cpu usages, etc.).  

## Process Environment Variable

One of the properties returned from `<kudu-service-url>/api/processes/{id}` is `environment_variables` which is a collection of all the environment variables in that process. You can filer scm processes from main site processes using another property called `is_scm_site`.

## Summary

* `GET /api/processes` => list of processes.
* `GET /api/processes/{id}` => detail information of process with `id`.
* `GET /api/processes/{id}/threads` => list of threads for process with `id`.
* `GET /api/processes/{id}/threads/{tid}` => details about the thread.
* `GET /api/processes/{id}/modules` => list of modules for process with `id`.
* `GET /api/processes/{id}/modules/{base_address}` => details about the module.
* `DELETE /api/processes/{id}` => terminate process with `id` and its children.
* `GET /api/processes/0` => detail process information of scm site's `w3wp.exe`. Or use -1 for main site.
* `DELETE /api/processes/0` => terminate scm site's `w3wp.exe` process and its children. Or use -1 for main site.
* `GET /api/processes/{id}/dump` => minidump of process with `id`.  By default, this is minimum dump without memory (dumptType=0).
* `GET /api/processes/{id}/dump?dumpType=2` => full memory minidump of process with `id`.  The available dumpType  value can be found at [MINIDUMP_TYPE](http://msdn.microsoft.com/en-us/library/windows/desktop/ms680519.aspx).
* `GET /api/processes/{id}/dump?dumpType=2&format=zip` => full memory minidump of process with `id`.  This will include matching sos.dll and mscordackws.dll in the zip package.  

For web sites running on multiple instances the WEBSITE_INSTANCE_ID environment variable can indicate the current instance ID. An instance ID can be used to modify the cookie that directs a request to a specific instance of the web site - before dump requests above. Listing all instance IDs for a web site requires use of the Azure Web Sites REST API.  

## Limitations

### It cannot get dumps for crashed processes

This feature is about getting dumps for running processes, and not for crashed processes.

### w3wp needs to be healthy enough to process the dump request

Since the Kudu service runs in the same w3wp process as the site, w3wp needs to be able to process the dump request. In most cases, it will be, but it's possible for w3wp to be corrupted enough not to be able to handle the request.

### Full dump could cause download quota to be reached

The resources used by the dump feature are counted toward the site limits. For the default small dump, it's not an issue, but if requesting a full dump for a Free Azure site, you could reach the quota which could cause some site downtime.