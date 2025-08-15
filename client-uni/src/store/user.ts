import { defineStore } from 'pinia'
import { login } from '@/api/auth'
import { LoginOutput, LoginInput } from '@/dtos/auth'

export const useUserStore = defineStore('user', {
  state: () => ({
    token: uni.getStorageSync('token') || '',
    userInfo: {} as LoginOutput,
    loading: false
  }),
  persist: true,
  getters: {
    isLogin: (state): boolean => !!state.token,
    getToken: (state): string => state.token
  },

  actions: {
    setToken(token: string) {
      this.token = token
      uni.setStorageSync('token', token)
    },

    setUserInfo(info: LoginOutput) {
      this.userInfo = info
      uni.setStorageSync('userInfo', info)
    },

    async login(formData: LoginInput) {
      this.loading = true
      try {
        const userInfo = await login(formData)
        this.setUserInfo(userInfo)
        uni.showToast({ title: '登录成功' })
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