import HttpClient from '@/common/HttpClient'

class EventsApi {
  urlPrefix = '/events'

  getAll() {
    return HttpClient()
      .get(`${this.urlPrefix}`)
      .catch(() => null)
  }
}

export default new EventsApi()
