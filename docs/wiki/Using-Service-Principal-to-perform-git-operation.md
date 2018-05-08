Currently, we don't support setting the deployment credential for Service Principal (likely in the future).   As a work around, you can perform git operation using Service Principal's bearer token directly.

- Login with Service Principal

```
az login --service-principal --username <app_id>  --password <secret_or_path_to_pfx_cert> --tenant <tenant>
```

See [this](https://docs.microsoft.com/sl-SI/cli/azure/create-an-azure-service-principal-azure-cli?toc=%2Fazure%2Fazure-resource-manager%2Ftoc.json&view=azure-cli-latest#log-in-using-the-service-principal) for details.

- Get bearer token of Service Principal

```
az account get-access-token --resource https://management.core.windows.net/
```

See [this](https://docs.microsoft.com/en-us/cli/azure/account?view=azure-cli-latest#az-account-get-access-token) for details.    The bearer token is returned in `accessToken` property.  I suggest to store accessToken to `ACCESS_TOKEN` env for later use (`set ACCESS_TOKEN=ey...`).

- Pass the bearer token to Git operations

```
git -c http.extraheader="Authorization: Bearer %ACCESS_TOKEN%"  push https://mysite.scm.azurewebsites.net master
```


