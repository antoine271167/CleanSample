param location string = resourceGroup().location
param resourcesName string

module createAppInsights 'modules/create-appinsights.bicep' = {
  name: 'createAppInsights'
  params: {
    location: location
    resourcesName: resourcesName
  }
}
