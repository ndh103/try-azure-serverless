import React, { ReactNode } from 'react'
import { NavLink, To } from 'react-router-dom'

function ActiveNavLink({ to, children }: { to: To; children: ReactNode }) {
  const navLinkClasses = 'm-2 p-2 rounded-lg block hover:bg-gray-300'
  return (
    <NavLink to={to} className={({ isActive }) => (isActive ? `${navLinkClasses} bg-gray-300` : `${navLinkClasses} bg-gray-200`)}>
      {children}
    </NavLink>
  )
}

export default function SidebarMenu() {
  return (
    <nav>
      <ul>
        <li>
          <ActiveNavLink to="today">Today</ActiveNavLink>
        </li>
        <li>
          <ActiveNavLink to="inbox">Inbox</ActiveNavLink>
        </li>
      </ul>
    </nav>
  )
}
