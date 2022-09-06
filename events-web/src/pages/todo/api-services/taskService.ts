import HttpClient from '@/common/HttpClient'

class TaskService {
  urlPrefix = '/tasks'

  getAll() {
    return HttpClient.get(`${this.urlPrefix}`).catch(() => null)
  }
}

export default new TaskService()
