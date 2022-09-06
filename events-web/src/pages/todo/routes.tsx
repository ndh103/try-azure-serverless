import { Navigate } from 'react-router-dom'
import TodoHome from './TodoHome'
import Inbox from './inbox/Inbox'
import Today from './today/Today'

const routes = [
  {
    path: '/',
    element: <TodoHome />,
    children: [
      { path: '/', index: true, element: <Navigate to="/today" replace /> },
      { path: '/today', element: <Today /> },
      { path: '/inbox', element: <Inbox /> },
    ],
  },
]

export default routes
