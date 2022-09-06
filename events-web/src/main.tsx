import axios from 'axios'
import React, { ReactElement } from 'react'
import ReactDOM from 'react-dom/client'
import { Provider } from 'react-redux'
import { BrowserRouter as Router } from 'react-router-dom'
import { PublicClientApplication, EventType } from '@azure/msal-browser'

import { msalConfig } from '@/configs/authConfig'
import { MsalProvider } from '@azure/msal-react'
import App from './App'
import { store } from './app/store'
import './index.css'
import { globalConfig, globalConfigUrl } from './configs/config'

export const msalInstance = new PublicClientApplication(msalConfig)

// Default to using the first account if no account is active on page load
if (!msalInstance.getActiveAccount() && msalInstance.getAllAccounts().length > 0) {
  // Account selection logic is app dependent. Adjust as needed for different use cases.
  msalInstance.setActiveAccount(msalInstance.getAllAccounts()[0])
}

// Optional - This will update account state if a user signs in from another tab or window
msalInstance.enableAccountStorageEvents()

msalInstance.addEventCallback((event) => {
  if (
    event.eventType === EventType.LOGIN_SUCCESS ||
    event.eventType === EventType.ACQUIRE_TOKEN_SUCCESS ||
    event.eventType === EventType.SSO_SILENT_SUCCESS
  ) {
    const { account } = event.payload as any
    msalInstance.setActiveAccount(account)
  }
})

const app = (
  <React.StrictMode>
    <Router>
      <Provider store={store}>
        <MsalProvider instance={msalInstance}>
          <App />
        </MsalProvider>
      </Provider>
    </Router>
  </React.StrictMode>
)

axios
  .get(globalConfigUrl)
  .then((response) => {
    globalConfig.set(response.data)
    return app
  })
  .catch((e) => {
    const errorMessage = 'Error while fetching global config, the App wil not be rendered. (This is NOT a React error.)'
    return <p style={{ color: 'red', textAlign: 'center' }}>{errorMessage}</p>
  })
  .then((reactElement: ReactElement) => {
    ReactDOM.createRoot(document.getElementById('root')!).render(reactElement)
  })
