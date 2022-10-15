/* eslint-disable jsx-a11y/click-events-have-key-events */
/* eslint-disable jsx-a11y/no-static-element-interactions */

import { loginRequest } from '@/configs/authConfig'
import { InteractionStatus } from '@azure/msal-browser'
import { useIsAuthenticated, useMsal } from '@azure/msal-react'

function ProfileSection(): JSX.Element {
  const { instance, inProgress } = useMsal()
  const isAuthenticated = useIsAuthenticated()

  const handleLogin = () => {
    instance.loginRedirect(loginRequest)
  }

  const handleLogout = () => {
    instance.logoutRedirect()
  }

  if (isAuthenticated) {
    return (
      <div>
        <button onClick={handleLogout}>Logout</button>
      </div>
    )
  }

  if (inProgress !== InteractionStatus.Startup && inProgress !== InteractionStatus.HandleRedirect) {
    // inProgress check prevents sign-in button from being displayed briefly after returning from a redirect sign-in. Processing the server response takes a render cycle or two
    return (
      <div>
        <button onClick={handleLogin}>Login</button>
      </div>
    )
  }

  return <div />
}

export default ProfileSection
