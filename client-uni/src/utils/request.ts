import { useUserStore } from '@/store/user'
import type { RequestConfig } from '@/dtos/http'

const defaultConfig: RequestConfig = {
	url: '',
	method: 'GET',
	header: {
		'Content-Type': 'application/json'
	},
	dataType: 'json',
	timeout: 10000,
	showLoading: true
}

type Interceptor<T> = (config: T) => T | Promise<T>
type ResponseInterceptor = (response: UniApp.RequestSuccessCallbackResult, config: RequestConfig) => boolean | any

class HttpRequest {
	config: RequestConfig
	requestInterceptor?: Interceptor<RequestConfig>
	responseInterceptor?: ResponseInterceptor

	constructor(config?: Partial<RequestConfig>) {
		this.config = { ...defaultConfig, ...config }
	}

	setRequestInterceptor(fn: Interceptor<RequestConfig>) {
		this.requestInterceptor = fn
	}

	setResponseInterceptor(fn: ResponseInterceptor) {
		this.responseInterceptor = fn
	}

	async request<T = any>(options: RequestConfig): Promise<T> {
		let config: RequestConfig = { ...this.config, ...options }

		// 绝对路径处理
		let url = config.url
		// 构建请求 URL
		if (!/^(http|https):\/\//.test(url)) {
			// 获取当前环境：development / production
			const isDev = import.meta.env.DEV; // true 表示开发环境
			const baseUrl = import.meta.env.VITE_API_BASE_URL;
			const prefix = import.meta.env.VITE_API_PREFIX;

			const normalizedBaseUrl = baseUrl?.endsWith('/') ? baseUrl.slice(0, -1) : baseUrl;
			const normalizedPrefix = prefix?.startsWith('/') ? prefix : `/${prefix}`;
			const normalizedUrl = url.startsWith('/') ? url.slice(1) : url;

			url = `${normalizedBaseUrl}${normalizedPrefix}/${normalizedUrl}`;
		}

		// 请求拦截器
		if (this.requestInterceptor) {
			config = await this.requestInterceptor(config)
		}

		// 显示 loading
		if (config.showLoading) {
			uni.showLoading({ title: '请求中...' })
		}

		return new Promise<T>((resolve, reject) => {
			uni.request({
				url,
				data: config.data,
				method: config.method,
				header: config.header,
				dataType: config.dataType,
				timeout: config.timeout,

				success: (res) => {
					if (config.showLoading) {
						uni.hideLoading()
					}

					// 响应拦截器
					if (this.responseInterceptor) {
						const handled = this.responseInterceptor(res, config)
						if (handled === false) {
							reject(new Error('Response intercepted and rejected'))
							return
						}
					}

					// 默认成功判断
					if (res.statusCode >= 200 && res.statusCode < 300) {
						resolve(res.data as T)
					} else {
						// 统一错误处理
						this.handleResponseError(res, config)
						reject(res)
					}
				},

				fail: (err) => {
					if (config.showLoading) {
						uni.hideLoading()
					}
					uni.showToast({ title: '网络异常', icon: 'none' })
					reject(err)
				}
			})
		})
	}

	private handleResponseError(res: UniApp.RequestSuccessCallbackResult, config: RequestConfig) {
		const { statusCode } = res

		if (statusCode === 401) {
			// 清除 token 并跳转登录
			const userStore = useUserStore()
			userStore.logout()
		} else if (statusCode === 404) {
			uni.showToast({ title: '接口不存在', icon: 'none' })
		} else {
			const msg = (res.data as any)?.message || '请求失败'
			uni.showToast({ title: msg, icon: 'none' })
		}
	}

	// 便捷方法
	get<T = any>(url: string, data?: any, config?: Partial<RequestConfig>) {
		return this.request<T>({ ...config, url, method: 'GET', data })
	}

	post<T = any>(url: string, data?: any, config?: Partial<RequestConfig>) {
		return this.request<T>({ ...config, url, method: 'POST', data })
	}

	put<T = any>(url: string, data?: any, config?: Partial<RequestConfig>) {
		return this.request<T>({ ...config, url, method: 'PUT', data })
	}

	delete<T = any>(url: string, data?: any, config?: Partial<RequestConfig>) {
		return this.request<T>({ ...config, url, method: 'DELETE', data })
	}
}

export default HttpRequest