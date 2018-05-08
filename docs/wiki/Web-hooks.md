**Scenario**: an external component wants to know when deployments are complete. It registers a callback url that will be called each time a deployment has finished, with the deployment information on whether the deployment was successful or not, the author, commit id and more.

**Possible use case**: With this feature you're able to hook a kudu site as a trigger in web services such as [Logic Apps](https://aka.ms/logicapps) or [zapier.com](https://zapier.com) which allows you to do a wide array of actions when a deployment is done, for example:
- Send an email when deployment is done
- Call when a deployment has failed
- Tweet when a deployment has succeeded
- And more...

## API exposed by the Kudu service ##

### Subscribe / Add Hook ###

    POST /api/hooks

**Body**

```
{
  "url": "http://www.callback.com/callback",
  "event": "PostDeployment",
  "insecure_ssl": false (set to true to ignore https certificate check, for test purposes only)
}
```

**Response**

201 Created or 409 Conflict (if url already exists as the address of a hook but the event is different)

```
{
  "id": 123,
  "url": "http://www.callback.com/callback",
  "event": "PostDeployment"
}
```

***

### Unsubscribe / Remove Hook ###

    DELETE /api/hooks/[id]

**Response**

200 OK

***

### List Hooks ###

    GET /api/hooks

**Response**

200 OK

```
[
  {
    "id": 123,
    "url": "http://www.callback.com/callback",
    "event": "postDeployment"
  },
  {
    ...
  }
  ...
]
```

***

### Get Hook ###

    GET /api/hooks/[id]

**Response**

200 OK

```
{
  "id": 123,
  "url": "http://www.callback.com/callback",
  "event": "PostDeployment",
  "last_datetime": "08/08/2013 13:24PM",
  "last_status": "OK",
  "last_reason": "internal server SSL issue or whatever",
  "last_context": "The last event content published"
}
```

***

### Publish Hook Type ###

    POST /api/hooks/hooks/publish/[hook type]

**Body**

```
{
  ...
}
```

**Response**

200 OK


***

## Hook request fired by the Kudu service ##

This is request sent by the Kudu service to the registered URL.

    POST [url]

**Body**

```
{
  "id": "982843aff56d37f2bfb9f532a9c0465031f4172d",
  "status": "success",
  "statusText": "",
  "authorEmail": "someone@somewhere.com",
  "author": "Someone",
  "message": "My fix",
  "deployer": "Someone",
  "receivedTime": "2015-12-16T00:52:07.7240633Z",
  "startTime": "2016-02-27T17:44:25.8567966Z",
  "endTime": "2016-02-27T17:45:06.2016537Z",
  "lastSuccessEndTime": "2016-02-27T17:45:06.2016537Z",
  "complete": true,
  "siteName": "MySite",
  "hostName": "MySite(-slotName).scm.azurewebsites.net"
}
```

## Hook scenarios

### Notify deployment status to Slack

See https://github.com/WCOMAB/KuduPostDeploymentSlackHook for details