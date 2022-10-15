# try-azure-serverless

Serverless micro-services using Azure Infrastructure.

# Architecture Overview

![Architecture Overview](./documents/images/try%20server%20less.drawio.png)

# Todos

- Custom login page
  - https://dev.to/425show/customize-the-look-and-feel-of-your-azure-ad-b2c-page-30m6
  - https://learn.microsoft.com/en-us/azure/active-directory-b2c/customize-ui-with-html?pivots=b2c-user-flow
- Integrate with Microsoft Graph API
  - https://learn.microsoft.com/en-us/azure/active-directory-b2c/microsoft-graph-get-started?tabs=app-reg-ga
- Design Authorization api and database
- Validate the JWT Token inside the function app
  - [https://medium.com/cheranga/azure-functions-validate-azure-active-directory-tokens-using-your-own-custom-binding-4b4ff648d8ac](https://medium.com/cheranga/azure-functions-validate-azure-active-directory-tokens-using-your-own-custom-binding-4b4ff648d8ac)
  - [https://damienbod.com/2020/09/24/securing-azure-functions-using-azure-ad-jwt-bearer-token-authentication-for-user-access-tokens/](https://damienbod.com/2020/09/24/securing-azure-functions-using-azure-ad-jwt-bearer-token-authentication-for-user-access-tokens/)
  - https://github.com/fmichellonet/AzureFunctions.Extensions.OpenIDConnect
- Connect to Azure Cosmos Db
- CI/CD Pipeline
- IAC Pipeline
