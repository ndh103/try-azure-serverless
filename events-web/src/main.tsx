import React from 'react'
import ReactDOM from 'react-dom/client'
import { Provider } from 'react-redux'
import { BrowserRouter } from 'react-router-dom'
import App from './App'
import { store } from './app/store'
import './index.css'

import mirageFakeServer from './pages/todo/api-services/fakeTaskApi'

// Just to get the fakeServer executed
if (mirageFakeServer) {
  console.log('Mirage Fake Server is ready', mirageFakeServer)
}

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <BrowserRouter>
      <Provider store={store}>
        <App />
      </Provider>
    </BrowserRouter>
  </React.StrictMode>
)
