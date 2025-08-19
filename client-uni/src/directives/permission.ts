import { App, Directive } from 'vue'
import { useUserStore } from '@/store/user' // 请根据你的 store 路径调整

/**
 * 权限指令 v-permission
 * 用法：
 * v-permission="'user.create'"
 * v-permission="['user.create', 'user.edit']"        // 包含任意一个即可
 * v-permission="{ value: ['user.create'], all: true }" // 必须全部拥有
 */
export const vPermission: Directive = {
    mounted(el: HTMLElement, binding: any) {
        // 获取用户权限
        const userStore = useUserStore()
        const userPermissions: string[] = userStore.permissions || [] // 注意：请确保 getter 名不冲突

        const { value, arg } = binding
        let requiredPermissions: string[] = []
        let requireAll = false // 是否必须全部满足

        if (!value) {
            console.warn('v-permission 缺少权限值')
            el.style.display = 'none'
            return
        }

        // 解析 binding 值
        if (typeof value === 'string') {
            requiredPermissions = [value]
        } else if (Array.isArray(value)) {
            requiredPermissions = value
        } else if (typeof value === 'object') {
            requiredPermissions = value.value || []
            requireAll = Boolean(value.all)
        }

        // 判断权限
        let hasPermission = false
        if (requireAll) {
            hasPermission = requiredPermissions.every(p => userPermissions.includes(p))
        } else {
            hasPermission = requiredPermissions.some(p => userPermissions.includes(p))
        }

        // 无权限则隐藏并去掉元素
        if (!hasPermission) {
            el.style.display = 'none'
            el.parentNode?.removeChild(el);
        }
    }
}

// 全局注册函数
export const setupPermissionDirective = (app: App) => {
    app.directive('permission', vPermission)
}