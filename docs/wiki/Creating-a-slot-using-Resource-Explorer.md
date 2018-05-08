This applies to both Web App and Function Apps.

### Creating a slot

- First, create a regular App using the Portal
- Go to https://resources.azure.com/
- Find your site in the tree or using the search box
- In the json for the site, capture the location (e.g. 'North Central US') and the serverFarmId (long ARM resource name).
- Under the site, click on the 'slots' node (you don't have any yet)
- Click on the Create tab
- Set the name to Staging (or whatever you want to call the slot)
- Set both the location and the serverFarmId to the same values that you saw earlier
- Click the PUT button at the top. You're done, you have a slot!

### Swapping

- Go to your slot
- Click on the Action tab
- Go down to the slotswap button
- Under targetSlot in the json, type 'production'
- Click the slotswap button
