import HttpRequest from '@/utils/request'
import { useUserStore } from '@/store/user'
import type { RequestConfig } from '@/dtos/http'

const http = new HttpRequest({})

// 请求拦截器
http.setRequestInterceptor((config: RequestConfig) => {
	const userStore = useUserStore()
	if (userStore.token) {
		if (config.header != null) {
			config.header['Authorization'] = `Bearer ${userStore.token}`
		}
	}
	return config
})

// 响应拦截器
http.setResponseInterceptor((response, config) => {
	const { data, statusCode, header } = response as {
		data: any;
		statusCode: number;
		header: Record<string, string>;
	};

	const userStore = useUserStore();
	if (header.accesstoken) {
		userStore.setToken(response.header.accesstoken);
	}
	if (statusCode == 204) {
		uni.showToast({ title: '操作成功', icon: 'success' })
		return;
	}
	else if (statusCode == 200) {
		return data;
	}
	else if (statusCode == 401) {
		uni.showToast({ title: '未检测到登录信息或者信息已失效', icon: 'none' })
		setTimeout(() => {
			uni.redirectTo({
				url: '/pages/login/login'
			})
		}, 3000)
	}
	else if (statusCode == 400) {
		uni.showToast({ title: data.error?.message || '请求失败', icon: 'exception' })
		return false
	}
	else if (statusCode == 403) {
		uni.showToast({ title: '无此接口权限', icon: 'fail' })
		return false
	}
	return response
})

export { http }