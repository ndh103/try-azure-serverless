import axios, { AxiosInstance } from 'axios'
import { globalConfig } from '@/configs/config'

let hasInstance = false
let httpClient: AxiosInstance

const HttpClient = () => {
  if (!hasInstance) {
    httpClient = axios.create({
      baseURL: globalConfig.config.apiUrl,
    })

    hasInstance = true
  }
  return httpClient
}

export default HttpClient
