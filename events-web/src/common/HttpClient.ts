import axios, { AxiosInstance } from 'axios'
import { globalConfig } from '@/configs/config'
import { msalInstance } from '@/main'
import { loginRequest } from '@/configs/authConfig'

let hasInstance = false
let httpClient: AxiosInstance

const HttpClient = () => {
  if (!hasInstance) {
    httpClient = axios.create({
      baseURL: globalConfig.config.apiUrl,
    })

    // Add a request interceptor
    httpClient.interceptors.request.use(
      async (config) => {
        const account = msalInstance.getActiveAccount()
        if (!account) {
          throw Error('No active account! Verify a user has been signed in and setActiveAccount has been called.')
        }

        const response = await msalInstance.acquireTokenSilent({
          ...loginRequest,
          account,
        })

        const { accessToken } = response

        // eslint-disable-next-line no-param-reassign
        config.headers!.Authorization = `Bearer ${accessToken}`

        // Do something before request is sent
        return config
      },
      (error) =>
        // Do something with request error
        Promise.reject(error)
    )

    // Add a response interceptor
    httpClient.interceptors.response.use(
      (response) =>
        // Any status code that lie within the range of 2xx cause this function to trigger
        // Do something with response data
        response,
      (error) =>
        // Any status codes that falls outside the range of 2xx cause this function to trigger
        // Do something with response error
        Promise.reject(error)
    )

    hasInstance = true
  }
  return httpClient
}

export default HttpClient
