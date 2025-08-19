import { useUserStore } from "@/store/user"
const whitePages = ['/pages/login/login', '/pages/forgot-password/forgot-password']
export const checkAuthAndRedirect = (url: string) => {
	if (!whitePages.includes(url)) {
		const userStore = useUserStore()
		if (!userStore.isLogin) {
			uni.showToast({ title: '请先登录', icon: 'none' })
			setTimeout(() => {
				uni.redirectTo({
					url: '/pages/login/login?redirect=' + encodeURIComponent(url)
				})
			}, 1000)
			return false // 阻止跳转
		}
	}
	return true
}
uni.addInterceptor('navigateTo', {
	invoke(e) {
		return checkAuthAndRedirect(e.url);
	}
})
uni.addInterceptor('switchTab', {
	invoke(e) {
		return checkAuthAndRedirect(e.url);
	}
})