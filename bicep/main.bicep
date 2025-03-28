@allowed([
    'canadaeast'
    'canadacentral'
])
param location string

param sqlServerName string

param sqlAdminLogin string

@minLength(10)
@maxLength(20)
@secure()
param sqlAdminPassword string

param storageName string

var apps = [
    {
        spName: 'tardi1'
        webApps: ['mvc', 'apiinteret']
        miseAEchelle: 'Non'
    }
    {
        spName: 'tardi2'
        webApps: [ 'apiassurance', 'apicredit']
        miseAEchelle: 'Non'
    }
]

var skuMap = {
    Non: 'F1'
    Manuel: 'B1'
    Auto: 'S1'
}

var databaseNames = [ 'mvc', 'assurance', 'cartecredits']

module appService 'modules/appService.bicep' = [ for app in apps: {
    name: 'appService${app.spName}'
    params: {
        location: location
        spName: app.spName
        webAppNames: app.webApps
        spSku: skuMap[app.miseAEchelle]
    }
}]

module sqldatabase 'modules/sqldatabase.bicep' = {
    name: 'sqldatabase${sqlServerName}'
    params: {
        sqlServerName: sqlServerName
        databaseNames: databaseNames
        location: location
        sqlAdminLogin: sqlAdminLogin
        sqlAdminPassword: sqlAdminPassword
        DTUmin: 5
        DTUmax: 5
        startIpAddress: '0.0.0.0'
        endIpAddress: '255.255.255.255'
    }
}

module storageService 'modules/storage.bicep' = {
    name: 'storageService_${storageName}'
     params: {
         storageName: storageName
          location: location
          storageSku: 'Standard_ZRS'
     }
}
