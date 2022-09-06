/* eslint-disable jsx-a11y/click-events-have-key-events */
/* eslint-disable jsx-a11y/no-static-element-interactions */
import { PropsWithChildren, useState } from 'react'
import { ReactComponent as CloseIcon } from '@/assets/images/closeIcon.svg'
import { ReactComponent as MenuIcon } from '@/assets/images/menuIcon.svg'
import SidebarMenu from './SidebarMenu'

function MainLayout({ children }: PropsWithChildren): JSX.Element {
  const [isDrawerOpen, setDrawerStatus] = useState(false)

  const toggleSidebar = () => {
    setDrawerStatus(!isDrawerOpen)
  }

  return (
    <div>
      <div className="h-12 w-full bg-teal-400 fixed p-3">
        <MenuIcon className="h-4 w-4 inline-block lg:hidden" onClick={toggleSidebar} />
        Vite React Todo
      </div>
      <div className="flex pt-12">
        {/* Overlay div */}

        <div className={`fixed top-0 left-0 h-screen w-screen bg-gray-300 z-10 opacity-60 ${isDrawerOpen ? 'block' : 'hidden'}`} onClick={toggleSidebar} />
        {/* Left sidebar */}
        <div className={`flex-none overflow-auto h-full fixed w-80 bg-gray-100 p-3 lg:block ${isDrawerOpen ? 'block z-20' : 'hidden'}`}>
          <div className="flex flex-row-reverse">
            <CloseIcon className="h-4 w-4 cursor-pointer lg:hidden" onClick={toggleSidebar} />
          </div>
          <SidebarMenu />
        </div>
        {/* Content */}
        <div className="flex-grow w-full p-5 lg:ml-80">{children}</div>
      </div>
    </div>
  )
}

export default MainLayout
