export enum TaskStatus {
  Initial = 0,
  InProgress = 1,
}

export interface TaskModel {
  id: number
  text: string
  status: TaskStatus
}
