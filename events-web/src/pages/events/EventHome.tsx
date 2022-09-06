import { Outlet } from 'react-router-dom'
import MainLayout from '../../layout/MainLayout'

function EventHome() {
  return (
    <MainLayout>
      <Outlet />
    </MainLayout>
  )
}

export default EventHome
