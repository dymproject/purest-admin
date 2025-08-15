
export interface ResponseData<T = any> {
    code: number
    message: string
    data: T
  }
  
export type RequestConfig = {
    url: string
    method?: 'GET' | 'POST' | 'PUT' | 'DELETE'
    data?: any,
    params?: any,
    header?: Record<string, string>
    dataType?: string
    responseType?: string
    timeout?: number
    showLoading?: boolean
    retry?: number // 重试次数
    retryDelay?: number // 重试延迟
}