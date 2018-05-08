**Important: the use of this API is highly discouraged. You should instead deploy your entire Function App as a unit. See [this page](https://github.com/Azure/Azure-Functions/wiki/Deploying-Function-Apps-vs-individual-Functions) for more info.**

Note: this document shows how the APIs are called via ARM. If calling them directly on the Kudu endpoint (i.e. `/api/functions/...`), the payload should only include what is shown *inside* the `properties` block below.

### Getting a function

    get /subscriptions/{sub}/resourceGroups/{rg}/providers/Microsoft.Web/sites/{functionapp}/functions/MyFunction?api-version=2015-08-01


Response:

```json
{
  "id": "/subscriptions/5f6d41f5-c638-4ef0-94be-15784b3c4be9/resourceGroups/MyFuncRG/providers/Microsoft.Web/functions/MyFunction",
  "name": "MyFunction",
  "type": "Microsoft.Web/functions",
  "location": "West US",
  "properties": {
    "name": "MyFunction",
    "function_app_id": "/subscriptions/5f6d41f5-c638-4ef0-94be-15784b3c4be9/resourceGroups/MyFuncRG/providers/Microsoft.Web/sites/funcapp123",
    "script_root_path_href": "https://funcapp123.scm.azurewebsites.net/api/vfs/site/wwwroot/MyFunction/",
    "script_href": "https://funcapp123.scm.azurewebsites.net/api/vfs/site/wwwroot/MyFunction/index.js",
    "config_href": "https://funcapp123.scm.azurewebsites.net/api/vfs/site/wwwroot/MyFunction/function.json",
    "secrets_file_href": "https://funcapp123.scm.azurewebsites.net/api/vfs/data/functions/secrets/MyFunction.json",
    "config": {
      "bindings": [
        {
          "webHookType": "genericJson",
          "direction": "in",
          "type": "httpTrigger"
        },
        {
          "type": "http",
          "direction": "out"
        }
      ]
    }
  }
}
```

### Listing functions

    get /subscriptions/{sub}/resourceGroups/{rg}/providers/Microsoft.Web/sites/{functionapp}/functions?api-version=2015-08-01

Response:

```json
{
  "value": [
    {
      ...
    }
  ]
}
```

### Creating or Updating a function

    put /subscriptions/{sub}/resourceGroups/{rg}/providers/Microsoft.Web/sites/{functionapp}/functions/MyFunction?api-version=2015-08-01

Request body (sample):

```json
{
  "location": "West US",
  "properties": {
    "config": {
      "bindings": [
        {
          "webHookType": "genericJson",
          "type": "httpTrigger",
          "direction": "in",
          "name": "req"
        },
        {
          "type": "http",
          "direction": "out",
          "name": "res"
        }
      ]
    },
    "files": {
      "index.js": "module.exports = function (context, data) {\r\n    context.res = {\r\n        body: { greeting: 'Hello ' + data.first + ' ' + data.last + '!'}\r\n    };\r\n\r\n    context.done();\r\n};\r\n"
    },
    "test_data": "{ 'first': 'David', 'last': 'Ebbo' }"
  }
}
```

Response: same as doing a GET above.

### Getting a function's secrets

    post /subscriptions/{sub}/resourceGroups/{rg}/providers/Microsoft.Web/sites/{functionapp}/functions/MyFunction/listsecrets?api-version=2015-08-01

Response body:

```json
{
  "key": "xyzxyzxyzxyzxyz",
  "trigger_url": "https://functionsbay.azurewebsites.net/api/HttpTriggerNodeJS1?code=xyzxyzxyzxyzxyz"
}
```

### Syncing the function app's triggers

    post /subscriptions/{sub}/resourceGroups/{rg}/providers/Microsoft.Web/sites/{functionapp}/syncfunctiontriggers?api-version=2015-08-01

Request body should be empty.

Response body:

```json
{
  "status":"success"
}
```
