const { VITE_HIDE_HOME } = import.meta.env;
const Layout = () => import("@/layout/index.vue");
import { useRenderIcon } from "@/components/ReIcon/src/hooks";
import HeartLine from "~icons/ri/heart-2-line";
export default {
  path: "/",
  name: "Home",
  component: Layout,
  redirect: "/welcome",
  meta: {
    icon: useRenderIcon(HeartLine),
    title: "欢迎使用",
    rank: 0
  },
  children: [
    {
      path: "/welcome",
      name: "Welcome",
      component: () => import("@/views/welcome/index.vue"),
      meta: {
        title: "系统主页",
        showLink: VITE_HIDE_HOME === "true" ? false : true
      }
    },
    {
      path: "/online_user",
      name: "system_onlineUser",
      meta: {
        title: "在线用户",
        permissions: ["system.onlineuser"]
      },
      component: () => import("@/views/system/onlineUser/index.vue")
    },
    {
      path: "/userprofile",
      name: "UserProfile",
      component: () => import("@/views/welcome/UserProfile.vue"),
      meta: {
        title: "个人信息",
        showLink: false
      }
    }
  ]
} satisfies RouteConfigsTable;
