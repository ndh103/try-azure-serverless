import { Navigate } from 'react-router-dom'
import EventHome from './EventHome'
import EventList from './pages/EventList'

const routes = [
  {
    path: '/',
    element: <EventHome />,
    children: [
      { path: '/', index: true, element: <Navigate to="/events" replace /> },
      { path: '/events', element: <EventList /> },
    ],
  },
]

export default routes
