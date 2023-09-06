import { useRenderIcon } from "@/components/ReIcon/src/hooks";
import Setting from "@iconify-icons/ep/setting";

const Layout = () => import("@/layout/index.vue");

export default {
  path: "/",
  name: "system",
  component: Layout,
  redirect: "/welcome",
  meta: {
    icon: useRenderIcon(Setting),
    title: "系统管理",
    permissions: ["system"],
    rank: 0
  },
  children: [
    {
      path: "/user",
      name: "system_user",
      meta: {
        title: "用户管理",
        permissions: ["system.user"]
      },
      component: () => import("@/views/system/user/index.vue")
    },
    {
      path: "/role",
      name: "system_role",
      meta: {
        title: "角色管理",
        permissions: ["system.role"]
      },
      component: () => import("@/views/system/role/index.vue")
    },
    {
      path: "/function",
      name: "system_function",
      meta: {
        title: "功能管理",
        permissions: ["system.function"]
      },
      component: () => import("@/views/system/function/index.vue")
    },
    {
      path: "/organization",
      name: "system_organization",
      meta: {
        title: "组织机构",
        permissions: ["system.organization"]
      },
      component: () => import("@/views/system/organization/index.vue")
    },
    {
      path: "/dictionary",
      name: "system_dictionary",
      meta: {
        title: "字典管理",
        permissions: ["system.dictionary"]
      },
      component: () => import("@/views/system/dictionary/index.vue")
    },
    {
      path: "/config",
      name: "system_config",
      meta: {
        title: "配置管理",
        permissions: ["system.systemconfig"]
      },
      component: () => import("@/views/system/systemconfig/index.vue")
    }
  ]
} as RouteConfigsTable;
