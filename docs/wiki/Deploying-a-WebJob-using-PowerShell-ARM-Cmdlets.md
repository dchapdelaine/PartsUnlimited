In the classic Powershell Cmdlets you could publish a WebJob using the New-AzureWebsiteJob cmdlet. 

There is no direct equivalent in the ARM PowerShell Cmdlets.

The script below will allow you to do this.

    #Resource details :
    $resourceGroupName = "<Resourcegroup name>";
    $webAppName = "<WebApp name>";
    $Apiversion = 2015-08-01

    #Function to get Publishing credentials for the WebApp :
    function Get-PublishingProfileCredentials($resourceGroupName, $webAppName){

    $resourceType = "Microsoft.Web/sites/config"
    $resourceName = "$webAppName/publishingcredentials"
    $publishingCredentials = Invoke-AzureRmResourceAction -ResourceGroupName $resourceGroupName -ResourceType 
    $resourceType -ResourceName $resourceName -Action list -ApiVersion $Apiversion -Force
       return $publishingCredentials
    }

    #Pulling authorization access token :
    function Get-KuduApiAuthorisationHeaderValue($resourceGroupName, $webAppName){

    $publishingCredentials = Get-PublishingProfileCredentials $resourceGroupName $webAppName
    return ("Basic {0}" -f [Convert]::ToBase64String([Text.Encoding]::ASCII.GetBytes(("{0}:{1}" -f 
    $publishingCredentials.Properties.PublishingUserName, $publishingCredentials.Properties.PublishingPassword))))
    }

    $accessToken = Get-KuduApiAuthorisationHeaderValue $resourceGroupName $webAppname
    #Generating header to create and publish the Webjob :
    $Header = @{
    'Content-Disposition'='attachment; attachment; filename=Copy.zip'
    'Authorization'=$accessToken
            }
    $apiUrl = "https://$webAppName.scm.azurewebsites.net/api/<Webjob-type>/<Webjob-name>"
    $result = Invoke-RestMethod -Uri $apiUrl -Headers $Header -Method put -InFile "<Complete path of the file>\
    <filename>.zip" -ContentType 'application/zip' 
    #NOTE: Update the above script with the parameters highlighted and run in order to push a new Webjob under the specified WebApp.


Once the WebJob is published you can use the following commands to list,start/stop or delete the WebJob.

    #Get list of all Webjobs
    Get-AzureRmResource -ResourceGroupName $ResourceGroupName -ResourceName $AppService -ResourceType 
    microsoft.web/sites/<Webjob-type> -ApiVersion $Apiversion 

    #Start/Stop the Webjobs
    Invoke-AzureRmResourceAction -ApiVersion $Apiversion  -ResourceGroupName $ResourceGroupName -ResourceName 
    $ResourceName -ResourceType microsoft.web/sites/<Webjob-type> -Action start/stop  -Force

    #Delete a Webjob
    Remove-AzureRmResource -ApiVersion $Apiversion  -ResourceGroupName $ResourceGroupName -ResourceName $ResourceName -
    ResourceType microsoft.web/sites/<Webjob-type>/<Webjob-name> -Force 

