param storageAccountName string = 'stg${uniqueString(resourceGroup().id)}'
param functionAppName string = 'func${uniqueString(resourceGroup().id)}'
param functionAppPlanName string = 'plan${uniqueString(resourceGroup().id)}'
param sqlServerName string = 'plan${uniqueString(resourceGroup().id)}'
param location string = resourceGroup().location
param administratorLogin string
@secure()
param administratorPassword string

var sqlConnectionString = 'Server=tcp:${sqlServer.name}${environment().suffixes.sqlServerHostname},1433;Initial Catalog=WarehouseDatabase;Persist Security Info=False;User ID=${administratorLogin};Password=${administratorPassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;'

// Storage Account and Queue Setup
resource storageAccount 'Microsoft.Storage/storageAccounts@2021-06-01' = {
  name: storageAccountName
  location: location
  kind: 'StorageV2'
  sku: {
    name: 'Standard_LRS'
  }
}
resource workItemQueue 'Microsoft.Storage/storageAccounts/queueServices/queues@2022-05-01' = {
  name: '${storageAccountName}/default/workitemchanges'
}

// Function App setup
resource hostingPlan 'Microsoft.Web/serverfarms@2021-03-01' = {
  name: functionAppPlanName
  location: location
  sku: {
    tier: 'Standard'
    name: 'S1'
    size: 'S1'
    family: 'S'
    capacity: 1
  }
  properties: {
  //  reserved: true
  }
}

resource functionApp 'Microsoft.Web/sites@2021-03-01' = {
  name: functionAppName
  location: location
  kind: 'functionapp'
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    serverFarmId: hostingPlan.id
    siteConfig: {
      //linuxFxVersion: 'dotnet|6.0'
      alwaysOn: true
      appSettings: [
        {
          name: 'AzureWebJobsStorage'
          value: 'DefaultEndpointsProtocol=https;AccountName=${storageAccountName};EndpointSuffix=${environment().suffixes.storage};AccountKey=${storageAccount.listKeys().keys[0].value}'
        }
        {
          name: 'WEBSITE_CONTENTAZUREFILECONNECTIONSTRING'
          value: 'DefaultEndpointsProtocol=https;AccountName=${storageAccountName};EndpointSuffix=${environment().suffixes.storage};AccountKey=${storageAccount.listKeys().keys[0].value}'
        }
        {
          name: 'WEBSITE_CONTENTSHARE'
          value: toLower(functionAppName)
        }
        {
          name: 'FUNCTIONS_EXTENSION_VERSION'
          value: '~4'
        }
        {
          name: 'FUNCTIONS_WORKER_RUNTIME'
          value: 'dotnet'
        }
      ]
      connectionStrings: [
        {
          name: 'WarehouseDatabase'
          type: 'SQLAzure'
          connectionString: sqlConnectionString
        }
      ]
      ftpsState: 'FtpsOnly'
      minTlsVersion: '1.2'
    }
    httpsOnly: true
  }
}

// SQL Server Deployment
resource sqlServer 'Microsoft.Sql/servers@2022-05-01-preview' = {
  name: sqlServerName
  location: location
  properties: {
    administratorLogin: administratorLogin
    administratorLoginPassword: administratorPassword
  }
}

resource sqlDB 'Microsoft.Sql/servers/databases@2022-05-01-preview' = {
  parent: sqlServer
  name: 'WarehouseDatabase'
  location: location
  sku: {
    name: 'Basic'
    tier: 'Basic'
    capacity: 5
  }
}

output functionAppName string = functionApp.name
output sqlConnectionString string = sqlConnectionString
