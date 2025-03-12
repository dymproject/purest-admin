import type { RouteRecordRaw } from 'vue-router';

import { BasicLayout } from '#/layouts';
import { $t } from '#/locales';

const routes: RouteRecordRaw[] = [
  {
    component: BasicLayout,
    meta: {
      icon: 'tdesign:system-setting-filled',
      keepAlive: true,
      order: 900,
      title: $t('page.system.title'),
    },
    name: 'System',
    path: '/system',
    children: [
      {
        meta: {
          title: $t('page.system.user.title'),
        },
        name: 'User',
        path: '/system/user',
        component: () => import('#/views/system/user/index.vue'),
      },
      {
        meta: {
          title: $t('page.system.role.title'),
        },
        name: 'Role',
        path: '/system/role',
        component: () => import('#/views/system/role/index.vue'),
      },
      {
        meta: {
          title: $t('page.system.function.title'),
        },
        name: 'Function',
        path: '/system/function',
        component: () => import('#/views/system/function/index.vue'),
      },
      {
        meta: {
          title: $t('page.system.organization.title'),
        },
        name: 'Organization',
        path: '/system/organization',
        component: () => import('#/views/system/organization/index.vue'),
      },
      {
        meta: {
          title: $t('page.system.dict.title'),
        },
        name: 'Dict',
        path: '/system/dict',
        component: () => import('#/views/system/dictionary/index.vue'),
      },
      {
        meta: {
          title: $t('page.system.config.title'),
        },
        name: 'Config',
        path: '/system/config',
        component: () => import('#/views/system/systemConfig/index.vue'),
      },
    ],
  },
];

export default routes;
