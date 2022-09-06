import { useAppDispatch, useAppSelector } from '@/app/hooks'
import { useEffect } from 'react'
import eventsApi from '../apis/eventsApi'
import { loadEvents, selectEvents } from '../eventsSlice'

export default function Inbox() {
  const events = useAppSelector(selectEvents)
  const dispatch = useAppDispatch()

  useEffect(() => {
    // declare the data fetching function
    const fetchData = async () => {
      const tasksData = await eventsApi.getAll()

      if (tasksData != null) {
        dispatch(loadEvents(tasksData.data))
      }
    }

    // call the function
    fetchData()
      // make sure to catch any error
      .catch(console.error)
  }, []) // only run once

  return (
    <div>
      {events.map((item) => (
        <li key={item.id}>{item.title}</li>
      ))}
    </div>
  )
}
