/* eslint-disable no-param-reassign */
import { createSlice } from '@reduxjs/toolkit'
import type { PayloadAction } from '@reduxjs/toolkit'
import { RootState } from '@/app/store'
import { TaskModel } from './api-services/taskModel'

export interface TasksState {
  tasks: Array<TaskModel>
}

const initialState: TasksState = {
  tasks: [],
}

export const tasksSlice = createSlice({
  name: 'tasks',
  initialState,
  reducers: {
    loadTask: (state, action: PayloadAction<Array<TaskModel>>) => {
      state.tasks = action.payload
    },
    addTask: (state, action: PayloadAction<TaskModel>) => {
      state.tasks.push(action.payload)
    },
    removeTask: (state, action: PayloadAction<number>) => {
      const taskIndex = state.tasks.findIndex((r) => r.id === action.payload)
      state.tasks = state.tasks.filter((r) => r.id !== taskIndex)
    },
  },
})

// Action creators are generated for each case reducer function
export const { addTask, loadTask, removeTask } = tasksSlice.actions

// Other code such as selectors can use the imported `RootState` type
export const selectTasks = (state: RootState) => state.tasks.tasks

export default tasksSlice.reducer
