import { createApp, watch, watchEffect } from 'vue';

import { registerAccessDirective } from '@vben/access';
import { preferences,usePreferences } from '@vben/preferences';
import { initStores } from '@vben/stores';
import '@vben/styles';
import '@vben/styles/ele';

import { useTitle } from '@vueuse/core';
import { ElLoading } from 'element-plus';

import { $t, setupI18n } from '#/locales';

import { initComponentAdapter } from './adapter/component';
import App from './app.vue';
import { router } from './router';

// 全局vxe
import VxeUI from 'vxe-pc-ui';
import 'vxe-pc-ui/lib/style.css';
import VxeUITable from 'vxe-table';
import 'vxe-table/lib/style.css';
import enUS from 'vxe-pc-ui/lib/language/en-US';
import zhCN from 'vxe-pc-ui/lib/language/zh-CN';

// 自定义组件
import { Page } from '@vben/common-ui';
import { ReVxeGrid } from './components/grid';
import { ReModal } from "./components/modal";

async function bootstrap(namespace: string) {
  // 初始化组件适配器
  await initComponentAdapter();
  const app = createApp(App);

  // 注册Element Plus提供的v-loading指令
  app.directive('loading', ElLoading.directive);
  
  // 注册vxe
  app.use(VxeUI).use(VxeUITable);

  // 注册自定义的组件
  app.component('RePage', Page);
  app.component('ReVxeGrid', ReVxeGrid);
  app.component('ReModal', ReModal);
  
  // 国际化 i18n 配置
  await setupI18n(app);

  // 配置 pinia-tore
  await initStores(app, { namespace });

  // 安装权限指令
  registerAccessDirective(app);

  // 配置路由及路由守卫
  app.use(router);

  const preference = usePreferences();
  const localMap = {
    'zh-CN': zhCN,
    'en-US': enUS,
  };
  
  watch(
    [() => preference.theme.value, () => preference.locale.value],
    ([theme, locale]) => {
      VxeUI.setTheme(theme === 'dark' ? 'dark' : 'light');
      VxeUI.setI18n(locale, localMap[locale]);
      VxeUI.setLanguage(locale);
    },
    {
      immediate: true,
    },
  );

  // 动态更新标题
  watchEffect(() => {
    if (preferences.app.dynamicTitle) {
      const routeTitle = router.currentRoute.value.meta?.title;
      const pageTitle =
        (routeTitle ? `${$t(routeTitle)} - ` : '') + preferences.app.name;
      useTitle(pageTitle);
    }
  });

  app.mount('#app');
}

export { bootstrap };
