import type { Activity } from "@/models/Activity"
import type { AxiosResponse, InternalAxiosRequestConfig, AxiosInterceptorOptions } from "axios"

import axios from "axios"

export const setInterceptorsRequest = (onFulfilled?: ((value: InternalAxiosRequestConfig<any>) => InternalAxiosRequestConfig<any> | Promise<InternalAxiosRequestConfig<any>>) | null | undefined, onRejected?: ((error: any) => any) | null, options?: AxiosInterceptorOptions) => {
    axios.interceptors.request.use(onFulfilled, onRejected, options)
}

export const setInterceptorsResponse = (onFulfilled?: ((value: AxiosResponse<any, any>) => AxiosResponse<any, any> | Promise<AxiosResponse<any, any>>) | null | undefined, onRejected?: ((error: any) => any) | null) => {
    axios.interceptors.response.use(onFulfilled, onRejected)
}

axios.defaults.baseURL = '/api'

const responseBody = <T>(response: AxiosResponse<T>) => response.data

const requests = {
    get: <T>(url: string) => axios.get<T>(url).then(responseBody),
    post: <T>(url: string, body: {}) => axios.post<T>(url, body).then(responseBody),
    put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
    del: <T>(url: string) => axios.delete<T>(url).then(responseBody)
}

const Activities = {
    list: () => requests.get<Activity[]>('/activity'),
    detail: (id: string) => requests.get<Activity>(`/activity/${id}`),
    create: (value: Activity) => requests.post<void>('/activity', value),
    update: (id: string, value: Activity) => requests.put<void>(`/activity/${id}`, value),
    delete: (id: string) => requests.del<void>(`/activity/${id}`)
}

const agent = {
    Activities
}
export default agent