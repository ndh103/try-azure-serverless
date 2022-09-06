import homeRoutes from './events/routes'
import aboutRoutes from './about/routes'

function NotFoundPath() {
  return (
    <main style={{ padding: '1rem' }}>
      <p>There's nothing here!</p>
    </main>
  )
}

const routes = [
  ...homeRoutes,
  ...aboutRoutes,
  {
    path: '*',
    element: <NotFoundPath />,
  },
]

export default routes
