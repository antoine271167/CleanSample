param siteName string
param appRegClientId string

resource authsettings 'Microsoft.Web/sites/config@2022-09-01' = {
  name: '${siteName}/authsettingsV2'
  properties: {
    platform: {
      enabled: true
      runtimeVersion: '~1'
    }
    globalValidation: {
      requireAuthentication: true
      unauthenticatedClientAction: 'Return401'
      redirectToProvider: 'azureactivedirectory'
    }
    identityProviders: {
      azureActiveDirectory: {
        enabled: true
        registration: {
          clientId: appRegClientId
          clientSecretSettingName: 'MICROSOFT_PROVIDER_AUTHENTICATION_SECRET'
        }
        login: {
          disableWWWAuthenticate: false
        }
        validation: {
          jwtClaimChecks: {}
          allowedAudiences: []
          defaultAuthorizationPolicy: {
            allowedPrincipals: {}
          }
        }
      }
    }
  }
}
