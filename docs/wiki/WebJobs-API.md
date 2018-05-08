This API is accessed the same way as the git endpoint. e.g. if your git URL is `https://yoursite.scm.azurewebsites.net/yoursite.git`, then the API to get the list of deployments will be `https://yoursite.scm.azurewebsites.net/deployments`.

The credentials you use are the same as when you git push. See [[Deployment-credentials]] for more details.

### List all web jobs ###

    GET /api/webjobs

## Triggered Jobs ##

### List all triggered jobs ###

    GET /api/triggeredwebjobs

**Response**

```javascript
[
  {
    name: "jobName",
    runCommand: "...\run.cmd",
    type: "triggered",
    url: "http://.../triggeredwebjobs/jobName",
    history_url: "http://.../triggeredwebjobs/jobName/history",
    extra_info_url: "http://.../",
    scheduler_logs_url: "https://.../vfs/data/jobs/triggered/jobName/job_scheduler.log",
    settings: { },
    using_sdk: false,
    latest_run:
      {
        id: "20131103120400",
        status: "Success",
        start_time: "2013-11-08T02:56:00.000000Z",
        end_time: "2013-11-08T02:57:00.000000Z",
        duration: "00:01:00",
        output_url: "http://.../vfs/data/jobs/triggered/jobName/20131103120400/output_20131103120400.log",
        error_url: "http://.../vfs/data/jobs/triggered/jobName/20131103120400/error_20131103120400.log",
        url: "http://.../triggeredwebjobs/jobName/history/20131103120400",
        trigger: "Schedule - 0 0 0 * * *"
      }
  }
]
```

### List all triggered jobs in swagger format ###

    GET /api/triggeredwebjobsswagger

**Response**

```json
{
  "swagger": "2.0",
  "info": {
    "version": "v1",
    "title": "WebJobs"
  },
  "host": "placeHolder",
  "schemes": [
    "https"
  ],
  "paths": {
    "/api/triggeredjobs/jobName/run": {
      "post": {
        "deprecated": false,
        "operationId": "jobName",
        "consumes": [],
        "produces": [],
        "responses": {
          "200": {
            "description": "Success"
          },
          "default": {
            "description": "Success"
          }
        },
        "parameters": [
          {
            "name": "arguments",
            "in": "query",
            "description": "Web Job Arguments",
            "required": false,
            "type": "string"
          }
        ]
      }
    }
  }
}
```

### List all triggered jobs in swagger format###

    GET /api/triggeredwebjobsswagger

**Response**

```javascript
[
  {
    name: "jobName",
    runCommand: "...\run.cmd",
    type: "triggered",
    url: "http://.../triggeredwebjobs/jobName",
    history_url: "http://.../triggeredwebjobs/jobName/history",
    extra_info_url: "http://.../",
    latest_run:
      {
        id: "20131103120400",
        status: "Success",
        start_time: "2013-11-08T02:56:00.000000Z",
        end_time: "2013-11-08T02:57:00.000000Z",
        duration: "00:01:00",
        output_url: "http://.../vfs/data/jobs/triggered/jobName/20131103120400/output_20131103120400.log",
        error_url: "http://.../vfs/data/jobs/triggered/jobName/20131103120400/error_20131103120400.log",
        url: "http://.../triggeredwebjobs/jobName/history/20131103120400"
      }
  }
]
```

### Get a specific triggered job by name ###

    GET /api/triggeredwebjobs/{job name}

**Response**

```json
[
{
  "swagger": "2.0",
  "info": {
    "version": "v1",
    "title": "WebJobs"
  },
  "host": "placeHolder",
  "schemes": [
    "https"
  ],
  "paths": {
    "/api/triggeredjobs/jobName/run": {
      "post": {
        "deprecated": false,
        "operationId": "jobName",
        "consumes": [],
        "produces": [],
        "responses": {
          "200": {
            "description": "Success"
          },
          "default": {
            "description": "Success"
          }
        },
        "parameters": [
          {
            "name": "arguments",
            "in": "query",
            "description": "Web Job Arguments",
            "required": false,
            "type": "string"
          }
        ]
      }
    }
  }
}
]
```

### Upload a triggered job as zip ###

Using a zip file containing the files for it, or just a single file (e.g foo.exe).

    PUT /api/zip/site/wwwroot/App_Data/jobs/triggered/{job name}/

or

    PUT /api/triggeredwebjobs/{job name}

Use `Content-Type: application/zip` for zip otherwise it's treated as a regular script file.

The file name should be in the `Content-Dispostion` header, example:

    Content-Disposition: attachement; filename=run.cmd

**Note**: the difference between the two techniques is that the first just adds files into the folder, while the second first deletes any existing content before adding new files.

### Delete a triggered job ###

    DELETE /api/vfs/site/wwwroot/App_Data/jobs/triggered/{job name}?recursive=true

or

    DELETE /api/triggeredwebjobs/{job name}


### Invoke a triggered job ###

    POST /api/triggeredwebjobs/{job name}/run

**To run with arguments use the arguments parameters that will be added to the script when invoked. It also gets passed to the WebJob as the `WEBJOBS_COMMAND_ARGUMENTS` environment variable.**

    POST /api/triggeredwebjobs/{job name}/run?arguments={arguments}

Note: if the site has multiple instances, the job will run on one of them arbitrarily. This is the same behavior as regular requests sent to the site.

In the http response, you get back a location attribute, with a URL to the details of the run that was started. e.g.

    Location: https://mysite.scm.azurewebsites.net/api/triggeredwebjobs/SomeJob/history/201605192149381933



### List all triggered job runs history ###

    GET /api/triggeredwebjobs/{job name}/history

**Response**

```javascript
{
  runs:
    [
      {
        id: "20131103120400",
        status: "Success",
        start_time: "2013-11-08T02:56:00.000000Z",
        end_time: "2013-11-08T02:57:00.000000Z",
        duration: "00:01:00",
        output_url: "http://.../vfs/data/jobs/triggered/jobName/20131103120400/output_20131103120400.log",
        error_url: "http://.../vfs/data/jobs/triggered/jobName/20131103120400/error_20131103120400.log",
        url: "http://.../triggeredwebjobs/jobName/history/20131103120400",
        trigger: "Schedule - 0 0 0 * * *"
      },
      ...
    ]
}
```
**Note**: The job history is kept in `D:\home\data\jobs\triggered\jobName` folder.   Each history of job is kept under different folder by datetime of the execution.  The api returns all job history in datetime descending order (meaning latest on top).  We only keep most recent **50** job history (configurable by `WEBJOBS_HISTORY_SIZE` appSettings).

### Get a specific run for a specific triggered job ###

    GET /api/triggeredwebjobs/{job name}/history/{id}

**Response**

```javascript
{
  id: "20131103120400",
  status: "Success",
  start_time: "2013-11-08T02:56:00.000000Z",
  end_time: "2013-11-08T02:57:00.000000Z",
  duration: "00:01:00",
  output_url: "http://.../vfs/data/jobs/triggered/jobName/20131103120400/output_20131103120400.log",
  error_url: "http://.../vfs/data/jobs/triggered/jobName/20131103120400/error_20131103120400.log",
  url: "http://.../triggeredwebjobs/jobName/history/20131103120400",
  trigger: "Schedule - 0 0 0 * * *"
}
```

## Continuous Jobs ##

### List all continuous jobs ###

    GET /api/continuouswebjobs

**Response**

```javascript
[
  {
    name: "jobName",
    status: "Running",
    runCommand: "...\run.cmd",
    log_url: "http://.../vfs/data/jobs/continuous/jobName/job.log",
    extra_info_url: "http://.../",
    url: "http://.../continuouswebjobs/jobName",
    type: "continuous"
  }
]
```

### Get a specific continuous job by name ###

    GET /api/continuouswebjobs/{job name}

**Response**

```javascript
{
  name: "jobName",
  status: "Running",
  runCommand: "...\run.cmd",
  log_url: "http://.../vfs/data/jobs/continuous/jobName/job.log",
  extra_info_url: "http://.../",
  url: "http://.../continuouswebjobs/jobName",
  type: "continuous"
}
```

The `status` can take the following values:

- Initializing
- Starting
- Running
- PendingRestart
- Stopped
- Aborted
- Abandoned
- Success
- Failure


### Upload a continuous job as zip ###

Using a zip file containing the files for it.

    PUT /api/zip/site/wwwroot/App_Data/jobs/continuous/{job name}/

or

    PUT /api/continuouswebjobs/{job name}

Use `Content-Type: application/zip` for zip otherwise it's treated as a regular script file.

The file name should be in the `Content-Dispostion` header, example:

    Content-Disposition: attachement; filename=run.cmd

**Note**: the difference between the two techniques is that the first just adds files into the folder, while the second first deletes any existing content before adding new files.

### Delete a continuous job ###

    DELETE /api/vfs/site/wwwroot/App_Data/jobs/continuous/{job name}?recursive=true

or

    DELETE /api/continuouswebjobs/{job name}

### Start a continuous job ###

    POST /api/continuouswebjobs/{job name}/start

### Stop a continuous job ###

    POST /api/continuouswebjobs/{job name}/stop

### Get continuous job settings ###

    GET /api/continuouswebjobs/{job name}/settings

**Response**

```json
{
  "is_singleton": true
}
```

### Set a continuous job as singleton ###

If a continuous job is set as singleton it'll run only on a single instance opposed to running on all instances. By default, it runs on all instances.

    PUT /api/continuouswebjobs/{job name}/settings

Body

```json
{
  "is_singleton": true
}
```

> To set a continuous job as singleton during deployment (without the need for the REST API) you can simply create a file called ```settings.job``` with the content: ```{ "is_singleton": true }``` and put it at the root of the (specific) WebJob directory.

### Set the schedule for a triggered job ###

You can set the schedule for invoking a triggered job by providing a cron expression made of 6 fields (second, minute, hour, day, month, day of the week).

    PUT /api/triggeredwebjobs/{job name}/settings

Body

```json
{
  "schedule": "0 */2 * * * *"
}
```

> To set the schedule for a triggered job during deployment (without the need for the REST API) you can simply create a file called ```settings.job``` with the content: ```{ "schedule": "0 */2 * * * *" }``` and put it at the root of the (specific) WebJob directory.