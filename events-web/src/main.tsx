import axios from 'axios'
import React, { ReactElement } from 'react'
import ReactDOM from 'react-dom/client'
import { Provider } from 'react-redux'
import { BrowserRouter } from 'react-router-dom'
import App from './App'
import { store } from './app/store'
import './index.css'
import { globalConfig, globalConfigUrl } from './configs/config'

const app: ReactElement = (
  <React.StrictMode>
    <BrowserRouter>
      <Provider store={store}>
        <App />
      </Provider>
    </BrowserRouter>
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
