/* eslint-disable no-param-reassign */
import { createSlice } from '@reduxjs/toolkit'
import type { PayloadAction } from '@reduxjs/toolkit'
import { RootState } from '@/app/store'
import { EventModel } from './apis/eventModel'

export interface EventsState {
  events: Array<EventModel>
}

const initialState: EventsState = {
  events: [],
}

export const eventsSlice = createSlice({
  name: 'events',
  initialState,
  reducers: {
    loadEvents: (state, action: PayloadAction<Array<EventModel>>) => {
      state.events = action.payload
    },
    addEvent: (state, action: PayloadAction<EventModel>) => {
      state.events.push(action.payload)
    },
    removeEvent: (state, action: PayloadAction<number>) => {
      const taskIndex = state.events.findIndex((r) => r.id === action.payload)
      state.events = state.events.filter((r) => r.id !== taskIndex)
    },
  },
})

// Action creators are generated for each case reducer function
export const { addEvent, loadEvents, removeEvent } = eventsSlice.actions

// Other code such as selectors can use the imported `RootState` type
export const selectEvents = (state: RootState) => state.events.events

export default eventsSlice.reducer
