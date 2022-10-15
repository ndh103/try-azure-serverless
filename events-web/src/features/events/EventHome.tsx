import { AuthenticatedTemplate, UnauthenticatedTemplate } from '@azure/msal-react'
import { Outlet } from 'react-router-dom'
import MainLayout from '../../layout/MainLayout'

function EventHome() {
  return (
    <div>
      <MainLayout>
        <AuthenticatedTemplate>
          <Outlet />
        </AuthenticatedTemplate>

        <UnauthenticatedTemplate>
          <div>Please sign-in to see your profile information.</div>
        </UnauthenticatedTemplate>
      </MainLayout>
    </div>
  )
}

export default EventHome
