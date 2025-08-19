import { defineStore } from 'pinia'
import { login, getUserPermissions } from '@/api/auth'
import { LoginOutput, LoginInput } from '@/dtos/auth'

export const useUserStore = defineStore('user', {
  state: () => ({
    token: '' as string,
    userInfo: {} as LoginOutput,
    loading: false,
    permissions: [] as string[]
  }),
  persist: true,
  getters: {
    isLogin: (state): boolean => !!state.token
  },

  actions: {
    setToken(token: string) {
      this.token = token
    },

    setUserInfo(info: LoginOutput) {
      this.userInfo = info
    },

    setPermissions(permissions: string[]) {
      this.permissions = permissions
    },

    async login(formData: LoginInput) {
      this.loading = true
      try {
        const userInfo = await login(formData)
        this.setUserInfo(userInfo)
        uni.showToast({ title: '登录成功' })
        const permissions = await getUserPermissions()
        this.setPermissions(permissions)
        uni.switchTab({ url: '/pages/index/index' })
      } catch (error) {
        uni.showToast({ title: '登录失败', icon: 'none' })
      } finally {
        this.loading = false
      }
    },

    logout() {
      this.token = ''
      this.userInfo = {} as LoginOutput
      uni.removeStorageSync('token')
      uni.removeStorageSync('userInfo')
      uni.reLaunch({ url: '/pages/login/login' })
    },
  }
})