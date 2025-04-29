@description('Navnsprefix for ressursene')
param baseName string = 'minedyrapi'
@description('Administrator login')
param administratorLogin string
@secure()
@description('Administrator passord')
param administratorPassword string
@description('Lokasjon')
param location string = resourceGroup().location

resource pgServer 'Microsoft.DBforPostgreSQL/flexibleServers@2022-12-01' = {
  name: '${baseName}-pg'
  location: location
  sku: { name: 'Standard_B1ms', tier: 'Burstable' }
  properties: {
    administratorLogin: administratorLogin
    administratorLoginPassword: administratorPassword
    version: '14'
    storage: { storageSizeGB: 32 }
  }
}

resource appDatabase 'Microsoft.DBforPostgreSQL/flexibleServers/databases@2022-12-01' = {
  parent: pgServer
  name: 'appdb'
}

resource plan 'Microsoft.Web/serverfarms@2022-03-01' = {
  name: '${baseName}-plan'
  location: location
  sku: { name: 'F1', tier: 'Basic', capacity: 1 }
}

resource webApp 'Microsoft.Web/sites@2022-03-01' = {
  name: '${baseName}-web'
  location: location
  kind: 'app'
  properties: {
    serverFarmId: plan.id
    siteConfig: {
      appSettings: [
        {
          name: 'ConnectionStrings__DefaultConnection'
          value: 'Host=${pgServer.name}.postgres.database.azure.com;Port=5432;Database=appdb;Username=${administratorLogin}@${pgServer.name};Password=${administratorPassword};'
        }
      ]
    }
  }
}