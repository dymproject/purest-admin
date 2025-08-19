import { createSSRApp } from 'vue'
import { createPinia } from 'pinia'
import piniaPluginPersistedstate from 'pinia-plugin-persistedstate';
import { setupPermissionDirective } from './directives/permission';
import "@/utils/auth-guard"
import App from './App.vue'


export function createApp() {
  const app = createSSRApp(App)
  setupPermissionDirective(app)
  const pinia = createPinia()
  pinia.use(piniaPluginPersistedstate)
  app.use(pinia)
  return {
    app,
    pinia
  }
}