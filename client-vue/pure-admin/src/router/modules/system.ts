import { useRenderIcon } from "@/components/ReIcon/src/hooks";
import Setting from "~icons/ri/settings-2-line";

const Layout = () => import("@/layout/index.vue");

export default {
  path: "/system",
  name: "system",
  component: Layout,
  meta: {
    icon: useRenderIcon(Setting),
    title: "系统管理",
    permissions: ["system"],
    rank: 0
  },
  children: [
    {
      path: "/system/user",
      name: "system_user",
      meta: {
        title: "用户管理",
        permissions: ["system.user"]
      },
      component: () => import("@/views/system/user/index.vue")
    },
    {
      path: "/system/role",
      name: "system_role",
      meta: {
        title: "角色管理",
        permissions: ["system.role"]
      },
      component: () => import("@/views/system/role/index.vue")
    },
    {
      path: "/system/function",
      name: "system_function",
      meta: {
        title: "功能管理",
        permissions: ["system.function"]
      },
      component: () => import("@/views/system/function/index.vue")
    },
    {
      path: "/system/organization",
      name: "system_organization",
      meta: {
        title: "组织机构",
        permissions: ["system.organization"]
      },
      component: () => import("@/views/system/organization/index.vue")
    },
    {
      path: "/system/dictionary",
      name: "system_dictionary",
      meta: {
        title: "字典管理",
        permissions: ["system.dictionary"]
      },
      component: () => import("@/views/system/dictionary/index.vue")
    },
    {
      path: "/system/config",
      name: "system_config",
      meta: {
        title: "配置管理",
        permissions: ["system.systemconfig"]
      },
      component: () => import("@/views/system/systemConfig/index.vue")
    },
    {
      path: "/system/notice",
      name: "system_notice",
      meta: {
        title: "通知公告",
        permissions: ["system.notice"]
      },
      component: () => import("@/views/system/notice/index.vue")
    },
    {
      path: "/system/profile-system",
      name: "system_profile_system",
      meta: {
        title: "系统文件",
        permissions: ["system.profilesystem"]
      },
      component: () => import("@/views/system/profileSystem/index.vue")
    },
    {
      path: "/system/request_log",
      name: "system_requestLog",
      meta: {
        title: "请求日志",
        permissions: ["system.requestlog"]
      },
      component: () => import("@/views/system/requestLog/index.vue")
    }
  ]
} satisfies RouteConfigsTable;
