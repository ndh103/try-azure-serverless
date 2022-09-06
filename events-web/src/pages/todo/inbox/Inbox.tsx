import { useAppDispatch, useAppSelector } from '@/app/hooks'
import { useEffect } from 'react'
import taskService from '../api-services/taskService'
import { loadTask, selectTasks } from '../tasksSlice'

export default function Inbox() {
  const tasks = useAppSelector(selectTasks)
  const dispatch = useAppDispatch()

  useEffect(() => {
    // declare the data fetching function
    const fetchData = async () => {
      const tasksData = await taskService.getAll()

      if (tasksData != null) {
        dispatch(loadTask(tasksData.data))
      }
    }

    // call the function
    fetchData()
      // make sure to catch any error
      .catch(console.error)
  }, []) // only run once

  return (
    <div>
      {tasks.map((item) => (
        <li key={item.id}>{item.text}</li>
      ))}
    </div>
  )
}
