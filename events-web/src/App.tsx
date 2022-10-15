import { useRoutes } from 'react-router-dom'
import routes from './features/routes'

function App() {
  const appRoutes = useRoutes(routes)
  return appRoutes
}

export default App
