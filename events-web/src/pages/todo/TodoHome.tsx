import { Outlet } from 'react-router-dom'
import MainLayout from '../../layout/MainLayout'

function TodoHome() {
  return (
    <MainLayout>
      <Outlet />
    </MainLayout>
  )
}

export default TodoHome
